using PuntoDeventa.Core.LocalData.DataBase;
using PuntoDeventa.Core.LocalData.DataBase.Entities.Sales;
using PuntoDeventa.Core.LocalData.Files;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Data.DTO.Auth;
using PuntoDeventa.Data.DTO.CatalogueProduct;
using PuntoDeventa.Data.DTO.EmissionSystem;
using PuntoDeventa.Data.DTO.Sales;
using PuntoDeventa.Data.Mappers;
using PuntoDeventa.Data.Models;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PuntoDeventa.Data.Repository.EmissionSystem
{
    internal class OpenFacturaRepository : BaseRepository, IOpenFacturaRepository
    {
        #region Fields
        private readonly IElectronicEmissionSystem _emissionSystem;
        private readonly IDataStore _dataStore;
        private readonly IDataPreferences _dataPreferences;
        private EcommerceDTO _ecommerceData;
        private AccountingDto _accounting;
        private ReportSaleDto _reportSale;
        private IEnumerable<ProductSales> _inventoryReport;
        private readonly IDataAccessObject _dao;
        private readonly IFileManager _fileManager;
        private readonly UserDataDTO _userData;
        #endregion

        public OpenFacturaRepository()
        {

            _emissionSystem = DependencyService.Get<IElectronicEmissionSystem>();
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            _userData = _dataPreferences.GetUserData();
            _ecommerceData = _dataPreferences.GetEcommerceData();
            _dao = DependencyService.Get<IDataAccessObject>();
            _fileManager = DependencyService.Get<IFileManager>();
        }
        public async Task<SalesState> EmitFactura(PaymentSales paymentSales)
        {
            if (_ecommerceData.IsNull())
                _ecommerceData = _dataPreferences.GetEcommerceData();

            var dteDto = paymentSales.ToDocumentElectronicDto(_ecommerceData,
                new[] { "FOLIO", "TIMBRE", "RESOLUCION" });

            var resultType = await MakeCallNetwork<EmissionReposeDTO>(() => _emissionSystem
                .PostAsync(dteDto, FactoryUri("document")));

            if (!resultType.Success)
                return SalesState.Error.Instance(string.Join(Environment.NewLine, resultType.Errors));


            //TODO create pdfManager

            var pathPdf = await _fileManager.CreatePdf(dteDto.Dte, resultType.Data, _ecommerceData.RegionalDirection);


            var base64Stream = Convert.ToBase64String(new StreamReader(pathPdf).BaseStream.ToBytes());

            InsertReports(resultType.Data.Token, resultType.Data.Folio, base64Stream, paymentSales);

            return SalesState.Success.Instance(pathPdf);

        }

        public async Task<SalesState> SyncInformationTributary()
        {

            var callFireBase = await MakeCallNetwork<EcommerceDTO>(async () => await _dataStore.GetAsync<EcommerceDTO>(new Uri(
                Path.Combine(Properties.Resources.BaseUrlRealDataBase,
                    $"DataEcommerce.json?auth={_userData.IdToken}"))));
            if (!callFireBase.Success) return SalesState.Error.Instance(string.Join(Environment.NewLine, callFireBase.Errors));

            var ecommerceDto = callFireBase.Data;

            var callSystemEmission = await MakeCallNetwork<EcommerceDTO>(async () => await _emissionSystem.GetAsync(ecommerceDto.ApiKey, FactoryUri("organization")));

            if (!callSystemEmission.Success) return SalesState.Error.Instance(string.Join(Environment.NewLine, callFireBase.Errors));

            _ecommerceData = callSystemEmission.Data;

            _ecommerceData.CopyPropertiesFrom(ecommerceDto);

            _dataPreferences.SetEcommerceData(_ecommerceData);

            return SalesState.Success.Instance("Actualización Exitosa");
        }

        public async Task<SalesState> InsertNotaDePedido(PaymentSales paymentSales)
        {
            var invoice = int.Parse($"{paymentSales.Sale.DateSale:yyMMddmmss}");

            var pathPdf = await _fileManager.CreatePdf(paymentSales.ToDocumentElectronicDto(_ecommerceData, new[] { "" }, DteType.NotaDePedido).Dte, new EmissionReposeDTO(), _ecommerceData.RegionalDirection);


            var base64Stream = Convert.ToBase64String(new StreamReader(pathPdf).BaseStream.ToBytes());

            InsertReports(Factory.GenerateId(), invoice, base64Stream, paymentSales);

            return SalesState.Success.Instance(null);
        }

        private async Task<HttpResponseMessage> CreateAccountingReport(string localId, int invoice, PaymentSales paymentSales)
        {
            _accounting = new AccountingDto()
            {
                Dte = paymentSales.DocumentType,
                Delivery = paymentSales.Sale.DateSale,
                Invoice = invoice,
                Name = paymentSales.Sale.Client.Name,
                Rut = paymentSales.Sale.Client.Rut,
                Amount = paymentSales.Sale.TotalSale(_ecommerceData.Iva),
                SellerCode = "01",
                Payments = new List<PaymentTypeDto>()

            };
            paymentSales.PaymentTypes?.ForEach(p =>
            {
                switch (p)
                {
                    case PaymentType.BankCheck bankCheck:
                        _accounting.Payments.Add(new PaymentTypeDto.BankCheckDto()
                        {
                            Date = DateTime.Now,
                            Amount = bankCheck.Amount

                        });
                        break;
                    case PaymentType.BankDeposit bankDeposit:
                        _accounting.Payments.Add(new PaymentTypeDto.BankDepositDto()
                        {
                            Date = DateTime.Now,
                            Amount = bankDeposit.Amount

                        });
                        break;
                    case PaymentType.BankTransfer bankTransfer:
                        _accounting.Payments.Add(new PaymentTypeDto.BankTransferDto()
                        {
                            Date = DateTime.Now,
                            Amount = bankTransfer.Amount

                        });
                        break;
                    case PaymentType.Cash cash:
                        _accounting.Payments.Add(new PaymentTypeDto.CashDto()
                        {
                            Date = DateTime.Now,
                            Amount = cash.Amount

                        });
                        break;
                }
            });

            var uri = paymentSales.PaymentMethod == PaymentMethod.Counted
                ? FactoryUrlRealDataBase("Accounting", paymentSales.Sale.DateSale, localId)
                : FactoryUrlRealDataBase($"Accounting/Pending/{localId}");
            return await _dataStore.PutAsync(_accounting, uri);

        }

        private async Task<HttpResponseMessage> CreateReportSale(string localId, int invoice, string base64StringPdf, PaymentSales paymentSales)
        {
            _reportSale = new ReportSaleDto
            {
                Dte = paymentSales.DocumentType,
                Date = paymentSales.Sale.DateSale,
                Delivery = paymentSales.Sale.DateSale,
                Iva = _ecommerceData.Iva,
                Invoice = invoice,
                Name = paymentSales.Sale.Client.Name,
                Rut = paymentSales.Sale.Client.Rut,
                Products = paymentSales.Sale.Products.SelectMany((p, index) => new List<ProductSalesDto>()
                {
                    new ProductSalesDto()
                    {
                        InReport = p.InReport,
                        Itm = index + 1,
                        Name = p.Name,
                        PriceGross = p.PriceGross,
                        Quantity = p.Quantity,
                        Sku = p.Sku,
                        Udm = p.UDM,
                        BarCode = p.BarCode
                    }

                })


            };
            return await _dataStore.PutAsync(_reportSale,
                FactoryUrlRealDataBase("ReportSales", paymentSales.Sale.DateSale, localId)); //MakeCallNetwork<ReportSaleDto>(() => _dataStore.PutAsync(dto, FactoryUrlRealDataBase("ReportSale", paymentSales.Sale.DateSale, data.Token)));
        }

        private void SaveDataBase(PendingDocumentEntity obj)
        {
            _dao.InsertOrUpdate(obj);
        }

        private static Uri FactoryUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlEelectronicEmission, path));
        }

        private Uri FactoryUrlRealDataBase(string path)
        {
            return new Uri(Path.Combine(new[]
            {
                Properties.Resources.BaseUrlRealDataBase,
                path,
                $".json?auth={_userData.IdToken}"
            }));
        }
        private Uri FactoryUrlRealDataBase(string path, DateTime date, string localId)
        {
            return new Uri(Path.Combine(new[]
            {
                Properties.Resources.BaseUrlRealDataBase,
                path, date.Year.ToString(),
                $"{date.Day:D2}/{localId}.json?auth={_userData.IdToken}"
            }));


        }
        private async void InsertReports(string localId, int invoice, string base64Stream, PaymentSales paymentSales)
        {
            var reportSale = CreateReportSale(localId, invoice, base64Stream, paymentSales);

            var accountingReport = CreateAccountingReport(localId, invoice, paymentSales);

            var inventoryReport = CreateInventoryReport(paymentSales.Sale.Products);
            await Task.Run(async () =>
            {


                await Task.WhenAll(reportSale, accountingReport).ContinueWith(task =>
                {
                    if (!task.IsFaulted || task.Exception.IsNull()) return;
                    foreach (var exception in task.Exception.InnerExceptions)
                    {
                        Console.WriteLine($"Error: {exception.Message}");
                        //Todo mapear errores de firebase // se puede usar crashlytics
                        //https://medium.com/@freakyali/firebase-crashlytics-with-xamarin-5421089bb561
                    }

                });
                var reportSaleDeferred = await MakeCallNetwork<EmissionReposeDTO>(reportSale.Result);
                if (!reportSaleDeferred.Success)
                    SaveDataBase(_reportSale.ToPendingDocument());

                var accountingDeferred = await MakeCallNetwork<EmissionReposeDTO>(accountingReport.Result);
                if (!accountingDeferred.Success)
                    SaveDataBase(_accounting.ToPendingDocument());
                var inventoryDeferred = await MakeCallNetwork<EmissionReposeDTO>(inventoryReport.Result);
                if (!inventoryDeferred.Success)
                    SaveDataBase(_inventoryReport.ToPendingDocument());

            }).ConfigureAwait(false);
        }
        private async Task<HttpResponseMessage> CreateInventoryReport(IEnumerable<ProductSales> products)
        {
            try
            {
                _inventoryReport = products;

                var inventoryUpdates = new Dictionary<string, object>();

                var oldInventory = await GetCategoryAll();

                var productGroup = products.GroupBy(p => p.CategoryId);

                var productDictionary = productGroup.ToDictionary(g => g.Key, g => g.ToList());

                productDictionary.ForEach(it =>
                {
                    var category = oldInventory.FirstOrDefault(c => c.Id == it.Key);
                    it.Value.ForEach(product =>
                    {
                        var productTarget = category?.Products.FirstOrDefault(p => p.Key == product.Id).Value;
                        var stock = productTarget!.Stock - product.Quantity;
                        inventoryUpdates.Add($"{it.Key}/Products/{product.Id}/Stock", stock);
                    });
                });

                return await _dataStore.PatchAsync(inventoryUpdates, FactoryUrlRealDataBase("CategoryProduct"));
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        private async Task<IEnumerable<CategoryDTO>> GetCategoryAll()
        {
            var resultType = await MakeCallNetwork<Dictionary<string, CategoryDTO>>(() => _dataStore.GetAsync<Dictionary<string, CategoryDTO>>(FactoryUrlRealDataBase("CategoryProduct")));
            if (!resultType.Success) return null;

            return resultType.Data.Select(kvp =>
            {
                kvp.Value.Id = kvp.Key;
                return kvp.Value;
            }).ToList();
        }

    }
}

using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO.Auth;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PuntoDeventa.Data.DTO.EmissionSystem;
using PuntoDeventa.Data.DTO.CatalogueClient;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Header;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;
using PuntoDeventa.Data.Mappers;
using PuntoDeventa.Data.Models;
using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.UI.CatalogueClient.Model;
using System.Linq;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Detail;

namespace PuntoDeventa.Data.Repository.EmissionSystem
{
    internal class OpenFacturaRepository : BaseRepository, IOpenFacturaRepository
    {
        private readonly IElectronicEmissionSystem _emissionSystem;
        private readonly IDataStore _dataStore;
        private readonly IDataPreferences _dataPreferences;
        private EcommerceDTO _ecommerceData;
        private UserDataDTO _userData;
        public OpenFacturaRepository()
        {
            _emissionSystem = DependencyService.Get<IElectronicEmissionSystem>();
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            GetEcommerceData();
        }
        public async Task<SalesState> ToEmitDte(PaymentSales paymentSales)
        {
            Console.WriteLine(paymentSales);
            switch (paymentSales.DocumentType)
            {
                case DocumentType.Factura:
                    var doc = GetDte33(paymentSales);
                    var resultType = await MakeCallNetwork<EmissionReposeDTO>(() => _emissionSystem.PostAsync(doc, _ecommerceData.ApiKey, GetUri("document")));
                    if (resultType.Success)
                    {
                        return new SalesState.Success(resultType.Data);
                    }
                    else
                    {
                        return new SalesState.Error(string.Join(Environment.NewLine, resultType.Errors));
                    }
                case DocumentType.NotaDePedido:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

           
        }

        private async void GetEcommerceData()
        {
            _userData = _dataPreferences.GetUserData();
            _ecommerceData = _dataPreferences.GetEcommerceData();
                _ecommerceData = new EcommerceDTO()
            {
                ApiKey = "928e15a2d14d4a6292345f04960f4bd3",
                Iva = 0.19
            };

            if (_ecommerceData.IsNotNull() && _ecommerceData.BranchOffices.IsNotNull()) return;

            
           
            //if (_ecommerceData.BranchOffices.IsNotNull()) return;

            var ecommerce = await GetInformationTributaryAsync();

            //_ecommerceData.CopyPropertiesFrom(ecommerce);

            _dataPreferences.SetEcommerceData(_ecommerceData);
        }

        private async Task<EcommerceDTO> GetInformationTributaryAsync()
        {
            var resultType = await MakeCallNetwork<EcommerceDTO>(() => _emissionSystem.GetAsync(_ecommerceData.ApiKey, GetUri("organization")));

            return !resultType.Success ? null : resultType.Data;
        }

        private static Uri GetUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlEelectrinicEmision, path));
        }

        private SalesState ResultTypeToSaleStates(OperationDTO insertOrUpdate, Client client, ResultType<ClientDTO> resultType)
        {
            throw new NotImplementedException();
        }

        private DocumentElectronicDTO GetDte33(PaymentSales sales)
        {

            return new DocumentElectronicDTO()
            {
                ResponseStrings = new[] { "FOLIO", "PDF", "RESOLUCION", "TIMBRE" },
                Dte = new Dte33DTO()
                {
                    Id = Factory.GenerateId(),
                    Headers = new HeaderDTO()
                    {
                        IdDoc = new IdDoc(DteType.Factura, sales.Sale.DateSale, (int)sales.PaymentMethod),
                        IssuingCompany = _ecommerceData.ToIssuingCompany(),
                        Receiver = new Receiver()
                        {
                            Address = sales.Sale.SelectBranchOffices.Address,
                            Commune = sales.Sale.SelectBranchOffices.Commune,
                            Name = sales.Sale.Client.Name,
                            Rut = sales.Sale.Client.Rut,
                            Turn = sales.Sale.SelectEconomicActivities.Turn
                        },
                        Totals = new Totals()
                        {
                            Amount = sales.Sale.TotalSale(0.19f)
                        }
                    },
                    Detalle = sales.Sale.Products
                        .SelectMany((p, index) => new List<DetailDTO>()
                        {
                            new DetailDTO()
                            {
                                Id = index++,
                                QtyItem = p.Quantity,
                                NmbItem = p.Name,
                                PriceItem = (int)p.PriceNeto,
                                CdgItem = new List<ItemCode>()
                                {
                                    new ItemCode("SKU",p.SkuCode),
                                    new ItemCode("EAN13", p.BarCode.ToString())
                                }

                            }
                        }).ToList()


                }
            };
            // var resultType = await MakeCallNetwork<DocumentElectronicDTO>(() => _emissionSystem.PostAsync(model: document, "928e15a2d14d4a6292345f04960f4bd3", new Uri(Path.Combine(Properties.Resources.BaseUrlEelectrinicEmision, $"document"))));
        }
    }
}

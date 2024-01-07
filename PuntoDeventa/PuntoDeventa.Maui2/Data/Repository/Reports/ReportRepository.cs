using PuntoDeventa.Core.LocalData.Files;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO.Report;
using PuntoDeventa.Data.DTO.Sales;
using PuntoDeventa.Data.Mappers;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.Reports.Models;
using PuntoDeventa.UI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Data.Repository.Reports
{
    internal class ReportRepository : BaseRepository, IReportRepository
    {
        private readonly IDataStore _dataStore;
        private readonly IFileManager _fileManager;
        private readonly string _tokenId;

        public ReportRepository()
        {
            _dataStore = DependencyService.Get<IDataStore>();
            var dataPreferences = DependencyService.Get<IDataPreferences>();
            _fileManager = DependencyService.Get<IFileManager>();
            _tokenId = dataPreferences.GetUserData().IdToken;
        }

        public async IAsyncEnumerable<ReportSale> GetReportSale(DateTime date)
        {
            var resultType = await MakeCallNetwork<Dictionary<string, ReportSaleDto>>(async () => await _dataStore.GetAsync<ReportSaleDto>(FactoryUri($"{date:yyyy}/{date:MM}")));

            if (!resultType.Success || resultType.Data.IsNull())
            {
                throw new CustomException(8, string.Join(Environment.NewLine, resultType.Errors));
            }

            foreach (KeyValuePair<string, ReportSaleDto> item in resultType.Data)
            {
                var report = new ReportSale();
                report.CopyPropertiesFrom(item.Value);
                report.Id = item.Key;

                yield return report;
            }
        }

        public async Task<string> CreateReportToExcel(string name, IEnumerable<string> headers, IEnumerable<List<string>> values)
        {
            return await _fileManager.CreateReportExcel(name, new ExcelDataDto()
            {
                Headers = headers.ToList(),
                Values = values.ToList()
            });
        }

        public async Task<string> CreateReportToPdf(string fileName, IEnumerable<ProductSales> products)
        {
            return await _fileManager.CreateReportPdf(
                fileName,
                products.Select(p => p.ToProductSalesDto()).ToList());
        }

        private Uri FactoryUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlRealDataBase, $"ReportSales/{path}.json?auth={_tokenId}"));
        }
    }
}

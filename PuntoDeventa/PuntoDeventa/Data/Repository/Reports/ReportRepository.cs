using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO.Sales;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Reports.Models;
using PuntoDeventa.UI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace PuntoDeventa.Data.Repository.Reports
{
    internal class ReportRepository : BaseRepository, IReportRepository
    {
        private readonly IDataStore _dataStore;
        private readonly IDataPreferences _dataPreferences;
        private readonly string tokenID;

        public ReportRepository()
        {
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            tokenID = _dataPreferences.GetUserData().IdToken;
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
        private Uri FactoryUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlRealDataBase, $"ReportSales/{path}.json?auth={tokenID}"));
        }
    }
}

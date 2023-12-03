using PuntoDeventa.Data.Repository.Reports;
using PuntoDeventa.UI.Reports.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.Report.Implementation
{
    internal class GetReportSales : IGetReportSales
    {
        private readonly IReportRepository _repository;

        public GetReportSales()
        {
            _repository = DependencyService.Get<IReportRepository>();
        }
        public async IAsyncEnumerable<ReportSale> EmitSales(DateTime date)
        {

            await foreach (var report in _repository.GetReportSale(date))
            {
                yield return report;
            }

        }
    }
}

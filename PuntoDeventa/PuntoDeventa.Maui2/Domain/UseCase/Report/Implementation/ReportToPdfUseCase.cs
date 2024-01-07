using PuntoDeventa.Data.Repository.Reports;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.Reports.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.Report.Implementation
{
    internal class ReportToPdfUseCase : IReportToPdfUseCase
    {
        private readonly IReportRepository _repository;

        public ReportToPdfUseCase()
        {
            _repository = DependencyService.Get<IReportRepository>();
        }
        public async Task<ExportState> Create(string fileName, IEnumerable<ProductSales> products)
        {
            return ExportState.Success.Instance(await _repository.CreateReportToPdf(fileName, products));
        }
    }
}

using PuntoDeventa.Data.Repository.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PuntoDeventa.UI.Reports.State;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.Report.Implementation
{
    internal class ReportToExcel : IReportToExcelUseCase
    {
        private readonly IReportRepository _repository;

        public ReportToExcel()
        {
            _repository = DependencyService.Get<IReportRepository>();
        }
        public async Task<ExportState> Create(string name, IEnumerable<string> headers, IEnumerable<List<string>> values)
        {
            //Todo manejar logica de validacion de parametros
            return ExportState.Success.Instance(await _repository.CreateReportToExcel(name, headers, values));
        }
    }
}

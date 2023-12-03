using PuntoDeventa.UI.Reports.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Report
{
    public interface IReportToExcelUseCase
    {
        Task<ExportState> Create(string name, IEnumerable<string> headers, IEnumerable<List<string>> values);
    }
}

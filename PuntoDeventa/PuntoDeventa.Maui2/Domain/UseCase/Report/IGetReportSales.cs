using PuntoDeventa.UI.Reports.Models;
using System;
using System.Collections.Generic;

namespace PuntoDeventa.Domain.UseCase.Report
{
    public interface IGetReportSales
    {
        IAsyncEnumerable<ReportSale> EmitSales(DateTime date);
    }
}

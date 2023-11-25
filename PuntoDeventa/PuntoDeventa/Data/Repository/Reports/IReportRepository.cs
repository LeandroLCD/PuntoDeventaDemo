using PuntoDeventa.UI.Reports.Models;
using System;
using System.Collections.Generic;

namespace PuntoDeventa.Data.Repository.Reports
{
    public interface IReportRepository
    {
        IAsyncEnumerable<ReportSale> GetReportSale(DateTime date);
    }
}

using PuntoDeventa.Data.Repository.Reports;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.Reports.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.Report
{
    public interface IGetReportSales
    {
        IAsyncEnumerable<ReportSale> EmitSales(DateTime date);
    }
}

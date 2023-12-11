using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.Reports.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Reports
{
    public interface IReportRepository
    {
        IAsyncEnumerable<ReportSale> GetReportSale(DateTime date);

        Task<string> CreateReportToExcel(string fileName, IEnumerable<string> headers, IEnumerable<List<string>> values);
        Task<string> CreateReportToPdf(string fileName, IEnumerable<ProductSales> products);
    }
}

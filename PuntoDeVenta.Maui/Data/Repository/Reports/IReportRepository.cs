using PuntoDeVenta.Maui.UI.CategoryProduct.Models;
using PuntoDeVenta.Maui.UI.Reports.Models;

namespace PuntoDeVenta.Maui.Data.Repository.Reports
{
    public interface IReportRepository
    {
        IAsyncEnumerable<ReportSale> GetReportSale(DateTime date);

        Task<string> CreateReportToExcel(string fileName, IEnumerable<string> headers, IEnumerable<List<string>> values);
        Task<string> CreateReportToPdf(string fileName, IEnumerable<ProductSales> products);
    }
}

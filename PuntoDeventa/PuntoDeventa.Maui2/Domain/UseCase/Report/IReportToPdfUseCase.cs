using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PuntoDeventa.UI.Reports.State;

namespace PuntoDeventa.Domain.UseCase.Report
{
    public interface IReportToPdfUseCase
    {
        Task<ExportState> Create(string fileName, IEnumerable<ProductSales> products);
    }
}

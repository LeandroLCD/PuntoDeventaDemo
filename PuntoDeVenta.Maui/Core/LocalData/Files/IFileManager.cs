using PuntoDeVenta.Maui.Data.DTO.EmissionSystem;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes;
using PuntoDeVenta.Maui.Data.DTO.Report;
using PuntoDeVenta.Maui.Data.DTO.Sales;

namespace PuntoDeVenta.Maui.Core.LocalData.Files
{
    public interface IFileManager
    {
        /// <summary>
        /// Retorna el path de un pdf creada y almacenada en cache
        /// </summary>
        /// <param name="documentDto">Representa el Dto del documento a emitir</param>
        /// <param name="response">Representa la respuesta de la emisión del sistema de facturación</param>
        /// <param name="regionalDirectionSii">Información de la Dirección Regional del SII, que pertenece el Cliente.</param>
        /// <returns></returns>
        Task<string> CreatePdf(DteDTO documentDto, EmissionReposeDTO response, string regionalDirectionSii);

        Task<string> CreateReportExcel(string fileName, ExcelDataDto data);

        Task<string> CreateReportPdf(string fileName, List<ProductSalesDto> products);
    }
}

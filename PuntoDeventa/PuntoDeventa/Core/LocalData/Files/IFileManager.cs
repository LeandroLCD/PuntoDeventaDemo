﻿using PuntoDeventa.Data.DTO.EmissionSystem;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;
using PuntoDeventa.Data.DTO.Report;
using PuntoDeventa.Data.DTO.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.LocalData.Files
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

using Newtonsoft.Json;
using PuntoDeventa.Core.LocalData.DataBase.Entities.Sales;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Data.DTO.EmissionSystem;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Detail;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Header;
using PuntoDeventa.Data.DTO.Sales;
using PuntoDeventa.Data.Models;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Sales.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PuntoDeventa.Data.Mappers
{
    public static class SystemElectronicDtoMapper
    {
        //TODO mejorar logica dte
        public static DocumentElectronicDTO ToDocumentElectronicDto(this PaymentSales payment, EcommerceDTO ecommerce, string[] resposne, DteType dteType = DteType.Factura)
        {
            if (resposne is null || ecommerce is null)
            {
                throw new ArgumentNullException($"No se puede completar la llamada porque {nameof(resposne)} o {nameof(ecommerce)} es nulo.");
            }
            var vat = ecommerce.Iva;
            return new DocumentElectronicDTO()
            {
                ResponseStrings = resposne,
                Dte = new Dte33DTO()
                {
                    Id = Factory.GenerateId(),
                    Headers = new HeaderDTO()
                    {
                        IdDoc = new IdDoc(dteType, payment.Sale.Date, (int)payment.PaymentMethod),
                        IssuingCompany = ecommerce.ToIssuingCompany(),
                        Receiver = new Receiver()
                        {
                            Address = payment.Sale.SelectBranchOffices.Address,
                            Commune = payment.Sale.SelectBranchOffices.Commune,
                            Name = payment.Sale.Client.Name,
                            Rut = payment.Sale.Client.Rut,
                            Turn = payment.Sale.SelectEconomicActivities.Turn[..40]
                        },
                        Totals = new Totals()
                        {
                            Amount = payment.Sale.TotalSale(vat),
                            AmountPay = payment.Sale.TotalSale(vat),
                            Net = payment.Sale.TotalNeto,
                            VatRate = (vat * 100).ToString(CultureInfo.CurrentCulture),
                            PeriodAmount = payment.Sale.TotalSale(vat),
                            Vat = (int)Math.Floor(payment.Sale.TotalNeto * vat)

                        }
                    },
                    Detalle = payment.Sale.Products
                        .SelectMany((p, index) => new List<DetailDTO>()
                        {
                            new DetailDTO()
                            {
                                Id = index + 1 ,
                                QtyItem = p.Quantity,
                                NmbItem = p.Name,
                                PriceItem = (int)p.PriceNeto,
                                CdgItem = new List<ItemCode>()
                                {
                                    new ItemCode("SKU",p.SkuCode),
                                    new ItemCode("EAN13", p.BarCode.ToString())
                                }

                            }
                        }).ToList()


                }
            };
        }

        public static PendingDocumentEntity ToPendingDocument<T>(this T dto)
        {
            return new PendingDocumentEntity()
            {
                DocumentType = nameof(dto),
                DocumentDataJson = JsonConvert.SerializeObject(dto)
            };
        }

        public static PaymentDto ToPaymentDto(this Payment model)
        {
            var dto = new PaymentDto();
            dto.CopyPropertiesFrom(model);
            return dto;
        }

    }
}

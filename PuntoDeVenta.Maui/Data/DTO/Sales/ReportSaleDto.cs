using Newtonsoft.Json;
using PuntoDeVenta.Maui.Domain.Helpers;

namespace PuntoDeVenta.Maui.Data.DTO.Sales
{
    public class ReportSaleDto : DataReportDto
    {


        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("PdfBase64")]
        public string PdfBase64 { get; set; }

        [JsonProperty("TotalNet")]
        public double TotalNet => Products.IsNull() ? 0 : Math.Floor(Products.Sum(p => p.SubTotal));

        [JsonProperty("Iva")]
        public double Iva { get; set; }

        [JsonIgnore]
        public double AmountIva => Iva == 0 ? 0 : Math.Floor(TotalNet * (1 + Iva));

        public IEnumerable<ProductSalesDto> Products { get; set; }

        public IEnumerable<string> ToReportString(int distributorCode, int officeCode)
        {
            return Products.Where(p => p.InReport).Select(r =>

                $"{distributorCode};{officeCode};{Dte};{Invoice};{r.Itm};{Date:yyyyMMdd};{Delivery:yyyyMMdd};{SellerCode};{Rut};{r.Sku};{r.Quantity};{r.Udm};{r.PriceGross};{r.PriceGross * r.Quantity}"

            );
        }
    }
}

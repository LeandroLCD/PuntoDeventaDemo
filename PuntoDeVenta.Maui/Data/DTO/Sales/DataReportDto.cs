using Newtonsoft.Json;
using PuntoDeVenta.Maui.Data.Models;

namespace PuntoDeVenta.Maui.Data.DTO.Sales
{
    public abstract class DataReportDto
    {
        [JsonProperty("Rut")]
        public string Rut { get; set; }

        [JsonProperty("Dte")]
        public DteType Dte { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Delivery")]
        public DateTime Delivery { get; set; }

        [JsonProperty("Invoice")]
        public long Invoice { get; set; }

        [JsonProperty("SellerCode")]
        public string SellerCode { get; set; }
    }
}

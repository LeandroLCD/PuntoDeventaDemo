using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.Sales
{
    public class ProductSalesDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Sku")]
        public int Sku { get; set; }

        [JsonProperty("Udm")]
        public string Udm { get; set; }

        [JsonProperty("Rut")]
        public string SkuCode => $"{Sku}_{Udm}";

        [JsonProperty("InReport")]
        public bool InReport { get; set; }

        [JsonProperty("Itm")]
        public int Itm { get; set; }

        [JsonProperty("BarCode")]
        public long BarCode { get; set; }

        [JsonProperty("PriceGross")]
        public double PriceGross { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public double SubTotal => PriceGross * Quantity;
    }
}

using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.CatalogueProduct
{
    public class ProductDTO
    {
        [JsonProperty("name")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("BarCode")]
        public long BarCode { get; set; }

        [JsonProperty("Sku")]
        public int Sku { get; set; }

        [JsonProperty("UDM")]
        public string UDM { get; set; }

        [JsonProperty("IsOffer")]
        public bool IsOffer { get; set; }

        [JsonProperty("InReport")]
        public bool InReport { get; set; }

        [JsonProperty("Percentage")]
        public float Percentage { get; set; }

        [JsonProperty("PriceGross")]
        public double PriceGross { get; set; }

        [JsonProperty("Stock")]
        public int Stock { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace PuntoDeventa.Data.DTO.CatalogueProduct
{
    public class CategoryDTO
    {
        [JsonProperty("name")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("Products")]
        public Dictionary<string, ProductDTO> Products { get; set; }
    }
}

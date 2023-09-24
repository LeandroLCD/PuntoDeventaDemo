using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace PuntoDeventa.Data.DTO.CatalogueClient
{
    internal class SalesRoutesDTO
    {
        [JsonProperty("name")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<ClientDTO> Clients { get; set; }
    }
}

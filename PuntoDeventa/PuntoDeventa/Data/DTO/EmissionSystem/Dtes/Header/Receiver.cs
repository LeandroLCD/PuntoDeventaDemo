using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes
{
    internal class Receiver
    {
        [JsonProperty("RUTRecep")]
        public string Rut { get; set; }

        [JsonProperty("RznSocRecep")]
        public string Name { get; set; }

        [JsonProperty("RUTRecep")]
        public string Turn { get; set; }

        [JsonProperty("DirRecep")]
        public string Address { get; set; }

        [JsonProperty("CmnaRecep")]
        public string Commune { get; set; }
    }
}
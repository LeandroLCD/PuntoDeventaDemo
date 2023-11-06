using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes
{
    public class Resolution
    {
        [JsonProperty("fecha")]
        public string Date { get; set; }

        [JsonProperty("numero")]
        public int Number { get; set; }
    }
}

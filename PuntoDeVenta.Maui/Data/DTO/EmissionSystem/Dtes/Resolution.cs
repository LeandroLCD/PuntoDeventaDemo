using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes
{
    public class Resolution
    {
        [JsonProperty("fecha")]
        public string Date { get; set; }

        [JsonProperty("numero")]
        public int Number { get; set; }
    }
}

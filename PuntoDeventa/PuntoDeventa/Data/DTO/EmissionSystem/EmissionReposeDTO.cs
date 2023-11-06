using Newtonsoft.Json;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;

namespace PuntoDeventa.Data.DTO.EmissionSystem
{
    public class EmissionReposeDTO
    {
        [JsonProperty("TOKEN")] public string Token { get; set; }

        [JsonProperty("WARNING")] public string Warning { get; set; }

        [JsonProperty("XML")] public string Xml { get; set; }

        [JsonProperty("PDF")] public string Pdf { get; set; }

        [JsonProperty("TIMBRE")] public string Timbre { get; set; }

        [JsonProperty("FOLIO")] public int Folio { get; set; }

        [JsonProperty("RESOLUCION")] public Resolution Resolution { get; set; }

    }

}

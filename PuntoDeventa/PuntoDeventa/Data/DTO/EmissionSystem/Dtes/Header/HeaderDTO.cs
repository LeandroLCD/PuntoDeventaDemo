using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Header
{
    internal class HeaderDTO
    {
        [JsonProperty("IdDoc")]
        public IdDoc IdDoc { get; set; }

        [JsonProperty("Emisor")]
        public IssuingCompany IssuingCompany { get; set; }

        [JsonProperty("Receptor")]
        public Receiver Receiver { get; set; }

        [JsonProperty("Totales")]
        public Totals Totals { get; set; }
    }
}
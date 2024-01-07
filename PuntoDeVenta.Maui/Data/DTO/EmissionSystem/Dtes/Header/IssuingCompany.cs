using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes.Header
{
    public class IssuingCompany
    {
        [JsonProperty("RUTEmisor")]
        public string Rut { get; set; }

        [JsonProperty("RznSoc")]
        public string Name { get; set; }

        [JsonProperty("GiroEmis")]
        public string Turn { get; set; }

        [JsonProperty("Acteco")]
        public int EconomicActivityCode { get; set; }

        [JsonProperty("DirOrigen")]
        public string Address { get; set; }

        [JsonProperty("CmnaOrigen")]
        public string Commune { get; set; }

        [JsonProperty("Telefono")]
        public string Phone { get; set; }

        [JsonProperty("CdgSIISucur")]
        public string AddressCode { get; set; }
    }
}
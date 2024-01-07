using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.CatalogueClient
{
    public class BranchOfficesDTO
    {
        [JsonProperty("cdgSIISucur")]
        public int Code { get; set; }

        [JsonProperty("comuna")]
        public string Commune { get; set; }

        [JsonProperty("direccion")]
        public string Address { get; set; }

        [JsonProperty("ciudad")]
        public string City { get; set; }

        [JsonProperty("telefono")]
        public string Phone { get; set; }
    }
}

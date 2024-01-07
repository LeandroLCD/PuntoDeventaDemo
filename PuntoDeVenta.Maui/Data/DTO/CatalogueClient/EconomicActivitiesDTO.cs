using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.CatalogueClient
{
    public class EconomicActivitiesDTO
    {
        [JsonProperty("giro")]
        public string Turn { get; set; }

        [JsonProperty("actividadEconomica")]
        public string Name { get; set; }

        [JsonProperty("codigoActividadEconomica")]
        public int Code { get; set; }

        [JsonProperty("actividadPrincipal")]
        public bool IsMain { get; set; }
    }
}

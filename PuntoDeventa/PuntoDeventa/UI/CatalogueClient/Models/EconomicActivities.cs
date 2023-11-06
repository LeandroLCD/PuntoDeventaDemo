using Newtonsoft.Json;

namespace PuntoDeventa.UI.CatalogueClient.Model
{
    public class EconomicActivities
    {
        [JsonProperty("giro")]
        public string Turn { get; set; }

        [JsonProperty("actividadEconomica")]
        public string Name { get; set; }

        [JsonProperty("codigoActividadEconomica")]
        public int Code { get; set; }

        [JsonProperty("actividadPrincipal")]
        public bool IsMain { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is int other)
            {
                return Code == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

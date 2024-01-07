using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.UI.CatalogueClient.Model
{
    public class BranchOffices
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

        [JsonProperty("IsMatrixHouse")]
        public bool IsMatrixHouse { get; set; }

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

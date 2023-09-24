using Newtonsoft.Json;
using System.Collections.Generic;

namespace PuntoDeventa.Data.DTO.CatalogueClient
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ClientDTO
    {
        [JsonProperty("name")]
        public string Id { get; set; }

        [JsonProperty("rut")]
        public string Rut { get; set; }

        [JsonProperty("razonSocial")]        
        public string Name { get; set; }

        [JsonProperty("direccion")]
        public string Address { get; set; }

        [JsonProperty("comuna")]
        public string CommuneName { get; set; }

        [JsonProperty("route")]
        public string RouteId { get; set; }

        [JsonProperty("telefono")]
        public string Phone { get; set; }

        [JsonProperty("sucursales")]
        public List<BranchOfficesDTO> BranchOffices { get; set; }

        [JsonProperty("actividades")]
        public List<EconomicActivitiesDTO> EconomicActivities { get; set; }
    }
}

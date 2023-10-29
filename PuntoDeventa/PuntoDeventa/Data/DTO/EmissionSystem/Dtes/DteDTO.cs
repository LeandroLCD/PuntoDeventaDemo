using Newtonsoft.Json;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Detail;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Header;
using System.Collections.Generic;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes
{
    internal abstract class DteDTO
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("Encabezado")]
        public HeaderDTO Headers { get; set; }

        [JsonProperty("Detalle")]
        public List<DetailDTO> Detalle { get; set; }
    }
}

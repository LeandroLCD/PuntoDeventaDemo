using Newtonsoft.Json;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes.Detail;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes.Header;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes
{
    public abstract class DteDTO
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("Encabezado")]
        public HeaderDTO Headers { get; set; }

        [JsonProperty("Detalle")]
        public List<DetailDTO> Detalle { get; set; }
    }
}

using Newtonsoft.Json;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes.References;

namespace PuntoDeventa.Data.DTO.EmissionSystem
{
    internal class Dte61DTO : DteDTO
    {
        

        [JsonProperty("Referencia")]
        public Reference[] References { get; set; }

    }
}

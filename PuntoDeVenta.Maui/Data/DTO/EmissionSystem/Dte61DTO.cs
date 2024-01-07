using Newtonsoft.Json;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes.References;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem
{
    internal class Dte61DTO : DteDTO
    {


        [JsonProperty("Referencia")]
        public Reference[] References { get; set; }

    }
}

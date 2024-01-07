using Newtonsoft.Json;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem
{
    public class DocumentElectronicDTO
    {
        [JsonProperty("response")]
        public string[] ResponseStrings { get; set; }

        [JsonProperty("dte")]
        public DteDTO Dte { get; set; }


    }
}

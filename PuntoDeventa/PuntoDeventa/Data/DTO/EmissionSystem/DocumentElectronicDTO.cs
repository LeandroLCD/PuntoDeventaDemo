using Newtonsoft.Json;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;

namespace PuntoDeventa.Data.DTO.EmissionSystem
{
    public class DocumentElectronicDTO
    {
        [JsonProperty("response")]
        public string[] ResponseStrings { get; set; }

        [JsonProperty("dte")]
        public DteDTO Dte { get; set; }


    }
}

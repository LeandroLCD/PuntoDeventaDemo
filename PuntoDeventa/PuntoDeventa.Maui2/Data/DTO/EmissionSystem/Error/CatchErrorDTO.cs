using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Error
{
    public class CatchErrorDTO
    {
        [JsonProperty("error")]
        public ErrorDTO Error { get; set; }
    }
}

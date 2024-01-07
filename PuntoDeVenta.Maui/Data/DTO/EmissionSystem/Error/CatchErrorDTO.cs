using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Error
{
    public class CatchErrorDTO
    {
        [JsonProperty("error")]
        public ErrorDTO Error { get; set; }
    }
}

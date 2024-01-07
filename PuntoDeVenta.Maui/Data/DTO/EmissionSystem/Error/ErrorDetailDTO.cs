using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Error
{
    public class ErrorDetailDTO
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("issue")]
        public string Issue { get; set; }

        public override string ToString()
        {
            return $"Campo:{Field}->{Issue}";
        }
    }
}
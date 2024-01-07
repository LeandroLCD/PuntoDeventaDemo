using Newtonsoft.Json;
using PuntoDeVenta.Maui.Domain.Helpers;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Error
{
    public class ErrorDTO
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("details")]
        public List<ErrorDetailDTO> Details { get; set; }

        public override string ToString()
        {
            return Details.IsNull()
                ? $"Error Code:{Code}.\n Mensaje: {Message}."
                : $"Error Code:{Code}.\n Mensaje: {Message}.\n {string.Join(Environment.NewLine, Details)}";
        }
    }
}

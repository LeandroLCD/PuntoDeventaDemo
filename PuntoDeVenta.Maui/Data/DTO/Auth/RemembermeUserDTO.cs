using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.Auth
{
    public class RemembermeUserDTO(string email, bool isRememberme)
    {
        [JsonProperty("email")]
        public string Email { get; set; } = email;
        [JsonProperty("isRememberme")]
        public bool IsRememberme { get; set; } = isRememberme;
    }
}

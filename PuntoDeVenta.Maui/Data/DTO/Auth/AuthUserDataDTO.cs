using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.Auth
{
    internal class AuthUserDataDTO
    {
        public AuthUserDataDTO(string email, string password)
        {
            Email = email;
            Password = password;
            ReturnSecureToken = true;
        }
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
}

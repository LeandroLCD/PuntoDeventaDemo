using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.Auth
{
    public class UserDataDTO
    {
        [JsonProperty("localId")]
        public string LocalId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("idToken")]
        public string IdToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("expiresIn")]
        public long ExpiresIn { get; set; }

        public DateTime DateLogin { get; set; }
    }
}

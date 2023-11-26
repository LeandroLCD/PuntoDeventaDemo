using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.Auth
{
    internal class RefreshTokenDTO
    {
        public RefreshTokenDTO(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        [JsonProperty("grant_type")]
        public string Grant_type => "refresh_token";

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}

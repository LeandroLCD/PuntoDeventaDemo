using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.Auth
{
    public class RemembermeUserDTO
    {
        public RemembermeUserDTO(string email, bool isRememberme)
        {
            Email = email;
            IsRememberme = isRememberme;
        }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("isRememberme")]
        public bool IsRememberme { get; set; }
    }
}

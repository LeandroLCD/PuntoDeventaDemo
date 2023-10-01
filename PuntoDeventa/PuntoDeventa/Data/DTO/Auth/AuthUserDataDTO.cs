using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Data.DTO.Auth
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

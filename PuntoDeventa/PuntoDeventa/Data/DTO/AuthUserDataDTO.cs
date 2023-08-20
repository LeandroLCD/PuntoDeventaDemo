using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Data.DTO
{
    internal class AuthUserDataDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
}

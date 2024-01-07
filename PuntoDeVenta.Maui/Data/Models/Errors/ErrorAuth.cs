using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.Models.Errors
{
    public class ErrorAuth
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
    }

}

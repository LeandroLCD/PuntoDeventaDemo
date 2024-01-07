using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes.References
{
    public class Reference
    {
        public Reference()
        {
            Id = 1;
            Code = "1";
            DocumentNumber = "33";
        }

        [JsonProperty("NroLinRef")]
        public int Id { get; set; }

        [JsonProperty("TpoDocRef")]
        public string DocumentNumber { get; set; }

        [JsonProperty("FolioRef")]
        public string Invoce { get; set; }

        [JsonProperty("FchRef")]
        public string Date { get; set; }

        [JsonProperty("CodRef")]
        public string Code { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PuntoDeventa.Data.DTO.EmissionSystem
{
    internal class EmissionReposeDTO
    {
        [JsonProperty("TOKEN")] public string Token { get; set; }

        [JsonProperty("WARNING")] public string Warning { get; set; }

        [JsonProperty("XML")] public string Xml { get; set; }

        [JsonProperty("PDF")] public string Pdf { get; set; }

        [JsonProperty("TIMBRE")] public string Timbre { get; set; }

        [JsonProperty("FOLIO")] public int Folio { get; set; }
        
    }

}

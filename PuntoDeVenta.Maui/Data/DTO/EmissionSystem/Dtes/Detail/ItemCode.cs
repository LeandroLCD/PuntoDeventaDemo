using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes.Detail
{
    public class ItemCode
    {
        public ItemCode(string type, string value)
        {
            Type = type;
            Value = value;
        }

        [JsonProperty("TpoCodigo")]
        public string Type { get; set; }

        [JsonProperty("VlrCodigo")]
        public string Value { get; set; }
    }
}
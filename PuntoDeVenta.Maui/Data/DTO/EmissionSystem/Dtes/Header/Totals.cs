using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes.Header
{
    public class Totals
    {
        [JsonProperty("MntNeto")]
        public int Net { get; set; }

        [JsonProperty("TasaIVA")]
        public string VatRate { get; set; }

        [JsonProperty("Vat")]
        public int Vat { get; set; }

        [JsonProperty("MntTotal")]
        public int Amount { get; set; }

        [JsonProperty("MontoPeriodo")]
        public int PeriodAmount { get; set; }

        [JsonProperty("VlrPagar")]
        public int AmountPay { get; set; }
    }
}
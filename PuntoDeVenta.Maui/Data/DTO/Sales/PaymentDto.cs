using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.Sales
{
    public class PaymentDto
    {
        //TODO una vez creada la capa UI descomentar PaymentType
        //[JsonProperty("Type")]
        //public PaymentType Type { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }

        [JsonProperty("Invoice")]
        public int Invoice { get; set; }

        [JsonProperty("AccountingId")]
        public string AccountingId { get; set; }
    }
}
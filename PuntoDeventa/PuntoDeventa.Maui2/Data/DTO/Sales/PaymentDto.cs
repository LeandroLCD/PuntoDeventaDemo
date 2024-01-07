using Newtonsoft.Json;
using PuntoDeventa.UI.Sales.Models;
using System;

namespace PuntoDeventa.Data.DTO.Sales
{
    public class PaymentDto
    {
        [JsonProperty("Type")]
        public PaymentType Type { get; set; }

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
using System;

namespace PuntoDeVenta.Maui.UI.Sales.Models
{
    public class Payment
    {
        public Payment(int amount, PaymentType type)
        {
            Amount = amount;
            Type = type;
        }
        public PaymentType Type { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int Amount { get; set; }

        public int Invoice { get; set; }

        public string AccountingId { get; set; }
    }
}

using System;

namespace PuntoDeventa.UI.Sales.Models
{
    public class Payment
    {
        public Payment(double amount, PaymentType type)
        {
            Amount = amount;
            Type = type;
        }
        public PaymentType Type { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public double Amount { get; set; }

        public int Invoice { get; set; }

        public string AccountingId { get; set; }
    }
}

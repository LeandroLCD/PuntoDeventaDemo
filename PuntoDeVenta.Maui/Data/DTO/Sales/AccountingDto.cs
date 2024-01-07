using Newtonsoft.Json;
using PuntoDeVenta.Maui.Domain.Helpers;

namespace PuntoDeVenta.Maui.Data.DTO.Sales
{
    public class AccountingDto : DataReportDto
    {
        private const double TOLERANCE = 0.99;
        public bool IsPaid => Math.Abs(Amount - TotalPayment) < TOLERANCE;
        public double Amount { get; set; }

        [JsonProperty("Payments")]
        public List<PaymentDto> Payments { get; set; }

        public double TotalPayment => (Payments.IsNull() ? 0 : Payments.Sum(p => p.Amount));

        public double PendingPayment => (Payments.IsNull() ? Amount : Amount - TotalPayment);

        public string Note { get; set; }

    }
}

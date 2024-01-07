using Newtonsoft.Json;

namespace PuntoDeVenta.Maui.Data.DTO.Sales
{
    public class PaymentTypeDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Type")]
        public Type PaymentType { get; set; }

        public sealed class CashDto : PaymentTypeDto
        {
            public CashDto()
            {
                PaymentType = GetType();
            }
        }

        public sealed class BankDepositDto : PaymentTypeDto
        {
            public BankDepositDto()
            {
                PaymentType = GetType();
            }
        }

        public sealed class BankTransferDto : PaymentTypeDto
        {
            public BankTransferDto()
            {
                PaymentType = GetType();
            }
        }

        public sealed class BankCheckDto : PaymentTypeDto
        {
            public BankCheckDto()
            {
                PaymentType = GetType();
            }
        }
    }
}

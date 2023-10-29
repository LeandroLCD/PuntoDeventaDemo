namespace PuntoDeventa.UI.Sales.Models
{
    public abstract class PaymentType
    {
        private PaymentType() { }
        public double Amount { get; private set; }
        public sealed class Cash : PaymentType
        {
            public Cash(double amount)
            {
                Amount = amount;
            }
        }

        public sealed class BankDeposit : PaymentType
        {
            public BankDeposit(double amount)
            {
                Amount = amount;
            }
        }

        public sealed class BankTransfer : PaymentType
        {
            public BankTransfer(double amount)
            {
                Amount = amount;
            }
        }

        public sealed class BankCheck : PaymentType
        {
            public BankCheck(double amount)
            {
                Amount = amount;
            }
        }
    }
}

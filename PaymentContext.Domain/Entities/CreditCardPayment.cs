using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            int cardHolderName,
            int cardNumber,
            int lastTransactionNumber,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string payer,
            Document document,
            Address billingAddress,
            Email email
            ) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                payer,
                document,
                billingAddress,
                email)

        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public int CardHolderName { get; private set; }
        public int CardNumber { get; private set; }
        public int LastTransactionNumber { get; private set; }
    }
}
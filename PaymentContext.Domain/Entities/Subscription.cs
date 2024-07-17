using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            IsActive = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<Payment> Payments
        {
            get
            {
                return _payments.ToArray();
            }
        }

        public void AddPayment(Payment payment)
        {
            AddNotifications(
                new Contract<Subscription>()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "Payment date should be in the future")
            );

            _payments.Add(payment);
        }
        public void Activate()
        {
            IsActive = true;
            LastUpdateDate = DateTime.Now;
        }
        public void Deactivate()
        {
            IsActive = false;
            LastUpdateDate = DateTime.Now;
        }

    }
}
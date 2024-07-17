using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        public IList<string> Notifications;
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get
            {
                return _subscriptions.ToArray();
            }
        }

        public void AddSubscription(Subscription subscription)
        {
            var hasActiveSubscription = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.IsActive)
                {
                    hasActiveSubscription = true;
                }

            }
            AddNotifications(
                    new Contract<Subscription>()
                    .Requires()
                    .IsFalse(hasActiveSubscription, "Student.Subscription", "There is already an Active Subscription")
                    .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "This subscription has no Payments"));
        }
    }
}
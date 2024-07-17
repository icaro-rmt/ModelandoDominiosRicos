using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(
                new Contract<Name>()
                .Requires()
                .IsNotEmpty(FirstName, "Name.FirstName", "Invalid FirstName")
                .IsNotEmpty(LastName, "Name.LastName", "Invalid LastName")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
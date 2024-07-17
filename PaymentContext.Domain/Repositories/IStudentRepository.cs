using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        // Abstracion for Domain implementation, working with abstractions segregates 
        // the domain and secures the application
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);

    }
}
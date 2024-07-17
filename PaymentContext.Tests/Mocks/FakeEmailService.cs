using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void SendEmail(string to, string email, string subject, string body)
        {

        }
    }
}
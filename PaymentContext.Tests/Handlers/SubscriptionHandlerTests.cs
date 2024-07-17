using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "teste@example.com";
            command.BarCode = "123455";
            command.BoletoNumber = "1";
            command.PaymentNumber = "123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 100;
            command.TotalPaid = 100;
            command.Payer = "WAYNE CORP";
            command.PayerDocument = "111111111111";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.Street = "RUA DAS COUVES";
            command.Number = "1";
            command.Neighborhood = "SAO PAULO";
            command.City = "SAO PAULO";
            command.State = "SP";
            command.Country = "BRASIL";
            command.PayerEmail = "teste2@example.com";

            handler.Handle(command);

            Assert.AreEqual(false, !command.IsValid);

        }

        [TestMethod]
        public void ShouldReturnErrorWhenEmailExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "hello@example.com";
            command.BarCode = "123455";
            command.BoletoNumber = "1";
            command.PaymentNumber = "123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 100;
            command.TotalPaid = 100;
            command.Payer = "WAYNE CORP";
            command.PayerDocument = "111111111111";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.Street = "RUA DAS COUVES";
            command.Number = "1";
            command.Neighborhood = "SAO PAULO";
            command.City = "SAO PAULO";
            command.State = "SP";
            command.Country = "BRASIL";
            command.PayerEmail = "teste2@example.com";

            handler.Handle(command);

            Assert.AreEqual(false, !command.IsValid);

        }
    }
}
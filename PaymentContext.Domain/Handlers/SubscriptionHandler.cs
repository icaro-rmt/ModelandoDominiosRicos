using System.Diagnostics.Contracts;
using System.Net.Mail;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
     Notifiable<Notification>,
     IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail fast validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "It was not possible to create a subscription");
            }
            // Verify if Document is already signed up
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "This document is already being used");
            }

            // Verify if Email is already sined up
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Email", "This E-mail is already being used");
            }
            // Generate the value Objects
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country);
            var email = new Email(command.Email);
            // Generate the entities

            var student = new Student(name, document, email);
            var subscription = new Subscription(null);
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            // Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Group Validations
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Save information
            _studentRepository.CreateSubscription(student);

            // Send Welcome Email
            _emailService.SendEmail(
                student.Name.ToString(),
                student.Email.Address,
                "Welcome to the Course!",
                "Your subscription was created!"
            );
            // Return Information
            return new CommandResult(true, "Subscription sign up successfully");

        }
    }
}
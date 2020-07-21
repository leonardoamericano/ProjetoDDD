using System;
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
    public class SubscriptionHandler : Notifiable, 
        IHandler<CreateBoletoSubscriptionCommand>, 
        IHandler<CreatePayPalSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IEmailService _emailService;
        private readonly IStudentRepository _repository;
        //Implementação para trabalhar com injeção de dependência
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command); // Para agrupar as notificações
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }
            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");
            
            //Verificar se e-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.AddressNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));//Assinaturas mensais
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, 
            command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument,command.PayerDocumentType), 
            address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if(Invalid)
                return new CommandResult(false, "Não foi possível realizada sua assinatura");

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Adress, "Assinatura criada", "Seja bem vindo");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");

        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command); // Para agrupar as notificações
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }
            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");
            
            //Verificar se e-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.AddressNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));//Assinaturas mensais

            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate, 
            command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument,command.PayerDocumentType), 
            address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if(Invalid)
                return new CommandResult(false, "Não foi possível realizada sua assinatura");

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Adress, "Assinatura criada", "Seja bem vindo");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command); // Para agrupar as notificações
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }
            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");
            
            //Verificar se e-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.AddressNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));//Assinaturas mensais

            var payment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber,
            command.PaidDate, command.ExpireDate, 
            command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument,command.PayerDocumentType), 
            address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

             //Checar as notificações
            if(Invalid)
                return new CommandResult(false, "Não foi possível realizada sua assinatura");

           //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Adress, "Assinatura criada", "Seja bem vindo");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
}        
}
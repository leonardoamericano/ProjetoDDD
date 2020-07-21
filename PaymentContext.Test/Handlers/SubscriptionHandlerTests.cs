using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Test.Mocks;

namespace PaymentContext.Test.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExistis()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "leonardo";
            command.LastName = "viana";
            command.Document = "99999999999";
            command.Email = "leo@leo.com1";

            command.PaymentNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "Wane corp";
            command.PayerDocument = "12345678909";
            command.PayerDocumentType = EDocumentType.CPF;

            command.PayerEmail = "bat@dc.com";
            command.Street = "aaa";
            command.AddressNumber = "aaa";
            command.Neighborhood = "aaa";
            command.City = "aaaa";
            command.State = "aaa";
            command.Country = "aaa";
            command.ZipCode = "aaa";

            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenEmailExistis()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "leonardo";
            command.LastName = "viana";
            command.Document = "19999999999";
            command.Email = "leo@leo.com";

            command.PaymentNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "Wane corp";
            command.PayerDocument = "12345678909";
            command.PayerDocumentType = EDocumentType.CPF;

            command.PayerEmail = "bat@dc.com";
            command.Street = "aaa";
            command.AddressNumber = "aaa";
            command.Neighborhood = "aaa";
            command.City = "aaaa";
            command.State = "aaa";
            command.Country = "aaa";
            command.ZipCode = "aaa";

            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
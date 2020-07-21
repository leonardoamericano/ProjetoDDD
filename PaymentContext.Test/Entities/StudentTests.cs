using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Test
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Student _studant;
        private readonly Address _address;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("12345678909", EDocumentType.CPF);
            _email = new Email("bruce@dc.com");
            _studant = new Student(_name, _document, _email);
            _address = new Address("rua nova", "1", "teste", "gotan", "SP", "Brasil", "08330285");
            _subscription = new Subscription(null);            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5),10,10,"WAYNE COORP", _document, _address, _email );
            
            _subscription.AddPayment(payment);
            _studant.AddSubscription(_subscription);
            _studant.AddSubscription(_subscription);

            Assert.IsTrue(_studant.Invalid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5),10,10,"WAYNE COORP", _document, _address, _email );
            
            _subscription.AddPayment(payment);
            _studant.AddSubscription(_subscription);

            Assert.IsTrue(_studant.Valid);
        }
        [TestMethod]
        public void ShouldReturnErroWhenValorPagoZero()
        {
            var payment = new PayPalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5),10,0,"WAYNE COORP", _document, _address, _email );

            Assert.IsTrue(payment.Invalid);
        }
    }
}

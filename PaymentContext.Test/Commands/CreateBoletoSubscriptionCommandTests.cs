using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Test.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var boletoCommand = new CreateBoletoSubscriptionCommand();
            boletoCommand.FirstName = "";
            
            boletoCommand.Validate();
            Assert.AreEqual(false, boletoCommand.Valid);
        }
    }
}
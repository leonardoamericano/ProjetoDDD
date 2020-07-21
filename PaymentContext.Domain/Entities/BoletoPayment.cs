using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{

    public class BoletoPayment :Payment
    {
        private string v1;
        private object now;
        private object p;
        private int v2;
        private int v3;
        private string v4;
        private object document;
        private object address;
        private object email;
        public BoletoPayment(string barCode, 
        string boletoNumber, 
        DateTime paidDate, 
        DateTime expireDate, 
        decimal total, 
        decimal totalPaid, 
        string payer, 
        Document document, 
        Address adress, 
        Email email): base( paidDate, 
                            expireDate, 
                            total, 
                            totalPaid, 
                            payer, 
                            document, 
                            adress, 
                            email)
        {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string  BarCode { get; private set; }
        public string  BoletoNumber { get; private set; }
        
    }
}
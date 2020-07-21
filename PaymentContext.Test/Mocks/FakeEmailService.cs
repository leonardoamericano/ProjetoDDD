using PaymentContext.Domain.Services;

namespace PaymentContext.Test.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subjetc, string body)
        {
            
        }
    }
}
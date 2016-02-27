using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCreditCardTokenService : MobService<CreditCardToken>, IPayUCreditCardTokenService
    {
        public PayUCreditCardTokenService(IPayUCreditCardTokenRepository repository) : base(repository) { }
    }
}

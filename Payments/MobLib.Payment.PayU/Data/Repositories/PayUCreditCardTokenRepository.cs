using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUCreditCardTokenRepository : MobRepository<CreditCardToken>, IPayUCreditCardTokenRepository
    {
        public PayUCreditCardTokenRepository(IPayUContext context) : base(context) { }
    }
}

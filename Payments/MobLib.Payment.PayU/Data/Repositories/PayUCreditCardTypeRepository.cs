using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUCreditCardTypeRepository : MobRepository<CreditCardType>, IPayUCreditCardTypeRepository
    {
        public PayUCreditCardTypeRepository(IPayUContext context) : base(context) { }
    }
}

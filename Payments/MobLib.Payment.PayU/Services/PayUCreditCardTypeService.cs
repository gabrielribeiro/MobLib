using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCreditCardTypeService : MobService<CreditCardType>, IPayUCreditCardTypeService
    {
        public PayUCreditCardTypeService(IPayUCreditCardTypeRepository repository) : base(repository) { }
    }
}

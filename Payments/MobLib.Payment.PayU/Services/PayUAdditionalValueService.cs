using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUAdditionalValueService : MobService<AdditionalValue>, IPayUAdditionalValueService
    {
        public PayUAdditionalValueService(IPayUAdditionalValueRepository repository) : base(repository) { }
    }
}

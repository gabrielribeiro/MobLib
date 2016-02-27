using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCurrencyService : MobService<Currency>, IPayUCurrencyService
    {
        public PayUCurrencyService(IPayUCurrencyRepository repository) : base(repository) { }
    }
}

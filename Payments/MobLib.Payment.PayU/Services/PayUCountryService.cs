using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCountryService : MobService<Country>, IPayUCountryService
    {
        public PayUCountryService(IPayUCountryRepository repository) : base(repository) { }
    }
}

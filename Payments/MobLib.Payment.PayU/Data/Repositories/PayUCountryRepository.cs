using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUCountryRepository : MobRepository<Country>, IPayUCountryRepository
    {
        public PayUCountryRepository(IPayUContext context) : base(context) { }
    }
}

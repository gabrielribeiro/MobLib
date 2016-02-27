using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUCurrencyRepository : MobRepository<Currency>, IPayUCurrencyRepository
    {
        public PayUCurrencyRepository(IPayUContext context) : base(context) { }
    }
}

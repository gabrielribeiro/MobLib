using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUSubscriptionRepository : MobRepository<Subscription>, IPayUSubscriptionRepository
    {
        public PayUSubscriptionRepository(IPayUContext context) : base(context) { }
    }
}

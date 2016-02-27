using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUSubscriptionService : MobService<Subscription>, IPayUSubscriptionService
    {
        public PayUSubscriptionService(IPayUSubscriptionRepository repository) : base(repository) { }
    }
}

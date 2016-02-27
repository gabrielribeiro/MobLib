using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUPlanService : MobService<Plan>, IPayUPlanService
    {
        public PayUPlanService(IPayUPlanRepository repository) : base(repository) { }
    }
}

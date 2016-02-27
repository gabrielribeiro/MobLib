using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUPlanIntervalService : MobService<PlanInterval>, IPayUPlanIntervalService
    {
        public PayUPlanIntervalService(IPayUPlanIntervalRepository repository) : base(repository) { }
    }
}

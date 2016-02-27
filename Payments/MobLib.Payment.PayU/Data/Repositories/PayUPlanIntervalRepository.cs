using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUPlanIntervalRepository : MobRepository<PlanInterval>, IPayUPlanIntervalRepository
    {
        public PayUPlanIntervalRepository(IPayUContext context) : base(context) { }
    }
}

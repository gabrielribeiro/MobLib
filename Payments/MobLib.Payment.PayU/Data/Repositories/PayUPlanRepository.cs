using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUPlanRepository : MobRepository<Plan>, IPayUPlanRepository
    {
        public PayUPlanRepository(IPayUContext context) : base(context) { }
    }
}

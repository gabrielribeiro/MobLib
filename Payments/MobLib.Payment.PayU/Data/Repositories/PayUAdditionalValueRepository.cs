using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUAdditionalValueRepository : MobRepository<AdditionalValue>, IPayUAdditionalValueRepository 
    {
        
        public PayUAdditionalValueRepository(IPayUContext context) : base(context) { }
    }
}

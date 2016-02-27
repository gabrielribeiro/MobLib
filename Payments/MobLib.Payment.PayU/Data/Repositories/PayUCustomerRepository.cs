using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Data.Repositories
{
    public class PayUCustomerRepository : MobRepository<Customer>, IPayUCustomerRepository
    {
        public PayUCustomerRepository(IPayUContext context) : base(context) { }
    }
}

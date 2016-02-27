using MobLib.Core.Services;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCustomerService : MobService<Customer>, IPayUCustomerService
    {
        public PayUCustomerService(IPayUCustomerRepository repository) : base(repository) { }
    }
}

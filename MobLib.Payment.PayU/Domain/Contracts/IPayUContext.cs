using MobLib.Payment.PayU.Domain.Entities;
using System.Data.Entity;

namespace MobLib.Payment.PayU.Domain.Contracts
{
    public interface IPayUContext
    {
        DbSet<Customer> PayUCustomer { get; set; }
    }
}

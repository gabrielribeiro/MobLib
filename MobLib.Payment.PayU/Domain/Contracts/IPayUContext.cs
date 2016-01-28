using MobLib.Payment.PayU.Domain.Entities;
using System.Data.Entity;

namespace MobLib.Payment.PayU.Domain.Contracts
{
    public interface IPayUContext<TCustomer, TAddress, TToken>
        where TCustomer : class, IPayUCustomer, new()
        where TAddress : class, IPayUAddress, new()
        where TToken : class, IPayUToken, new()
    {
        DbSet<TCustomer> PayUCustomer { get; set; }
        DbSet<TAddress> PayUAddress { get; set; }
        DbSet<TToken> PayUToken { get; set; }
    }
}

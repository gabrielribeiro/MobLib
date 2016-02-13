using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Entities;
using System.Data.Entity;

namespace MobLib.Payment.PayU.Domain.Contracts
{
    public interface IPayUContext : IMobContext
    {
        DbSet<Customer> PayUCustomer { get; set; }
        DbSet<Country> PayUCountry { get; set; }
        DbSet<AdditionalValue> PayUAditionalValue { get; set; }
        DbSet<CreditCardToken> PayUCreditCardToken { get; set; }
        DbSet<CreditCardType> PayUCreditCardType { get; set; }
        DbSet<Currency> PayUCurrency { get; set; }
        DbSet<Plan> PayUPlan { get; set; }
        DbSet<PlanInterval> PayUPlanInterval { get; set; }
        DbSet<Subscription> PayUSubscription { get; set; }
    }
}

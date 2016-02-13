using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Payment.Tests.TestContext
{
    public class PayUContext : MobDbContext, IPayUContext
    {
        public PayUContext()
            : base("PayU")
        {
        }

        public DbSet<Customer> PayUCustomer { get; set; }
        public DbSet<Country> PayUCountry { get; set; }
        public DbSet<AdditionalValue> PayUAditionalValue { get; set; }
        public DbSet<CreditCardToken> PayUCreditCardToken { get; set; }
        public DbSet<CreditCardType> PayUCreditCardType { get; set; }
        public DbSet<Currency> PayUCurrency { get; set; }
        public DbSet<Plan> PayUPlan { get; set; }
        public DbSet<PlanInterval> PayUPlanInterval { get; set; }
        public DbSet<Subscription> PayUSubscription { get; set; }
    }
}

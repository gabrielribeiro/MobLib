using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Data.Mapping;
using MobLib.Payment.PayU.Domain.Entities;
using System.Data.Entity;

namespace MobLib.Payment.PayU.Data
{
    public class PayUDataModeler : IDataModeler
    {
        public void RegisterModelConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new AdditionalValueMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new CreditCardTypeMap());
            modelBuilder.Configurations.Add(new CreditCardTokenMap());
            modelBuilder.Configurations.Add(new PlanMap());
            modelBuilder.Configurations.Add(new PlanIntervalMap());
            modelBuilder.Configurations.Add(new SubscriptionMap());

            modelBuilder.ComplexType<Address>().Property(x => x.Line1).IsRequired();
            modelBuilder.ComplexType<Address>().Property(x => x.Line2).IsOptional();
            modelBuilder.ComplexType<Address>().Property(x => x.Line3).IsOptional();
            modelBuilder.ComplexType<Address>().Property(x => x.PostalCode).IsRequired();
            modelBuilder.ComplexType<Address>().Property(x => x.State).IsRequired();
            modelBuilder.ComplexType<Address>().Property(x => x.City).IsRequired();
        }

        public int Order
        {
            get { return 100; }
        }
    }
}

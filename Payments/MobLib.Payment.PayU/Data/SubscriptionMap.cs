using MobLib.Payment.PayU.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data
{
    public class SubscriptionMap : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionMap()
        {
            this.ToTable("PayU.Subscription");
            this.HasKey(x => x.Id);
            this.Property(x => x.CustomerId).IsRequired();
            this.Property(x => x.PlanId).IsRequired();
            this.Property(x => x.CreditCardTokenId).IsRequired();
            this.Property(x => x.SubscriptionPayUId).IsRequired();
            this.Property(x => x.Quantity).IsRequired();
            this.Property(x => x.Installments).IsRequired();
            this.Property(x => x.TrialDays).IsRequired();
            this.Property(x => x.StartPeriod).IsRequired();
            this.Property(x => x.EndPeriod).IsRequired();

            this.HasRequired(x => x.Customer)
                .WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.CustomerId);

            this.HasRequired(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId);

            this.HasRequired(x => x.CreditCardToken)
                .WithMany()
                .HasForeignKey(x => x.CreditCardTokenId);

        }
    }
}

using MobLib.Payment.PayU.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data
{
    public class PlanMap : EntityTypeConfiguration<Plan>
    {
        public PlanMap()
        {
            this.ToTable("PayU.Plan");
            this.HasKey(x => x.Id);
            this.Property(x => x.PlanPayUId).IsRequired();
            this.Property(x => x.AccountId).IsRequired();
            this.Property(x => x.PlanCode).IsRequired();
            this.Property(x => x.Description).IsRequired();
            this.Property(x => x.IntervalId).IsRequired();
            this.Property(x => x.MaxPaymentsAllowed).IsRequired();
            this.Property(x => x.MaxPaymentAttempts).IsOptional();
            this.Property(x => x.PaymentAttemptsDelay).IsOptional();
            this.Property(x => x.MaxPendingPayments).IsOptional();
            this.Property(x => x.TrialDays).IsOptional();

            this.HasRequired(x => x.PlanInterval)
                .WithMany()
                .HasForeignKey(x => x.IntervalId);
        }
    }
}

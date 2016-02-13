using MobLib.Payment.PayU.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data
{
    public class AdditionalValueMap : EntityTypeConfiguration<AdditionalValue>
    {
        public AdditionalValueMap()
        {
            this.ToTable("PayU.AdditionalValue");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Value).IsRequired();
            this.Property(x => x.CurrencyId).IsRequired();

            this.HasRequired(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId);
        }
    }
}

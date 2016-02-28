using MobLib.Payment.PayU.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data.Mapping
{
    public class CreditCardTokenMap : EntityTypeConfiguration<CreditCardToken>
    {
        public CreditCardTokenMap()
        {
            this.ToTable("PayU.CreditCardToken");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.CustomerId).IsRequired();
            this.Property(x => x.Document).IsRequired();
            this.Property(x => x.CreditCardTypeId).IsRequired();
            this.Property(x => x.CustomerId).IsRequired();
            this.Property(x => x.Token).IsRequired();
            this.Property(x => x.Number).IsRequired();
            this.Property(x => x.ExpirationDate).IsRequired();
            this.Property(x => x.CountryId).IsRequired();
            this.Ignore(x => x.VerificationCode);

            this.HasRequired(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            this.HasRequired(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId);

            this.HasRequired(x => x.CreditCardType)
                .WithMany()
                .HasForeignKey(x => x.CreditCardTypeId);
        }
    }
}

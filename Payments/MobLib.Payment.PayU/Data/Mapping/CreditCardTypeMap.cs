using MobLib.Payment.PayU.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data.Mapping
{
    public class CreditCardTypeMap : EntityTypeConfiguration<CreditCardType>
    {
        public CreditCardTypeMap()
        {
            this.ToTable("PayU.CreditCardType");
            this.HasKey(x => x.CreditCardTypeCode);

            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.CreditCardTypeCode).IsRequired();
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Code).IsRequired();
        }
    }
}

using MobLib.Payment.PayU.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            this.ToTable("PayU.Country");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Code).IsRequired();
        }
    }
}

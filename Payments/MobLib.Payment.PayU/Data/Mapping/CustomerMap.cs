using MobLib.Payment.PayU.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("PayU.Customer");
            this.HasKey(x => x.Id);
            this.Property(x => x.CustomerPayUId).IsRequired();
            this.Property(x => x.FullName).IsRequired();
            this.Property(x => x.ContactPhone).IsRequired();
            this.Property(x => x.EmailAddress).IsRequired();
        }
    }
}

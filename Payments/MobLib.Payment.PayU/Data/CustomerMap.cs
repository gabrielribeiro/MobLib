using MobLib.Payment.PayU.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Payment.PayU.Data
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

using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MobLib.Core.Infra.Data
{
    public class MobDbContext : DbContext
    {
        public MobDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //removes stupids convetions of the entity framework
            //delete cascade and automatic plural for table names
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //defines that all string columns will be VARCHAR(255) by default if not mapped
            modelBuilder.Properties<string>().Configure(s => s.HasMaxLength(255).HasColumnType("varchar"));
            //defines that all decimal columns will be DECIMAL(10,2) by default if not mapped
            modelBuilder.Properties<decimal>().Configure(d => d.HasPrecision(10, 2));

            //defines that all columns in BaseEntity is not null
            modelBuilder.Properties<int>().Where(d => d.Name == "Id").Configure(d => d.IsRequired());
            modelBuilder.Properties<DateTime>().Where(d => d.Name == "CreatedDate").Configure(d => d.IsRequired());
            modelBuilder.Properties<DateTime>().Where(d => d.Name == "CreatedDate").Configure(d => d.IsRequired());
            modelBuilder.Properties<bool>().Where(x => x.Name == "Active").Configure(b => b.IsRequired());


            base.OnModelCreating(modelBuilder);
        }
    }
}

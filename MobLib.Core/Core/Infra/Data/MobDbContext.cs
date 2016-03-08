using MobLib.Core.Domain.Contracts;
using MobLib.Core.Domain.Entities;
using MobLib.Core.Infra.Dependency;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace MobLib.Core.Infra.Data
{
    public class MobDbContext : DbContext, IMobContext
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
            modelBuilder.Properties<DateTime>().Where(d => d.Name == "UpdatedDate").Configure(d => d.IsRequired());
            modelBuilder.Properties<bool>().Where(x => x.Name == "Active").Configure(b => b.IsRequired());

            //var typeFinder = new MobTypeFinder();
            //var modelers = typeFinder.GetInstancesOf<IDataModeler>().OrderBy(x => x.Order);

            //foreach (var modeler in modelers)
            //{
            //    modeler.RegisterModelConfiguration(modelBuilder);
            //}

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var operationDate = DateTime.Now;
            var trackerStatuses = new[] { System.Data.Entity.EntityState.Added, System.Data.Entity.EntityState.Modified };

            var entries = this.ChangeTracker.Entries()
                            .Where(e => trackerStatuses.Contains(e.State) && e.Entity is MobEntity)
                            .Select(x => x.Cast<MobEntity>());

            foreach (var entry in entries)
            {
                if (entry.State == System.Data.Entity.EntityState.Added)
                {
                    entry.Entity.CreatedDate = operationDate;
                    entry.Entity.UpdatedDate = operationDate;
                    entry.Entity.Active = true;
                }
                else
                {
                    entry.Property(e => e.CreatedDate).IsModified = false;
                    entry.Entity.UpdatedDate = operationDate;
                }
            }

            return base.SaveChanges();
        }
    }
}

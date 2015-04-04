using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MobLib.Core.Infra.Data
{
    public class MobDbContext : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<string>().Configure(s => s.HasMaxLength(255).HasColumnType("varchar"));
            modelBuilder.Properties<decimal>().Configure(d => d.HasPrecision(10, 2));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MobLib.Core.Infra.Data
{
    public interface IMobContext : IDisposable
    {
        int SaveChanges();

        Database Database { get; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}

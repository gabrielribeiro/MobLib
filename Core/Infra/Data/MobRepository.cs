using EntityFramework.Extensions;
using MobLib.Core.Domain.Interfaces;
using MobLib.Core.Infra.Data.Extentions;
using MobLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace MobLib.Core.Infra.Data
{
    public class MobRepository<T> : IMobRepository<T>
        where T : class, IMobEntity, new()
    {
        #region .::Fields::.

        private MobDbContext db;
        private DbSet<T> entitySet;

        #endregion

        #region .::Properties::.

        public bool AutoCommitEnabled { get; set; }
        public MobDbContext Context
        {
            get
            {
                return db;
            }
        }
        protected DbSet<T> Entities
        {
            get
            {
                if (entitySet == null)
                {
                    entitySet = db.Set<T>();
                }
                return entitySet as DbSet<T>;
            }
        }

        #endregion

        #region .::Ctor::.

        public MobRepository(MobDbContext context)
        {
            this.AutoCommitEnabled = true;
            SetOrChangeContext(context);
        }

        #endregion

        #region .::Read Actions::.

        public T Find(params object[] keyValues)
        {

            return this.Entities.Find(keyValues);
        }

        public IEnumerable<T> Get()
        {
            return this.Entities;
        }

        public IEnumerable<T> GetBySearch(T search)
        {

            IQueryable<T> result = this.Entities;

            foreach (var property in search.GetAllComplexProperties())
            {
                result = result.Include(property.Name);
            }
            result = result.FilterPopulatedProperties(search);
            return result;
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            return this.Entities.Where(filterExpression);
        }

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            return this.Entities.SingleOrDefault(filterExpression);
        }

        #endregion

        #region .::Write Actions::.

        public void Insert(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.Active = true;

            this.Entities.Add(entity);
            if (this.AutoCommitEnabled)
            {
                db.SaveChanges();
            }
        }

        public void InsertRange(IEnumerable<T> entities, int batchSize = 100)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                if (entities.Any())
                {
                    if (batchSize <= 0)
                    {
                        // insert all in one step
                        entities.Each(x => this.Entities.Add(x));
                        Context.SaveChanges();
                    }
                    else
                    {
                        int i = 1;
                        bool saved = false;
                        foreach (var entity in entities)
                        {
                            entity.CreatedDate = DateTime.UtcNow;
                            entity.UpdatedDate = DateTime.UtcNow;
                            entity.Active = true;

                            this.Entities.Add(entity);
                            saved = false;
                            if (i % batchSize == 0)
                            {
                                if (this.AutoCommitEnabled)
                                { 
                                    Context.SaveChanges();
                                }
                                i = 0;
                                saved = true;
                            }
                            i++;
                        }

                        if (!saved)
                        {
                            if (this.AutoCommitEnabled)
                            {
                                this.Context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <remarks>this operation its not in Change tracker and will be executed directly on database</remarks>
        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            this.Entities.Where(filterExpression).Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="updateExpression"></param>
        /// <remarks>this operation its not in Change tracker and will be executed directly on database</remarks>
        public void Update(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression, System.Linq.Expressions.Expression<Func<T, T>> updateExpression)
        {
            this.Entities.Update(filterExpression, updateExpression);
        }

        public void Update(T entity)
        {
            this.Entities.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.Entry(entity).Property(t => t.CreatedDate).IsModified = false;
            db.Entry(entity).Property(t => t.UpdatedDate).CurrentValue = DateTime.UtcNow;
            if (this.AutoCommitEnabled)
            {
                this.Context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                this.Entities.Attach(entity);
            }
            this.Entities.Remove(entity);

            if (this.AutoCommitEnabled)
            {
                this.Context.SaveChanges();
            }

        }
        #endregion

        #region .::IDisposable::.
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion

        #region .::Helpers::.

        protected void SetOrChangeContext(MobDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "The context can't be null");
            }

            if (context.Database.Connection.State == System.Data.ConnectionState.Broken
                || context.Database.Connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    context.Database.Connection.Open();
                }
                catch (Exception ex)
                {

                    throw new InvalidOperationException("It's not possible to connect to the server", ex);
                }
            }

            entitySet = context.Set<T>();
            if (entitySet == null)
            {
                throw new ArgumentNullException("Entities", "Set for Entity T not located in the DbContext");
            }

            db = context;
        }

        public void Commit() 
        {
            this.Context.SaveChanges();
        }

        #endregion
    }
}

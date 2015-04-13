using MobLib.Core.Domain.Interfaces;
using MobLib.Core.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MobLib.Core.Services
{
    public class MobService<T> : IMobService<T> where T : class, IEntity, new()
    {
        #region .::Fields::.
        private IMobRepository<T> repository;
        #endregion

        #region .::Ctor::.

        public MobService(MobDbContext dbContext)
        {
            repository = CreateRepository(dbContext);
        }

        protected virtual MobRepository<T> CreateRepository(MobDbContext dbContext)
        {
            return new MobRepository<T>(dbContext);
        }

        public MobService(IMobRepository<T> repo)
        {
            this.repository = repo;
        }

        #endregion

        #region .::Properties::.

        public virtual IMobRepository<T> Repository
        {
            get { return repository; }
        }

        #endregion

        #region .::Read Actions::.

        public virtual T Find(params object[] keyValues)
        {
            if (keyValues == null) 
            {
                throw new ArgumentNullException("keyValues");
            }

            return this.Repository.Find(keyValues);

        }

        public virtual IEnumerable<T> Get()
        {
            return this.Repository.Get();

        }

        public virtual IEnumerable<T> GetBySearch(T search)
        {
            return this.repository.GetBySearch(search);
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> filterExpression)
        {
            return this.Repository.Where(filterExpression);
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> filterExpression, T defaultValue)
        {
            T result = this.Repository.SingleOrDefault(filterExpression);

            if (result == null && defaultValue != null)
            {
                result = defaultValue;
            }
            return result;
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> filterExpression)
        {
            return SingleOrDefault(filterExpression, null);
        }

        public virtual bool Exists(Expression<Func<T, bool>> filterExpression)
        {
            return this.Where(filterExpression).Any();
        }
        #endregion

        #region .::Write Actions::.

        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.Repository.Insert(entity);
        }

        public virtual void InsertRange(IEnumerable<T> entities, int batchSize)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities");
            }

            this.Repository.InsertRange(entities, batchSize);
        }
        public virtual void InsertRange(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities");
            }

            this.InsertRange(entities, 10);
        }

        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            this.Repository.Delete(filterExpression);
        }

        public virtual void Delete(T entity)
        {
            this.Repository.Delete(entity);
        }


        public virtual void Update(Expression<Func<T, bool>> filterExpression, 
            Expression<Func<T, T>> updateExpression)
        {
            this.Repository.Update(filterExpression, updateExpression);
        }

        public virtual void Update(T entity)
        {
            this.Repository.Update(entity);
        }
        #endregion

        public void Dispose()
        {
            //in case of the property been overrided
            if (this.repository.GetType().FullName != this.Repository.GetType().FullName)
            {
                this.repository.Dispose();
            }

            this.Repository.Dispose();

        }
    }
}

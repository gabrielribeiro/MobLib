using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobLib.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace MobLib.Core.Services
{
    public class MobService<T> : IService<T> where T : class, IEntity, new()
    {
        #region .::Fields::.
        private IRepository<T> repository;
        #endregion

        #region .::Ctor::.

        public MobService(IRepository<T> repo)
        {
            this.repository = repo;
        }

        #endregion

        #region .::Properties::.

        public IRepository<T> Repository
        {
            get { return repository; }
        }

        #endregion

        #region .::Read Actions::.

        public T Find(params object[] keyValues)
        {
            if (keyValues == null) 
            {
                throw new ArgumentNullException("keyValues");
            }

            return this.Repository.Find(keyValues);

        }

        public IEnumerable<T> Get()
        {
            return this.Repository.Get();

        }

        public IEnumerable<T> GetBySearch(T search)
        {
            return this.repository.GetBySearch(search);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> filterExpression)
        {
            return this.Repository.Where(filterExpression);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> filterExpression, T defaultValue = null)
        {
            T result = this.Repository.SingleOrDefault(filterExpression);

            if (result == null && defaultValue != null)
            {
                result = defaultValue;
            }
            return result;
        }

        public bool Exists(Expression<Func<T, bool>> filterExpression)
        {
            return this.Where(filterExpression).Any();
        }
        #endregion

        #region .::Write Actions::.

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.Repository.Insert(entity);
        }

        public void InsertRange(IEnumerable<T> entities, int batchSize)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities");
            }

            this.Repository.InsertRange(entities, batchSize);
        }
        public void InsertRange(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities");
            }

            this.InsertRange(entities, 10);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            this.Repository.Delete(filterExpression);
        }

        public void Delete(T entity)
        {
            this.Repository.Delete(entity);
        }


        public void Update(Expression<Func<T, bool>> filterExpression, 
            Expression<Func<T, T>> updateExpression)
        {
            this.Repository.Update(filterExpression, updateExpression);
        }

        public void Update(T entity)
        {
            this.Repository.Update(entity);
        }
        #endregion

        public void Dispose()
        {
            this.Repository.Dispose();
        }
    }
}

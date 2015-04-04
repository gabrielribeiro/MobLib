using MobLib.Core.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MobLib.Core.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        #region .::Properties::.
        MobDbContext Context { get; }
        bool AutoCommitEnabled { get; set; }
        #endregion

        #region .::Read Actions::.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Gel all Entitys of the type
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// /
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetBySearch(TEntity search);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filterExpression);
        #endregion

        #region .::Write Actions::.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void InsertRange(IEnumerable<TEntity> entities, int batchSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        void Delete(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="updateExpression"></param>
        void Update(Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        #endregion
    }
}

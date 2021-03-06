﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Core.Domain.Contracts
{
    public interface IMobService<TEntity> : IDisposable where TEntity : IMobEntity
    {
        IMobRepository<TEntity> Repository { get; }
        bool AutoSaveEnabled { get; set; }

        #region .::Read Options::.
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
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filterExpression,
            TEntity defaultValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> filterExpression);
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
        void InsertRange(IEnumerable<TEntity> entities);

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

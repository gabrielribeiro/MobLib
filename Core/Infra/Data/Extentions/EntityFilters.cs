using MobLib.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MobLib.Core.Infra.Data.Extentions
{
    public static class EntityFilters
    {
        /// <summary>
        /// Create a query for searching in the entityFramework based on a populated class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">the list of entities to be filtered</param>
        /// <param name="filter">class used as parameter to filter</param>
        /// <returns></returns>
        public static IQueryable<T> FilterPopulatedProperties<T>(this IQueryable<T> collection, T filter) where T : new()
        {
            //get de type of the entity
            var item = Expression.Parameter(typeof(T), "x");

            //get all properties
            PropertyInfo[] properties = typeof(T).GetProperties();

            //create a new instace of T to be compared as base
            T baseFilter = new T();

            foreach (var prop in properties)
            {
                //just compare if is a value type or is string
                if (prop.PropertyType.IsValueType || prop.PropertyType.IsPrimitive || prop.PropertyType.Name == "String")
                {
                    Object value = prop.GetValue(filter, null);
                    Object defaultValue = prop.GetValue(baseFilter, null);
                    //compares if the value passed is equal of a new instace of the property
                    if (value != null && (defaultValue == null || !value.Equals(defaultValue)))
                    {
                        //build the lambda expression
                        var lambda = Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(Expression.Property(item, prop),
                                             Expression.Constant(value, prop.PropertyType)
                                            ), item);
                        //applicate the lambda into the collection
                        collection = collection.Where(lambda);
                    }
                }
            }
            return collection;
        }

        /// <summary>
        /// Validate all properties from a class if it respects the nullable clauses from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">entity class to be validated</param>
        public static void ValidateNotNullProperties<T>(this T entity) where T : new()
        {
            //get all properties
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(entity, null);
                //just compare if is a value type or is string
                if (prop.PropertyType.IsValueType || prop.PropertyType.IsPrimitive)
                    //if is nullable continue
                    if ((prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        continue;
                    //if is not nullable and the value is null throws execption
                    else if (value == null)
                        throw new ArgumentNullException(prop.Name);
            }
        }

        /// <summary>
        /// Returns all properties that are virtual from 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetAllComplexProperties<T>(this T entity) where T : new()
        {
            //get all properties
            PropertyInfo[] properties = typeof(T).GetProperties();

            //recover all properties from entity interface;
            var names = typeof(IMobEntity).GetProperties().Select(x => x.Name);

            //remove properties from interface because they are all virtual 
            properties = properties.Where(x => !names.Contains(x.Name)).ToArray();

            foreach (var prop in properties)
            {
                //just compare if is a value type or is string
                if (prop.GetGetMethod().IsVirtual)
                {
                    yield return prop;
                }
            }
        }
    }
}

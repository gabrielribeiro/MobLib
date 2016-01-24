using System;
using System.Collections.Generic;

namespace MobLib.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Converts any object to Int32
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int ToInt<T>(this T type)
        {
            return Convert.ToInt32(type);
        }

        /// <summary>
        /// Converts any object to Int64
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static long ToLong<T>(this T type)
        {
            return Convert.ToInt64(type);
        }

        /// <summary>
        /// Converts the elements in the current IEnumerable to another type, 
        /// and returns a collection containing the converted elements.
        /// </summary>
        /// <param name="collection">Current collection to be converted.</param>
        /// <param name="converter">A Converter delegate that converts each element from one type to another type.</param>
        /// <returns>A IEnumerable of the target type containing the converted elements from the current IEnumerable.</returns>
        public static IEnumerable<TOutput> ConvertAll<T, TOutput>
            (this IEnumerable<T> collection, Func<T, TOutput> converter)
        {
            if (null == converter)
                throw new ArgumentNullException("converter");

            foreach (T item in collection)
                yield return converter(item);
        }

        /// <summary>
        /// Performs an action on each item while iterating through a list. 
        /// This is a handy shortcut for <c>foreach(item in list) { ... }</c>
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="source">The list, which holds the objects.</param>
        /// <param name="action">The action delegate which is called on each item while iterating.</param>
        //[DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T t in source)
            {
                action(t);
            }
        }
    }
}

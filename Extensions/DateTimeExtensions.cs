using System;

namespace MobLib.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a T object to datetime
        /// </summary>
        /// <typeparam name="T">Type of the object to be converted</typeparam>
        /// <param name="element">the object to be converted</param>
        /// <returns>a valid datetime</returns>
        /// <exception cref="System.FormatException"/>
        /// <exception cref="System.InvalidCastException"/>
        public static DateTime ToDateTime<T>(this T element)
        {
            return Convert.ToDateTime(element);
        }
    }
}

using System;

namespace MobLib.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime<T>(this T element)
        {
            return Convert.ToDateTime(element);
        }
    }
}

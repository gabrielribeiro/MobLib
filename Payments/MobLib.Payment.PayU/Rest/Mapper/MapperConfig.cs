using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Payment.PayU.Rest.Mapper
{
    internal static class MapperConfig
    {
        static MapperConfig()
        {
        }

        internal static TDestination Map<TSource, TDestination>(this TSource source)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}

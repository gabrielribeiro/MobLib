using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Payment.PayU.Rest.Mapper
{
    internal static class MapperConfig
    {
        private static IMapper mapper;
        static MapperConfig()
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });
            mapper = config.CreateMapper();
        }

        internal static TDestination Map<TSource, TDestination>(this TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }
    }
}

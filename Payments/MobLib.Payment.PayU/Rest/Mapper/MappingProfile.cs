using AutoMapper;
using MobLib.Payment.PayU.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Payment.PayU.Rest.Mapper
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Plan, Models.Plan>()
               .ForMember(dest => dest.id, conf => conf.MapFrom(src => src.PlanPayUId))
               .ForMember(dest => dest.Interval, conf => conf.MapFrom(src => src.PlanInterval.Code));

            this.CreateMap<Models.Plan, Plan>()
                .ForMember(dest => dest.PlanPayUId, conf => conf.MapFrom(src => src.id))
               .ForMember(dest => dest.PlanInterval, conf => conf.MapFrom(src => new PlanInterval { Code = src.Interval }))
               .ForMember(dest => dest.Id, conf => conf.Ignore());

            this.CreateMap<AdditionalValue, Models.AdditionalValue>()
               .ForMember(dest => dest.Currency, conf => conf.MapFrom(src => src.Currency.Code));

            this.CreateMap<Models.AdditionalValue, AdditionalValue>()
               .ForMember(dest => dest.Currency, conf => conf.MapFrom(src => new Currency { Code = src.Currency }));
        }
    }
}
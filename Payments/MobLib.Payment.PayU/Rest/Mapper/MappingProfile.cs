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
               .ForMember(dest => dest.PlanId, conf => conf.MapFrom(src => src.PlanPayUId));

            this.CreateMap<AdditionalValue, Models.AdditionalValue>()
               .ForMember(dest => dest.Currency, conf => conf.MapFrom(src => src.Currency.Code));
        }
    }
}
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
               .ForMember(dest => dest.PlanId, conf => conf.MapFrom(src => src.PlanPayUId))
               .ForMember(dest => dest.Interval, conf => conf.MapFrom(src => src.PlanInterval.Code));

            this.CreateMap<Models.Plan, Plan>()
                .ForMember(dest => dest.PlanPayUId, conf => conf.MapFrom(src => src.PlanId))
               .ForMember(dest => dest.PlanInterval, conf => conf.MapFrom(src => new PlanInterval { Code = src.Interval }))
               .ForMember(dest => dest.Id, conf => conf.Ignore());

            this.CreateMap<Customer, Models.Customer>();
            this.CreateMap<Models.Customer, Customer>();

            this.CreateMap<CreditCardToken, Models.CreditCard>()
               .ForMember(dest => dest.CustomerId, conf => conf.MapFrom(src => src.Customer.CustomerPayUId))
               .ForMember(dest => dest.CreditCardTypeId, conf => conf.MapFrom(src => src.CreditCardType.Code))
               .ForMember(dest => dest.ExpirationMonth, conf => conf.MapFrom(src => src.ExpirationDate.ToString("MM")))
               .ForMember(dest => dest.ExpirationYear, conf => conf.MapFrom(src => src.ExpirationDate.ToString("yy")))
               .AfterMap((src, dest) =>
               {
                   dest.Address.Country = src.Country.Code;
                   dest.Address.Phone = src.Customer.ContactPhone;
               });
            this.CreateMap<Models.CreditCard, CreditCardToken>();

            this.CreateMap<Address, Models.Address>();
            this.CreateMap<Models.Address, Address>();

            this.CreateMap<Subscription, Models.Subscription>()
                .ForMember(src => src.StartPeriod, conf => conf.Ignore())
                .ForMember(src => src.EndPeriod, conf => conf.Ignore());
            this.CreateMap<Models.Subscription, Subscription>()
                .ForMember(src => src.StartPeriod, conf => conf.Ignore())
                .ForMember(src => src.EndPeriod, conf => conf.Ignore())
                .AfterMap((src, dest) =>
                {
                    var dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
                    if (src.StartPeriod.HasValue)
                    {
                        dest.StartPeriod = dateTime.AddMilliseconds(src.StartPeriod.Value);
                    }

                    if (src.EndPeriod.HasValue)
                    {
                        dest.EndPeriod = dateTime.AddMilliseconds(src.EndPeriod.Value);
                    }
                });

            this.CreateMap<CreditCardToken, Models.SubscriptionCreditCard>();
            this.CreateMap<Models.SubscriptionCreditCard, CreditCardToken>();

            this.CreateMap<Customer, Models.SubscriptionCustomer>()
               .ForMember(dest => dest.CreditCards, conf => conf.MapFrom(src => src.CreditCardTokens));
            this.CreateMap<Models.SubscriptionCustomer, Customer>()
               .ForMember(dest => dest.CreditCardTokens, conf => conf.MapFrom(src => src.CreditCards));

            this.CreateMap<Plan, Models.SubscriptionPlan>();
            this.CreateMap<Models.SubscriptionPlan, Plan>();

            this.CreateMap<AdditionalValue, Models.AdditionalValue>()
               .ForMember(dest => dest.Currency, conf => conf.MapFrom(src => src.Currency.Code));

            this.CreateMap<Models.AdditionalValue, AdditionalValue>()
               .ForMember(dest => dest.Currency, conf => conf.MapFrom(src => new Currency { Code = src.Currency }));
        }
    }
}
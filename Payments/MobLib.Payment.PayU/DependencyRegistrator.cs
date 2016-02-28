using Autofac;
using MobLib.Core.Infra.Dependency;
using MobLib.Payment.PayU.Data.Repositories;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Rest;
using MobLib.Payment.PayU.Services;

namespace MobLib.Payment.PayU
{
    public class DependencyRegistrator : IDependencyRegistrator
    {
        public void Register(Autofac.ContainerBuilder builder, ITypeFinder typeFinder)
        {

            builder.RegisterType<CreditCardTokenRestClient>().SingleInstance();
            builder.RegisterType<CustomerRestClient>().SingleInstance();
            builder.RegisterType<PlanRestClient>().SingleInstance();
            builder.RegisterType<SubscriptionRestClient>().SingleInstance();

            builder.RegisterType<PayUAdditionalValueRepository>().As<IPayUAdditionalValueRepository>().InstancePerRequest();
            builder.RegisterType<PayUCountryRepository>().As<IPayUCountryRepository>().InstancePerRequest();
            builder.RegisterType<PayUCreditCardTokenRepository>().As<IPayUCreditCardTokenRepository>().InstancePerRequest();
            builder.RegisterType<PayUCreditCardTypeRepository>().As<IPayUCreditCardTypeRepository>().InstancePerRequest();
            builder.RegisterType<PayUCurrencyRepository>().As<IPayUCurrencyRepository>().InstancePerRequest();
            builder.RegisterType<PayUCustomerRepository>().As<IPayUCustomerRepository>().InstancePerRequest();
            builder.RegisterType<PayUPlanIntervalRepository>().As<IPayUPlanIntervalRepository>().InstancePerRequest();
            builder.RegisterType<PayUPlanRepository>().As<IPayUPlanRepository>().InstancePerRequest();
            builder.RegisterType<PayUSubscriptionRepository>().As<IPayUSubscriptionRepository>().InstancePerRequest();

            builder.RegisterType<PayUAdditionalValueService>().As<IPayUAdditionalValueService>().InstancePerRequest();
            builder.RegisterType<PayUCountryService>().As<IPayUCountryService>().InstancePerRequest();
            builder.RegisterType<PayUCreditCardTokenService>().As<IPayUCreditCardTokenService>().InstancePerRequest();
            builder.RegisterType<PayUCreditCardTypeService>().As<IPayUCreditCardTypeService>().InstancePerRequest();
            builder.RegisterType<PayUCurrencyService>().As<IPayUCurrencyService>().InstancePerRequest();
            builder.RegisterType<PayUCustomerService>().As<IPayUCustomerService>().InstancePerRequest();
            builder.RegisterType<PayUPlanIntervalService>().As<IPayUPlanIntervalService>().InstancePerRequest();
            builder.RegisterType<PayUPlanService>().As<IPayUPlanService>().InstancePerRequest();
            builder.RegisterType<PayUSubscriptionService>().As<IPayUSubscriptionService>().InstancePerRequest();
        }

        public int Order
        {
            get { return 100; }
        }
    }
}

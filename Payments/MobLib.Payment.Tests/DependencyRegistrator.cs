using MobLib.Core.Infra.Dependency;
using Autofac;
using MobLib.Payment.Tests.TestContext;
using MobLib.Payment.PayU.Domain.Contracts;

namespace MobLib.Payment.Tests
{
    public class DependencyRegistrator : IDependencyRegistrator
    {
        public void Register(Autofac.ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<PayUContext>().As<IPayUContext>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 10; }
        }
    }
}

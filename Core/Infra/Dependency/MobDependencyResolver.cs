using Autofac;
using MobLib.Extensions;
using System.Linq;

namespace MobLib.Core.Infra.Dependency
{
    public abstract class MobDependencyResolver : IDependencyResolver
    {
        public abstract ILifetimeScope Scope { get; }

        public abstract void Initialize();


        public MobDependencyResolver(bool initialize)
        {
            if (initialize)
            {
                this.Initialize();
            }
        }

        protected virtual IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IDependencyResolver>().SingleInstance();
            builder.RegisterType<MobTypeFinder>().As<ITypeFinder>().SingleInstance();

            var typeFinder = new MobTypeFinder();
            var registrators = typeFinder.GetInstancesOf<IDependencyRegistrator>().OrderBy(x=> x.Order);

            foreach (var registrator in registrators)
            {
                registrator.Register(builder, typeFinder);
            }

            builder.Update(container);
            return container;
        }


        public virtual T Resolve<T>(string name = null) where T : class
        {
            if (!name.IsNullOrWhiteSpace())
            {
                return Scope.ResolveNamed<T>(name);
            }
            return Scope.Resolve<T>();
        }

    }
}

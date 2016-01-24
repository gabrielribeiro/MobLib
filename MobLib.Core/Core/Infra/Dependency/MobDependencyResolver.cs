using Autofac;
using MobLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MobLib.Core.Infra.Dependency
{
    public abstract class MobDependencyResolver : IDependencyResolver
    {
        public abstract ILifetimeScope Scope { get; }

        public IEnumerable<IDependencyRegistrator> Registrators { get; set; }

        public abstract void Initialize();


        public MobDependencyResolver(bool initialize)
        {
            if (initialize)
            {
                this.Initialize();
            }
        }

        public MobDependencyResolver(bool initalize, IEnumerable<IDependencyRegistrator> registrators)
            : this(initalize)
        {
            this.Registrators = registrators;
        }

        protected virtual IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IDependencyResolver>().SingleInstance();
            builder.RegisterType<MobTypeFinder>().As<ITypeFinder>().SingleInstance();

            var typeFinder = new MobTypeFinder();

            if (Registrators == null || !Registrators.Any())
            {
                Registrators = typeFinder.GetInstancesOf<IDependencyRegistrator>().OrderBy(x => x.Order);
            }

            foreach (var registrator in Registrators)
            {
                registrator.Register(builder, typeFinder);
            }

            builder.Update(container);
            return container;
        }

        public virtual T Resolve<T>() where T : class
        {
            return this.Resolve<T>(null);
        }

        public virtual T Resolve<T>(string name) where T : class
        {
            if (!name.IsNullOrWhiteSpace())
            {
                return Scope.ResolveNamed<T>(name);
            }
            return Scope.Resolve<T>();
        }
    }
}

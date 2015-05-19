using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Core.Infra.Dependency
{
    public abstract class MobDependencyResolver : IDependencyResolver
    {
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
            builder.Update(container);

            var typeFinder = container.Resolve<ITypeFinder>();
            var registrators = typeFinder.GetInstancesOf<IDependencyRegistrator>();

            foreach (var registrator in registrators)
            {
                registrator.Register(builder, typeFinder);
            }

            builder.Update(container);
            return container;
        }

        public abstract void Initialize();
    }
}

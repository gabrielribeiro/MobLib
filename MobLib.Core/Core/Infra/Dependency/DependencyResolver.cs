using Autofac;
using Autofac.Integration.WebApi;
using System.Collections.Generic;
using Http = System.Web.Http;

namespace MobLib.Core.Infra.Dependency
{
    public class DependencyResolver : MobDependencyResolver
    {
        private IContainer container;

        public DependencyResolver()
            : base(true)
        {

        }

        public DependencyResolver(IEnumerable<IDependencyRegistrator> registrators)
            : base(true, registrators)
        {

        }

        public override ILifetimeScope Scope
        {
            get
            {
                ILifetimeScope scope = null;
              
                if (scope == null)
                {
                    scope = container.BeginLifetimeScope("AutofacWebRequest");
                }

                return scope ?? container;
            }
        }

        public override void Initialize()
        {
            this.container = this.CreateContainer();
        }
    }
}

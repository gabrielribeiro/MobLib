using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Collections.Generic;
using Http = System.Web.Http;

namespace MobLib.Core.Infra.Dependency
{
    public class MvcDependencyResolver : MobDependencyResolver
    {

        public MvcDependencyResolver()
            : base(true)
        {

        }

        public MvcDependencyResolver(IEnumerable<IDependencyRegistrator> registrators)
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

            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}

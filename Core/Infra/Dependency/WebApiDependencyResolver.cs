using Autofac;
using Autofac.Integration.WebApi;
using Http = System.Web.Http;

namespace MobLib.Core.Infra.Dependency
{
    public class WebApiDependencyResolver : MobDependencyResolver
    {
        private IContainer container;

        public WebApiDependencyResolver()
            : base(true)
        {

        }

        public override ILifetimeScope Scope
        {
            get
            {
                ILifetimeScope scope = null;
                try
                {
                    scope = Http.GlobalConfiguration.Configuration.DependencyResolver.GetRequestLifetimeScope();
                }
                catch { }

                if (scope == null)
                {
                    // really hackisch. But strange things are going on ?? :-)
                    scope = container.BeginLifetimeScope("AutofacWebRequest");
                }

                return scope ?? container;
            }/  
        }

        public override void Initialize()
        {
            var config = Http.GlobalConfiguration.Configuration;
            this.container = this.CreateContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(this.container);
        }

    }
}

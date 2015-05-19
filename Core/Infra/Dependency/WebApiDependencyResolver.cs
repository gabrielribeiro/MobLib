using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Http = System.Web.Http;

namespace MobLib.Core.Infra.Dependency
{
    public class WebApiDependencyResolver : MobDependencyResolver
    {
        public override void Initialize()
        {
            var config = Http.GlobalConfiguration.Configuration;
            var container = this.CreateContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }
}

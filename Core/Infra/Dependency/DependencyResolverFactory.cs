using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MobLib.Core.Infra.Dependency
{
    public static class DependencyResolverFactory
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDependencyResolver Create(ResolverType type, IEnumerable<IDependencyRegistrator> registrators = null)
        {
            IDependencyResolver resolver = null;
            switch (type)
            {
                case ResolverType.WebApi:
                    if (registrators != null)
                    {
                        Singleton<WebApiDependencyResolver>.Instance = new WebApiDependencyResolver(registrators);
                    }
                    else
                    {
                        if (Singleton<WebApiDependencyResolver>.Instance == null)
                        {
                            Singleton<WebApiDependencyResolver>.Instance = new WebApiDependencyResolver();
                        }
                    }
                    resolver = Singleton<WebApiDependencyResolver>.Instance;
                    break;
                case ResolverType.Mvc:
                    throw new NotImplementedException("The type informed it's not implemented");
                default:
                    throw new NotSupportedException("The type informed it's not supported");
            }

            return resolver;
        }
    }

    public enum ResolverType
    {
        WebApi,
        Mvc
    }
}

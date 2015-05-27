using System;
using System.Runtime.CompilerServices;

namespace MobLib.Core.Infra.Dependency
{
    public static class DependencyResolverFactory
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDependencyResolver Current(ResolverType type)
        {
            IDependencyResolver resolver = null;
            switch (type)
            {
                case ResolverType.WebApi:
                    if (Singleton<WebApiDependencyResolver>.Instance == null)
                    {
                        Singleton<WebApiDependencyResolver>.Instance = new WebApiDependencyResolver();
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

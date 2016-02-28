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
                    if (registrators != null)
                    {
                        Singleton<DependencyResolver>.Instance = new DependencyResolver(registrators);
                    }
                    else
                    {
                        if (Singleton<DependencyResolver>.Instance == null)
                        {
                            Singleton<DependencyResolver>.Instance = new DependencyResolver();
                        }
                    }
                    resolver = Singleton<DependencyResolver>.Instance;
                    break;
            }
           
            Singleton<IDependencyResolver>.Instance = resolver;
            return resolver;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDependencyResolver GetCurrentResolver() 
        {
            if (Singleton<IDependencyResolver>.Instance == null) 
            {
                throw new NullReferenceException("There is no dependency resolver registrated");
            }
            return Singleton<IDependencyResolver>.Instance;
        }
    }

    public enum ResolverType
    {
        None,
        Default,
        WebApi,
        Mvc
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MobLib.Core.Infra.Dependency
{
    public static class ITypeFinderExtensions
    {
        public static IEnumerable<Type> FindClassesOfType<T>(this ITypeFinder finder)
        {
            return finder.FindClassesOfType(typeof(T), finder.GetAssemblies());
        }

        public static IEnumerable<T> GetInstancesOf<T>(this ITypeFinder finder)
        {
            var types = finder.FindClassesOfType<T>();
            var instances = new List<T>();
            foreach (var type in types)
            {
                instances.Add((T)Activator.CreateInstance(type));
            }
            return instances;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MobLib.Core.Infra.Dependency
{
    /// <summary>
    /// Classes implementing this interface provide information about types 
    /// to various services in the MobLib engine.
    /// </summary>
    public interface ITypeFinder
    {
        IList<Assembly> GetAssemblies();
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies);
    }
}

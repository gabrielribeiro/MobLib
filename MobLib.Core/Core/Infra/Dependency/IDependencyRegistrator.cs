using Autofac;

namespace MobLib.Core.Infra.Dependency
{
    public interface IDependencyRegistrator
    {

        /// <summary>
        /// Lets the implementor register dependencies withing the global dependency container
        /// </summary>
        /// <param name="builder">The container builder instance</param>
        /// <param name="typeFinder">The type finder instance with wich all application types can be reflected</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);
        int Order { get; }
    }
}

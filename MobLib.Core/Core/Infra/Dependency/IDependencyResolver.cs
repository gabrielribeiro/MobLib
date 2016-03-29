
namespace MobLib.Core.Infra.Dependency
{
    public interface IDependencyResolver
    {
        void Initialize();
        
        T Resolve<T>(string name = null) where T : class;
        
        void Register<T>(T instance) where T : class;

        void Register<T>(T instance, string name) where T : class;
    }
}

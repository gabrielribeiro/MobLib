using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Core.Infra.Dependency
{
    public interface IDependencyResolver
    {
        void Initialize();
        T Resolve<T>(string name = null) where T : class;
    }
}

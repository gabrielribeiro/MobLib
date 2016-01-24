using System.Data.Entity;

namespace MobLib.Core.Infra.Data
{
    public interface IDataModeler
    {
        void RegisterModelConfiguration(DbModelBuilder modelBuilder);
        int Order { get; }
    }
}

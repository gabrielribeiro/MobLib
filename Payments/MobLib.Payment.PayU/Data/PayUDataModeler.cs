using MobLib.Core.Infra.Data;
using System.Data.Entity;

namespace MobLib.Payment.PayU.Data
{
    public class PayUDataModeler : IDataModeler
    {
        public void RegisterModelConfiguration(DbModelBuilder modelBuilder)
        {
        }

        public int Order
        {
            get { return 100; }
        }
    }
}

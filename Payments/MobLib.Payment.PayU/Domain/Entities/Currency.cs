using MobLib.Core.Domain.Entities;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Currency : MobEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

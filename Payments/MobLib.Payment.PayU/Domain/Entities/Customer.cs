using MobLib.Core.Domain.Entities;
namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Customer : MobEntity
    {
        public string CustomerPayUId { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string ContactPhone { get; set; }
    }
}

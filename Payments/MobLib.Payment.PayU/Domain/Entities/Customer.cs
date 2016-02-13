using MobLib.Core.Domain.Entities;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Customer : MobEntity
    {
        public Customer()
        {
            this.CreditCardTokens = new HashSet<CreditCardToken>();
            this.Subscriptions = new HashSet<Subscription>();
        }

        public string CustomerPayUId { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string ContactPhone { get; set; }

        public virtual ICollection<CreditCardToken> CreditCardTokens { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }

    }
}

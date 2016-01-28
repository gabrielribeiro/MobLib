using MobLib.Core.Domain.Entities;
using System;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Subscription : MobEntity
    {
        public string SubscriptionPayUId { get; set; }
        public int Quantity { get; set; }
        public int Installments { get; set; }
        public int CustomerId { get; set; }
        public int PlanId { get; set; }
        public DateTime? StartPeriod { get; set; }
        public DateTime? EndPeriod { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Plan Plan { get; set; }
    }
}

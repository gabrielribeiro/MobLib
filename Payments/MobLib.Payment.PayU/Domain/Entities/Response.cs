using MobLib.Core.Domain.Entities;
using System;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Response : MobEntity
    {
        public int? SubscriptionId { get; set; }
        public bool SubscritionFound { get; set; }
        public int ResponseCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionId { get; set; }
        public DateTime? DateNextPayment { get; set; }
        public string Sign { get; set; }
        public string Reference { get; set; }
        public string ReferenceSale { get; set; }
        public string AuthorizationCode { get; set; }
        public string ReferenceRecurringPayment { get; set; }

        public virtual Subscription Subscription { get; set; }
    }
}

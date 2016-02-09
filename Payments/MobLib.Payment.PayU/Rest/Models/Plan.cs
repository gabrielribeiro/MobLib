using RestSharp.Serializers;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class Plan
    {
        [SerializeAs(Name = "id")]
        public string PlanId { get; set; }
        [SerializeAs(Name = "accountId")]
        public int AccountId { get; set; }
        [SerializeAs(Name = "planCode")]
        public string PlanCode { get; set; }
        [SerializeAs(Name = "description")]
        public string Description { get; set; }
        [SerializeAs(Name = "interval")]
        public string Interval { get; set; }
        [SerializeAs(Name = "intervalCount")]
        public int IntervalCount { get; set; }
        [SerializeAs(Name = "maxPaymentsAllowed")]
        public int MaxPaymentsAllowed { get; set; }
        [SerializeAs(Name = "maxPaymentAttempts")]
        public int? MaxPendingPayments { get; set; }
        [SerializeAs(Name = "paymentAttemptsDelay")]
        public int PaymentAttemptsDelay { get; set; }
        [SerializeAs(Name = "trialDays")]
        public int TrialDays { get; set; }

        [SerializeAs(Name = "additionalValues")]
        public virtual ICollection<AdditionalValue> AdditionalValues { get; set; }
    }
}

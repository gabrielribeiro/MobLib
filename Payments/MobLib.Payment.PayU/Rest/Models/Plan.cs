using Newtonsoft.Json;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class Plan
    {
        [JsonProperty("id")]
        public string PlanId { get; set; }
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("planCode")]
        public string PlanCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("interval")]
        public string Interval { get; set; }
        [JsonProperty("intervalCount")]
        public int IntervalCount { get; set; }
        [JsonProperty("maxPaymentsAllowed")]
        public int MaxPaymentsAllowed { get; set; }
        [JsonProperty("maxPaymentAttempts")]
        public int? MaxPendingPayments { get; set; }
        [JsonProperty("paymentAttemptsDelay")]
        public int PaymentAttemptsDelay { get; set; }
        [JsonProperty("trialDays")]
        public int TrialDays { get; set; }

        [JsonProperty("additionalValues")]
        public virtual List<AdditionalValue> AdditionalValues { get; set; }
    }
}

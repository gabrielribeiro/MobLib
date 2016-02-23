using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class Subscription
    {
        [JsonProperty("id")]
        public string SubscriptionPayUId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("installments")]
        public int Installments { get; set; }
        [JsonProperty("trialDays")]
        public int TrialDays { get; set; }
        [JsonProperty("customer")]
        public SubscriptionCustomer Customer { get; set; }
        [JsonProperty("plan")]
        public SubscriptionPlan Plan { get; set; }
    }
}

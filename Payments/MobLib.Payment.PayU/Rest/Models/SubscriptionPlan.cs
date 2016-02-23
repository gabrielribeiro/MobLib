using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionPlan
    {
        [JsonProperty("planCode")]
        public string PlanCode { get; set; }
    }
}

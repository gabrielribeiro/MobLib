using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionCreditCard
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}

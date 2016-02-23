using Newtonsoft.Json;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionCustomer
    {
        [JsonProperty("id")]
        public string CustomerPayUId { get; set; }
        [JsonProperty("creditCards")]
        public IEnumerable<SubscriptionCreditCard> CreditCards { get; set; }
    }
}

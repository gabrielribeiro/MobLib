using Newtonsoft.Json;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionCustomer
    {
        public SubscriptionCustomer()
        {
            this.CreditCards = new List<SubscriptionCreditCard>();
        }

        [JsonProperty("id")]
        public string CustomerPayUId { get; set; }
        [JsonProperty("creditCards")]
        public List<SubscriptionCreditCard> CreditCards { get; set; }
    }
}

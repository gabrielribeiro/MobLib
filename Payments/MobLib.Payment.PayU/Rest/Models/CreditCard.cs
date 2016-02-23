using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class CreditCard
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("type")]
        public int CreditCardTypeId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}

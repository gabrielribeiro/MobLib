using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class CreditCard
    {
        public CreditCard()
        {
            this.Address = new Address();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("document")]
        //public string Document { get; set; }

        [JsonProperty("type")]
        public string CreditCardTypeId { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("expMonth")]
        public string ExpirationMonth { get; set; }

        [JsonProperty("expYear")]
        public string ExpirationYear { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}

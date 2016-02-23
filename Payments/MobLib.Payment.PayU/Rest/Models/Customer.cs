using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class Customer
    {
        [JsonProperty("id")]
        public string CustomerPayUId { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("email")]
        public string EmailAddress { get; set; }
    }
}

using Newtonsoft.Json;
using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    class Address
    {
        [JsonProperty("line1")]
        public string Line1 { get; set; }
        [JsonProperty("line2")]
        public string Line2 { get; set; }
        [JsonProperty("line3")]
        public string Line3 { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }


    }
}

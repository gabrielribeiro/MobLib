using Newtonsoft.Json;

namespace MobLib.Payment.PayU.Rest.Models
{
    public class AdditionalValue
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public decimal? Value { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}

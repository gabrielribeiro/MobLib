using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class AdditionalValue
    {
        [SerializeAs(Name = "name")]
        public string Name { get; set; }
        [SerializeAs(Name = "value")]
        public decimal Value { get; set; }
        [SerializeAs(Name = "currency")]
        public string Currency { get; set; }
    }
}

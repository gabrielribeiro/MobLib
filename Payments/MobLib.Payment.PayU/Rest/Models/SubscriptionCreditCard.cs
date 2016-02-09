using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionCreditCard
    {
        [SerializeAs(Name = "token")]
        public string Token { get; set; }
    }
}

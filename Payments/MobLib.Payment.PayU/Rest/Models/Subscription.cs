using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class Subscription
    {
        [SerializeAs(Name = "id")]
        public string SubscriptionPayUId { get; set; }
        [SerializeAs(Name = "quantity")]
        public int Quantity { get; set; }
        [SerializeAs(Name = "installments")]
        public int Installments { get; set; }
        [SerializeAs(Name = "trialDays")]
        public int TrialDays { get; set; }
        [SerializeAs(Name = "customer")]
        public SubscriptionCustomer Customer { get; set; }
        [SerializeAs(Name = "plan")]
        public SubscriptionPlan Plan { get; set; }
    }
}

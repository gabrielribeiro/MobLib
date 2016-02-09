using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionPlan
    {
        [SerializeAs(Name = "planCode")]
        public string PlanCode { get; set; }
    }
}

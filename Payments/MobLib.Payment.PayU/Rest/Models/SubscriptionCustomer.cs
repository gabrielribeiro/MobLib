using RestSharp.Serializers;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class SubscriptionCustomer
    {
        [SerializeAs(Name = "id")]
        public string CustomerPayUId { get; set; }
        [SerializeAs(Name = "creditCards")]
        public IEnumerable<SubscriptionCreditCard> CreditCards { get; set; }
    }
}

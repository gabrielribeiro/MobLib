using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class CreditCard
    {
        [SerializeAs(Name = "name")]
        public string Name { get; set; }

        [SerializeAs(Name = "document")]
        public string Document { get; set; }

        [SerializeAs(Name = "type")]
        public int CreditCardTypeId { get; set; }

        [SerializeAs(Name = "customerId")]
        public int CustomerId { get; set; }

        [SerializeAs(Name = "token")]
        public string Token { get; set; }

        [SerializeAs(Name = "number")]
        public string Number { get; set; }

        [SerializeAs(Name = "address")]
        public Address Address { get; set; }
    }
}

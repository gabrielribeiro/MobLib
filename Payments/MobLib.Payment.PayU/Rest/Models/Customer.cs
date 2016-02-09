using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    internal class Customer
    {
        [SerializeAs(Name = "id")]
        public string CustomerPayUId { get; set; }
        [SerializeAs(Name = "fullName")]
        public string FullName { get; set; }
        [SerializeAs(Name = "email")]
        public string EmailAddress { get; set; }
    }
}

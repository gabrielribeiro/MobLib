using RestSharp.Serializers;

namespace MobLib.Payment.PayU.Rest.Models
{
    class Address
    {
        [SerializeAs(Name = "line1")]
        public string Line1 { get; set; }
        [SerializeAs(Name = "line2")]
        public string Line2 { get; set; }
        [SerializeAs(Name = "line3")]
        public string Line3 { get; set; }
        [SerializeAs(Name = "postalCode")]
        public string PostalCode { get; set; }
        [SerializeAs(Name = "city")]
        public string City { get; set; }
        [SerializeAs(Name = "country")]
        public string Country { get; set; }
        [SerializeAs(Name = "state")]
        public string State { get; set; }
        [SerializeAs(Name = "phone")]
        public string Phone { get; set; }


    }
}

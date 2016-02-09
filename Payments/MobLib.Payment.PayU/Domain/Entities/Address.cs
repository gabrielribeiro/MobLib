namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        public virtual Country Country { get; set; }
    }
}

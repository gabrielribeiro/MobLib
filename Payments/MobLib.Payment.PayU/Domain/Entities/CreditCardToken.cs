using MobLib.Core.Domain.Entities;
using System;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class CreditCardToken : MobEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public int CreditCardTypeId { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public string Number { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Address Address { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual CreditCardType CreditCardType { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

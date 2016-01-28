using MobLib.Core.Domain.Entities;


namespace MobLib.Payment.PayU.Domain.Entities
{
    public class CreditCardToken : MobEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public int CreditCardTypeId { get; set; }
        public string Token { get; set; }
        public Address Address { get; set; }

        public virtual CreditCardType CreditCardType { get; set; }
    }
}

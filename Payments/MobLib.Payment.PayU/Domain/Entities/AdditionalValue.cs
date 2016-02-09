using MobLib.Core.Domain.Entities;
using System;
namespace MobLib.Payment.PayU.Domain.Entities
{
    public class AdditionalValue : MobEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyCode CurrencyCode
        {
            get
            {
                return (CurrencyCode)this.CurrencyId;
            }
        }

        public virtual Currency Currency { get; set; }
    }
}

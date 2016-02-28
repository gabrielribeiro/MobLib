using MobLib.Core.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.PayU.Domain.Contracts
{
    public interface IPayUCreditCardTokenService : IMobService<CreditCardToken>
    {
        CreditCardTypeCode? GetCardTypeFromNumber(string cardNumber);

        bool IsValidNumber(string cardNumber);
    }
}

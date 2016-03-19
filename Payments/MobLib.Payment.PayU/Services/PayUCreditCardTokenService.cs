using MobLib.Core.Services;
using MobLib.Exceptions;
using MobLib.Extensions;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCreditCardTokenService : MobService<CreditCardToken>, IPayUCreditCardTokenService
    {
        private const string visaRegex = @"^4[0-9]{12}(?:[0-9]{3})?$";
        private const string masterRegex = @"^5[1-5][0-9]{14}$";
        private const string amexRegex = @"^3[47][0-9]{13}$";
        private const string dinersRegex = @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$";
        private const string auraRegex = @"^5[1-5][0-9]{14}$";
        private const string discoverRegex = @"^6(?:011|5[0-9]{2})[0-9]{12}$";
        private const string eloRegex = @"^(401178|401179|431274|438935|451416|457393|457631|457632|504175|627780|636297|636368|(506699|5067[0-6]\d|50677[0-8])|(50900\d|5090[1-9]\d|509[1-9]\d{2})|65003[1-3]|(65003[5-9]|65004\d|65005[0-1])|(65040[5-9]|6504[1-3]\d)|(65048[5-9]|65049\d|6505[0-2]\d|65053[0-8])|(65054[1-9]| 6505[5-8]\d|65059[0-8])|(65070\d|65071[0-8])|65072[0-7]|(65090[1-9]|65091\d|650920)|(65165[2-9]|6516[6-7]\d)|(65500\d|65501\d)|(65502[1-9]|6550[3-4]\d|65505[0-8]))[0-9]{10,12}";
        private const string hipercardRegex = @"^(38[0-9]{17}|60[0-9]{14})$";

        private readonly CreditCardTokenRestClient restClient;
        private readonly IPayUCreditCardTypeService typeService;
        private readonly IPayUCountryService countryService;

        public PayUCreditCardTokenService(IPayUCreditCardTokenRepository repository,
            IPayUCreditCardTypeService typeService,
            IPayUCountryService countryService,
            CreditCardTokenRestClient restClient)
            : base(repository)
        {
            this.restClient = restClient;
            this.typeService = typeService;
            this.countryService = countryService;
        }

        public override void Insert(CreditCardToken creditCardToken)
        {
            if (creditCardToken == null)
            {
                throw new MobException("creditCardToken nao pode ser nulo");
            }

            if (creditCardToken.Customer == null || string.IsNullOrEmpty(creditCardToken.Customer.CustomerPayUId))
            {
                throw new MobException("cliente inválido");
            }

            var typeId = this.GetCardTypeFromNumber(creditCardToken.Number);

            if (typeId == null) 
            {
                throw new MobException("cartão não suportado");
            }

            if (!this.IsValidNumber(creditCardToken.Number))
            {
                throw new MobException("cartão não é valido");            
            }

            var type = this.typeService.Find(typeId);

            if (type == null)
            {
                throw new MobException("tipo de cartão inválido");
            }

            // fixed country
            var country = this.countryService.Find(1);

            if (country == null)
            {
                throw new MobException("pais do cartão inválido");
            }

            creditCardToken.CreditCardType = type;
            creditCardToken.Country = country;

            var postedCreditCard = this.restClient.Post(creditCardToken);

            if (postedCreditCard == null || string.IsNullOrEmpty(postedCreditCard.Token))
            {
                throw new MobException("erro ao executar a solicitação ao PayU para inserir cartao");
            }

            creditCardToken.Token = postedCreditCard.Token;

            base.Insert(creditCardToken);
        }

        public override void Update(CreditCardToken creditCardToken)
        {
            if (creditCardToken == null)
            {
                throw new MobException("creditCardTokeno nao pode ser nulo");
            }

            if (creditCardToken.Customer == null || string.IsNullOrEmpty(creditCardToken.Customer.CustomerPayUId))
            {
                throw new MobException("cliente inválido");
            }

            var typeId = this.GetCardTypeFromNumber(creditCardToken.Number);

            if (typeId == null)
            {
                throw new MobException("cartão não suportado");
            }

            if (!this.IsValidNumber(creditCardToken.Number))
            {
                throw new MobException("cartão não é valido");
            }

            var type = this.typeService.Find(typeId);

            if (type == null)
            {
                throw new MobException("tipo de cartao inválido");
            }

            var country = this.countryService.Find(creditCardToken.CountryId);

            if (country == null)
            {
                throw new MobException("pais do cartao inválido");
            }

            creditCardToken.CreditCardType = type;
            creditCardToken.Country = country;

            var persistedCreditCardToken = this.Find(creditCardToken.Id);

            persistedCreditCardToken.Name = creditCardToken.Name;
            persistedCreditCardToken.Document = creditCardToken.Document;
            persistedCreditCardToken.CreditCardTypeId = creditCardToken.CreditCardTypeId;
            persistedCreditCardToken.CreditCardType = creditCardToken.CreditCardType;
            persistedCreditCardToken.Number = creditCardToken.Number;
            persistedCreditCardToken.ExpirationDate = creditCardToken.ExpirationDate;
            persistedCreditCardToken.Address = creditCardToken.Address;

            var putedCreditCardToken = this.restClient.Put(persistedCreditCardToken);
            if (putedCreditCardToken == null)
            {
                throw new MobException("erro ao executar a solicitação ao PayU para atualizar cartao");
            }

            base.Update(creditCardToken);
        }

        public override void Delete(CreditCardToken creditCardToken)
        {
            if (creditCardToken == null)
            {
                throw new MobException("creditCardToken nao pode ser nulo");
            }

            creditCardToken = this.Find(creditCardToken.Id);

            if (creditCardToken == null)
            {
                throw new MobException("creditCardToken nao pode ser nulo");
            }

            if (this.restClient.Delete(creditCardToken.Customer.CustomerPayUId, creditCardToken.Token))
            {
                base.Delete(creditCardToken);
            }
            else
            {
                throw new MobException("erro ao executar a solicitação ao PayU para excluir cartao");
            }
        }

        public override void Update(Expression<Func<CreditCardToken, bool>> filterExpression, Expression<Func<CreditCardToken, CreditCardToken>> updateExpression)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<CreditCardToken> entities)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<CreditCardToken> entities, int batchSize)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void Delete(Expression<Func<CreditCardToken, bool>> filterExpression)
        {
            throw new NotSupportedException("method not suported");
        }

        public virtual bool IsValidNumber(string cardNumber)
        {
            //Determine the card type based on the number
            CreditCardTypeCode? cardType = GetCardTypeFromNumber(cardNumber);

            //Call the base version of IsValidNumber and pass the 
            //number and card type
            return cardType != null && this.IsValidLuhnNumber(cardNumber);
        }

        public virtual CreditCardTypeCode? GetCardTypeFromNumber(string cardNumber)
        {
            cardNumber = cardNumber.RemoveNonNumerics();

            if (new Regex(amexRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.Amex;
            }
            else if (new Regex(masterRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.MasterCard;
            }
            else if (new Regex(visaRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.Visa;
            }
            else if (new Regex(discoverRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.Discover;
            }
            else if (new Regex(dinersRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.DinersClub;
            }
            else if (new Regex(eloRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.Elo;
            }
            else if (new Regex(hipercardRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.HiperCard;
            }
            else if (new Regex(auraRegex).IsMatch(cardNumber))
            {
                return CreditCardTypeCode.Aura;
            }
            else
            {
                return null;
            }
        }
        
        private bool IsValidLuhnNumber(string cardNumber)
        {
            //Clean the card number- remove dashes and spaces
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            //Convert ca°rd number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
            }

            //Luhn Algorithm
            //Adapted from code availabe on Wikipedia at
            //http://en.wikipedia.org/wiki/Luhn_algorithm
            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int curDigit = digits[i];
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                }
                sum += curDigit;
                alt = !alt;
            }

            //If Mod 10 equals 0, the number is good and this will return true
            return sum % 10 == 0;
        }
    }
}


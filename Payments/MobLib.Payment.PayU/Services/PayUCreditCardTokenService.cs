using MobLib.Core.Services;
using MobLib.Exceptions;
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
        private const string cardRegex = @"^(?:(?<Visa>4\d{3})|(?<MasterCard>5[1-5]\d{2})|(?<Discover>6011)|(?<DinersClub>(?:36\d{2})|(?:38\d{2})|(?:30[0-5]\d))|(?<Amex>3[47]\d{2}))([ -]?)(?(DinersClub)(?:\d{6}\1\d{4}))|(?(Amex)(?:\d{6}\1\d{5})|(?:\d{4}\1\d{4}\1\d{4}))$";

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
                throw new MobException("creditCardTokeno nao pode ser nulo");
            }

            if (creditCardToken.Customer == null || string.IsNullOrEmpty(creditCardToken.Customer.CustomerPayUId))
            {
                throw new MobException("cliente inválido");
            }

            var type = this.typeService.Find(creditCardToken.CreditCardTypeId);

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

            var postedCreditCard = this.restClient.Post(creditCardToken);

            if (postedCreditCard == null || string.IsNullOrEmpty(postedCreditCard.Token))
            {
                throw new MobException("erro ao executar a solicitação ao PayU");
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

            var type = this.typeService.Find(creditCardToken.CreditCardTypeId);

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
                throw new MobException("erro ao executar a solicitação ao PayU");
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
                throw new MobException("erro ao executar a solicitação ao PayU");
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


        public bool IsValidNumber(string cardNum)
        {
            Regex cardTest = new Regex(cardRegex);

            //Determine the card type based on the number
            CreditCardTypeCode? cardType = GetCardTypeFromNumber(cardNum);

            //Call the base version of IsValidNumber and pass the 
            //number and card type
            if (IsValidNumber(cardNum, cardType))
                return true;
            else
                return false;
        }

        public bool IsValidNumber(string cardNum, CreditCardTypeCode? cardType)
        {
            //Create new instance of Regex comparer with our 
            //credit card regex pattern
            Regex cardTest = new Regex(cardRegex);

            //Make sure the supplied number matches the supplied
            //card type
            if (cardTest.Match(cardNum).Groups[cardType.ToString()].Success)
            {
                //If the card type matches the number, then run it
                //through Luhn's test to make sure the number appears correct
                if (PassesLuhnTest(cardNum))
                    return true;
                else
                    //The card fails Luhn's test
                    return false;
            }
            else
                //The card number does not match the card type
                return false;
        }

        public virtual CreditCardTypeCode? GetCardTypeFromNumber(string cardNum)
        {
            //Create new instance of Regex comparer with our
            //credit card regex pattern
            Regex cardTest = new Regex(cardRegex);

            //Compare the supplied card number with the regex
            //pattern and get reference regex named groups
            GroupCollection gc = cardTest.Match(cardNum).Groups;

            //Compare each card type to the named groups to 
            //determine which card type the number matches
            if (gc[CreditCardTypeCode.Amex.ToString()].Success)
            {
                return CreditCardTypeCode.Amex;
            }
            else if (gc[CreditCardTypeCode.MasterCard.ToString()].Success)
            {
                return CreditCardTypeCode.MasterCard;
            }
            else if (gc[CreditCardTypeCode.Visa.ToString()].Success)
            {
                return CreditCardTypeCode.Visa;
            }
            else if (gc[CreditCardTypeCode.Discover.ToString()].Success)
            {
                return CreditCardTypeCode.Discover;
            }
            else if (gc[CreditCardTypeCode.DinersClub.ToString()].Success)
            {
                return CreditCardTypeCode.DinersClub;
            }
            else
            {
                //Card type is not supported by our system, return null
                //(You can modify this code to support more (or less)
                // card types as it pertains to your application)
                return null;
            }
        }

        public string GetCardTestNumber(CreditCardTypeCode cardType)
        {
            //According to PayPal, the valid test numbers that should be used
            //for testing card transactions are:
            //Credit Card Type              Credit Card Number
            //American Express              378282246310005
            //American Express              371449635398431
            //American Express Corporate    378734493671000
            //Diners Club                   30569309025904
            //Diners Club                   38520000023237
            //Discover                      6011111111111117
            //Discover                      6011000990139424
            //MasterCard                    5555555555554444
            //MasterCard                    5105105105105100
            //Visa                          4111111111111111
            //Visa                          4012888888881881
            //Src: https://www.paypal.com/en_US/vhelp/paypalmanager_help/credit_card_numbers.htm
            //Credit: Scott Dorman, http://www.geekswithblogs.net/sdorman

            //Return bogus CC number that passes Luhn and format tests
            switch (cardType)
            {
                case CreditCardTypeCode.Amex:
                    return "3782 822463 10005";
                case CreditCardTypeCode.Discover:
                    return "6011 1111 1111 1117";
                case CreditCardTypeCode.MasterCard:
                    return "5105 1051 0510 5100";
                case CreditCardTypeCode.Visa:
                    return "4111 1111 1111 1111";
                default:
                    return null;
            }
        }

        public bool PassesLuhnTest(string cardNumber)
        {
            //Clean the card number- remove dashes and spaces
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            //Convert card number into digits array
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


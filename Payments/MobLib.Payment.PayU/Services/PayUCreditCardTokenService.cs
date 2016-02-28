using MobLib.Core.Services;
using MobLib.Exceptions;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCreditCardTokenService : MobService<CreditCardToken>, IPayUCreditCardTokenService
    {
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
    }
}

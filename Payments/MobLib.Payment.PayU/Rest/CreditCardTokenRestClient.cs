using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest.Mapper;
using MobLib.Rest;
using RestSharp;
using System;
using System.Net;

namespace MobLib.Payment.PayU.Rest
{
    internal class CreditCardTokenRestClient : PayURestClient
    {
        internal CreditCardToken Get(string creditCardId)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/creditCards/{0}", creditCardId), Method.GET);

            var response = this.ExecuteRequest<Models.CreditCard>(request);

            return response.Data.Map<Models.CreditCard, CreditCardToken>();
        }

        internal CreditCardToken Post(CreditCardToken creditCard)
        {
            if (creditCard == null)
            {
                throw new ArgumentNullException("creditCard");
            }

            var creditCardModel = creditCard.Map<CreditCardToken, Models.CreditCard>();

            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/customers/{0}/creditCards/", creditCardModel.CustomerId), Method.POST);
            request.AddBody(creditCardModel);

            var response = this.ExecuteRequest<Models.CreditCard>(request);

            return response.Data.Map<Models.CreditCard, CreditCardToken>();
        }

        internal CreditCardToken Put(CreditCardToken creditCard)
        {
            if (creditCard == null)
            {
                throw new ArgumentNullException("creditCard");
            }

            var creditCardModel = creditCard.Map<CreditCardToken, Models.CreditCard>();

            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/creditCards/{0}", creditCardModel.Token), Method.PUT);
            request.AddBody(creditCardModel);

            var response = this.ExecuteRequest<Models.CreditCard>(request);

            return response.Data.Map<Models.CreditCard, CreditCardToken>();
        }

        internal bool Delete(string customerId, string creditCardToken)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/customers/{0}/creditCards/{1}", customerId, creditCardToken), Method.DELETE);

            var response = this.ExecuteRequest(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}

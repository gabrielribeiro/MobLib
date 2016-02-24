using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest.Mapper;
using MobLib.Rest;
using RestSharp;
using System;
using System.Net;

namespace MobLib.Payment.PayU.Rest
{
    public class SubscriptionRestClient : PayURestClient 
    {

        public Subscription Get(string subscriptionId)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/subscriptions/{0}", subscriptionId), Method.GET);

            var response = this.ExecuteRequest<Models.Subscription>(request);

            return response.Data.Map<Models.Subscription, Subscription>();
        }

        public Subscription Post(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }

            var subscriptionModel = subscription.Map<Subscription, Models.Subscription>();

            var request = this.CreateJsonRequest("/rest/v4.3/subscriptions/", Method.POST);
            request.AddBody(subscriptionModel);

            var response = this.ExecuteRequest<Models.Subscription>(request);

            return response.Data.Map<Models.Subscription, Subscription>();
        }

        public Subscription Put(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }

            var subscriptionModel = subscription.Map<Subscription, Models.Subscription>();

            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/subscriptions/{0}", subscriptionModel.SubscriptionPayUId), Method.PUT);
            request.AddBody(subscriptionModel);

            var response = this.ExecuteRequest<Models.Subscription>(request);

            return response.Data.Map<Models.Subscription, Subscription>();
        }

        public bool Delete(string subscriptionId)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/subscriptions/{0}", subscriptionId), Method.DELETE);

            var response = this.ExecuteRequest(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}

using MobLib.Rest;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace MobLib.Payment.PayU.Rest
{
    internal class PayURestClient : BaseRestClient
    {
        protected override bool IgnoreSllValidation
        {
            get { return true; }
        }

        protected override RestSharp.RestClient GetRestClient()
        {
            string url = Configuration.GetConfigurationValue("PayU_WsUrl");
            string userName = Configuration.GetConfigurationValue("PayU_Login");
            string password = Configuration.GetConfigurationValue("PayU_Api_Key");
            
            var restClient = base.GetRestClient();

            restClient.BaseUrl = new Uri(url);
            restClient.Authenticator = new HttpBasicAuthenticator(userName, password);

            return restClient;
        }
    }
}

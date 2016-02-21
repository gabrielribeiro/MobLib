using RestSharp;
using RestSharp.Authenticators;

namespace MobLib.Payment.PayU.Rest
{
    internal abstract class BaseRestClient
    {
        protected readonly RestClient restClient;

        public BaseRestClient()
        {
            string url = Configuration.GetConfigurationValue("PayU_WsUrl");
            string userName = Configuration.GetConfigurationValue("PayU_Login");
            string password = Configuration.GetConfigurationValue("PayU_ApiKey");
            this.restClient = new RestClient(url);
            this.restClient.Authenticator = new HttpBasicAuthenticator(userName, password);
        }

        public IRestRequest CreateJsonRequest(string source, Method method)
        {
            var request = new RestRequest(source, method);
            request.RequestFormat = DataFormat.Json;

            return request;
        }
    }
}

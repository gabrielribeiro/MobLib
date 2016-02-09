using RestSharp;
using RestSharp.Authenticators;

namespace MobLib.Payment.PayU.Rest
{
    internal abstract class BaseRestClient
    {
        private readonly RestClient restClient;

        public BaseRestClient()
        {
            string url = Configuration.GetConfigurationValue("PayU_WsUrl");
            string userName = Configuration.GetConfigurationValue("PayU_Username");
            string password = Configuration.GetConfigurationValue("PayU_Password");
            this.restClient = new RestClient(url);
            this.restClient.Authenticator = new HttpBasicAuthenticator(userName, password);
        }
    }
}

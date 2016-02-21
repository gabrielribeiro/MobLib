using MobLib.Serializers.Serializers;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace MobLib.Rest
{
    public abstract class BaseRestClient
    {
        protected const string defaultErrorMessage = "Error while executing the request.";

        protected readonly RestClient restClient;

        public BaseRestClient()
        {
            string url = Configuration.GetConfigurationValue("PayU_WsUrl");
            string userName = Configuration.GetConfigurationValue("PayU_Login");
            string password = Configuration.GetConfigurationValue("PayU_Api_Key");
            this.restClient = new RestClient(url);
            this.restClient.Authenticator = new HttpBasicAuthenticator(userName, password);
        }

        protected IRestRequest CreateJsonRequest(string source, Method method)
        {
            var request = new RestRequest(source, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.AddHeader("Accept", "application/json");

            return request;
        }

        protected IRestResponse<T> ExecuteRequest<T>(IRestRequest request)
        {
            var task = this.restClient.ExecuteTaskAsync<T>(request);
            task.Wait();

            if (task.Exception != null
                || task.Result.ErrorException != null)
            {
                var message = task.Result != null && task.Result.ErrorMessage != null ? task.Result.ErrorMessage : defaultErrorMessage;
                throw new ApplicationException(message, task.Exception ?? task.Result.ErrorException);
            }

            return task.Result;
        }

        protected IRestResponse ExecuteRequest(IRestRequest request)
        {
            var task = this.restClient.ExecuteTaskAsync(request);
            task.Wait();

            if (task.Exception != null
                || task.Result.ErrorException != null)
            {
                var message = task.Result != null && task.Result.ErrorMessage != null ? task.Result.ErrorMessage : defaultErrorMessage;
                throw new ApplicationException(message, task.Exception ?? task.Result.ErrorException);
            }

            return task.Result;
        }
    }
}

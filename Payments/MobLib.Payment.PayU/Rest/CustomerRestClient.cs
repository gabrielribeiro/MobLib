using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest.Mapper;
using MobLib.Rest;
using RestSharp;
using System;
using System.Net;

namespace MobLib.Payment.PayU.Rest
{
    public class CustomerRestClient : PayURestClient 
    {
        public Customer Get(string planCode)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/customers/{0}", planCode), Method.GET);

            var response = this.ExecuteRequest<Models.Customer>(request);

            return response.Data.Map<Models.Customer, Customer>();
        }

        public Customer Post(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            var customerModel = customer.Map<Customer, Models.Customer>();

            var request = this.CreateJsonRequest("/rest/v4.3/customers/", Method.POST);
            request.AddBody(customerModel);

            var response = this.ExecuteRequest<Models.Customer>(request);

            return response.Data.Map<Models.Customer, Customer>();
        }

        public Customer Put(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            var customerModel = customer.Map<Customer, Models.Customer>();

            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/customers/{0}", customerModel.CustomerPayUId), Method.PUT);
            request.AddBody(customerModel);

            var response = this.ExecuteRequest<Models.Customer>(request);

            return response.Data.Map<Models.Customer, Customer>();
        }

        public bool Delete(string customerPayUId)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/customers/{0}", customerPayUId), Method.DELETE);

            var response = this.ExecuteRequest(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}

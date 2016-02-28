using MobLib.Payment.PayU.Rest.Mapper;
using MobLib.Payment.PayU.Domain.Entities;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Net;
using MobLib.Rest;

namespace MobLib.Payment.PayU.Rest
{
    public class PlanRestClient : PayURestClient 
    {
        internal Plan Get(string planCode)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/plans/{0}", planCode), Method.GET);

            var response = this.ExecuteRequest<Models.Plan>(request);

            return response.Data.Map<Models.Plan, Plan>();
        }

        internal Plan Post(Plan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }

            var planModel = plan.Map<Plan, Models.Plan>();

            var request = this.CreateJsonRequest("rest/v4.3/plans", Method.POST);
            request.AddBody(planModel);

            var response = this.ExecuteRequest<Models.Plan>(request);

            return response.Data.Map<Models.Plan, Plan>();
        }

        internal Plan Put(Plan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }

            var planModel = plan.Map<Plan, Models.Plan>();

            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/plans/{0}", planModel.PlanCode), Method.PUT);
            request.AddBody(planModel);

            var response = this.ExecuteRequest<Models.Plan>(request);

            return response.Data.Map<Models.Plan, Plan>();
        }

        internal bool Delete(string planCode)
        {
            var request = this.CreateJsonRequest(string.Format("/rest/v4.3/plans/{0}", planCode), Method.DELETE);

            var response = this.ExecuteRequest(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}

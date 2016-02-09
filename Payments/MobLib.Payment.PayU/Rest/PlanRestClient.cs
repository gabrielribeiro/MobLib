using MobLib.Payment.PayU.Rest.Mapper;
using MobLib.Payment.PayU.Domain.Entities;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace MobLib.Payment.PayU.Rest
{
    internal class PlanRestClient : BaseRestClient
    {
        public Task<Plan> Post(Plan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }

            var planModel = plan.Map<Plan, Models.Plan>();
            throw new NotImplementedException();
        }
    }
}

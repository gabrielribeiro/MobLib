using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest;
using System.Net;

namespace MobLib.Payment.Tests
{
    /// <summary>
    /// Summary description for PlanRestClientTest
    /// </summary>
    [TestClass]
    public class PlanRestClientTest
    {
        [TestMethod]
        public void Post_ShouldCreatePlan()
        {
            var rand = new Random();
            var plan = new Plan
            {
                PlanCode = "teste-plan-number" + rand.Next(),
                AccountId = Configuration.GetConfigurationValue("PayU_Account_Id"),
                Description = "Plano de teste Moblib",
                PlanInterval = new PlanInterval { Code = "MONTH" },
                IntervalCount = 1,
                MaxPaymentsAllowed = 12,
                MaxPaymentAttempts = 3,
                PaymentAttemptsDelay = 1,
                MaxPendingPayments = 2,
                TrialDays = 0,
                AdditionalValues = new List<AdditionalValue> 
                { 
                    new AdditionalValue 
                    {
                        Name = "PLAN_VALUE",
                        Currency = new Currency { Code = "BRL" },
                        Value = 1000
                    }
                }
            };

            var sut = new PlanRestClient();

            var postedPlan = sut.Post(plan);
            //sut.Post(plan);

            Assert.IsNotNull(postedPlan.PlanPayUId);
        }
    }
}

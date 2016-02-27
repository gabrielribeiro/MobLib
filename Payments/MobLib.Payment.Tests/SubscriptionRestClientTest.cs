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
    /// Summary description for CreditCardRestClientTest
    /// </summary>
    [TestClass]
    public class SubscriptionRestClientTest
    {
        [TestMethod]
        public void Post_ShouldCreateSubscription()
        {
            #region data generation
            var rand = new Random();
            var customer = new Customer
            {
                FullName = "Guilherme de castro Titschkoski",
                EmailAddress = "guilherme.ti@outlook.com"
            };

            var customerRest = new CustomerRestClient();

            var postedCustomer = customerRest.Post(customer);
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

            var planRestClient = new PlanRestClient();

            var postedPlan = planRestClient.Post(plan);

            var creditCard = new CreditCardToken
            {
                Name = customer.FullName,
                Document = "41847645844",
                Customer = new Customer { ContactPhone = "19997323581", CustomerPayUId = postedCustomer.CustomerPayUId },
                CreditCardType = new CreditCardType { Code = "MASTERCARD" },
                Number = "5555666677778884",
                ExpirationDate = new DateTime(2021, 11, 01),
                Country = new Country { Code = "BR" },
                Address = new Address
                {
                    Line1 = "R Jose Paulino, 1875",
                    Line2 = "APTO 41 B",
                    Line3 = null,
                    PostalCode = "13023102",
                    City = "Campinas",
                    State = "SP"
                }
            };

            var creditCardRestClient = new CreditCardTokenRestClient();
            var postedCreditCard = creditCardRestClient.Post(creditCard);
            #endregion

            var subscription = new Subscription
            {
                Quantity = 12,
                Installments = 12,
                TrialDays = 0,
                Customer = new Customer
                {
                    CustomerPayUId = postedCustomer.CustomerPayUId,
                    CreditCardTokens = new List<CreditCardToken> { new CreditCardToken { Token = postedCreditCard.Token } }
                },
                Plan = new Plan { PlanCode = postedPlan.PlanCode }
            };

            var sut = new SubscriptionRestClient();

            var postedSubscrition = sut.Post(subscription);
        }
    }
}

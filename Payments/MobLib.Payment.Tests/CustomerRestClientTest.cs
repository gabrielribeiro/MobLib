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
    public class CustomerRestClientTest
    {
        [TestMethod]
        public void Post_ShouldCreateCustomer()
        {
            var rand = new Random();
            var customer = new Customer
            {
                FullName = "Guilherme de castro Titschkoski",
                EmailAddress = "guilherme.ti@outlook.com"
            };

            var sut = new CustomerRestClient();

            var postedCustomer = sut.Post(customer);
            //sut.Post(plan);

            Assert.IsNotNull(postedCustomer.CustomerPayUId);
        }
    }
}

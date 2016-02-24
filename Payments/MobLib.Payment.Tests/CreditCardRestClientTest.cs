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
    public class CreditCardRestClientTest
    {
        public CreditCardRestClientTest()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        [TestMethod]
        public void Post_ShouldCreateCreditCard()
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
            #endregion

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
            
            var sut = new CreditCardTokenRestClient();
            var postedCreditCard = sut.Post(creditCard);
            var postedCreditCard2 = sut.Post(creditCard);

            Assert.IsNotNull(postedCreditCard.Token);
            Assert.IsNotNull(postedCreditCard2.Token);
            Assert.AreEqual<string>(postedCreditCard.Token,postedCreditCard2.Token);
        }
    }
}

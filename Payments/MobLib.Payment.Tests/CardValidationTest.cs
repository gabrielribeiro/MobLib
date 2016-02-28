using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using MobLib.Payment.PayU.Services;
using MobLib.Core.Infra.Dependency;
using MobLib.Payment.PayU;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;

namespace MobLib.Payment.Tests
{
    /// <summary>
    /// Summary description for CardValidationTest
    /// </summary>
    [TestClass]
    public class CardValidationTest
    {
        IDependencyResolver resolver;
        public CardValidationTest()
        {

            resolver = DependencyResolverFactory.Create(ResolverType.Default, new List<IDependencyRegistrator> 
            { 
                new DependencyRegistrator(),
                new MobLib.Payment.PayU.DependencyRegistrator()
            });
        }
        [TestMethod]
        public void TestMethod1()
        {
            var service = resolver.Resolve<IPayUCreditCardTokenService>();

            var typeVisa = service.GetCardTypeFromNumber("4012 0010 3714 1112");

            var typeMaster = service.GetCardTypeFromNumber("5453010000066167");

            var typeVisa2 = service.GetCardTypeFromNumber("4012001038443335");

            var typeMaster2 = service.GetCardTypeFromNumber("5453010000066167");

            var typeAmex = service.GetCardTypeFromNumber("376449047333005");

            var typeElo = service.GetCardTypeFromNumber("6362970000457013");

            var typeDiners = service.GetCardTypeFromNumber("36490102462661");

            var typeHipercard = service.GetCardTypeFromNumber("36490102462661");

            var typeDiscover = service.GetCardTypeFromNumber("6011020000245045");

            Assert.AreEqual(typeVisa, CreditCardTypeCode.Visa);
            Assert.AreEqual(typeVisa2, CreditCardTypeCode.Visa);
            Assert.AreEqual(typeMaster, CreditCardTypeCode.MasterCard);
            Assert.AreEqual(typeMaster2, CreditCardTypeCode.MasterCard);
            Assert.AreEqual(typeAmex, CreditCardTypeCode.Amex);
            Assert.AreEqual(typeHipercard, CreditCardTypeCode.HiperCard);
            //Assert.AreEqual(typeElo, CreditCardTypeCode.Elo);
            Assert.AreEqual(typeDiners, CreditCardTypeCode.DinersClub);
            Assert.AreEqual(typeDiscover, CreditCardTypeCode.Discover);             
        }
    }
}

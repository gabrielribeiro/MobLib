using MobLib.Core.Infra.Data;
using MobLib.Payment.PayU.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MobLib.Payment.PayU.Data
{
    public class PayUDataSeeder : IDataSeeder
    {
        public void SeedData(IMobContext context)
        {
            context.Set<Country>().AddOrUpdate
                (x => x.Id,
                  new Country { Id = 1, Code = "BR", Name = "Brasil", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );

            context.Set<Currency>().AddOrUpdate
                (x => x.Id,
                  new Currency { Id = 1, Code = "BRL", Name = "Real", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );

            context.Set<CreditCardType>().AddOrUpdate
                (x => x.Id,
                  new CreditCardType { Id = 1, CreditCardTypeCode = CreditCardTypeCode.Visa , Code = "VISA", Name = "Visa", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                  new CreditCardType { Id = 2, CreditCardTypeCode = CreditCardTypeCode.Amex, Code = "AMEX", Name = "Amex", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                  new CreditCardType { Id = 3, CreditCardTypeCode = CreditCardTypeCode.MasterCard, Code = "MASTERCARD", Name = "Master Card", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                  new CreditCardType { Id = 4, CreditCardTypeCode = CreditCardTypeCode.Elo, Code = "ELO", Name = "Elo", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                  new CreditCardType { Id = 5, CreditCardTypeCode = CreditCardTypeCode.HiperCard, Code = "HIPERCARD", Name = "Hipercard", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                  new CreditCardType { Id = 6, CreditCardTypeCode = CreditCardTypeCode.DinersClub, Code = "DINERS", Name = "Diners", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );

            context.Set<PlanInterval>().AddOrUpdate
                (x => x.Id,
                  new PlanInterval { Id = 1, Code = "MONTH", Name = "Mensal", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );
        }
    }
}

﻿using MobLib.Core.Domain.Entities;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class CreditCardType : MobEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
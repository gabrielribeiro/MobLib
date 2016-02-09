using MobLib.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Country : MobEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

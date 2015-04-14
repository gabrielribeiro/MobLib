using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLib.Core.Domain.Exceptions
{
    public class MobException : Exception
    {
        public MobException()
            : base()
        {

        }
        public MobException(string message)
            : base(message)
        {

        }
        public MobException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

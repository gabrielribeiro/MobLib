﻿using System;

namespace MobLib.Exceptions
{
    [Serializable]
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

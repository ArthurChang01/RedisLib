using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisLoggerLib.Exceptions
{
    public class SubscribeException : Exception
    {
        private const string _msg = "Log saving encounter error";

        public SubscribeException()
            : base(_msg)
        { }

        public SubscribeException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

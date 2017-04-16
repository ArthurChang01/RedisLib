using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisLoggerLib.Exceptions
{
    public class ReceiveLogException:Exception
    {
        private const string _msg = "Log receiving encounter error";
        public ReceiveLogException()
            : base(_msg)
        { }

        public ReceiveLogException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

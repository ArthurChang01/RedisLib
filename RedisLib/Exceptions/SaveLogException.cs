using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisLoggerLib.Exceptions
{
    public class SaveLogException : Exception
    {
        private const string _msg = "Log saving encounter error";

        public SaveLogException()
            : base(_msg)
        { }

        public SaveLogException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

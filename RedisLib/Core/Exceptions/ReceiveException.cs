using System;

namespace RedisLib.Core.Exceptions
{
    public class ReceiveException : Exception
    {
        private const string _msg = "Log receiving encounter error";
        public ReceiveException()
            : base(_msg)
        { }

        public ReceiveException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

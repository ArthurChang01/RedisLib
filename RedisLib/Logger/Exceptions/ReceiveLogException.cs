using System;

namespace RedisLib.Logger.Exceptions
{
    public class ReceiveLogException : Exception
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

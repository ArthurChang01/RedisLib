using System;

namespace RedisLib.Logger.Exceptions
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

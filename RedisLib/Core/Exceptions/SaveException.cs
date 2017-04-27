using System;

namespace RedisLib.Core.Exceptions
{
    public class SaveException : Exception
    {
        private const string _msg = "Log saving encounter error";

        public SaveException()
            : base(_msg)
        { }

        public SaveException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

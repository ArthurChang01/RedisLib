using System;

namespace RedisLib.Logger.Exceptions
{
    public class PublishException : Exception
    {
        private const string _msg = "Log publishing encounter error";

        public PublishException()
            : base(_msg)
        { }

        public PublishException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

using System;

namespace RedisLib.Core.Exceptions
{
    public class GetHashTableException : Exception
    {
        private const string _msg = "GetHashTableException";

        public GetHashTableException()
            : base(_msg)
        { }

        public GetHashTableException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

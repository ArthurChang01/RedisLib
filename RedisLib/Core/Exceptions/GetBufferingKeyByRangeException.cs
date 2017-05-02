using System;

namespace RedisLib.Core.Exceptions
{
    class GetBufferingKeyByRangeException : Exception
    {
        private const string _msg = "GetBufferingKeyByRangeException";

        public GetBufferingKeyByRangeException()
            : base(_msg)
        { }

        public GetBufferingKeyByRangeException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    class BufferingKeyException : Exception
    {
        private const string _msg = "BufferingKeyException";

        public BufferingKeyException()
            : base(_msg)
        { }

        public BufferingKeyException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

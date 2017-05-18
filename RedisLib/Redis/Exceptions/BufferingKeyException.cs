using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Exceptions
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

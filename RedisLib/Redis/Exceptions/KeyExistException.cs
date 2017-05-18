using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class KeyExistException : Exception
    {
        private const string _msg = "KeyExistException";

        public KeyExistException()
            : base(_msg)
        { }

        public KeyExistException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

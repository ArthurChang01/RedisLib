using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Exceptions
{
    [ExcludeFromCodeCoverage]
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

using System;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class RemoveAllException : Exception
    {
        private const string _msg = "Log remove encounter error";

        public RemoveAllException()
            : base(_msg)
        { }

        public RemoveAllException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

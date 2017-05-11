using System;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SetHashTableException : Exception
    {
        private const string _msg = "SetHashTable";

        public SetHashTableException()
            : base(_msg)
        { }

        public SetHashTableException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

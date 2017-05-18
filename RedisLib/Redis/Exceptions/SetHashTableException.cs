using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Exceptions
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

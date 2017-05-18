using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SetHashTablePlusException : Exception
    {
        private const string _msg = "SetHashTablePlusException";

        public SetHashTablePlusException()
            : base(_msg)
        { }

        public SetHashTablePlusException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

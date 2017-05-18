using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SaveException : Exception
    {
        private const string _msg = "Log saving encounter error";

        public SaveException()
            : base(_msg)
        { }

        public SaveException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

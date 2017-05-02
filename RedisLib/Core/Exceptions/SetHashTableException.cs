using System;

namespace RedisLib.Core.Exceptions
{
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

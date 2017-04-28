using System;

namespace RedisLib.Core.Exceptions
{
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

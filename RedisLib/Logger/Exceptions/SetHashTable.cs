using System;

namespace RedisLib.Logger.Exceptions
{
    public class SetHashTable : Exception
    {
        private const string _msg = "SetHashTable";

        public SetHashTable()
            : base(_msg)
        { }

        public SetHashTable(Exception inner)
            : base(_msg, inner)
        { }
    }
}

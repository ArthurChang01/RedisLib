using System;

namespace CoreLib.DB.Exceptions
{
    public class BulkInsertException : Exception
    {
        private const string _msg = "BulkInsertException";

        public BulkInsertException()
            : base(_msg)
        { }

        public BulkInsertException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

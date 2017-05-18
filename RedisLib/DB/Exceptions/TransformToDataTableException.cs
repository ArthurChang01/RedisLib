using System;

namespace CoreLib.DB.Exceptions
{
    public class TransformToDataTableException : Exception
    {
        private const string _msg = "TransformToDataTableException";
        public TransformToDataTableException()
            : base(_msg)
        { }

        public TransformToDataTableException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

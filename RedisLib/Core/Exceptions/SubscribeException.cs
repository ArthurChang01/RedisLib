﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SubscribeException : Exception
    {
        private const string _msg = "Log subscribing encounter error";

        public SubscribeException()
            : base(_msg)
        { }

        public SubscribeException(Exception inner)
            : base(_msg, inner)
        { }
    }
}

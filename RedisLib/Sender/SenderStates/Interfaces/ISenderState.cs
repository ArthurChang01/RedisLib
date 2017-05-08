using System;

namespace RedisLib.Sender.SenderStates.Interfaces
{
    public interface ISenderState : IDisposable
    {
        string StateName { get; }

        void Execute();
    }
}

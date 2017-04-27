using System;

namespace RedisLib.Receiver.ReceiverStates.Interfaces
{
    public interface IReceiverState : IDisposable
    {
        string StateName { get; }

        void Execute();
    }
}

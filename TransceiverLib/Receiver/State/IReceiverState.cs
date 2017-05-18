using System;

namespace Transceiver.Receiver.State
{
    public interface IReceiverState : IDisposable
    {
        string StateName { get; }

        void Execute();
    }
}

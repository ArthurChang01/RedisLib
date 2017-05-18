using System;

namespace Transceiver.Sender.State
{
    public interface ISenderState : IDisposable
    {
        string StateName { get; }

        void Execute();
    }
}

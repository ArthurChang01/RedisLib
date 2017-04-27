using System;

namespace Redis.Sender.SenderStates.Interfaces
{
    public interface ISenderState : IDisposable
    {
        string StateName { get; }

        void Execute();
    }
}

using RedisLib.Receiver.Context;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class ProcessState : BaseState
    {
        #region Constructor
        public ProcessState(ReceiverContext logContext)
        {
            this._ctx = logContext;
        }
        #endregion

        #region Property
        public override string StateName => "ProcessState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
#if DEBUG
            Console.WriteLine("ProcessState");
#endif

            //do something

            this.Users.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            if (this._ctx != null) this._ctx = null;
            if (this.Users != null) this.Users.Clear();
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

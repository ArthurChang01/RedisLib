using RedisLib.Receiver.Context;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class FinishState : BaseState
    {
        private ReceiverContext logContext;

        public FinishState(ReceiverContext logContext)
        {
            this.logContext = logContext;
        }

        #region Property
        public override string StateName => "FinishState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            if (this._ctx != null) this._ctx = null;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

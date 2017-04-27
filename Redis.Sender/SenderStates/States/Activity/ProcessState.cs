using Redis.Sender.Context;
using Redis.Sender.SenderStates.States.Base;
using System;

namespace Redis.Sender.SenderStates.States.Activity
{
    class ProcessState : BaseState
    {
        #region Constructor
        public ProcessState(SenderContext ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "ProcessState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

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

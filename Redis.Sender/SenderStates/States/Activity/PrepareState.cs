using Redis.Sender.Context;
using Redis.Sender.SenderStates.States.Base;
using System;
using System.Linq;

namespace Redis.Sender.SenderStates.States.Activity
{
    class PrepareState : BaseState
    {
        #region Constructor
        public PrepareState(SenderContext ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "PrepareState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. generate key
            int mod = this.ReceiverTable.Receivers
                                .Select(o => o.ReceiverNodeId)
                                .OrderBy(o => o)
                                .ToList<int>()
                                .FindIndex(o => o == this.ReceiverTable.CandidateNodeId);
            this.ReceiverTable.CandidateNodeId = (this.ReceiverTable.CandidateNodeId + 1) % mod;
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

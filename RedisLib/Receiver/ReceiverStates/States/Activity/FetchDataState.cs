using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class FetchDataState<T> : BaseState<T>
    {
        #region Constructor
        public FetchDataState(ReceiverContext<T> logContext)
        {
            this._ctx = logContext;
        }

        #endregion

        #region Property
        public override string StateName => "FetchDataState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {

            //step1. prepare keys
            enLogType candidate = this.ExecutedRecords.Last();
            string keyPattern = string.Format(@"{{{0}/{1}}}", candidate.ToString(), this.NodeId);
            //TODO: received key

            //step2. fetch data
            //TODO: fetch data
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            if (this.DataKey != null) this.DataKey.Clear();
            if (this.DataObjs != null) this.DataObjs.Clear();
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

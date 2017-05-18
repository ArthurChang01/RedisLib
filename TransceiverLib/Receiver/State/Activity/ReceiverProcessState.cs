using System;
using System.Diagnostics.CodeAnalysis;
using Transceiver.Constant;

namespace Transceiver.Receiver.State.Activity
{
    class ReceiverProcessState<T> : ReceiverBaseState<T> where T : class, new()
    {
        #region Constructor
        public ReceiverProcessState(ReceiverContext<T> logContext)
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
            //step1. Processing
            DB.BulkInsert<T>(this.DataObjs);
            ES.BulkInsert<T>(this.DataObjs);

            //step2. 
            this.DataConnection.RemoveAll(this.Key);
            this.DataConnection.SetHashTable_Plus(KeyName.ReceiverReply, this.NodeId.ToString(), -1);
            this.DataObjs.Clear();
        }

        [ExcludeFromCodeCoverage]
        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            if (this.DataObjs != null) this.DataObjs.Clear();
            if (this._ctx != null) this._ctx = null;
        }

        [ExcludeFromCodeCoverage]
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

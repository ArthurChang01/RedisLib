using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class ProcessState<T> : BaseState<T>
    {
        #region Constructor
        public ProcessState(ReceiverContext<T> logContext)
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
            //do something
            this.DataConnection.RemoveAll(this.Key);

            //step final. clean
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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Transceiver.Constant;
using Transceiver.Model;

namespace Transceiver.Sender.State.Activity
{
    class SenderProcessState<T> : BaseState<T>
    {
        #region Constructor
        public SenderProcessState(SenderContext<T> ctx)
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
            ReceiverRecord target = this.ReceiverTable.Receivers.FirstOrDefault(
                    o => o.ReceiverNodeId.Equals(this.ReceiverTable.CandidateInfo[this.LogType]));
            string receiverId = target == null ? string.Empty : target.ReceiverId;

            //step1. Save data
            this.DataConnection.Save<T>(this.DataKey, this.DataValue);
            this.DataConnection.SetHashTable_Plus(KeyName.ReceiverReply, target.ReceiverNodeId.ToString());
        }

        [ExcludeFromCodeCoverage]
        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;
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

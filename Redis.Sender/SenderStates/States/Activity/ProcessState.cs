using Redis.Sender.Constants;
using Redis.Sender.Context;
using Redis.Sender.SenderStates.States.Base;
using System;
using System.Linq;

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
            string receiverId =
                this.ReceiverTable.Receivers.FirstOrDefault(
                    o => o.ReceiverNodeId.Equals(this.ReceiverTable.CandidateNodeId))
                .ReceiverId;

            //step1. Save data
            this.DataConnection.Save(this.DataKey, this.DataValue);
            this.DataConnection.SetHashTable_Plus(MsgConstant.ReceiverReply, receiverId, 1);
            this.DataConnection.BufferingKey(MsgConstant.KeyBuffer, this.DataKey);

            //step2. Publish receiver's reply
            if (!string.IsNullOrEmpty(receiverId))
                this.MsgConnection.PublishMessage<string>(
                    string.Format(@"ReceiveReply_{0}", receiverId), this.DataKey);

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

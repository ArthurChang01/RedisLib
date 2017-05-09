﻿using RedisLib.Sender.Constants;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using RedisLib.Sender.SenderStates.States.Base;
using System;
using System.Linq;

namespace RedisLib.Sender.SenderStates.States.Activity
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
            ReceiverRecord target = this.ReceiverTable.Receivers.FirstOrDefault(
                    o => o.ReceiverNodeId.Equals(this.ReceiverTable.CandidateNodeId));
            string receiverId = target == null ? string.Empty : target.ReceiverId;

            //step1. Save data
            this.DataConnection.Save(this.DataKey, this.DataValue);
            this.DataConnection.SetHashTable_Plus(KeyName.ReceiverReply, receiverId, 1);

            //step2. Publish receiver's reply
            if (!string.IsNullOrEmpty(receiverId))
                this.MsgConnection.PublishMessage<string>(
                    string.Format(ChannelName.ReceiveReply, receiverId), this.DataKey);

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

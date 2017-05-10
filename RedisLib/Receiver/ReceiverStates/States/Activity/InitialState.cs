using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class InitialState<T> : BaseState<T>
    {
        #region Constructor
        public InitialState(ReceiverContext<T> logContext)
        {
            _ctx = logContext;
        }
        #endregion

        #region Property
        public override string StateName => "InitialState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. Initial SyncUp message connection and channel
            this.MsgConnection.SubscribeMessage<string>(string.Format(ChannelName.ReceiveReply, this.ID), key =>
            {
                this.DataConnection.BufferingKey(KeyName.KeyBuffer, key);
            });

            //step2. pick up node id
            int candidateId = 0;
            bool keyExist = this.DataConnection.KeyExist(KeyName.ReceiverRegistry);
            if (keyExist)
            {
                IEnumerable<int> ieNodeIds = this.DataConnection.GetHashTable<int>(KeyName.ReceiverRegistry).Keys.Select(o => int.Parse(o)).OrderBy(o => o);
                IEnumerable<int> ieComparer = Enumerable.Range(0, ieNodeIds.Max() + 1); //0~(max+1)
                candidateId = ieComparer.Except<int>(ieNodeIds).First(); //pick up lake slot
            }
            this.NodeId = candidateId;

            //step3. register node
            this.DataConnection.SetHashTable<int>(KeyName.ReceiverRegistry, this.ID, this.NodeId);

            //step4. register reply infor
            this.DataConnection.SetHashTable<int>(KeyName.ReceiverReply, this.NodeId.ToString(), 0);
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

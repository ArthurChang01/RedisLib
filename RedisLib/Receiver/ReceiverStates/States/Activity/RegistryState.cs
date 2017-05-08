using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class RegistryState : BaseState
    {
        #region Constructor
        public RegistryState(ReceiverContext ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "RegistryState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. pick up node id
            int candidateId = 0;
            bool keyExist = this.DataConnection.KeyExist(KeyName.ReceiverRegistry);
            if (keyExist)
            {
                IEnumerable<int> ieNodeIds = this.DataConnection.GetHashTable<int>(KeyName.ReceiverRegistry).Keys.Select(o => int.Parse(o)).OrderBy(o => o);
                IEnumerable<int> ieComparer = Enumerable.Range(0, ieNodeIds.Max() + 1); //0~(max+1)
                candidateId = ieComparer.Except<int>(ieNodeIds).First(); //pick up lake number
            }
            this.NodeId = candidateId;

            //step2. register node
            this.DataConnection.SetHashTable<DateTimeOffset>(KeyName.ReceiverRegistry, this.NodeId.ToString(), DateTimeOffset.UtcNow);

            //step3. register reply infor
            this.DataConnection.SetHashTable<int>(KeyName.ReceiverReply, this.ID, 0);
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

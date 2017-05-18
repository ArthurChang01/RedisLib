using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Transceiver.Constant;
using Transceiver.Model;

namespace Transceiver.Receiver.State.Activity
{
    class ReceiverInitialState<T> : ReceiverBaseState<T> where T : class, new()
    {
        #region Constructor
        public ReceiverInitialState(ReceiverContext<T> logContext)
        {
            _ctx = logContext;
        }
        #endregion

        #region Property
        public override string StateName => "InitialState";
        #endregion

        #region Private Methods
        private int getCandidateId()
        {
            int candidateId = 0;
            bool keyExist = this.DataConnection.KeyExist(KeyName.ReceiverRegistry);

            if (!keyExist) return candidateId;

            IEnumerable<int> availabler = this.DataConnection.GetHashTable<int>(KeyName.ReceiverReply).Where(o => o.Value <= 20).Select(o => int.Parse(o.Key));

            IEnumerable<int> ieComparer = Enumerable.Range(0, availabler.Count() + 1); //0~(max+1)
            candidateId = ieComparer.Except<int>(availabler).First(); //pick up lake slot

            return candidateId;
        }
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. pick up node id
            this.NodeId = getCandidateId();

            //step2. register node
            this.DataConnection.SetHashTable<int>(KeyName.ReceiverRegistry, this.ID, this.NodeId);

            //step3. register reply infor
            this.DataConnection.SetHashTable<int>(KeyName.ReceiverReply, this.NodeId.ToString(), 0);

            //step4. publish receiver info
            this.MsgConnection.PublishMessage<ReceiverRecord>(ChannelName.ReceiverRegistry,
                new ReceiverRecord { ReceiverId = this.ID, ReceiverNodeId = this.NodeId }
            );
        }

        [ExcludeFromCodeCoverage]
        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

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

using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class FetchDataState : BaseState
    {
        #region Constructor
        public FetchDataState(ReceiverContext logContext)
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
#if DEBUG
            Console.WriteLine("FetchDataState");
#endif

            ResourceRecord rcd = this.ResourceTable.Records.First(o => o.Id.Equals(this.ID));

            //step1. Fetch Data
            this.Users.Clear();
            IEnumerable<string> bufferedKeys = DataConnection.GetBufferingKeyByRange("KeyBuffer", 0, 100);
            IEnumerable<string> existingKeys = DataConnection.Fetch(string.Format(@"{{{0}}/{{1}}}*", rcd.Resource, this.NodeId));
            this.Users.AddRange(bufferedKeys.Union<string>(existingKeys));

            //step2. Update information
            rcd.AmountOfLog = (this.Users == null) ? 0 : this.Users.LongCount();
            this.MsgConnection.PublishMessage<ResourceRecord>("Sync_Message", rcd);
            this.DataConnection.SetHashTable_Plus("ReceiverReply", this.ID, -1); //pay back replying debt
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            if (this.Users != null) this.Users.Clear();
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

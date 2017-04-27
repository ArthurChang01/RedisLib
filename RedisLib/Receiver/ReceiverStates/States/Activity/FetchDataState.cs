using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
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
            this.Users.AddRange(DataConnection.Receive(string.Format(@"{{{0}}}*", rcd.Resource)));

            //step2. Update information
            rcd.AmountOfLog = (this.Users == null) ? 0 : this.Users.LongCount();
            this.MsgConnection.PublishMessage<ResourceRecord>("Sync_Message", rcd);
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

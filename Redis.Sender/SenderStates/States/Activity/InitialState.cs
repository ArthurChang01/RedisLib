using Redis.Sender.Context;
using Redis.Sender.SenderStates.Models;
using Redis.Sender.SenderStates.States.Base;
using RedisLib.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Redis.Sender.SenderStates.States.Activity
{
    class InitialState : BaseState
    {
        #region Constructor
        public InitialState(SenderContext ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "InitialState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            string conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;

            //step1. Initial SyncUp message connection and channel
            this.MsgConnection = new Rediser(conString);

            //step2. Initial Data connection
            this.DataConnection = new Rediser(conString);

            //step3. Fetch receiver registry information
            bool keyExist = this.DataConnection.KeyExist("ReceiverRegistry");

            if (keyExist)
                this.ReceiverTable.Receivers =
                    this.DataConnection.GetHashTable<string>("ReceiverRegistry")
                          .Select(o =>
                            new ReceiverRecord { ReceiverNodeId = int.Parse(o.Key), ReceiverId = o.Value });
            else //Default is 0
                this.ReceiverTable.Receivers = new List<ReceiverRecord> { new ReceiverRecord { ReceiverNodeId = 0, ReceiverId = string.Empty, UnReplyCounter = 0 } };

            //step4. Fetch receiver 
            keyExist = this.DataConnection.KeyExist("ReceiverReply");

            if (keyExist)
                this.ReceiverTable.Receivers =
                    from registry in this.ReceiverTable.Receivers
                    join reply in this.DataConnection.GetHashTable<int>("ReceiverReply")
                    on registry.ReceiverId equals reply.Key
                    select new ReceiverRecord { ReceiverId = registry.ReceiverId, ReceiverNodeId = registry.ReceiverNodeId, UnReplyCounter = reply.Value };

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

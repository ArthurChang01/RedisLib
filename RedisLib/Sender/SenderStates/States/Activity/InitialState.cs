using RedisLib.Sender.Constants;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using RedisLib.Sender.SenderStates.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Sender.SenderStates.States.Activity
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
            //step1. Concret receiver registry information
            bool keyExist = this.DataConnection.KeyExist(KeyName.ReceiverRegistry);

            if (keyExist)
                this.ReceiverTable.Receivers =
                     this.DataConnection.GetHashTable<IDictionary<string, int>>(KeyName.ReceiverRegistry)
                          .SelectMany(o => o.Value)
                          .Select(o =>
                            new ReceiverRecord { ReceiverId = o.Key, ReceiverNodeId = o.Value }).ToList();
            else //Default is 0
                this.ReceiverTable.Receivers = new List<ReceiverRecord> {
                    new ReceiverRecord { ReceiverNodeId = 0, ReceiverId = string.Empty, UnReplyCounter = 0 } };

            //step2. Concret receiver 
            keyExist = this.DataConnection.KeyExist(KeyName.ReceiverReply);

            if (keyExist)
            {
                IEnumerable<KeyValuePair<string, int>> replyTable =
                    this.DataConnection.GetHashTable<IDictionary<string, int>>(KeyName.ReceiverReply)
                          .SelectMany(o => o.Value);

                this.ReceiverTable.Receivers =
                    (from registry in this.ReceiverTable.Receivers
                     join reply in replyTable
                     on registry.ReceiverNodeId.ToString() equals reply.Key
                     select new ReceiverRecord
                     {
                         ReceiverId = registry.ReceiverId,
                         ReceiverNodeId = registry.ReceiverNodeId,
                         UnReplyCounter = reply.Value
                     }).ToList();
            }

            //step3. Subscribe Channel
            this._ctx.MsgConnection.SubscribeMessage<string>("ReceiverRegistry", msg =>
            {
                //TODO: Upsert node information
            });
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

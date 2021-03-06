﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Transceiver.Constant;
using Transceiver.Model;

namespace Transceiver.Sender.State.Activity
{
    class SenderInitialState<T> : BaseState<T>
    {
        #region Constructor
        public SenderInitialState(SenderContext<T> ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "InitialState";
        #endregion

        #region Private Methods
        private IList<ReceiverRecord> getReceiverRegistryInfo()
        {
            IList<ReceiverRecord> records = new List<ReceiverRecord>() {
                new ReceiverRecord { ReceiverNodeId = 0, ReceiverId = "0", UnReplyCounter = 0 }
            };

            bool keyExist = this.DataConnection.KeyExist(KeyName.ReceiverRegistry);

            if (!keyExist) return records;

            records = this.DataConnection.GetHashTable<int>(KeyName.ReceiverRegistry)
                          .Select(o =>
                            new ReceiverRecord { ReceiverId = o.Key, ReceiverNodeId = o.Value }).ToList();

            return records;
        }

        private IList<ReceiverRecord> getFullReceiverInfo(IEnumerable<ReceiverRecord> registryInfo)
        {
            IList<ReceiverRecord> records = null;

            IEnumerable<KeyValuePair<string, int>> replyTable =
                    this.DataConnection.GetHashTable<int>(KeyName.ReceiverReply);

            records =
                    (from registry in registryInfo
                     join reply in replyTable
                     on registry.ReceiverNodeId.ToString() equals reply.Key
                     select new ReceiverRecord
                     {
                         ReceiverId = registry.ReceiverId,
                         ReceiverNodeId = registry.ReceiverNodeId,
                         UnReplyCounter = reply.Value
                     }
                     ).ToList();

            return records;
        }
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. Concret receiver registry information
            this.ReceiverTable.Receivers = getReceiverRegistryInfo();

            //step2. Concret receiver 
            bool keyExist = this.DataConnection.KeyExist(KeyName.ReceiverReply);

            if (keyExist)
                this.ReceiverTable.Receivers = getFullReceiverInfo(this.ReceiverTable.Receivers);

            //step3. Subscribe Channel
            this._ctx.MsgConnection.SubscribeMessage<ReceiverRecord>(ChannelName.ReceiverRegistry, rcd =>
            {
                ReceiverRecord target =
                    this.ReceiverTable.Receivers.FirstOrDefault(o => o.ReceiverNodeId == rcd.ReceiverNodeId);

                if (target == null)
                    this.ReceiverTable.Receivers.Add(
                        new ReceiverRecord
                        {
                            ReceiverId = rcd.ReceiverId,
                            ReceiverNodeId = rcd.ReceiverNodeId,
                            UnReplyCounter = 0
                        });
                else
                    target.ReceiverId = rcd.ReceiverId;
            });
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

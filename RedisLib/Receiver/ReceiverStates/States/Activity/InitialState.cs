using RedisLib.Core;
using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using RedisLib.Sender.Context;
using System;
using System.Configuration;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class InitialState : BaseState
    {
        #region Members
        private SenderContext senderContext;
        #endregion

        #region Constructor
        public InitialState(ReceiverContext logContext)
        {
            _ctx = logContext;
        }

        public InitialState(SenderContext senderContext)
        {
            this.senderContext = senderContext;
        }
        #endregion

        #region Property
        public override string StateName => "InitialState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
#if DEBUG
            Console.WriteLine("InitialState");
#endif

            string conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;

            //step1. Initial SyncUp message connection and channel
            this.MsgConnection = new Rediser(conString);
            this.MsgConnection.SubscribeMessage<ResourceRecord>(ChannelName.Sync_Message, rcd =>
           {
               ResourceRecord rcdTarget = this.ResourceTable.Records.FirstOrDefault(o => o.Id.Equals(rcd.Id));

               if (rcdTarget == null)
                   this.ResourceTable.Add(rcd);
               else
                   rcdTarget.Update(rcd); //update record
           });
            this.MsgConnection.SubscribeMessage<string>(string.Format(ChannelName.ReceiveReply, this.ID), key =>
            {
                this.DataConnection.BufferingKey(KeyName.KeyBuffer, key);
            });

            //step2. publish self
            ResourceRecord rcdSelf = new ResourceRecord { Id = ID, UpdateTime = DateTime.Now };
            this.ResourceTable.Add(rcdSelf);
            this.MsgConnection.PublishMessage<ResourceRecord>(ChannelName.Sync_Message, rcdSelf);

            //step3. Initial Data connection
            this.DataConnection = new Rediser(conString);
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

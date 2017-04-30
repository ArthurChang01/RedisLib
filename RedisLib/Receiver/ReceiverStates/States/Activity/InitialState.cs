using RedisLib.Core;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Configuration;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class InitialState : BaseState
    {
        #region Member
        private string _syncUpChannelName = "Sync_Message";
        #endregion

        #region Constructor
        public InitialState(ReceiverContext logContext)
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
#if DEBUG
            Console.WriteLine("InitialState");
#endif

            string conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;

            //step1. Initial SyncUp message connection and channel
            this.MsgConnection = new Rediser(conString);
            this.MsgConnection.SubscribeMessage<ResourceRecord>(this._syncUpChannelName, rcd =>
            {
                ResourceRecord rcdTarget = this.ResourceTable.Records.FirstOrDefault(o => o.Id.Equals(rcd.Id));

                if (rcdTarget == null)
                    this.ResourceTable.Add(rcd);
                else
                    rcdTarget.Update(rcd); //update record
            });
            this.MsgConnection.SubscribeMessage<string>(string.Format(@"ReceiveReply_{0}", this.ID), id => {

            });

            //step2. publish self
            ResourceRecord rcdSelf = new ResourceRecord { Id = ID, UpdateTime = DateTime.Now };
            this.ResourceTable.Add(rcdSelf);
            this.MsgConnection.PublishMessage<ResourceRecord>(this._syncUpChannelName, rcdSelf);

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

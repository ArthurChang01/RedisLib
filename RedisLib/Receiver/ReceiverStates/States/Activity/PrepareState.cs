using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class PrepareState : BaseState
    {
        #region Constructor
        public PrepareState(ReceiverContext logContext)
        {
            this._ctx = logContext;
        }
        #endregion

        #region Property
        public override string StateName => "PrepareState";
        #endregion

        #region Private Methods
        private ResourceRecord chooseTargetResource(string[] slotList)
        {
            ResourceRecord rcd = this.ResourceTable.Records.First(o => o.Id.Equals(this._ctx.ID));

            Random rd = new Random(DateTime.Now.Millisecond);

            IEnumerable<string> existResource = this.ResourceTable.Records.Where(o => !o.Equals(this.ID)).Select(o => o.Resource);

            IEnumerable<string> newSlotList = slotList.Except(existResource);
            if (newSlotList.Count() > 0)
            {
                int rn = rd.Next(0, newSlotList.Count());
                rcd.Resource = newSlotList.ElementAt(rn);

            }
            rcd.AmountOfLog = 0; //reset

            return rcd;
        }
        #endregion

        #region Interface Method
        public override void Execute()
        {
#if DEBUG
            Console.WriteLine("PrepareState");
#endif

            //step1. pick-up host
            var slotList = new string[] { "systemlog", "bolog", "apilog" };
            ResourceRecord rcd = chooseTargetResource(slotList);
            rcd.UpdateTime = DateTime.Now; //reset

#if DEBUG
            Console.WriteLine("ResourceRecord: {0}", rcd.Resource);
#endif

            //step2. publish new inform to other nodes
            MsgConnection.PublishMessage<ResourceRecord>(ChannelName.Sync_Message, rcd);
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

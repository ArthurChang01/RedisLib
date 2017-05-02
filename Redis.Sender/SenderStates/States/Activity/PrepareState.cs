using Redis.Sender.Context;
using Redis.Sender.SenderStates.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Redis.Sender.SenderStates.States.Activity
{
    class PrepareState : BaseState
    {
        #region Constructor
        public PrepareState(SenderContext ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "PrepareState";
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. pick up next candidate node_Id
            List<int> nodeIds = this.ReceiverTable.Receivers
                                .Select(o => o.ReceiverNodeId)
                                .Distinct()
                                .OrderBy(o => o)
                                .ToList<int>();
            int lastCandiateIndex = nodeIds.FindIndex(o => o == this.ReceiverTable.CandidateNodeId);
            int candiateIndex = (lastCandiateIndex + 1) % nodeIds.Count();
            this.ReceiverTable.CandidateNodeId = nodeIds[candiateIndex];

            //step2. generate key
            this.DataKey =
                string.Format(
                    @"{{{0}/{1}}}:{2}",
                    this.LogType.ToString(),
                    this.ReceiverTable.CandidateNodeId,
                    Guid.NewGuid().ToString());
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

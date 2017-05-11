using RedisLib.Sender.Context;
using RedisLib.Sender.SenderStates.States.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RedisLib.Sender.SenderStates.States.Activity
{
    class PrepareState<T> : BaseState<T>
    {
        #region Constructor
        public PrepareState(SenderContext<T> ctx)
        {
            this._ctx = ctx;
        }
        #endregion

        #region Property
        public override string StateName => "PrepareState";
        #endregion

        #region Private Methods
        private int getNodeId()
        {
            int nodeId = 0;

            //get existing nodeId list
            List<int> nodeIds = this.ReceiverTable.Receivers
                                .Select(o => o.ReceiverNodeId)
                                .Distinct()
                                .OrderBy(o => o)
                                .ToList<int>();

            //get last nodeId index
            int lastCandiateIndex = nodeIds.FindIndex(o => o == this.ReceiverTable.CandidateNodeId);
            int candiateIndex = (nodeIds.Count() == 0) ?
                                            0 :
                                            (lastCandiateIndex + 1) % nodeIds.Count();

            //get nodeId
            nodeId = (candiateIndex == 0) ? 0 : nodeIds[candiateIndex];

            return nodeId;
        }
        #endregion

        #region Interface Methods
        public override void Execute()
        {
            //step1. pick up next candidate node_Id
            this.ReceiverTable.CandidateNodeId = getNodeId();

            //step2. generate key
            this.DataKey =
                string.Format(
                    @"{{{0}/{1}}}:{2}",
                    this.LogType.ToString(),
                    this.ReceiverTable.CandidateNodeId,
                    Guid.NewGuid().ToString());
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

﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Transceiver.Model;

namespace Transceiver.Receiver.State.Activity
{
    class ReceiverPrepareState<T> : ReceiverBaseState<T> where T : class, new()
    {
        private enLogType[] _logTypes = new enLogType[3];

        #region Constructor
        public ReceiverPrepareState(ReceiverContext<T> logContext)
        {
            this._ctx = logContext;
            _logTypes[0] = enLogType.API;
            _logTypes[1] = enLogType.BO;
            _logTypes[2] = enLogType.System;
        }
        #endregion

        #region Property
        public override string StateName => "PrepareState";
        #endregion

        #region Interface Method
        public override void Execute()
        {
            //step1. pick-up log type
            IEnumerable<enLogType> executed = this.ExecutedRecords.ToArray();
            enLogType candidate = _logTypes.Except(executed).First();
            if (executed.Count() > 1)
                this.ExecutedRecords.Dequeue(); //Pop-up oldest 
            this.ExecutedRecords.Enqueue(candidate); //Push-In newest

            //step2. generate key pattern
            this.Key = string.Format(@"{{{0}/{1}}}:*", candidate.ToString(), this.NodeId);
            IEnumerable<T> objs = this.DataConnection.Fetch<T>(this.Key);
            if (objs != null) this.DataObjs.AddRange(objs);
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

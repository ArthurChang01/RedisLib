﻿using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Receiver.ReceiverStates.States.Activity
{
    class PrepareState<T> : BaseState<T>
    {
        private enLogType[] _logTypes = new enLogType[3];

        #region Constructor
        public PrepareState(ReceiverContext<T> logContext)
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
            this.ExecutedRecords.Dequeue();
            this.ExecutedRecords.Enqueue(candidate);
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

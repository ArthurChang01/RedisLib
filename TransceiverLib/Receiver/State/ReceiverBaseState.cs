﻿using CoreLib.DB;
using CoreLib.ES;
using CoreLib.Redis;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Transceiver.Model;

namespace Transceiver.Receiver.State
{
    [ExcludeFromCodeCoverage]
    abstract class ReceiverBaseState<T> : IReceiverState where T : class, new()
    {
        #region Member
        protected ReceiverContext<T> _ctx = null;
        protected bool disposedValue = false; // To detect redundant calls
        #endregion

        #region Property
        public abstract string StateName { get; }

        protected string ID => this._ctx.ID;

        protected int NodeId { get { return this._ctx.NodeId; } set { this._ctx.NodeId = value; } }

        protected string Key { get { return this._ctx.Key; } set { this._ctx.Key = value; } }

        protected List<T> DataObjs { get { return this._ctx.DataObjs; } set { this._ctx.DataObjs = value; } }

        protected Queue<enLogType> ExecutedRecords => this._ctx.ExecutedRecords;

        protected IRediser MsgConnection { get { return this._ctx.MsgConnection; } }

        protected IRediser DataConnection { get { return this._ctx.DataConnection; } }

        protected IDBer DB { get { return this._ctx.DB; } }

        protected IESer ES { get { return this._ctx.ES; } }
        #endregion

        #region Interface(Abstract) Methods
        public abstract void Execute();

        #region IDisposable Support
        protected abstract void Dispose(bool disposing);
        public abstract void Dispose();
        #endregion
        #endregion
    }
}

using RedisLib.Core;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using RedisLib.Sender.SenderStates.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Sender.SenderStates.States.Base
{
    [ExcludeFromCodeCoverage]
    abstract class BaseState<T> : ISenderState
    {
        #region Member
        protected SenderContext<T> _ctx = null;
        protected bool disposedValue = false; // To detect redundant calls
        #endregion

        #region Property
        public abstract string StateName { get; }

        protected string ID => this._ctx.ID;

        protected ReceiverTable ReceiverTable => this._ctx.ReceiverTable;

        protected string DataKey { get { return this._ctx.DataKey; } set { this._ctx.DataKey = value; } }

        protected T DataValue => this._ctx.DataValue;

        protected enLogType LogType => this._ctx.LogType;

        protected IRediser MsgConnection { get { return this._ctx.MsgConnection; } set { this._ctx.MsgConnection = value; } }

        protected IRediser DataConnection { get { return this._ctx.DataConnection; } set { this._ctx.DataConnection = value; } }
        #endregion

        #region Inerface(Abstract) Methods
        public abstract void Execute();

        protected abstract void Dispose(bool disposing);

        public abstract void Dispose();
        #endregion
    }
}

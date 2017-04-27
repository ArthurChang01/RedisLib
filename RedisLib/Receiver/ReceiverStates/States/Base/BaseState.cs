using RedisLib.Logger;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.Interfaces;
using System.Collections.Generic;

namespace RedisLib.Receiver.ReceiverStates.States.Base
{
    abstract class BaseState : IReceiverState
    {
        #region Member
        protected ReceiverContext _ctx = null;
        protected bool disposedValue = false; // To detect redundant calls
        #endregion

        #region Property
        public abstract string StateName { get; }

        protected string ID => this._ctx.ID;

        protected ResourceTable ResourceTable => this._ctx.ResourceTable;

        protected List<string> Users { get { return this._ctx.Users; } }

        protected RedisLogger MsgConnection { get { return this._ctx.MsgConnection; } set { this._ctx.MsgConnection = value; } }

        protected RedisLogger DataConnection { get { return this._ctx.DataConnection; } set { this._ctx.DataConnection = value; } }
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

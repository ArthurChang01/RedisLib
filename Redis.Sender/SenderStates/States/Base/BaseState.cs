using Redis.Sender.Context;
using Redis.Sender.SenderStates.Interfaces;
using RedisLib.Logger;
using System.Collections.Generic;

namespace Redis.Sender.SenderStates.States.Base
{
    abstract class BaseState : ISenderState
    {
        #region Member
        protected SenderContext _ctx = null;
        protected bool disposedValue = false; // To detect redundant calls
        #endregion

        #region Property
        public abstract string StateName { get; }

        protected string ID => this._ctx.ID;

        protected List<string> Users { get { return this._ctx.Users; } }

        protected RedisLogger MsgConnection { get { return this._ctx.MsgConnection; } set { this._ctx.MsgConnection = value; } }

        protected RedisLogger DataConnection { get { return this._ctx.DataConnection; } set { this._ctx.DataConnection = value; } }
        #endregion

        #region Inerface(Abstract) Methods
        public abstract void Execute();

        protected abstract void Dispose(bool disposing);

        public abstract void Dispose();
        #endregion
    }
}

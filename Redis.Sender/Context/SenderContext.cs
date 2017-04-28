using Redis.Sender.SenderStates.Interfaces;
using Redis.Sender.SenderStates.Models;
using Redis.Sender.SenderStates.States.Activity;
using RedisLib.Core;
using System;
using System.Collections.Generic;

namespace Redis.Sender.Context
{
    class SenderContext
    {
        #region Members
        private string _id = Guid.NewGuid().ToString();
        private ISenderState _currentState = null;
        private List<string> _users = new List<string>();
        private IDictionary<string, ISenderState> _logState = new Dictionary<string, ISenderState>();
        private bool disposedValue;
        private ReceiverTable _receiver = new ReceiverTable();

        private static Rediser _msgConnection = null;
        private static Rediser _dataConnection = null;
        #endregion

        public SenderContext()
        {
            _logState.Add("InitialState", new InitialState(this));
            _logState.Add("PrepareState", new PrepareState(this));
            _logState.Add("ProcessState", new ProcessState(this));
        }

        #region Property
        public string ID => this._id;
        public List<string> Users => this._users;
        public IDictionary<string, ISenderState> LogState => this._logState;
        public ReceiverTable ReceiverTable => this._receiver;
        public Rediser MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }
        public Rediser DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Run()
        {

        }

        #region IDisposable Support
        protected void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            _msgConnection.Client.Database.Multiplexer.Close();
            _msgConnection = null;

            _dataConnection.Client.Database.Multiplexer.Close();
            _dataConnection = null;

            this._logState.Clear();
            this._users.Clear();

            this._logState = null;
            this._users = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

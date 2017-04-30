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
        private IDictionary<string, ISenderState> _senderState = new Dictionary<string, ISenderState>();
        private bool disposedValue;
        private ReceiverTable _receiver = new ReceiverTable();
        private string _dataKey = string.Empty;
        private object _dataValue = null;
        private enLogType _logType;

        private static Rediser _msgConnection = null;
        private static Rediser _dataConnection = null;
        #endregion

        public SenderContext()
        {
            _senderState.Add("InitialState", new InitialState(this));
            _senderState.Add("PrepareState", new PrepareState(this));
            _senderState.Add("ProcessState", new ProcessState(this));
        }

        #region Property
        public string ID => this._id;
        public string DataKey { get; set; }
        public object DataValue { get; set; }
        public List<string> Users => this._users;
        public enLogType LogType => this._logType;
        public IDictionary<string, ISenderState> SenderState => this._senderState;
        public ReceiverTable ReceiverTable => this._receiver;
        public Rediser MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }
        public Rediser DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Initial()
        {
            _senderState["InitialState"].Execute();
        }

        public void Run(enLogType logType) {
            this._logType = logType;

            _senderState["PrepareState"].Execute();

            _senderState["ProcessState"].Execute();
        }

        #region IDisposable Support
        protected void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            _msgConnection.Client.Database.Multiplexer.Close();
            _msgConnection = null;

            _dataConnection.Client.Database.Multiplexer.Close();
            _dataConnection = null;

            this._senderState.Clear();
            this._users.Clear();

            this._senderState = null;
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

using RedisLib.Core;
using RedisLib.Sender.Models;
using RedisLib.Sender.SenderStates.Interfaces;
using RedisLib.Sender.SenderStates.States.Activity;
using System;
using System.Collections.Generic;

namespace RedisLib.Sender.Context
{
    public class SenderContext
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

        private static IRediser _msgConnection = null;
        private static IRediser _dataConnection = null;
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
        public object DataValue => this._dataValue;
        public ISenderState CurrentState => this._currentState;
        public List<string> Users => this._users;
        public enLogType LogType => this._logType;
        public ReceiverTable ReceiverTable => this._receiver;
        public IRediser MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }
        public IRediser DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Initial()
        {
            this._currentState = _senderState["InitialState"];
            this._currentState.Execute();
        }

        public void Send(enLogType logType, object dataValue)
        {
            this._logType = logType;
            this._dataValue = dataValue;

            this._currentState = _senderState["PrepareState"];
            this._currentState.Execute();

            this._currentState = _senderState["ProcessState"];
            this._currentState.Execute();
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

using RedisLib.Core;
using RedisLib.Sender.Models;
using RedisLib.Sender.SenderStates.Interfaces;
using RedisLib.Sender.SenderStates.States.Activity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Sender.Context
{
    public class SenderContext<T>
    {
        #region Members
        private string _id = Guid.NewGuid().ToString();
        private ISenderState _currentState = null;
        private IDictionary<string, ISenderState> _senderState = new Dictionary<string, ISenderState>();
        private bool disposedValue;
        private ReceiverTable _receivers = new ReceiverTable();
        private string _dataKey = string.Empty;
        private T _dataValue = default(T);
        private enLogType _logType;

        private static IRediser _msgConnection = null;
        private static IRediser _dataConnection = null;
        #endregion

        public SenderContext()
        {
            this.disposedValue = true;
            this._senderState.Add("InitialState", new InitialState<T>(this));
            this._senderState.Add("PrepareState", new PrepareState<T>(this));
            this._senderState.Add("ProcessState", new ProcessState<T>(this));

            this.ReceiverTable.CandidateInfo = new Dictionary<enLogType, int> {
                { enLogType.API,-1}, { enLogType.BO,-1}, { enLogType.System,-1},
            };
        }

        #region Property
        public string ID => this._id;
        public string DataKey { get; set; }
        public T DataValue => this._dataValue;
        public ISenderState CurrentState => this._currentState;
        public enLogType LogType => this._logType;
        public ReceiverTable ReceiverTable => this._receivers;
        public IRediser MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }
        public IRediser DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Initial()
        {
            this._currentState = _senderState["InitialState"];
            this._currentState.Execute();
        }

        public void Send(enLogType logType, T dataValue)
        {
            this._logType = logType;
            this._dataValue = dataValue;

            this._currentState = _senderState["PrepareState"];
            this._currentState.Execute();

            this._currentState = _senderState["ProcessState"];
            this._currentState.Execute();
        }

        #region IDisposable Support

        [ExcludeFromCodeCoverage]
        protected void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            _msgConnection.Client.Database.Multiplexer.Close();
            _msgConnection = null;

            _dataConnection.Client.Database.Multiplexer.Close();
            _dataConnection = null;

            this._senderState.Clear();

            this._senderState = null;
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

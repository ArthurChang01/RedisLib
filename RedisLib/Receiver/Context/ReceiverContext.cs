using RedisLib.Core;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.Interfaces;
using RedisLib.Receiver.ReceiverStates.States.Activity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Receiver.Context
{
    public class ReceiverContext<T> : IDisposable
    {
        #region Member
        private bool disposedValue = false; // To detect redundant calls
        private string _Id = Guid.NewGuid().ToString();
        private List<T> _dataObjs = new List<T>();
        private Queue<enLogType> _execRecords = new Queue<enLogType>(2);
        private IReceiverState _currentState = null;
        private IDictionary<string, IReceiverState> _receiverState = new Dictionary<string, IReceiverState>();

        private static IRediser _msgConnection = null;
        private static IRediser _dataConnection = null;
        #endregion

        #region Constructor
        public ReceiverContext()
        {
            _receiverState.Add("InitialState", new InitialState<T>(this));
            _receiverState.Add("PrepareState", new PrepareState<T>(this));
            _receiverState.Add("ProcessState", new ProcessState<T>(this));
        }
        #endregion

        #region Property
        public string ID => this._Id;

        public int NodeId { get; set; }

        public string Key { get; set; }

        public IReceiverState CurrentState => this._currentState;

        public IReceiverState ReceiverState { get; set; }

        public IDictionary<string, IReceiverState> LogStateTable => this._receiverState;

        public Queue<enLogType> ExecutedRecords => this._execRecords;

        public List<T> DataObjs { get { return this._dataObjs; } set { this._dataObjs = value; } }

        public IRediser MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }

        public IRediser DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Initial()
        {
            this._currentState = LogStateTable["InitialState"];
            this._currentState.Execute();
        }

        public void Run()
        {
            this._currentState = LogStateTable["PrepareState"];
            this._currentState.Execute();

            if (this.DataObjs.Count > 0)
            {
                this._currentState = LogStateTable["ProcessState"];
                this._currentState.Execute();
            }

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

            this._receiverState.Clear();
            this._dataObjs.Clear();

            this._receiverState = null;
            this._dataObjs = null;
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

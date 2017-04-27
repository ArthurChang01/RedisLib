﻿using RedisLib.Logger;
using RedisLib.Receiver.Models;
using RedisLib.Receiver.ReceiverStates.Interfaces;
using RedisLib.Receiver.ReceiverStates.States.Activity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RedisLib.Receiver.Context
{
    public class ReceiverContext : IDisposable
    {
        #region Member
        private bool disposedValue = false; // To detect redundant calls
        private string _Id = Guid.NewGuid().ToString();
        private CancellationTokenSource _cancelToken = new CancellationTokenSource();
        private List<string> _users = new List<string>();
        private IReceiverState _currentState = null;
        private IDictionary<string, IReceiverState> _logState = new Dictionary<string, IReceiverState>();
        private ResourceTable _resourceTable = new ResourceTable();

        private static RedisLogger _msgConnection = null;
        private static RedisLogger _dataConnection = null;
        #endregion

        #region Constructor
        public ReceiverContext()
        {
            _logState.Add("InitialState", new InitialState(this));
            _logState.Add("PrepareState", new PrepareState(this));
            _logState.Add("FetchDataState", new FetchDataState(this));
            _logState.Add("ProcessState", new ProcessState(this));
            _logState.Add("FinishState", new FinishState(this));
        }
        #endregion

        #region Property
        public string ID => this._Id;

        public CancellationTokenSource CancelToken => this._cancelToken;

        public IReceiverState LogState { get; set; }

        public IDictionary<string, IReceiverState> LogStateTable => this._logState;

        public ResourceTable ResourceTable => this._resourceTable;

        public List<string> Users => this._users;

        public RedisLogger MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }

        public RedisLogger DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Run()
        {
            this._currentState = LogStateTable["InitialState"];
            this._currentState.Execute();

            new Task(() =>
            {
                while (!_cancelToken.Token.IsCancellationRequested)
                {
                    try
                    {
                        this._currentState = LogStateTable["PrepareState"];
                        this._currentState.Execute();

                        this._currentState = LogStateTable["FetchDataState"];
                        this._currentState.Execute();

                        if (this.Users.Count > 0)
                        {
                            this._currentState = LogStateTable["ProcessState"];
                            this._currentState.Execute();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    System.Threading.Thread.Sleep(800);
                }

                this._currentState = LogStateTable["FinishState"];
                this._currentState.Execute();

            }, _cancelToken.Token, TaskCreationOptions.LongRunning)
            .Start();
        }

        #region IDisposable Support
        protected void Dispose(bool disposing)
        {
            if (!this.disposedValue) return;

            this._resourceTable.Records = null;

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
using RedisLib.Core;
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
        private int _nodeId = 0;
        private string _Id = Guid.NewGuid().ToString();
        private CancellationTokenSource _cancelToken = new CancellationTokenSource();
        private List<string> _users = new List<string>();
        private IDictionary<string, IReceiverState> _receiverState = new Dictionary<string, IReceiverState>();
        private ResourceTable _resourceTable = new ResourceTable();

        private static Rediser _msgConnection = null;
        private static Rediser _dataConnection = null;
        #endregion

        #region Constructor
        public ReceiverContext()
        {
            _receiverState.Add("InitialState", new InitialState(this));
            _receiverState.Add("RegistryState", new RegistryState(this));
            _receiverState.Add("PrepareState", new PrepareState(this));
            _receiverState.Add("FetchDataState", new FetchDataState(this));
            _receiverState.Add("ProcessState", new ProcessState(this));
            _receiverState.Add("FinishState", new FinishState(this));
        }
        #endregion

        #region Property
        public string ID => this._Id;

        public int NodeId { get; set; }

        public CancellationTokenSource CancelToken => this._cancelToken;

        public IReceiverState LogState { get; set; }

        public IDictionary<string, IReceiverState> LogStateTable => this._receiverState;

        public ResourceTable ResourceTable => this._resourceTable;

        public List<string> Users => this._users;

        public Rediser MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }

        public Rediser DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

        public void Run()
        {
            LogStateTable["InitialState"].Execute();

            LogStateTable["RegistryState"].Execute();

            new Task(() =>
            {
                while (!_cancelToken.Token.IsCancellationRequested)
                {
                    try
                    {
                        LogStateTable["PrepareState"].Execute();

                        LogStateTable["FetchDataState"].Execute();

                        if (this.Users.Count > 0)
                        {
                            LogStateTable["ProcessState"].Execute();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    System.Threading.Thread.Sleep(800);
                }

                LogStateTable["FinishState"].Execute();

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

            this._receiverState.Clear();
            this._users.Clear();

            this._receiverState = null;
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

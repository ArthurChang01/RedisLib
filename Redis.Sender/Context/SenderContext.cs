using Redis.Sender.SenderStates.Interfaces;
using Redis.Sender.SenderStates.States.Activity;
using RedisLib.Logger;
using System;
using System.Collections.Generic;

namespace Redis.Sender.Context
{
    class SenderContext
    {
        #region Members
        private string _id = Guid.NewGuid().ToString();
        private ISenderState _currentState = null;
        private List<string> _user = new List<string>();
        private IDictionary<string, ISenderState> _logState = new Dictionary<string, ISenderState>();

        private static RedisLogger _msgConnection = null;
        private static RedisLogger _dataConnection = null;
        #endregion

        public SenderContext() {
            _logState.Add("InitialState", new InitialState(this));
            _logState.Add("PrepareState", new PrepareState(this));
            _logState.Add("ProcessState", new ProcessState(this));
        }

        #region Property
        public string ID => this._id;
        public List<string> Users => this._user;
        public IDictionary<string, ISenderState> LogState => this._logState;
        public RedisLogger MsgConnection { get { return _msgConnection; } set { _msgConnection = value; } }
        public RedisLogger DataConnection { get { return _dataConnection; } set { _dataConnection = value; } }
        #endregion

    }
}

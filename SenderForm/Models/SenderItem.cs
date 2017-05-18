using RedisLib.Core;
using RedisLib.Core.Enums;
using System.Configuration;
using Transceiver.Model;
using Transceiver.Sender;

namespace SenderForm.Models
{
    public class SenderItem
    {
        #region Members
        private SenderContext<object> _sender = null;
        #endregion

        #region Constructor
        public SenderItem(SenderContext<object> sender)
        {
            this._sender = sender;
        }
        #endregion

        #region Property
        public string SenderId { get { return this._sender.ID; } }

        public bool IsInitiate { get; set; }

        public string CurrentState
        {
            get
            {
                return this._sender.CurrentState == null ?
                    string.Empty :
                    this._sender.CurrentState.StateName;
            }
        }

        public string DataKey { get { return this._sender.DataKey ?? string.Empty; } }

        #endregion

        #region Method
        public void InitialSender()
        {
            string conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;

            this._sender.MsgConnection = new Rediser(conString, SerializerType.NewtonJson);
            this._sender.DataConnection = new Rediser(conString, SerializerType.NewtonJson);

            this._sender.Initial();
        }

        public void SendMsg(enLogType logType, string msg)
        {
            this._sender.Send(logType, msg);
        }
        #endregion
    }
}

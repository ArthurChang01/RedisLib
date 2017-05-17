using RedisLib.Core;
using RedisLib.Core.Enums;
using RedisLib.Receiver.Context;
using System.Configuration;

namespace ReceiverForm.Models
{
    public class ReceiverItem
    {
        private ReceiverContext<object> _receiver = null;

        #region Constructor
        public ReceiverItem(ReceiverContext<object> _receiver)
        {
            this._receiver = _receiver;
        }
        #endregion

        #region Property
        public string ReceiverId { get { return this._receiver.ID; } }

        public int NodeId { get { return this._receiver.NodeId; } }

        public bool IsInitiate { get; set; }

        public string CurrentState { get {
                return this._receiver.CurrentState == null ? 
                    string.Empty : 
                    this._receiver.CurrentState.StateName;
            }
        }

        public string DataKey { get {
                return this._receiver == null ? 
                    string.Empty : 
                    this._receiver.Key;
            }
        }
        #endregion

        #region Method
        public void InitialReceiver()
        {
            string conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;

            this._receiver.MsgConnection = new Rediser(conString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(conString, SerializerType.NewtonJson);

            this._receiver.Initial();
        }

        public void ReceiveMsg()
        {
            this._receiver.Run();
        }

        #endregion
    }
}

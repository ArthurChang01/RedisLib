using CoreLib.DB;
using CoreLib.ES;
using CoreLib.Redis;
using CoreLib.Redis.Enums;
using System.Configuration;
using Transceiver.Receiver;

namespace ReceiverForm.Models
{
    public class ReceiverItem
    {
        private ReceiverContext<DTO> _receiver = null;

        #region Constructor
        public ReceiverItem(ReceiverContext<DTO> _receiver)
        {
            this._receiver = _receiver;
        }
        #endregion

        #region Property
        public string ReceiverId { get { return this._receiver.ID; } }

        public int NodeId { get { return this._receiver.NodeId; } }

        public bool IsInitiate { get; set; }

        public string CurrentState
        {
            get
            {
                return this._receiver.CurrentState == null ?
                    string.Empty :
                    this._receiver.CurrentState.StateName;
            }
        }

        public string DataKey
        {
            get
            {
                return this._receiver == null ?
                    string.Empty :
                    this._receiver.Key;
            }
        }
        #endregion

        #region Method
        public void InitialReceiver()
        {
            string redisConString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString,
                      dbConString = ConfigurationManager.ConnectionStrings["db"].ConnectionString,
                      esConString = ConfigurationManager.ConnectionStrings["es"].ConnectionString;

            this._receiver.MsgConnection = new Rediser(redisConString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(redisConString, SerializerType.NewtonJson);
            this._receiver.DB = new DBer(dbConString);
            this._receiver.ES = new ESer(esConString);

            this._receiver.Initial();
        }

        public void ReceiveMsg()
        {
            this._receiver.ReceiveMsg();
        }

        #endregion
    }
}

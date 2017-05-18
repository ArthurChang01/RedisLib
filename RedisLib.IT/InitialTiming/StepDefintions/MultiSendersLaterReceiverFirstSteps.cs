using CoreLib.Redis;
using CoreLib.Redis.Enums;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Transceiver.Model;
using Transceiver.Receiver;
using Transceiver.Sender;

namespace TransceiverLib.IT.InitialTiming.StepDefintions
{
    [ExcludeFromCodeCoverage]
    [Binding]
    [Scope(Feature = "MultiSendersLaterReceiverFirst")]
    public class MultiSendersLaterReceiverFirstSteps
    {
        private List<SenderContext<object>> _senders;
        private ReceiverContext<object> _receiver;
        private string _redisConString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private string _dbConString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        private string _esConString = ConfigurationManager.ConnectionStrings["es"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._senders = new List<SenderContext<object>>();
            this._receiver = new ReceiverContext<object>();

            this._checker = new Rediser(_redisConString);
        }

        [AfterScenario]
        public async Task ScenarioTeardown()
        {
            await this._checker.RemoveAllAsync("ReceiverRegistry");
            await this._checker.RemoveAllAsync("ReceiverReply");
            await this._checker.RemoveAllAsync("{API/0}:*");
        }

        [Given(@"A receiver has been initiated")]
        public void AReceiverHasBeenInitiated()
        {
            this._receiver = new ReceiverContext<object>();
            this._receiver.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);

            this._receiver.Initial();
        }

        [Given(@"This receiver is waiting for trigger")]
        public void ThisReceiverIsWaitingForTrigger()
        {
            //ScenarioContext.Current.Pending();
        }

        [When(@"Initiate multi-sender")]
        public void InitiateMulti_Sender()
        {
            SenderContext<object> senderA = new SenderContext<object>(),
                                                    senderB = new SenderContext<object>();
            senderA.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            senderA.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);

            senderB.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            senderB.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);

            senderA.Initial();
            senderB.Initial();

            this._senders.Add(senderA);
            this._senders.Add(senderB);
        }

        [Then(@"Multi-sender can save data into redis")]
        public void Multi_SenderCanSaveDataIntoRedis()
        {
            object transferObj = new object();
            this._senders.ForEach(o => o.Send(enLogType.API, transferObj));
        }

        [Then(@"A receiver can fetch data which are saved by senders")]
        public void AReceiverCanFetchDataWhichAreSavedBySenders()
        {
            _receiver.ReceiveMsg();
        }
    }
}

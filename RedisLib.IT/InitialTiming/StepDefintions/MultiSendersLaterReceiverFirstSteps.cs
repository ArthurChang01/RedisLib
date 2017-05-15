using RedisLib.Core;
using RedisLib.Core.Enums;
using RedisLib.Receiver.Context;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "MultiSendersLaterReceiverFirst")]
    public class MultiSendersLaterReceiverFirstSteps
    {
        private List<SenderContext<object>> _senders;
        private ReceiverContext<object> _receiver;
        private string _conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._senders = new List<SenderContext<object>>();
            this._receiver = new ReceiverContext<object>();

            this._checker = new Rediser(_conString);
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
            this._receiver.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

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
            senderA.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            senderA.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            senderB.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            senderB.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

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
            _receiver.Run();
        }
    }
}

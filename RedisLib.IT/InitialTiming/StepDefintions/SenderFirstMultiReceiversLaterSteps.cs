using FluentAssertions;
using RedisLib.Core;
using RedisLib.Core.Enums;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Transceiver.Constant;
using Transceiver.Model;
using Transceiver.Receiver;
using Transceiver.Sender;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [ExcludeFromCodeCoverage]
    [Binding]
    [Scope(Feature = "SenderFirstMultiReceiversLater")]
    public class SenderFirstMultiReceiversLaterSteps
    {
        private SenderContext<object> _senders;
        private List<ReceiverContext<object>> _receivers;
        private string _conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._senders = new SenderContext<object>();
            this._receivers = new List<ReceiverContext<object>>();

            this._checker = new Rediser(_conString);
        }

        [AfterScenario]
        public async Task ScenarioTeardown()
        {
            await this._checker.RemoveAllAsync("ReceiverRegistry");
            await this._checker.RemoveAllAsync("ReceiverReply");
            await this._checker.RemoveAllAsync("{API/0}:*");
        }

        [Given(@"A sender has been initiated")]
        public void ASenderHasBeenInitiated()
        {
            this._senders.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._senders.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            this._senders.Initial();
        }

        [Given(@"This sender is going to send data")]
        public void ThisSenderIsGoingToSendData()
        {
            object transferObj = new object();
            this._senders.Send(enLogType.API, transferObj);
        }

        [When(@"Initiate multi-receiver")]
        public void InitiateMulti_Receiver()
        {
            ReceiverContext<object> receiverA = new ReceiverContext<object>(),
                                                        receiverB = new ReceiverContext<object>();
            receiverA.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            receiverA.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            receiverB.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            receiverB.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            receiverA.Initial();
            receiverB.Initial();

            this._receivers.Add(receiverA);
            this._receivers.Add(receiverB);
        }

        [Then(@"A sender can save data into redis")]
        public void ASenderCanSaveDataIntoRedis()
        {
            IEnumerable<object> result = this._checker.Fetch<object>("{API/0}:*");

            result.Should().NotBeNull().And.NotBeEmpty();
        }

        [Then(@"Multi-receiver can fetch data which are saved by sender")]
        public void Multi_ReceiverCanFetchDataWhichAreSavedBySender()
        {
            _receivers.ForEach(o => o.Run());
        }

        [Then(@"Every receiver gets different node id")]
        public void EveryReceiverGetsDifferentNodeId()
        {
            IEnumerable<string> receiverNodeId = this._checker.GetHashTable<int>(KeyName.ReceiverRegistry).Keys;
            bool isDiff = true;

            string temp = string.Empty;
            foreach (string id in receiverNodeId)
            {
                isDiff = !temp.Equals(id);
                temp = id;
            }

            isDiff.Should().Be(true);
        }
    }
}

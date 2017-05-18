using FluentAssertions;
using RedisLib.Core;
using RedisLib.Core.Enums;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Transceiver.Model;
using Transceiver.Receiver;
using Transceiver.Sender;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [ExcludeFromCodeCoverage]
    [Binding]
    [Scope(Feature = "MultiSendersFirstReceiverLater")]
    public class MultiSendersFirstReceiverLaterSteps
    {
        private List<SenderContext<object>> _senders;
        private ReceiverContext<object> _receivers;
        private string _conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._senders = new List<SenderContext<object>>();
            this._receivers = new ReceiverContext<object>();

            this._checker = new Rediser(_conString);
        }

        [AfterScenario]
        public async Task ScenarioTeardown()
        {
            await this._checker.RemoveAllAsync("ReceiverRegistry");
            await this._checker.RemoveAllAsync("ReceiverReply");
            await this._checker.RemoveAllAsync("{API/0}:*");
        }

        [Given("Multi-sender has been initiated")]
        public void MultiSenderHasBeenInitialed()
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

        [Given("This multi-sender are going to send data")]
        public void ThisMultiSenderAreGoingToSendData()
        {
            object transferObj = new object();
            this._senders.ForEach(o => o.Send(enLogType.API, transferObj));
        }

        [When("Initiate a receiver")]
        public void InitiateAReceiver()
        {
            this._receivers.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._receivers.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            this._receivers.Initial();
        }

        [Then("Multi-sender can save data into redis")]
        public void MultiSenderCanSaveDataIntoRedis()
        {
            IEnumerable<object> result = this._checker.Fetch<object>("{API/0}:*");

            result.Should().NotBeNull().And.NotBeEmpty();
        }

        [Then("A receiver can fetch data which are saved by senders")]
        public void AReceiverCanFetchDataWhichAreSavedBySenders()
        {
            this._receivers.Run();
        }
    }
}

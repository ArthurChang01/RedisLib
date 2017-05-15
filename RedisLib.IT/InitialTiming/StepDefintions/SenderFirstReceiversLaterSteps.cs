using FluentAssertions;
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
    [Scope(Feature = "SenderFirstReceiversLater")]
    public class SenderFirstReceiversLaterSteps
    {
        private SenderContext<object> _sender;
        private ReceiverContext<object> _receiver;
        private string _conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._sender = new SenderContext<object>();
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

        [Given(@"A sender has been initiated")]
        public void ASenderHasBeenInitiated()
        {
            this._sender.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._sender.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            this._sender.Initial();
        }

        [Given(@"This sender is going to send data")]
        public void ThisSenderIsGoingToSendData()
        {
            object transferObj = new object();
            this._sender.Send(enLogType.API, transferObj);
        }

        [When(@"Initiate a receiver")]
        public void InitiateAReceiver()
        {
            this._receiver.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._receiver.Initial();
        }

        [Then(@"A sender can save data into redis")]
        public void ASenderCanSaveDataIntoRedis()
        {
            IEnumerable<object> result = this._checker.Fetch<object>("{API/0}:*");

            result.Should().NotBeNull().And.NotBeEmpty();
        }

        [Then(@"A receiver can fetch data which are saved by sender")]
        public void AReceiverCanFetchDataWhichAreSavedBySender()
        {
            this._receiver.Run();
        }
    }
}

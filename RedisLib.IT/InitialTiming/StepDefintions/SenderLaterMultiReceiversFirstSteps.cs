using FluentAssertions;
using RedisLib.Core;
using RedisLib.Core.Enums;
using RedisLib.Receiver.Constants;
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
    [Scope(Feature = "SenderLaterMultiReceiversFirst")]
    public class SenderLaterMultiReceiversFirstSteps
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

        [Given(@"Multi-receiver have been initiated")]
        public void Multi_ReceiverHaveBeenInitiated()
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

        [When(@"Initiate a sender")]
        public void InitiateASender()
        {
            this._senders.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._senders.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

            this._senders.Initial();
        }

        [Then(@"A sender can save data into redis")]
        public void ASenderCanSaveDataIntoRedis()
        {
            object transferObj = new object();
            this._senders.Send(enLogType.API, transferObj);
        }

        [Then(@"First one receiver can fetch data which are saved by sender")]
        public void FirstOneReceiverCanFetchDataWhichAreSavedBySender()
        {
            _receivers.ForEach(o => o.Run());
        }

        [Then(@"Every receiver get different node id")]
        public void EveryReceiverGetDifferentNodeId()
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

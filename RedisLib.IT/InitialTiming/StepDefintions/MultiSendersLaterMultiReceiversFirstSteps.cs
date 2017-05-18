using CoreLib.Redis;
using CoreLib.Redis.Enums;
using FluentAssertions;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Transceiver.Constant;
using Transceiver.Model;
using Transceiver.Receiver;
using Transceiver.Sender;

namespace TransceiverLib.IT.InitialTiming.StepDefintions
{
    [ExcludeFromCodeCoverage]
    [Binding]
    [Scope(Feature = "MultiSendersLaterMultiReceiversFirst")]
    public class MultiSendersLaterMultiReceiversFirstSteps
    {
        private List<SenderContext<object>> _senders;
        private List<ReceiverContext<object>> _receivers;
        private string _redisConString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private string _dbConString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        private string _esConString = ConfigurationManager.ConnectionStrings["es"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._senders = new List<SenderContext<object>>();
            this._receivers = new List<ReceiverContext<object>>();

            this._checker = new Rediser(_redisConString);
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
            receiverA.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            receiverA.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);

            receiverB.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            receiverB.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);

            receiverA.Initial();
            receiverB.Initial();

            this._receivers.Add(receiverA);
            this._receivers.Add(receiverB);
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

        [Then(@"Multi-receiver can fetch data which are saved by senders")]
        public void Multi_ReceiverCanFetchDataWhichAreSavedBySenders()
        {
            _receivers.ForEach(o => o.ReceiveMsg());
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

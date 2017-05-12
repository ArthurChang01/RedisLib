using FluentAssertions;
using RedisLib.Core;
using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using System.Collections.Generic;
using System.Configuration;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    public class MultiSendersFirstMultiReceiversLaterSteps
    {
        private List<SenderContext<object>> _senders;
        private List<ReceiverContext<object>> _receivers;
        private string _conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private IRediser _checker = null;

        public MultiSendersFirstMultiReceiversLaterSteps()
        {
            this._senders = new List<SenderContext<object>>();
            this._receivers = new List<ReceiverContext<object>>();

            this._checker = new Rediser(_conString);
        }

        [Given(@"Multi-sender have been initiated")]
        public void GivenMulti_SenderHaveBeenInitiated()
        {
            SenderContext<object> senderA = new SenderContext<object>(),
                                                    senderB = new SenderContext<object>();
            senderA.MsgConnection = new Rediser(_conString);
            senderA.DataConnection = new Rediser(_conString);

            senderB.MsgConnection = new Rediser(_conString);
            senderB.DataConnection = new Rediser(_conString);

            senderA.Initial();
            senderB.Initial();

            this._senders.Add(senderA);
            this._senders.Add(senderB);
        }

        [Given(@"This multi-sender are going to send data")]
        public void GivenThisMulti_SenderAreGoingToSendData()
        {
            object transferObj = new object();
            this._senders.ForEach(o => o.Send(enLogType.API, transferObj));
        }

        [When(@"I initiate multi-receiver")]
        public void WhenIInitiateMulti_Receiver()
        {
            ReceiverContext<object> receiverA = new ReceiverContext<object>(),
                                                        receiverB = new ReceiverContext<object>();
            receiverA.MsgConnection = new Rediser(_conString);
            receiverA.DataConnection = new Rediser(_conString);

            receiverB.MsgConnection = new Rediser(_conString);
            receiverB.DataConnection = new Rediser(_conString);

            this._receivers.Add(receiverA);
            this._receivers.Add(receiverB);
        }

        [Then(@"Multi-sender can save data into redis")]
        public void ThenMulti_SenderCanSaveDataIntoRedis()
        {
            IEnumerable<object> result = this._checker.Fetch<object>("{API/0}:*");

            result.Should().NotBeNull().And.NotBeEmpty();
        }

        [Then(@"Multi-receiver can fetch data which are saved by senders")]
        public void ThenMulti_ReceiverCanFetchDataWhichAreSavedBySenders()
        {
            IDictionary<string, int> receiverReplies = this._checker.GetHashTable<int>(KeyName.ReceiverReply);
            int debt = receiverReplies[this._receivers[0].ID];

            debt.Should().BeGreaterThan(0);
        }

        [Then(@"Every receiver get different node id")]
        public void ThenEveryReceiverGetDifferentNodeId()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

﻿using CoreLib.Redis;
using CoreLib.Redis.Enums;
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
    [Scope(Feature = "SenderLaterReceiverFirst")]
    public class SenderLaterReceiverFirstSteps
    {
        private SenderContext<object> _sender;
        private ReceiverContext<object> _receiver;
        private string _redisConString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;
        private string _dbConString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        private string _esConString = ConfigurationManager.ConnectionStrings["es"].ConnectionString;
        private IRediser _checker = null;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            this._sender = new SenderContext<object>();
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
            this._receiver.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            this._receiver.Initial();
        }

        [When(@"Initiate a sender")]
        public void InitiateASender()
        {
            this._sender.MsgConnection = new Rediser(_redisConString, SerializerType.NewtonJson);
            this._sender.DataConnection = new Rediser(_redisConString, SerializerType.NewtonJson);

            this._sender.Initial();
        }

        [Then(@"A sender can save data into redis")]
        public void ASenderCanSaveDataIntoRedis()
        {
            object transferObj = new object();
            this._sender.Send(enLogType.API, transferObj);
        }

        [Then(@"A receiver can fetch data which is saved by sender")]
        public void AReceiverCanFetchDataWhichIsSavedBySender()
        {
            this._receiver.ReceiveMsg();
        }
    }
}

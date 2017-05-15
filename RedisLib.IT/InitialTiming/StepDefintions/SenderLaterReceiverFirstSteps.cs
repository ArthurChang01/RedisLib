using RedisLib.Core;
using RedisLib.Core.Enums;
using RedisLib.Receiver.Context;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using System.Configuration;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "SenderLaterReceiverFirst")]
    public class SenderLaterReceiverFirstSteps
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

        [Given(@"A receiver has been initiated")]
        public void AReceiverHasBeenInitiated()
        {
            this._receiver.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._receiver.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._receiver.Initial();
        }

        [When(@"Initiate a sender")]
        public void InitiateASender()
        {
            this._sender.MsgConnection = new Rediser(_conString, SerializerType.NewtonJson);
            this._sender.DataConnection = new Rediser(_conString, SerializerType.NewtonJson);

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
            this._receiver.Run();
        }
    }
}

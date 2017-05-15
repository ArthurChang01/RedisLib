using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "MultiSendersLaterReceiverFirst")]
    public class MultiSendersLaterReceiverFirstSteps
    {
        [Given(@"A receiver has been initiated")]
        public void GivenAReceiverHasBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"This receiver is waiting for trigger")]
        public void GivenThisReceiverIsWaitingForTrigger()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I initiate multi-sender")]
        public void WhenIInitiateMulti_Sender()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Multi-sender can save data into redis")]
        public void ThenMulti_SenderCanSaveDataIntoRedis()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A receiver can fetch data which are saved by senders")]
        public void ThenAReceiverCanFetchDataWhichAreSavedBySenders()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

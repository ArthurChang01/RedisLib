using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "SenderLaterReceiverFirst")]
    public class SenderLaterReceiverFirstSteps
    {
        [Given(@"A receiver has been initiated")]
        public void GivenAReceiverHasBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"A receiver is waiting for trigger")]
        public void GivenAReceiverIsWaitingForTrigger()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I initiate a sender")]
        public void WhenIInitiateASender()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A sender can save data into redis")]
        public void ThenASenderCanSaveDataIntoRedis()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A receiver can fetch data which is saved by sender")]
        public void ThenAReceiverCanFetchDataWhichIsSavedBySender()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

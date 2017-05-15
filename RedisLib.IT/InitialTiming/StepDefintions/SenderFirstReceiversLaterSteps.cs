using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "SenderFirstReceiversLater")]
    public class SenderFirstReceiversLaterSteps
    {
        [Given(@"A sender has been initiated")]
        public void GivenASenderHasBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"This sender is going to send data")]
        public void GivenThisSenderIsGoingToSendData()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I initiate a receiver")]
        public void WhenIInitiateAReceiver()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A sender can save data into redis")]
        public void ThenASenderCanSaveDataIntoRedis()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A receiver can fetch data which are saved by sender")]
        public void ThenAReceiverCanFetchDataWhichAreSavedBySender()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "SenderFirstMultiReceiversLater")]
    public class SenderFirstMultiReceiversLaterSteps
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

        [When(@"I initiate multi-receiver")]
        public void WhenIInitiateMulti_Receiver()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A sender can save data into redis")]
        public void ThenASenderCanSaveDataIntoRedis()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Multi-receiver can fetch data which are saved by sender")]
        public void ThenMulti_ReceiverCanFetchDataWhichAreSavedBySender()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Every receiver get different node id")]
        public void ThenEveryReceiverGetDifferentNodeId()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

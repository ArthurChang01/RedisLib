using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "SenderLaterMultiReceiversFirst")]
    public class SenderLaterMultiReceiversFirstSteps
    {
        [Given(@"Multi-receiver have been initiated")]
        public void GivenMulti_ReceiverHaveBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Multi-receiver are waiting for trigger")]
        public void GivenMulti_ReceiverAreWaitingForTrigger()
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

        [Then(@"First one receiver can fetch data which are saved by sender")]
        public void ThenFirstOneReceiverCanFetchDataWhichAreSavedBySender()
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

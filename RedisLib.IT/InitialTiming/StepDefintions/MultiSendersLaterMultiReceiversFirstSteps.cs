using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "MultiSendersLaterMultiReceiversFirst")]
    public class MultiSendersLaterMultiReceiversFirstSteps
    {
        [Given(@"Multi-receiver have been initiated")]
        public void GivenMulti_ReceiverHaveBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"This multi-receiver are waiting for tirgger")]
        public void GivenThisMulti_ReceiverAreWaitingForTirgger()
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

        [Then(@"Multi-receiver can fetch data which are saved by senders")]
        public void ThenMulti_ReceiverCanFetchDataWhichAreSavedBySenders()
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

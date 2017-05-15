using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    [Scope(Feature = "MultiSendersFirstReceiverLater")]
    public sealed class MultiSendersFirstReceiverLaterSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        [Given("A multi-sender has been initiated")]
        public void AMultiSenderHasBeenInitialed()
        {
            ScenarioContext.Current.Pending();
        }

        [Given("this multi-sender are going to send data")]
        public void ThisMultiSenderAreGoingToSendData()
        {
            ScenarioContext.Current.Pending();
        }

        [When("I initiate a receiver")]
        public void IInitiateAReceiver()
        {
            ScenarioContext.Current.Pending();
        }

        [Then("Multi-sender can save data into redis")]
        public void MultiSenderCanSaveDataIntoRedis()
        {
            ScenarioContext.Current.Pending();
        }

        [Then("A receiver can fetch data which are saved by senders")]
        public void AReceiverCanFetchDataWhichAreSavedBySenders()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

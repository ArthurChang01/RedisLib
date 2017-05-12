using System;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    public class MultiSendersFirstReceiverLaterSteps
    {
        [Given(@"A multi-sender has been initiated")]
        public void GivenAMulti_SenderHasBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"this multi-sender are going to send data")]
        public void GivenThisMulti_SenderAreGoingToSendData()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I initiate a receiver")]
        public void WhenIInitiateAReceiver()
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

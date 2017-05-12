using System;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    public class SenderLaterMultiReceiversFirstSteps
    {
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
        
        [Then(@"First one receiver can fetch data which are saved by sender")]
        public void ThenFirstOneReceiverCanFetchDataWhichAreSavedBySender()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

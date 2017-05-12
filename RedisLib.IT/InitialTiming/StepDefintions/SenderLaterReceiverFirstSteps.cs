using System;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    public class SenderLaterReceiverFirstSteps
    {
        [Given(@"A receiver is waiting for trigger")]
        public void GivenAReceiverIsWaitingForTrigger()
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

using System;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    public class MultiSendersLaterReceiverFirstSteps
    {
        [Given(@"A receiver has been initiated")]
        public void GivenAReceiverHasBeenInitiated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"This receiver is waiting for tirgger")]
        public void GivenThisReceiverIsWaitingForTirgger()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

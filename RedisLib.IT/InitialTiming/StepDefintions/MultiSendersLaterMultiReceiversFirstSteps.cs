using System;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
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
    }
}

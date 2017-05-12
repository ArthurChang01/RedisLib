using System;
using TechTalk.SpecFlow;

namespace RedisLib.IT.InitialTiming.StepDefintions
{
    [Binding]
    public class SenderFirstReceiversLaterSteps
    {
        [Then(@"A receiver can fetch data which are saved by sender")]
        public void ThenAReceiverCanFetchDataWhichAreSavedBySender()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

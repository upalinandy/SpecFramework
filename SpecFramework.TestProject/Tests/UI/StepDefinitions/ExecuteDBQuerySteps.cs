using System;
using TechTalk.SpecFlow;

namespace SpecFramework.SL.TestProject.Tests.UI.StepDefinitions
{
    [Binding]
    public class ExecuteDBQuerySteps
    {
        [Given(@"User connects to the DB")]
        public void GivenUserConnectsToTheDB()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Select query is executed")]
        public void WhenSelectQueryIsExecuted()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Result is returned and displayed")]
        public void ThenResultIsReturnedAndDisplayed()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

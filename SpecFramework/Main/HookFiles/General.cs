using SpecFramework.GlobalParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFramework.HookFiles
{
    [Binding]
    public sealed class General
    {
   
        
        [BeforeScenario("excel", "table")]
        public void BeforeScneario()
        {

        }

        [AfterScenario("excel", "table")]
        public void AfterScenario()
        {
            if (ObjectRepo.driver != null)
            {
                ObjectRepo.driver.Close();
                ObjectRepo.driver.Quit();
            }
        }

        [AfterScenario("cefapp")]
        public void AfterScenarioForCEF()
        {
            if (ObjectRepo.driver != null)
            {
                ObjectRepo.driver.Quit();
            }
        }

    }
}

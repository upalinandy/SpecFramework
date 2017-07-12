using SpecFramework.Core.CommonUtils;
using System;
using TechTalk.SpecFlow;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SpecFramework.SL.TestProject.Tests.UI.StepDefinitions
{
    [Binding]
    public class RegisterXpo_NewSteps
    {
        Dictionary<String, Dictionary<String, String>> LoginobjectRepo = new Dictionary<string, Dictionary<string, string>>();
        
        [Given(@"User is on registration page (.*)")]
        public void GivenUserIsOnRegistrationPage(string p0)
        {
           String pathName =  Path.GetFullPath("..\\..\\Resources\\Objects\\LoginPage.xml");
           LoginobjectRepo = XMLUtil.GetObjectDetails(String.Empty,pathName);
            
           string temp = LoginobjectRepo["UserName"]["Value"];
           
           ScenarioContext.Current.Pending();
        }
        
        [When(@"User entered (.*) (.*) (.*) (.*) (.*) (.*) (.*)")]
        public void WhenUserEntered(string p0, string p1, string p2, string p3, string p4, string p5, string p6)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"also entered (.*) (.*) (.*) (.*) (.*) (.*) (.*) (.*) and (.*)")]
        public void WhenAlsoEnteredAnd(string p0, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)
        {
            ScenarioContext.Current.Pending();
        }
    }
}

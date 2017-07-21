using SpecFramework.Core.CommonUtils;
using System;
using TechTalk.SpecFlow;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFramework.SL.TestProject.Tests.Suite.StepDefinitions
{
    [Binding]
    public class RegisterUserSteps
    {
        public static IWebDriver webdriver;
        Dictionary<String, Dictionary<String, String>> LoginobjectRepo = new Dictionary<string, Dictionary<string, string>>();

        [Given(@"User is on registration page")]
        public void GivenUserIsOnRegistrationPage()
        {
            string projectDirPath = Directory.GetParent( Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
            String pathName = Path.GetFullPath(projectDirPath + "\\Resources\\Objects\\LoginPage.xml");
            LoginobjectRepo = XMLUtil.GetObjectDetails(String.Empty, pathName);

            // It will try read the xml file based on "name" tag e.g. name="USERNAME" 
            // and the 2nd parameter would be any of the remaining three tags i.e. "type", "property" or "value"
            string temp = LoginobjectRepo["USERNAME"]["Value"];

            ScenarioContext.Current.Pending();
        }

        [When(@"User enters name")]
        public void WhenUserEntersName()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"also entered (.*) (.*) (.*) (.*) (.*) (.*) (.*) (.*) and (.*)")]
        public void WhenAlsoEnteredAnd(string p0, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"User is navigated to home page")]
        public void ThenUserIsNavigatedToHomePage()
        {
            ScenarioContext.Current.Pending();
        }

        /*
         *  User may choose to set driver using different method like this or have it present in "Utilities"    
         */
        public void DriverSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            webdriver = new ChromeDriver(@"D:\SpecFramework_Latest\packages\Selenium.WebDriver.ChromeDriver.2.24.0.0\driver", options);
        }

        public void DriverTeardown()
        {
            webdriver.Quit();
        }


    }
}

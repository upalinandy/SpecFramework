using NUnit.Framework;
using SpecFramework.ActionClasses;
using SpecFramework.FeatureFilePath;
using SpecFramework.GlobalParam;
using SpecFramework.Jira.JiraApi;
using SpecFramework.Jira.JiraBug;
using SpecFramework.Jira.JiraNewFeature;
using SpecFramework.Jira.JiraUserStory;
using SpecFramework.Main.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFramework.StepDefinitions
{
    [Binding]
    public sealed class UberTest
    {
        //  UserStoryCreate userStory = new UserStoryCreate();
        NewFeatureCreate newfeature = new NewFeatureCreate();
        FeatureFileBasePath featurePathBase = new FeatureFileBasePath();
        private UISetup ur = new UISetup();
        BugCreate bug = new BugCreate();
        string exceptiontext = null;
        string bugsummary = null;
        bool bugcreateflag = false;
        BugState bugstate = new BugState();


        [Given(@"User is at homepage (.*)")]
        public void GivenUserIsAtHomepage(string url)
        {
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            string featureFilePath = featurePathBase.GetFeatureFilePath(featureName);
            Console.Out.WriteLine(featureFilePath);
            // userStory.UserStoryCheckCreate(featureName, featureFilePath);
            newfeature.NewFeatureCheckCreate(featureName, featureFilePath);
            UIActions.GoToUrl(url);
        }
        [When(@"User clicks on Signin")]
        public void WhenUserClicksOnSignin()
        {
           
            UIActions.Click(ur.signin);
        }

        [Then(@"User is navigate to (.*)")]
        public void ThenUserIsNavigateTo(string signinpage)
        {
            try
            {
                Assert.AreEqual(signinpage, UIActions.getTitle());
            }
            catch (Exception ex)
            {
                bugcreateflag = true;
                exceptiontext = ex.ToString();
                bugsummary = " Test does not navigate to expected sign in page";
                throw ex;
            }
            finally
            {
               if (bugcreateflag == true)
                {
                  bug.create(bugsummary, exceptiontext,bugstate);
                }
            }
        }

        [Then(@"User is navigated to Uber (.*)")]
        public void ThenUserIsNavigatedToUber(string signinpage)
        {
            try
            {
                Assert.AreEqual(signinpage, UIActions.getTitle());
            }
            catch (Exception ex)
            {
                bugcreateflag = true;
                exceptiontext = ex.ToString();
                bugsummary = " Test does not navigate to expected Uber sign in page";
                throw ex;
            }
            finally
            {
                if (bugcreateflag == true)
                {
                    bug.create(bugsummary, exceptiontext,bugstate);
                }
            }
        }

        [When(@"User clicks on Login")]
        public void WhenUserClicksOnLogin()
        {
            Console.WriteLine("In when");
            UIActions.elementExists(ur.airbnb_login);
        //    bool isdisplayed = UIActions.ElementDisplayed(ur.airbnb_login);    
       //   UIActions.Click(ur.airbnb_login);
            UIActions.javascriptclick(ur.airbnb_login);
        }

        [Then(@"User is navigated to Airbnb (.*)")]
        public void ThenUserIsNavigatedToAirbnb(string loginpage)
        {
            try
            {
                Assert.AreEqual(loginpage, UIActions.getTitle());
            }
            catch (Exception ex)
            {
                bugcreateflag = true;
                exceptiontext = ex.ToString();
                bugsummary = " Test does not navigate to expected Airbnb page";
                throw ex;
            }
            finally
            {
                if (bugcreateflag == true)
                {
                    bug.create(bugsummary, exceptiontext, bugstate);
                }
            }
        }


        [When(@"User clicks on rider signin")]
        public void WhenUserClicksOnRiderSignin()
        {
            UIActions.Click(ur.rider_signin);
        }

        [Then(@"User is at ridersignin page")]
        public void ThenUserIsAtRidersigninPage()
        {
            Console.WriteLine("In rider then");
            string expected = "hi";
            Assert.AreEqual(expected, UIActions.getTitle());
        }

        [Given(@"User being at login page (.*)")]
        public void GivenUserBeingAtLoginPage(string url)
        {
            UIActions.GoToUrl(url);
        }





    }
}

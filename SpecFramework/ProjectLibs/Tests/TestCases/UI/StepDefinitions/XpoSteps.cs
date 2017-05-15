using SpecFramework.ActionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using SpecFramework.FeatureFilePath;
using SpecFramework.GlobalParam;
using SpecFramework.Jira.JiraBug;
using SpecFramework.Jira.JiraNewFeature;
using SpecFramework.Jira.JiraUserStory;
using SpecFramework.ProjectLibs.UI;
using OpenQA.Selenium;
using SpecFramework.Main.CommonUtils;

namespace SpecFramework.StepDefinitions
{
    [Binding]
    public sealed class XpoSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        NewFeatureCreate newfeature = new NewFeatureCreate();
        FeatureFileBasePath featurePathBase = new FeatureFileBasePath();
       private UISetup xpotest = new UISetup();
        private XpoUI xui = new XpoUI();
  

        [Given(@"User is at registration page (.*)")]
        public void GivenUserIsAtRegistrationPage(string url)
        {
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            string featureFilePath = featurePathBase.GetFeatureFilePath(featureName);
            Console.Out.WriteLine(featureFilePath);
            newfeature.NewFeatureCheckCreate(featureName, featureFilePath);
            UIActions.GoToUrl(url);

        }
    

        [When(@"User enters (.*) (.*) (.*) (.*) (.*) (.*) (.*)")]
        public void WhenUserEnters(string fn, string ln, string jt, string em, string rem, string pwd, string repwd)
        {
            Console.WriteLine("repwd:" + repwd);
            
            ObjectRepo.driver.SwitchTo().Frame("iframeRegisterNow");
            UIActions.elementExists(xui.txt_fn);
            UIActions.sendKey(xui.txt_fn, fn);
            UIActions.sendKey(xui.txt_ln, ln);
            UIActions.sendKey(xui.txt_jobtitle, jt);
            UIActions.sendKey(xui.txt_email, em);
            UIActions.sendKey(xui.txt_reemail, rem);
            UIActions.sendKey(xui.txt_pwd, pwd);
            UIActions.sendKey(xui.txt_repwd, repwd);
            UIActions.Click(xui.chk_agree);
     

        }

        [When(@"also enters (.*) (.*) (.*) (.*) (.*) (.*) (.*) (.*) and (.*)")]
        public void WhenAlsoEnters(string accnt, string phno, string add1, string city, string state, string zip, string ind, string weekly, string cnt)
        {
            Console.WriteLine("accnt:" + accnt);
            Console.WriteLine("phno:" + phno);
            Console.WriteLine("add1:" + add1);
            Console.WriteLine("city:" + city);
            Console.WriteLine("state:" + state);
            Console.WriteLine("zip:" + zip);
            Console.WriteLine("ind:" + ind);
            Console.WriteLine("weekly:" + weekly);
            Console.WriteLine("cnt:" + cnt);



            UIActions.sendKey(xui.txt_acct, accnt);
         
            UIActions.sendKey(xui.txt_phno, phno);
            UIActions.sendKey(xui.txt_addr, add1);
            UIActions.selectByVisibleText(xui.dropdn_cnt, cnt);
                UIActions.sendKey(xui.txt_city, city);
               UIActions.selectByValue(xui.dropdn_state, state);
                UIActions.sendKey(xui.txt_zip, zip);
               UIActions.selectByVisibleText(xui.dropdn_ind, ind);
             // UIActions.selectByVisibleText(xui.dropdn_weekly, weekly);
            UIActions.selectByValue(xui.dropdn_weekly, weekly);

        }


        [When(@"user clicks on create account button")]
        public void WhenUserClicksOnCreateAccountButton()
        {
            UIActions.Click(xui.btn_createaccount);
            UIActions.waitFor(520);
        }

        [Then(@"User is navigated to Xpo (.*)")]
        public void ThenUserIsNavigatedToXpo(string text)
        {
                        
            UIActions.waitFor(360);
            UIActions.elementExists(xui.thankyoutext);
            Assert.True(UIActions.ElementDisplayed(xui.thankyoutext));
            
        }

    }
}

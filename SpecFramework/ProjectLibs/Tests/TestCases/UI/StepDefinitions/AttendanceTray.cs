using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SpecFramework.ActionClasses;
using SpecFramework.Config;
using SpecFramework.GlobalParam;
using SpecFramework.Main.CommonUtils;
using SpecFramework.ProjectLibs.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFramework.ProjectLibs.Tests.TestCases.UI.StepDefinitions
{
    [Binding]
    public sealed class AttendanceTray
    {
        //Object to access the Tray app UI elements 
        TrayApp_UIElements trayUI = new TrayApp_UIElements();

        IWebDriver webdriver;
        INavigation navigate;
        WebDriverWait wait;

        string existingCurrentLeaves;
        string existingAnnualLeaves;
        string updatedCurrentLeaves;
        string updatedAnnualLeaves;

    [Given(@"Tray App opens up and user logs in with his wipfli credenditals")]
        public void GivenTrayAppOpensUpAndUserLogsInWithHisWipfliCredenditals()
        {

            //Initiating Chrome driver for Chromium Embedded Framework(CEF) Attendance Tray and launch the tray.exe
            UISetup traySetup = new UISetup();

            Thread.Sleep(5000);

            if (UIActions.findCountElements(trayUI.login_btn) != 0)
            {

                UIActions.elementExists(trayUI.login_btn);
                UIActions.Click(trayUI.login_btn);
                Thread.Sleep(3000);
                if (UIActions.IsElementPresent(trayUI.workaccount_img))
                {
                    UIActions.Click(trayUI.workaccount_img);
                }
                Thread.Sleep(3000);
                if (UIActions.IsElementPresent(trayUI.workaccount_element))
                {
                    UIActions.Click(trayUI.workaccount_element);
                }
                Thread.Sleep(3000);
                Thread.Sleep(6000);
                UIActions.elementExists(trayUI.password_txtbox);
                UIActions.sendKey(trayUI.password_txtbox, ObjectRepo.Config.GetWipfliPassword());
                UIActions.elementExists(trayUI.signin_btn);
                UIActions.Click(trayUI.signin_btn);
                Thread.Sleep(6000);
            }

        }

        //For Basic (InOffice, InOffice) scenario
        [When(@"the user marks the attendance as first half (.*) and second half (.*)")]
        public void WhenTheUserMarksTheAttendanceAsFirstHalfAndSecondHalf(string firstHalfElement, string secondHalfElement)
        {
            if (!String.IsNullOrEmpty(firstHalfElement))
            {
                UIActions.elementExists(By.Id(firstHalfElement));
                UIActions.Click(By.Id(firstHalfElement));
            }
             if (!String.IsNullOrEmpty(secondHalfElement))
            { 
                UIActions.elementExists(By.Id(secondHalfElement));
                UIActions.Click(By.Id(secondHalfElement));
            }

            UIActions.elementExists(trayUI.submit_attendancepage_btn);
            UIActions.Click(trayUI.submit_attendancepage_btn);
            Thread.Sleep(6000);

        }

        //For permutation and combinations of the scenarios
        [When(@"User marks the attendance as first half (.*) and second half (.*) and adds the comments (.*) and recipients (.*)")]
        public void WhenUserMarksTheAttendanceAsFirstHalfAndSecondHalfAndAddsTheCommentsAndRecipients(string firstHalfElement, string secondHalfElement, string comment, string recipient)
        {
                UIActions.elementExists(By.Id(firstHalfElement));
                UIActions.Click(By.Id(firstHalfElement));
                UIActions.elementExists(By.Id(secondHalfElement));
                UIActions.Click(By.Id(secondHalfElement));
                UIActions.elementExists(trayUI.comment_txtbox);
                UIActions.sendKey(trayUI.comment_txtbox, comment);
                UIActions.elementExists(trayUI.email_dropdwn);
                UIActions.Click(trayUI.email_dropdwn);
                Thread.Sleep(2000);
                //Select recipient email id
                int count = UIActions.findCountElements(By.XPath("//*[@id='selectEmail']//div[@role='option']"));
                for (int i=1;i<=count;i++)
                    {
                        string text = UIActions.findElementbylocator(By.XPath("(//*[@id='selectEmail']//div[@role='option'])[" + i + "]/div/span")).Text;
                        if ( text == recipient)
                            {
                                UIActions.Click(By.XPath("(//*[@id='selectEmail']//div[@role='option'])[" + i + "]/div/span"));
                                break;
                            } 
                    }
                 UIActions.elementExists(trayUI.submit_attendancepage_btn);
                 UIActions.Click(trayUI.submit_attendancepage_btn);
                 Thread.Sleep(6000);
        }

        [Then(@"his attendance should get marked/saved as (.*) and (.*)")]
        public void ThenHisAttendanceShouldGetMarkedSavedAsAnd(string firstHalfExpected, string secondHalfExpected)
        {
            List<IWebElement> labels = UIActions.findElements(trayUI.actualattendance_label);
            Assert.AreEqual(2, labels.Count);
            Assert.AreEqual(firstHalfExpected, labels.ElementAt(0).GetAttribute("ng-if"));
            Assert.AreEqual(secondHalfExpected, labels.ElementAt(1).GetAttribute("ng-if"));
            Thread.Sleep(1000);

            //Print Records and then Delete to execute the next test
            PrintAndDeleteDBRecords();
         }

        [Given(@"the Tray App opens up and user logs in with his wipfli credenditals to mark leave")]
        public void GivenTheTrayAppOpensUpAndUserLogsInWithHisWipfliCredenditalsToMarkLeave()
        {
            //Get the existing leave balance of the user from the website dashbaord 
            List<string> leaveBalance = GetDasboardExistingLeaveBalanceFromWebsite();
            existingCurrentLeaves = leaveBalance.ElementAt(0);
            existingAnnualLeaves = leaveBalance.ElementAt(1);
            Console.WriteLine(existingCurrentLeaves);
            Console.WriteLine(existingAnnualLeaves);

            //Mark whole day leave using the tray app
            UISetup traySetup = new UISetup();
            Thread.Sleep(5000);
            if (UIActions.findCountElements(trayUI.login_btn) != 0)
            {

                UIActions.elementExists(trayUI.login_btn);
                UIActions.Click(trayUI.login_btn);
                UIActions.elementExists(trayUI.workaccount_img);
                UIActions.Click(trayUI.workaccount_img);
                Thread.Sleep(3000);
                UIActions.elementExists(trayUI.workaccount_element);
                UIActions.Click(trayUI.workaccount_element);
                Thread.Sleep(6000);
                UIActions.elementExists(trayUI.password_txtbox);
                UIActions.sendKey(trayUI.password_txtbox, ObjectRepo.Config.GetWipfliPassword());
                UIActions.elementExists(trayUI.signin_btn);
                UIActions.Click(trayUI.signin_btn);
                Thread.Sleep(6000);
            }
        }

        [Then(@"users attendance should get marked/saved as (.*) and (.*) and leave balance should be calculated based on leave count (.*)")]
        public void ThenUsersAttendanceShouldGetMarkedSavedAsAndAndLeaveBalanceShouldBeCalculatedBasedOnLeaveCount(string firstHalfExpected, string secondHalfExpected, double leaveCount)
        {
            List<IWebElement> labels = UIActions.findElements(trayUI.actualattendance_label);
            Assert.AreEqual(2, labels.Count);
            
            Assert.AreEqual(firstHalfExpected, labels.ElementAt(0).GetAttribute("ng-if"));
            Assert.AreEqual(secondHalfExpected, labels.ElementAt(1).GetAttribute("ng-if"));
            Thread.Sleep(1000);

            navigate = webdriver.Navigate();
            navigate.Refresh();
            Thread.Sleep(3000);

            //Get the updated leave balance of the user from the website dashbaord after user marks his leave
            List<string> updatedLeaveBalance = GetDasboardUpdatedLeaveBalanceFromWebsite();
            updatedCurrentLeaves = updatedLeaveBalance.ElementAt(0);
            updatedAnnualLeaves = updatedLeaveBalance.ElementAt(1);
            Console.WriteLine(updatedCurrentLeaves);
            Console.WriteLine(updatedAnnualLeaves);

            VerifyCalculatedLeaveCount(leaveCount);

            //Print Records and then Delete to execute the next test
              PrintAndDeleteDBRecords();

            //Navigate to Leave History screen
            Actions action = new Actions(webdriver);
            action.MoveToElement(webdriver.FindElement(By.LinkText("History"))).Build().Perform();
            Thread.Sleep(2000);
            webdriver.FindElement(By.LinkText("Leave History")).Click();
            Thread.Sleep(3000);

            //Cancel the leaves applied for current day
            int rowCntLeaveTable = webdriver.FindElements(By.XPath("//*[@id='LeaveHistory']/table/tbody//tr")).Count;

            for (int i = 1; i <= rowCntLeaveTable; i++)
            {
                DateTime now = DateTime.Now;
                string currdate = now.ToString("dd/MMM/yyyy");
                Console.WriteLine("date = " + currdate);
                string applied = webdriver.FindElement(By.XPath("//*[@id='LeaveHistory']/table/tbody/tr[" + i + "]/td[2]")).Text;
                if (applied.Equals(currdate))
                {
                    Console.WriteLine("two date strings matched");
                    int cntCancelBtn = webdriver.FindElements(By.XPath("//*[@id='LeaveHistory']/table/tbody/tr[" + i + "]/td[11]//div/button")).Count;
                    if (cntCancelBtn == 1)
                    {
                        webdriver.FindElement(By.XPath("//*[@id='LeaveHistory']/table/tbody/tr[" + i + "]/td[11]//div/button")).Click();
                    }

                }

            }

            webdriver.Quit();

        }

        [Given(@"try")]
        public void GivenTry()
        {
            //Login to website using wipfli credentials (using chrome browser)
                       LoginToWebsite();

                       //Navigate to Leave History screen
                       Actions action = new Actions(webdriver);
                       action.MoveToElement(webdriver.FindElement(By.LinkText("History"))).Build().Perform();
                       Thread.Sleep(2000);
                       webdriver.FindElement(By.LinkText("Leave History")).Click();
                       Thread.Sleep(3000);

                       //Cancel the leaves applied for current day
                       int rowCntLeaveTable = webdriver.FindElements(By.XPath("//*[@id='LeaveHistory']/table/tbody//tr")).Count;

                       for (int i = 1; i <= rowCntLeaveTable; i++)
                       {
                           DateTime now = DateTime.Now;
                           string currdate = now.ToString("dd/MMM/yyyy");
                           Console.WriteLine("date = " +currdate);
                           string applied = webdriver.FindElement(By.XPath("//*[@id='LeaveHistory']/table/tbody/tr[" + i + "]/td[2]")).Text;
                           if (applied.Equals(currdate))
                           {
                               Console.WriteLine("two date strings matched");
                               int cntCancelBtn = webdriver.FindElements(By.XPath("//*[@id='LeaveHistory']/table/tbody/tr[" + i + "]/td[11]//div/button")).Count;
                               if (cntCancelBtn == 1)
                               {
                                   webdriver.FindElement(By.XPath("//*[@id='LeaveHistory']/table/tbody/tr[" + i + "]/td[11]//div/button")).Click();
                               }

                           }

                       }
        }

        [When(@"cancel leave")]
        public void WhenCancelLeave()
        {
            Console.WriteLine("When");
        }

        [Then(@"gets canceled")]
        public void ThenGetsCanceled()
        {
            Console.WriteLine("Then");
        }

        //Method to get the existing leave balance of the user from the website dashbaord 
        public List<string> GetDasboardExistingLeaveBalanceFromWebsite()
        {
            //Login to website using wipfli credentials (using chrome browser)
            LoginToWebsite();

            //Get the existing leave balance of the user from the website dashbaord before user marks his leave
            List<string> leave_Balance = new List<string>();
            string existing_CurrentLeaves = webdriver.FindElement(By.XPath("(//div[@id='Dashboard']//div[@class='DashboardNumber ng-binding'])[1]")).Text;
            leave_Balance.Add(existing_CurrentLeaves);             
            string existing_AnnualLeaves = webdriver.FindElement(By.XPath("(//div[@id='Dashboard']//div[@class='DashboardNumber ng-binding'])[2]")).Text;
            leave_Balance.Add(existing_AnnualLeaves);

            return leave_Balance;
        }

        //Method to login to website using wipfli credentials (using chrome browser)
        public void LoginToWebsite()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            webdriver = new ChromeDriver(chromeOptions);
            navigate = webdriver.Navigate();
            navigate.GoToUrl("https://lmsdev.azurewebsites.net/#/signin");

            wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.ElementExists(trayUI.login_btn));
            webdriver.FindElement(trayUI.login_btn).Click();
            wait.Until(ExpectedConditions.ElementExists(trayUI.username_txtbox));

            AppConfigReader appConfig = new AppConfigReader();

            webdriver.FindElement(trayUI.username_txtbox).SendKeys(appConfig.GetWipfliUsername());
            webdriver.FindElement(trayUI.username_txtbox).SendKeys(Keys.Tab);
            wait.Until(ExpectedConditions.ElementExists(trayUI.workaccount_element));
            webdriver.FindElement(trayUI.workaccount_element).Click();
            //wait.Until(ExpectedConditions.ElementExists(trayUI.workaccount_img));
            //webdriver.FindElement(trayUI.workaccount_img).Click();

            ////Handle Wipfli Authentication popup using AutoIt
            HandleWipfliAuthenticationPopopWithAutoIt();

            Thread.Sleep(6000);

            Console.WriteLine(webdriver.FindElements(trayUI.password_txtbox).Count);
            if (webdriver.FindElements(trayUI.password_txtbox).Count == 1)
            {
                webdriver.FindElement(trayUI.password_txtbox).SendKeys(appConfig.GetWipfliPassword());
            }
            if (webdriver.FindElements(trayUI.signin_btn).Count == 1)
            {
                webdriver.FindElement(trayUI.signin_btn).Click();
            }
            Thread.Sleep(5000);
        }

        //Method to get the updated leave balance of the user from the website dashbaord after user marks his leave
        public List<string> GetDasboardUpdatedLeaveBalanceFromWebsite()
        {
            List<string> updated_LeaveBalance = new List<string>();

            string updated_CurrentLeaves = webdriver.FindElement(By.XPath("(//div[@id='Dashboard']//div[@class='DashboardNumber ng-binding'])[1]")).Text;
            updated_LeaveBalance.Add(updated_CurrentLeaves);
            string updated_AnnualLeaves = webdriver.FindElement(By.XPath("(//div[@id='Dashboard']//div[@class='DashboardNumber ng-binding'])[2]")).Text;
            updated_LeaveBalance.Add(updated_AnnualLeaves);

            return updated_LeaveBalance;
        }

        //Assert the existing and calculated leave counts
        public void VerifyCalculatedLeaveCount(double leaveCount)
        {
            var expectedCurrentLeaves = Convert.ToDouble(existingCurrentLeaves) - leaveCount;
            Assert.AreEqual(Convert.ToString(expectedCurrentLeaves), updatedCurrentLeaves);

            var expectedAnnualLeaves = Convert.ToDouble(existingAnnualLeaves) + leaveCount;
            Assert.AreEqual(Convert.ToString(expectedAnnualLeaves), updatedAnnualLeaves);
        }

        //Method to enter wipfli credentials on Authentication popup using AutoIt
        public void HandleWipfliAuthenticationPopopWithAutoIt()
        {
            try
            {
               // Process processo = new System.Diagnostics.Process();
              //  processo.StartInfo.FileName = "AutoIt3.exe";
           //     processo.StartInfo.Arguments = @"/AutoIt3ExecuteScript D:\ChromiumSample\HandleAuthentication.au3";
                //processo.Start("D:\\ChromiumSample\\HandleAuthentication.exe");
                Process.Start("D:\\ChromiumSample\\HandleAuthentication.exe");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        //Method to Print Records and then Delete to execute the next test
        public void PrintAndDeleteDBRecords()
        {
            try
            {
                DBUtils db = new DBUtils();
                var result = db.FetchRecords("select * from EmployeeAttendance where EmployeeId=2232 and CONVERT(date, Date) = CONVERT(date, GETDATE())");

                foreach (var row in result)
                {
                    Console.Write("\n");
                    foreach (var keyvalue in row.Value)
                    {
                        Console.Write(keyvalue.Key + ": ");
                        Console.WriteLine(keyvalue.Value);

                    }
                }

                int count = db.ExecuteNonQuery("delete from EmployeeAttendance where EmployeeId=2232 and CONVERT(date, Date) = CONVERT(date, GETDATE())");
                Console.WriteLine($"deleted {count} records");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Assert.Fail("SQL exception occured and so test failed");
            }
        }
    }
}

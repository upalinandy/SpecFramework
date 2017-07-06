using NUnit.Framework;
using OpenQA.Selenium;
using SpecFramework.ActionClasses;
using SpecFramework.Main.CommonUtils;
using SpecFramework.ProjectLibs.UI;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using AutoItX3Lib;
using Yort.Ntp;

namespace SpecFramework.ProjectLibs.Tests.TestCases.UI.StepDefinitions
{
    [Binding]
    public class TraySmokeTestsSteps
    {
        TrayApp_UIElements trayUI = new TrayApp_UIElements();

        [When(@"the attendance is not already marked")]
        public void WhenTheAttendanceIsNotAlreadyMarked()
        {
            int attendanceCountForTheDay=-1;

            try
            {
                DBUtils db = new DBUtils();

                attendanceCountForTheDay = db.ExecuteNonQuery("delete from EmployeeAttendance where EmployeeId=2232 and CONVERT(date, CreatedOn) = CONVERT(date, GETDATE())");
                Console.WriteLine($"deleted {attendanceCountForTheDay} records");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Assert.Fail("SQL exception occured and so test failed");
            }

            Assert.AreEqual(attendanceCountForTheDay, 0);
        }

        [Then(@"the attendance screen should contain four buttons in first section and four in second section and a submit button")]
        public void ThenTheAttendanceScreenShouldContainFourButtonsInFirstSectionAndFourInSecondSectionAndASubmitButton()
        {
            List<IWebElement> attendanceLabels = UIActions.findElements(trayUI.attendancescreen_labels);
            Assert.AreEqual(8, attendanceLabels.Count);
            
            Assert.AreEqual(trayUI.firsthalfoffice_label, attendanceLabels.ElementAt(0).GetAttribute("id"));
            Assert.AreEqual(trayUI.firsthalfwfh_label, attendanceLabels.ElementAt(1).GetAttribute("id"));
            Assert.AreEqual(trayUI.firsthalftravel_label, attendanceLabels.ElementAt(2).GetAttribute("id"));
            Assert.AreEqual(trayUI.firsthalfleave_label, attendanceLabels.ElementAt(3).GetAttribute("id"));

            Assert.AreEqual(trayUI.secondhalfoffice_label, attendanceLabels.ElementAt(4).GetAttribute("id"));
            Assert.AreEqual(trayUI.secondhalfwfh_label, attendanceLabels.ElementAt(5).GetAttribute("id"));
            Assert.AreEqual(trayUI.secondhalftravel_label, attendanceLabels.ElementAt(6).GetAttribute("id"));
            Assert.AreEqual(trayUI.secondhalfleave_label, attendanceLabels.ElementAt(7).GetAttribute("id"));

            Assert.True(UIActions.ElementDisplayed(trayUI.submit_attendancepage_btn));
        }


        [When(@"user clicks the close icon without marking his attendance")]
        public void WhenUserClicksTheCloseIconWithoutMarkingHisAttendance()
        {
            UIActions.elementExists(trayUI.firsthalfinoffice_element);
            List<IWebElement> attendanceLabels = UIActions.findElements(trayUI.attendancescreen_labels);
            Assert.AreEqual(8, attendanceLabels.Count);

            Thread.Sleep(5000);
                try
                 {
                    AutoItX3 autoitHandle = new AutoItX3();
                    autoitHandle.WinActivate("AttendanceTrayApp");
                    Thread.Sleep(2000);
                    autoitHandle.WinClose("AttendanceTrayApp");
                    Thread.Sleep(8000);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

            Thread.Sleep(6000);
        }
            
        [Then(@"the tray app popup should get closed and sit in the system tray")]
        public void ThenTheTrayAppPopupShouldGetClosedAndSitInTheSystemTray()
        {
            AutoItX3 autoit = new AutoItX3();
        //    autoit.WinSetState("AttendanceTrayApp", "", autoit.SW_SHOW);
            Thread.Sleep(3000);
            int windowState = autoit.WinGetState("AttendanceTrayApp");
            Console.WriteLine(windowState);
            Thread.Sleep(5000);

            //State not 15(shown on the screen) and state=5(hidden)
            Assert.True(windowState != 15 && windowState == 5);
            Thread.Sleep(5000);

            //Also verify if process still exists
            Assert.True(IsProcessRunning("AttendanceTrayApp"));

            Console.WriteLine("success");
            Thread.Sleep(5000);

        }

        [When(@"the user marks the attendance as in office for the day")]
        public void WhenTheUserMarksTheAttendanceAsInOfficeForTheDay()
        {
                UIActions.elementExists(trayUI.firsthalfinoffice_element);
                UIActions.Click(trayUI.firsthalfinoffice_element);
                UIActions.elementExists(trayUI.secondhalfinoffice_element);
                UIActions.Click(trayUI.secondhalfinoffice_element);
                Thread.Sleep(3000);
        }


        [Then(@"comment and recipient fields should not be displayed\.")]
        public void ThenCommentAndRecipientFieldsShouldNotBeDisplayed_()
        {
            Assert.False(UIActions.ElementDisplayed(trayUI.comment_txtbox));
            Assert.False(UIActions.ElementDisplayed(trayUI.email_dropdwn));
        }

        [When(@"the attendance is not marked till Three PM for the day")]
        public void WhenTheAttendanceIsNotMarkedTillThreePMForTheDay()
        {
            Console.WriteLine("Execute this script after 3.10 PM IST without manually marking attendance for that day");   
        }

        [Then(@"AutoGenerated Leave is marked for the user for that day")]
        public void ThenAutoGeneratedLeaveIsMarkedForTheUserForThatDay()
        {
            const int expectedCurrentTimeHr = 15; //3.00 PM

            DateTime now = DateTime.Now;
            Console.WriteLine(now.Hour);
            Console.WriteLine(now.Minute);

            if (now.Hour >= expectedCurrentTimeHr)
            {
                if (now.Minute >= 10)
                {
                    try
            {
                        DBUtils db = new DBUtils();
                        var result = db.FetchRecords("select * from EmployeeAttendance where EmployeeId=2232 and CONVERT(date, Date) = CONVERT(date, GETDATE())");
                Console.WriteLine(result.Count);
                        Assert.AreEqual(result.Count, 1);
                        if (result.Count == 1)
                        {
                            Assert.AreEqual(result[1]["FirstHalf"], 4);
                            Assert.AreEqual(result[1]["SecondHalf"], 4);
                            Assert.AreEqual(result[1]["Comments"], "Auto-Marked");
                        }

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


        public bool IsProcessRunning(string expectedProcessName)
        {
            bool isRunning = false;
            Process[] processList = Process.GetProcesses();
            isRunning = processList.Any(k => k.ProcessName == expectedProcessName);
            return (isRunning);
        }
    }

}

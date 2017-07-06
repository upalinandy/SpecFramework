using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpecFramework.ProjectLibs.Tests.TestCases.UI.CEF
{
     public class AttendanceTray
    {
        public static void Main(string[] args)
        {
            TestMethod1();
        }
        public static void TestMethod1()
        {
            // Path to the ChromeDriver executable.

            // System.setProperty("webdriver.chrome.driver", "D:/ChromiumSample/chromedriver.exe");
                     Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"D:\ChromiumSample\chromedriver.exe");
                     // Path to the CEF executable.
                     //     ChromeOptions options = new ChromeOptions();
                     //    options.setBinary("D:/ChromiumSample/Attendance Tray App/AttendanceTrayApp.exe");
                     DesiredCapabilities capability = DesiredCapabilities.Chrome();
                     ChromeOptions options = new ChromeOptions();
                     options.BinaryLocation = @"D:\ChromiumSample\Attendance Tray App\AttendanceTrayApp.exe";
                     capability.SetCapability(ChromeOptions.Capability, options);
           


        }
    }
}
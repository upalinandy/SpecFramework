using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using SpecFramework.CommonUtils;
using SpecFramework.Config;
using SpecFramework.GlobalParam;
using SpecFramework.ProjectLibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.TestClasses
{
    public class LoginTest : UIElements
    {
        public LoginTest()
        {
            Console.WriteLine("Inside Test initialize");
            ObjectRepo.Config = new AppConfigReader();
            ObjectRepo.ds = new DriverSetup();
            ObjectRepo.driver = ObjectRepo.ds.InitDriver(ObjectRepo.driver, ObjectRepo.Config);
            ObjectRepo.wait = new WebDriverWait(ObjectRepo.driver, TimeSpan.FromSeconds(30));
            ObjectRepo.js = (IJavaScriptExecutor)ObjectRepo.driver;
         //   ObjectRepo.phantom = new PhantomJSDriver();
        //    ObjectRepo.pwait = new WebDriverWait(ObjectRepo.phantom, TimeSpan.FromSeconds(30));


        }
    }
}

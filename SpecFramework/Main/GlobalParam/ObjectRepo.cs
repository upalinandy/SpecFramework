using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using SpecFramework.CommonUtils;
using SpecFramework.Config.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.GlobalParam
{
    class ObjectRepo
    {
        public static Iconfig Config { get; set; }
        public static IWebDriver driver { get; set; }
        public static DriverSetup ds { get; set; }
        public static WebDriverWait wait { get; set; }
        public static IJavaScriptExecutor js { get; set; }
        public static PhantomJSDriver phantom { get; set; }
        public static WebDriverWait pwait { get; set; }
    }
}

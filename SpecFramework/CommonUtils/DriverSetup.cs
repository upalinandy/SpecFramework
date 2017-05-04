using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecFramework.Config.enumfolder;
using SpecFramework.Config.Interfaces;
using SpecFramework.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.CommonUtils
{
    class DriverSetup
    {
        private IWebDriver GetFirefoxDriver()
        {

            FirefoxBinary binary = new FirefoxBinary("C:\\Program Files\\Mozilla Firefox\\firefox.exe");
            var profile = new FirefoxProfile();
            IWebDriver driver = new FirefoxDriver(binary, profile);
            //   IWebDriver driver = new FirefoxDriver();
            return driver;
        }
        private IWebDriver GetChromeDriver()
        {
            IWebDriver driver = new ChromeDriver();
            return driver;
        }
        private IWebDriver GetIEDriver()
        {
            IWebDriver driver = new InternetExplorerDriver();
            return driver;
        }

        public IWebDriver InitDriver(IWebDriver driver, Iconfig Config)
        {
            Console.WriteLine("upali");
            Console.WriteLine("Inside InitDriver");
            Console.WriteLine("browser type:" + Config.GetBrowser());

            switch (Config.GetBrowser())
            {
                case BrowserType.Firefox:
                    driver = GetFirefoxDriver();
                    break;

                case BrowserType.Chrome:
                  //  driver = GetChromeDriver();
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case BrowserType.IExplorer:
                    driver = GetIEDriver();
                    break;

                default:
                    throw new NoDriverFound("Driver not found : " + Config.GetBrowser().ToString());
            }
            return driver;
        }

    }
}

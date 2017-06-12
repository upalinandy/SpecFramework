using SpecFramework.Config.configkeys;
using SpecFramework.Config.enumfolder;
using SpecFramework.Config.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Config
{
    public class AppConfigReader : Iconfig

    {
        public BrowserType GetBrowser()
        {
            Console.WriteLine("Now here");
            string browser = ConfigurationManager.AppSettings.Get(AppConfigKeys.browser);
            return (BrowserType)Enum.Parse(typeof(BrowserType), browser);
        }

        public string GetUrl()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.url);
        }

        public string GetDBConnectionString()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.dbconnection);
        }

        //Get the path of the Chromium Emebdded Framework Application (.exe file) 
        public string GetCEFAppPath()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.cefapppath);
        }


    }
}

using SpecFramework.Config.enumfolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Config.Interfaces
{
    interface Iconfig
    {
        BrowserType GetBrowser();
        string GetUrl();
        string GetDBConnectionString();
        string GetCEFAppPath();
    }
}

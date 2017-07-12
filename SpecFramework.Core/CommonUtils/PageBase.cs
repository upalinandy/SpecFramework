using OpenQA.Selenium;
using SpecFramework.Core.GlobalParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Core.CommonUtils
{
    public class PageBase
    {
        public static IWebDriver driver = ObjectRepo.driver;
    }
}
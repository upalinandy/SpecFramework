using OpenQA.Selenium.Support.UI;
using SpecFramework.Core.CommonUtils;
using SpecFramework.Core.Config;
using SpecFramework.Core.GlobalParam;
//using SpecFramework.ProjectLibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Core.CommonUtils
{
   public class UISetup //: UIElements
    {
        public UISetup()
        {
            Console.WriteLine("Inside Test Initialize");
            ObjectRepo.Config = new AppConfigReader();
            ObjectRepo.ds = new DriverSetup();
            ObjectRepo.driver = ObjectRepo.ds.InitDriver(ObjectRepo.driver, ObjectRepo.Config);
            ObjectRepo.wait = new WebDriverWait(ObjectRepo.driver, TimeSpan.FromSeconds(30));

        }
      }
    }

using OpenQA.Selenium;
using SpecFramework.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.UI
{
   public class UIElements :PageBase

    {
        public By signin = By.XPath(".//*[contains(text(),'Sign In')]");
        public By rider_signin = By.XPath(".//*[contains(text(),'Rider sign in')]");
        public By airbnb_login = By.XPath(".//*[contains(text(),'Log In')]");
        public By user = By.CssSelector("input[id='username']");
        public By pass = By.CssSelector("input[id='password']");
        public By loginbutton = By.XPath(".//*[@id='workspace-wrapper-signin']/div/div/div/div/div/div/div/div/div[2]/form/button");
        public By loginpage = By.XPath(".//*[contains(text(),'Login to your account')]");
        public By addconsignment = By.XPath(".//*[contains(text(),'Add consignment')]");
        public By workarea = By.XPath(".//*[contains(text(),'< Workarea >')]");
        public string checkusername = ".//*[contains(text(),'REPLACE | Sign Out')]";
        public By createconsignmenttag = By.XPath(".//*[contains(text(),'Create Consigment')]");
       public By becomeahost = By.XPath(".//*[contains(text(),'Become a Host')]");
   //     public By becomeahost = By.XPath(".//*[@id='site-content']/div/div/div[1]/div[2]/div[1]/header/div/div/div[3]/div/nav/ul/li[4]/div/div/a/div/div/span");

    }
}

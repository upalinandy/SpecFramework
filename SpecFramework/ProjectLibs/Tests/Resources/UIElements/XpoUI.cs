using OpenQA.Selenium;
using SpecFramework.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.UI
{
    public class XpoUI:UIElements

    {

     //   public By txt_fn = By.Id("firstname");
        // public By txt_fn = By.CssSelector("input[id = firstname]");
        public By txt_fn = By.XPath(".//*[@id='firstname']");
        public By txt_ln = By.Id("lastname");
        public By txt_jobtitle = By.Id("jobtitle");
        public By txt_email = By.Id("email");
        public By txt_reemail = By.Id("reenteremail");
        public By txt_pwd = By.Id("password");
        public By txt_repwd = By.Id("reenterpassword");
        public By chk_agree = By.Id("agreement");
        public By txt_acct = By.Id("accountname");
        public By txt_phno = By.Id("phonenumber");
        public By txt_addr = By.Id("address1");
        public By dropdn_cnt = By.Id("country");
        public By txt_city = By.Id("city");
        public By dropdn_state = By.Id("State");
        public By txt_zip = By.Id("zipcode");
        public By dropdn_ind = By.Id("industry");
        public By dropdn_weekly = By.Id("weeklyshipments");
        public By btn_createaccount  = By.XPath(".//*[@id='btnCreateAccount']/img");
        public By thankyoutext = By.XPath(".//*[contains(text(),'Thank you for registering to be a XPO Logistics account holder')]");



    }
}

using OpenQA.Selenium;
using SpecFramework.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.UI
{
   public class TrayApp_UIElements : PageBase

    {
        public By login_btn = By.Id("SignInAzureADLnk");
        public By username_txtbox = By.Id("cred_userid_inputtext");
        public By password_txtbox = By.Id("passwordInput");
        public By signin_btn = By.Id("submitButton");
        public By workaccount_element = By.XPath("//div[contains(text(), 'Work or school account')]");
        public By workaccount_img = By.XPath("//img[@alt='Work or school account symbol']");
        public By submit_attendancepage_btn = By.XPath("//button[@id='submit']");
        public By comment_txtbox = By.Id("form-comments");
        public By email_dropdwn = By.XPath("//*[@id='selectEmail']/div[1]/input");
        public By actualattendance_label = By.XPath("//div[@class='viewAttendance ng-scope']//div//label");
        public By attendancescreen_labels = By.XPath("//div[@class='editAttendance ng-scope']//div//label[@role='button']");
        public By firsthalfinoffice_element = By.Id("firstHalfInOffice");
        public By secondhalfinoffice_element = By.Id("secondHalfInOffice");
        public string firsthalfoffice_label = "firstHalfInOffice";
        public string firsthalfwfh_label = "firstHalfWfh";
        public string firsthalftravel_label = "firstHalfOfficial";
        public string firsthalfleave_label = "firstHalfLeave";
        public string secondhalfoffice_label = "secondHalfInOffice";
        public string secondhalfwfh_label = "secondHalfWfh";
        public string secondhalftravel_label = "secondHalfOfficial";
        public string secondhalfleave_label = "secondHalfLeave";
    }
}

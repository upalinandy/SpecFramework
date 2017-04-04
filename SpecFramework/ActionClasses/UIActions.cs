using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFramework.GlobalParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ActionClasses
{
    public class UIActions
    {
        public static void GoToUrl(string url)
        {
            ObjectRepo.driver.Navigate().GoToUrl(url);

        }
        
        public static void Click(By elem)
        {
            ObjectRepo.driver.FindElement(elem).Click();
        }

        public static string getTitle()
        {
            return ObjectRepo.driver.Title;
        }

        public static void sendKey(By elem, string data)
        {
            ObjectRepo.driver.FindElement(elem).SendKeys(data);
        }

        public static void elementExists(By elem)
        {
            ObjectRepo.wait.Until(ExpectedConditions.ElementExists(elem));
        }

        
        public static IWebElement findElement(string locator, string replacer)
        {
            return ObjectRepo.driver.FindElement(By.XPath(locator.Replace("REPLACE", ""+ replacer)));
        }

        public static Boolean ElementDisplayed(By elem)
        {
            return ObjectRepo.driver.FindElement(elem).Displayed;
        }

        public static string getUrl()
        {
            return ObjectRepo.driver.Url;
        }

     

        public static bool pageload()
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)ObjectRepo.driver;
           return (bool)je.ExecuteScript("return document.readyState").Equals("complete");
        }

        



    }



    }
    

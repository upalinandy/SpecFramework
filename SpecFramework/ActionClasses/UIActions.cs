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
            Console.WriteLine("element upali: " + elem);
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

        public static void javascriptclick(By elem)
        {
            IWebElement becomeahost = ObjectRepo.driver.FindElement(elem);
            IJavaScriptExecutor je = (IJavaScriptExecutor)ObjectRepo.driver;
            je.ExecuteScript("arguments[0].click();", becomeahost);
        }

        public static void selectByVisibleText(By elem, string data)
        {
            Console.WriteLine("Inside Select by visible text");
            Console.WriteLine("elem: " + elem);
            Console.WriteLine("data: " + data);

            ObjectRepo.driver.FindElement(elem);
            SelectElement dropdown = new SelectElement(ObjectRepo.driver.FindElement(elem));   
            dropdown.SelectByText(data);
            Console.WriteLine(dropdown);
        }

        public static void waitFor(int timeoutinseconds)
        {
            ObjectRepo.wait = new WebDriverWait(ObjectRepo.driver, TimeSpan.FromSeconds(timeoutinseconds));
        }

        public static void selectByValue(By elem, string data)
        {
            SelectElement dropdown = new SelectElement(ObjectRepo.driver.FindElement(elem));
            dropdown.SelectByValue(data);
        }




    }



    }
    

using SpecFramework.ActionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using SpecFramework.FeatureFilePath;
using SpecFramework.GlobalParam;
using SpecFramework.Jira.JiraBug;
using SpecFramework.Jira.JiraNewFeature;
using SpecFramework.Jira.JiraUserStory;
using SpecFramework.ProjectLibs.UI;
using OpenQA.Selenium;
using SpecFramework.Main.CommonUtils;

namespace SpecFramework.StepDefinitions
{
    [Binding]
    public sealed class SqlDbSteps
    {
         [Given(@"User connects to the DB")]
        public void GivenUserConnectsToTheDB()
        {
            Console.WriteLine("Establishing connection");
        }

        [When(@"Select query is executed")]
        public void WhenSelectQueryIsExecuted()
        {
            DBUtils db = new DBUtils();
            var result = db.FetchRecords("Select * from EmployeeAttendance where EmployeeId=4613");

            foreach (var row in result)
            {
                Console.Write("\n");
                foreach (var keyvalue in row.Value)
                {
                    Console.Write(keyvalue.Key + ": ");
                    Console.WriteLine(keyvalue.Value);

                }
            }
        }

        [Then(@"Result is returned and displayed")]
        public void ThenResultIsReturnedAndDisplayed()
        {
            Console.WriteLine("Success");
        }


    }
}

using Newtonsoft.Json;
using NUnit.Framework;
using SpecFrame.GoogleAPI;
using SpecFramework.FeatureFileAPIPath;
using SpecFramework.FeatureFilePath;
using SpecFramework.Jira.JiraApi;
using SpecFramework.Jira.JiraBug;
using SpecFramework.Jira.JiraNewFeature;
using SpecFramework.Jira.JiraUserStory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFrame.StepDefinitionFiles
{
    [Binding]
    public sealed class Api_Steps
    {
       
        NewFeatureCreate newfeature = new NewFeatureCreate();
        FeatureFileAPIBasePath featurePath = new FeatureFileAPIBasePath();
        JiraTicketKeyIssue key = new JiraTicketKeyIssue();
        JiraTimeStamp ts = new JiraTimeStamp();
        private string googleapiurl;
        private string response;
        private string timestamp;

        BugCreate bug = new BugCreate();
        AddJiraComment comment = new AddJiraComment();
        BugStatus bugstatus = new BugStatus();
    
        string exceptiontext = null;
        string bugsummary = null;
        bool bugcreateflag = false;
        BugState bugstate = new BugState();



        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("dd-MM-yyyy, HH:mm");
        }


        [Given(@"Google api that takes address and returns latitude and longitude")]
        public void GivenGoogleApiThatTakesAddressAndReturnsLatitudeAndLongitude()
        {
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            string featureFilePath = featurePath.GetFeatureFileAPIPath(featureName);
            string ProjFolderPath = Directory.GetCurrentDirectory();
            newfeature.NewFeatureCheckCreate(featureName, featureFilePath);
            googleapiurl = "http://maps.googleapis.com/maps/api/geocode/json?address=";
        }

        [When(@"The client Gets response by (.*)")]
        public void WhenTheClientGetsResponseBy(string address)
        {
            HttpClient cl = new HttpClient();

            StringBuilder sb = new StringBuilder();
            sb.Append(googleapiurl);
            sb.Append(address);
            Uri uri = new Uri(sb.ToString());
            response = cl.GetStringAsync(uri).Result;
         
            var test = response;
        }

        [Then(@"The (.*) and (.*) returned should be as expected")]
        public void ThenTheAndReturnedShouldBeAsExpected(string exp_lat, string exp_lng)
        {
            var root = JsonConvert.DeserializeObject<GoogleAPI.RootObject>(response);
            var location = root.results[0].geometry.location;
            var latitude = location.lat;
            var longitude = location.lng;
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            string scenarioname = ScenarioContext.Current.ScenarioInfo.Title;
            bugsummary = "Google api test does not give correct result";
            string featureFilePath = featurePath.GetFeatureFileAPIPath(featureName);
            timestamp = GetTimestamp(DateTime.Now);
            List<string> Text = File.ReadAllLines(featureFilePath).ToList();
            int index = Text.FindIndex(x => x.Contains(scenarioname));
            index = index - 1;
            string latestexecuttext = "";
            try
            {
                Console.WriteLine("inside try");
                Assert.AreEqual(location.lat.ToString(), exp_lat);
                Assert.AreEqual(location.lng.ToString(), exp_lng);
          
                latestexecuttext = "#Last Execution Passed on: "+timestamp;
           
            }
            catch (Exception ex)
            {
                bugcreateflag = true;    
                latestexecuttext = "#Last Execution Failed on: "+timestamp;
                exceptiontext = ex.ToString();
                throw ex;
            }
            finally
            {
                if (bugcreateflag)
                {
                    bugstate.nobugcreated = false;
                    bugstate.bugcreateflag = true;
                    bugstate =   bug.create(bugsummary, exceptiontext, bugstate);
                  key.getJiraTicketId(featureFilePath, bugsummary, scenarioname,bugstate);
                }
                else
                {
                    Console.WriteLine("Bug Closed and Test case passed Upali");
                    bugstate = bugstatus.check(featureFilePath,bugsummary, scenarioname, bugstate);
                 }

              comment.addComment(bugsummary, latestexecuttext);                
              ts.update(featureFilePath, bugsummary, scenarioname, latestexecuttext, bugcreateflag,bugstate); 
            }


        }
    }
}

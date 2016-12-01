using Newtonsoft.Json;
using NUnit.Framework;
using SpecFrame.GoogleAPI;
using SpecFramework.FeatureFilePath;
using SpecFramework.Jira.JiraBug;
using SpecFramework.Jira.JiraUserStory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFrame.StepDefinitionFiles
{
    [Binding]
    public sealed class Api_Steps
    {
        UserStoryCreate userStory = new UserStoryCreate();
        FeatureFileBasePath featurePath = new FeatureFileBasePath();
        private string googleapiurl;
        private string response;
        BugCreate bug = new BugCreate();
        string exceptiontext = null;
        string bugsummary = null;
        bool bugcreateflag = false;

        [Given(@"Google api that takes address and returns latitude and longitude")]
        public void GivenGoogleApiThatTakesAddressAndReturnsLatitudeAndLongitude()
        {
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            string featureFilePath = featurePath.GetFeatureFilePath(featureName);
            userStory.UserStoryCheckCreate(featureName, featureFilePath);
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
            //    root = JsonConvert.DeserializeObject<RootObject>(response);
            var test = response;
        }

        [Then(@"The (.*) and (.*) returned should be as expected")]
        public void ThenTheAndReturnedShouldBeAsExpected(string exp_lat, string exp_lng)
        {
            var root = JsonConvert.DeserializeObject<RootObject>(response);
            var location = root.results[0].geometry.location;
            var latitude = location.lat;
            var longitude = location.lng;
            try
            {
                Assert.AreEqual(location.lat.ToString(), exp_lat);
                Assert.AreEqual(location.lng.ToString(), exp_lng);
            }
            catch (Exception ex)
            {
                bugcreateflag = true;
                exceptiontext = ex.ToString();
                bugsummary = "Google api test does not give correct result";
                throw ex;
            }
            finally
            {
                if (bugcreateflag == true)
                {
                    bug.create(bugsummary, exceptiontext);
                }              
            }


        }
    }
}

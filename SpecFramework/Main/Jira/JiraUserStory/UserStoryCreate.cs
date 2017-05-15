using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SpecFramework.Jira.JiraApi;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Configuration;

namespace SpecFramework.Jira.JiraUserStory
{
    public class UserStoryCreate
    {
        //private string issueKey;
        public void UserStoryCheckCreate(string featureName, string featureFilePath)
        {
            List<string> results = new List<string>();
            //string issuenum = null;
            string fileName = featureFilePath;
            string tktID = null;
            string tkyKey = null;
            string JiraUserName = ConfigurationManager.AppSettings["username"].ToString();
            string JiraPassword = ConfigurationManager.AppSettings["password"].ToString();
            string systemUsr = Environment.UserName;
            string ProjFolderPath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();

            //Checking whether the user story already exists, if true, do not create a new ticket 
            HttpClient client1 = new HttpClient();
            string Apiurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=description&fields=summary");
            var credentials = Encoding.ASCII.GetBytes(JiraUserName + ":" + JiraPassword);
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(Apiurl.ToString());
            string ApiResponse = client1.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);
            var Sumry = root.issues.Count;

            //Checking if the user story already exists by iterating though the issue list in jira
            bool FeatureExists = false;
            var issues = root.issues;
            string[] textLinesFT = System.IO.File.ReadAllLines(fileName);

            //Finding the feature name from the feature file which will be the name of the ticket
            string FeatureKeyword = "Feature";
            string FeaureTRName = string.Empty;

            foreach (string line2 in textLinesFT)
            {
                if (line2.Contains(FeatureKeyword))
                {
                    results.Add(line2);
                    FeaureTRName = line2;
                }
            }
            //Trim the User Story name to display only the Feature name
            FeaureTRName = FeaureTRName.Replace("Feature: ", "");
            foreach (var item in issues)
            {
                var fields = item.fields;
                var summary = (fields.summary).ToString();
                if (summary.Equals(FeaureTRName))
                {
                    Console.WriteLine("User story already exists in the project");
                    FeatureExists = true;
                    tktID = item.id;
                    tkyKey = item.key;
                }

            }
            //Check if the scenario already exists in JIRA within the Feature
            bool ScenarioExists = false;
            //If the user story does not exist, create a new user story by using the POST method in JIRA via API
            if (FeatureExists == false)
            {
                string text = System.IO.File.ReadAllText(fileName);
                string searchKeyword = "Feature";
                string[] textLines = System.IO.File.ReadAllLines(fileName);
                string FTRName = string.Empty;
                foreach (string line in textLines)
                {
                    if (line.Contains(searchKeyword))
                    {
                        results.Add(line);
                        FTRName = line;
                    }
                }
                string DescText;
                int pFrom = text.IndexOf("@") + "@".Length;
                string reslt = text.Substring(pFrom);
                DescText = "@" + reslt;
                string SnrKeyword = "Scenario Outline:";
                string ScrName = string.Empty;
                foreach (string line in textLines)
                {
                    if (line.Contains(SnrKeyword))
                    {
                        results.Add(line);
                        ScrName = line;
                    }
                }
                //Trim the User Story name to display only the Feature name
                FTRName = FTRName.Replace("Feature: ", "");
                //The fields required to create a new JIRA ticket
                var data = new Issue();
                data.fields.project.key = "SFLOW";
                data.fields.summary = FTRName;
                // data.fields.description = reslt;
                //data.fields.description = text;
                data.fields.description = DescText;
                data.fields.issuetype.name = "User Story";

                string postUrl = "https://spiderlogic.jira.com/rest/api/latest/";
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new System.Uri(postUrl);
                byte[] cred = UTF8Encoding.UTF8.GetBytes(JiraUserName + ":" + JiraPassword);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                System.Net.Http.Formatting.MediaTypeFormatter jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
                System.Net.Http.HttpContent content = new System.Net.Http.ObjectContent<Issue>(data, jsonFormatter);
                System.Net.Http.HttpResponseMessage response = client.PostAsync("issue", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.Write(result);
                }
                else
                {
                    Console.Write(response.StatusCode.ToString());
                }
            }
            else
            {
                //This section is to update the jira ticket if a new scenario is added in the feature file
                if (FeatureExists == true && ScenarioExists == false)
                {
                    Console.Out.WriteLine("New scenario found and hence updating the ticket with the new scenario");
                    ////string descriptNew = System.IO.File.ReadAllText(@"C:\Users\subrahp\Documents\Visual Studio 2015\Projects\FlipkartExcelSpecRunCartTest\FlipkartExcelSpecRunCartTest\FlipkartExcelSpecrunCartTest.feature");
                    //string descriptNew = System.IO.File.ReadAllText(fileName);
                    //string descriptNew2 = descriptNew.Replace(';', ' ').Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' ').Replace("\n\n", " ");
                    ////Get the id from the existing user story to update the description and post with the updated description
                    //var PostURL2 = "?jql=project=SFLOW&fields=description&fields=summary";
                    //var POSTRequest = "https://spiderlogic.jira.com/rest/api/2/issue/";
                    //var request = WebRequest.Create(POSTRequest + tkyKey + PostURL2);
                    //request.Method = "PUT";
                    //request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(JiraUserName + ":" + JiraPassword)));
                    //request.ContentType = "application/json";
                    //var json = string.Format("{{ \"update\": {{\"description\": [{{\"set\":\"{0}\"}}] }} }};", descriptNew2);
                    //request.GetRequestStream().Write(Encoding.ASCII.GetBytes(json), 0, json.Length);
                    //var response = request.GetResponse();
                    //var reader = new StreamReader(response.GetResponseStream());
                    //var output = reader.ReadToEnd();


                }
            }

        }
    }
}

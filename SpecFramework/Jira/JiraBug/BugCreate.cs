using Newtonsoft.Json;
using SpecFramework.Jira.JiraApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraBug
{
    public class BugCreate
    {
        public void create(string bugsummary, string errordetails)
        {
            string tktID = null;
            string tkyKey = null;
            //Checking whether the user story already exists, if true, do not create a new ticket 
            HttpClient client2 = new HttpClient();
            string issueurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issues&fields=summary");

            var credentials = Encoding.ASCII.GetBytes("psubrahmanya:Gonikoppal@1234");
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(issueurl.ToString());
            string ApiResponse = client2.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);

            var Sumry = root.issues.Count;

            //Checking if the user story already exists by iterating though the issue list in jira
            bool bugexists = false;
            var issues = root.issues;

            foreach (var issue in issues)
            {
                var fields = issue.fields;
                Console.WriteLine("fields:" + fields);
                var summary = (fields.summary).ToString();
                if (summary.Equals(bugsummary))
                {
                    Console.WriteLine("User story already exists in the project");
                    bugexists = true;
                    tktID = issue.id;
                    tkyKey = issue.key;
                }

            }

            //If the user story does not exist, create a new user story by using the POST method in JIRA via API
            if (bugexists == false)
            {
                //The fields required to create a new JIRA ticket
                var data = new Issue();

                data.fields.project.key = "SFLOW";
                data.fields.summary = bugsummary;
                data.fields.description = errordetails;
                data.fields.issuetype.name = "Bug";

                string postUrl = "https://spiderlogic.jira.com/rest/api/latest/";
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new System.Uri(postUrl);
                byte[] cred = UTF8Encoding.UTF8.GetBytes("psubrahmanya:Gonikoppal@1234");
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
                    Console.ReadLine();
                }

            }
        }

    }
}

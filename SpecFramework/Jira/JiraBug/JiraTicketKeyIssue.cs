using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFramework.Jira.JiraApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraBug
{
    public class JiraTicketKeyIssue
    {
        string tktID = null;
        string tktkey = null;
        public void getJiraTicketId(string featurpath, string bugSummary, string scenarioName)
        {
            HttpClient client1 = new HttpClient();

            string Apiurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issuetype&fields=summary&fields=description");

            var credentials = Encoding.ASCII.GetBytes("psubrahmanya:Gonikoppal@1234");
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(Apiurl.ToString());
            string ApiResponse = client1.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);
            var issues = root.issues;

            foreach (var item in issues)
            {
                var summary = (item.fields.summary).ToString();
                if (item.fields.issuetype.name == "Bug" & item.fields.summary == bugSummary)
                {
                    tktID = item.id;
                    tktkey = item.key;
                    break;
                }

            }


            List<string> Text = File.ReadAllLines(featurpath).ToList();
            string keyToInsert = "#" + tktkey;
            string trimmedText = keyToInsert.Remove(7);
            if (Text.Contains(keyToInsert))
            {
                Console.WriteLine("Key already exists");
            }
            else
            {
                int length = scenarioName.Length;
                int index = Text.FindIndex(x => x.Contains(scenarioName));
                index = index + 1;
                string a = Text[index];
                if (a.Contains(trimmedText))
                {
                    Text.Remove(a);
                    Text.Insert(index, keyToInsert);
                    //Text[index].Replace(a, keyToInsert);
                    System.IO.File.WriteAllLines(featurpath, Text);
                }
                else
                {
                    Text.Insert(index, keyToInsert);

                    System.IO.File.WriteAllLines(featurpath, Text);
                }
            }

        }

        public void DeleteJiraTicketId(string featurpath, string bugSummary, string scenarioName)
        {
            HttpClient client = new HttpClient();
            string Apiurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issuetype&fields=summary&fields=description");

            var credentials = Encoding.ASCII.GetBytes("psubrahmanya:Gonikoppal@1234");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(Apiurl.ToString());
            string ApiResponse = client.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);
            var issues = root.issues;

            foreach (var item in issues)
            {
                var summary = (item.fields.summary).ToString();
                if (item.fields.issuetype.name == "Bug" & item.fields.summary == bugSummary)
                {
                    tktID = item.id;
                    tktkey = item.key;
                    break;
                }

            }
            HttpClient client1 = new HttpClient();
            string postUrl = "https://spiderlogic.jira.com/rest/api/2/issue/" + tktkey;

            client1.BaseAddress = new System.Uri(postUrl);
            byte[] cred = UTF8Encoding.UTF8.GetBytes("psubrahmanya:Gonikoppal@1234");
            client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));

            System.Net.Http.HttpResponseMessage response = client1.DeleteAsync(postUrl).Result;

            List<string> Text = File.ReadAllLines(featurpath).ToList();

            string trimmedText = "#SFLOW-";


            int length = scenarioName.Length;
            int index = Text.FindIndex(x => x.Contains(scenarioName));
            index = index + 1;
            string a = Text[index];
            if (a.Contains(trimmedText))
            {
                Text.Remove(a);
                System.IO.File.WriteAllLines(featurpath, Text);
            }
            else
            {
                Console.WriteLine("Already deleted");
            }


        }

    }
}


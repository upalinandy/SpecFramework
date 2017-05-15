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
    public class AddJiraComment
    {
        public void addComment(string bugsummary,string commenttext)
        {
            string tktID = null;
            string tkyKey = null;
            string state = "";
            string issuetype = "";

            //Check if the issue exists
            HttpClient client2 = new HttpClient();
            string issueurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issues&fields=summary&fields=description&fields=status&fields=project&fields=issuetype");

            var credentials = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(issueurl.ToString());
            string ApiResponse = client2.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);

            Console.WriteLine("Inside Add Comment");
            //Checking if the issue exists by iterating through the issue list in jira
            var issues = root.issues;

            foreach (var issue in issues)
            {
                var fields = issue.fields;
                var summary = (fields.summary).ToString();
                state = (fields.status.name).ToString();
                issuetype = (fields.issuetype.name).ToString();

                if (issuetype == "Bug")
                {
                    Console.WriteLine("Issue Type" +issuetype);
                    if (summary.Equals(bugsummary))
                    {
                        Console.WriteLine("Issue exists in the project");
                        Console.WriteLine("state :" + state);
                        if (state == "Open")
                        {
                            tktID = issue.id;
                            tkyKey = issue.key;

                            var data = new Comment();

                            data.body = commenttext;

                            string postUrl = "https://spiderlogic.jira.com/rest/api/2/issue/" + tktID + "/comment";
                            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                            client.BaseAddress = new System.Uri(postUrl);
                            byte[] cred = UTF8Encoding.UTF8.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            System.Net.Http.Formatting.MediaTypeFormatter jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
                            System.Net.Http.HttpContent content = new System.Net.Http.ObjectContent<Comment>(data, jsonFormatter);
                            System.Net.Http.HttpResponseMessage response = client.PostAsync("comment", content).Result;

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

                            break;
                        }

                    }

                }
            }
        }

    }
}

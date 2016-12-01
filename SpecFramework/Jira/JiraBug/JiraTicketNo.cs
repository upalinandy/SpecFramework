using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SpecFramework.Jira.JiraApi;
using Newtonsoft.Json;

namespace SpecFramework.Jira.JiraBug
{
   public  class JiraTicketNo
    {
        string tktID = null;
        string tktkey = null;
        public void getJiraTicketId(string featurpath, string bugSummary)
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


            string text = File.ReadAllText(featurpath);
            text = text.Replace("TicketID", tktkey);
            File.WriteAllText(featurpath, text);

            //string text = File.ReadAllText(featurpath);
            // string insertPoint = "Scenario Outline:";
            //// int count = insertPoint.Length + 1;

            // int index = text.IndexOf(insertPoint) +insertPoint.Length;
            // text = text.Insert(index, tktkey);
            // File.WriteAllText(featurpath, text);
        }
    }
}

using Newtonsoft.Json;
using SpecFramework.Jira.JiraApi;
using SpecFramework.Jira.JiraChangeLogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraBug
{
    public class FetchBugCreationResolutionDate
    {
        public void fetchBugCreatedClosedDate(string bugsummary)
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

            //Checking if the issue exists by iterating through the issue list in jira
            var issues = root.issues;

            foreach (var issue in issues)
            {
                var fields = issue.fields;
                Console.WriteLine("fields:" + fields);
                var summary = (fields.summary).ToString();
                state = (fields.status.name).ToString();
                issuetype = (fields.issuetype.name).ToString();

                if (issuetype == "Bug")
                {
                    Console.WriteLine("Issue Type" + issuetype);
                    if (summary.Equals(bugsummary))
                    {
                        Console.WriteLine("Issue exists in the project");
                        Console.WriteLine("state :" + state);
  //                      if (state == "Open")
   //                     {
                            tktID = issue.id;
                            //            tkyKey = issue.key;
                            tkyKey = "SFLOW-522";

                            //Get the created and closed datetime of the issue using jira changelog api
                            HttpClient client3 = new HttpClient();
                            string issueurl_datetime = ("https://spiderlogic.jira.com/rest/api/2/issue/" + tkyKey + "?expand=changelog");
    
                            var credentials1 = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                            client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials1));
                            Uri uri_datetime = new Uri(issueurl_datetime.ToString());
                            string ApiResponse_datetime = client3.GetStringAsync(uri_datetime).Result;
                            var root_changelog = JsonConvert.DeserializeObject<RootObject2>(ApiResponse_datetime);
      
                            var issueCreatedtimestamp = root_changelog.fields2.created;
                            Console.WriteLine("Rasika Created date: " + issueCreatedtimestamp);
                            var issueClosedtimestamp = root_changelog.fields2.resolutiondate;
                            Console.WriteLine("Rasika Closed date: " + issueClosedtimestamp);

                            break;
     //                   }

                    }

                }
            }
        }

    }
}

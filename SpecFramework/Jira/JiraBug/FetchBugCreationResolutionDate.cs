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
        public BugState fetchBugCreatedClosedDate(BugState bg)
        {
                string tkyKey = null;
                //Get the created and closed datetime of the issue using jira changelog api
         if (bg.openedafterclosedflag)
             {
                    tkyKey = bg.reopentktkey;
                    Console.WriteLine("rasika reopen:" + tkyKey);
                HttpClient client3 = new HttpClient();
                string issueurl_datetime = ("https://spiderlogic.jira.com/rest/api/2/issue/" + tkyKey + "?expand=changelog");

                var credentials1 = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials1));
                Uri uri_datetime = new Uri(issueurl_datetime.ToString());
                string ApiResponse_datetime = client3.GetStringAsync(uri_datetime).Result;
                var root_changelog = JsonConvert.DeserializeObject<RootObject2>(ApiResponse_datetime);

                var issueCreatedtimestamp = root_changelog.fields2.created;
                 bg.bugcreationdate = issueCreatedtimestamp;
 
            }

   if (bg.bugopen)
            {
                {
                    tkyKey = bg.newopentktkey;
                    Console.WriteLine("rasika new open:" + tkyKey);
                    HttpClient client3 = new HttpClient();
                    string issueurl_datetime = ("https://spiderlogic.jira.com/rest/api/2/issue/" + tkyKey + "?expand=changelog");

                    var credentials1 = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                    client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials1));
                    Uri uri_datetime = new Uri(issueurl_datetime.ToString());
                    string ApiResponse_datetime = client3.GetStringAsync(uri_datetime).Result;
                    var root_changelog = JsonConvert.DeserializeObject<RootObject2>(ApiResponse_datetime);

                    var issueCreatedtimestamp = root_changelog.fields2.created;
                    bg.bugcreationdate = issueCreatedtimestamp;

                }
            }
   if (bg.closedflag)
             {
                    tkyKey = bg.closedtkyKey;
                    Console.WriteLine("rasika fetchBugCreatedClosedDate closed:" + tkyKey);
                HttpClient client3 = new HttpClient();
                string issueurl_datetime = ("https://spiderlogic.jira.com/rest/api/2/issue/" + tkyKey + "?expand=changelog");

                var credentials1 = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials1));
                Uri uri_datetime = new Uri(issueurl_datetime.ToString());
                string ApiResponse_datetime = client3.GetStringAsync(uri_datetime).Result;
                var root_changelog = JsonConvert.DeserializeObject<RootObject2>(ApiResponse_datetime);

                var issueClosedtimestamp = root_changelog.fields2.resolutiondate;
                 bg.bugcloseddate = issueClosedtimestamp;


            }
            else
             {
                    tkyKey = bg.newopentktkey;
                    Console.WriteLine("rasika new open:" + tkyKey);
                HttpClient client3 = new HttpClient();
                string issueurl_datetime = ("https://spiderlogic.jira.com/rest/api/2/issue/" + tkyKey + "?expand=changelog");

                var credentials1 = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials1));
                Uri uri_datetime = new Uri(issueurl_datetime.ToString());
                string ApiResponse_datetime = client3.GetStringAsync(uri_datetime).Result;
                var root_changelog = JsonConvert.DeserializeObject<RootObject2>(ApiResponse_datetime);

                var issueCreatedtimestamp = root_changelog.fields2.created;
                 bg.bugcreationdate = issueCreatedtimestamp;
  
            }

            return (bg);

        }

    }
}

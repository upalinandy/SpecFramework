using Newtonsoft.Json;
using SpecFramework.Jira.JiraApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraBug
{
    public class BugCreate
    {
        public BugState create(string bugsummary, string errordetails, BugState bg)
        {
            string tktID = null;
            string tkyKey = null;
            string state = "";
            
            string issuetype = "";
            string closedtktID = null;
            string closedtkyKey = null;        
           
            bg.bugclosed = false;
            bg.bugexists = false;
            bg.bugopen = false;

            //Checking whether the Bug already exists, if true, do not create a new ticket 
            HttpClient client2 = new HttpClient();

            string issueurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issues&fields=summary&fields=description&fields=status&fields=project&fields=issuetype");

            var credentials = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(issueurl.ToString());
            string ApiResponse = client2.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);

            var Sumry = root.issues.Count;

            //Checking if the user story already exists by iterating though the issue list in jira
           
            var issues = root.issues;
            Debug.WriteLine("Upali Debug: ");

            foreach (var issue in issues)
            {
                var fields = issue.fields;
                
                 var summary = (fields.summary).ToString();
                state = (fields.status.name).ToString();
                issuetype = (fields.issuetype.name).ToString();

       
           if ((issuetype == "Bug") & summary.Equals(bugsummary))
               {
                       
                  if (state == "Open")
                      {
                            // the control may not go to if block ever
                         if (bg.bugclosed)
                            {
                               Console.WriteLine("In BugCreate: Bug OPened after closed ");
                               bg.bugexists = true;
                               Console.WriteLine("Ticket Key: " + issue.key);
                            }
                          else
                            {
                                Console.WriteLine("In BugCreate : Bug exists");
                                bg.bugexists = true;
                                tktID = issue.id;
                                tkyKey = issue.key;
                                Console.WriteLine("Ticket Key: " + issue.key);
                                bg.bugopen = true;
                            }
                        }

                  else if (state == "Closed")
                        {
                            if (bg.bugopen)
                            {
                                Console.WriteLine("In BugCreate: Bug closed and ALSO REOPENED");
                                bg.bugexists = true;
                                bg.bugclosed = true;
                                bg.bugclosedcount = bg.bugclosedcount + 1;
                                bg.buglist.Add(closedtkyKey);

                            }
                            else
                            {
                                Console.WriteLine("In BugCreate: Bug exists bug closed ");
                                bg.bugexists = false;
                                closedtktID = issue.id;
                                closedtkyKey = issue.key;                  
                                bg.bugclosed = true;
                                bg.closedtkyKey = issue.key;
                                bg.bugclosedcount = bg.bugclosedcount + 1;
                                bg.buglist.Add(closedtkyKey);
                            }
                        }
                    }              
             
            }

            //If the Bug not exist, create a new user story by using the POST method in JIRA via API
            if (bg.bugexists == false)
            {
                //The fields required to create a new JIRA ticket
                var data = new CreateIssue();

                data.fields.project.key = "SFLOW";
                data.fields.summary = bugsummary;
                data.fields.description = errordetails;
                data.fields.issuetype.name = "Bug";
        
                string postUrl = "https://spiderlogic.jira.com/rest/api/latest/";
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new System.Uri(postUrl);
                byte[] cred = UTF8Encoding.UTF8.GetBytes("rdoshi@spiderlogic.com:spiderqa");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                System.Net.Http.Formatting.MediaTypeFormatter jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
                System.Net.Http.HttpContent content = new System.Net.Http.ObjectContent<CreateIssue>(data, jsonFormatter);
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

            return bg; 
        }

    }
}

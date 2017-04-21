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
        string closedtktID = null;
        string closedtktkey = null;
        string opentktID = null;
        string opentktkey = null;
        bool closedflag;
        bool openedafterclosedflag;
        string keyToInsert = "";
        string trimmedText = "";
        List<string> Text = null;
        string summary = "";
   
        public void getJiraTicketId(string featurpath, string bugSummary, string scenarioName, BugState bg)
        {
            Console.WriteLine("Bazooka : Entereed JIRA TICKET ID");
            HttpClient client1 = new HttpClient();

            string Apiurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issuetype&fields=summary&fields=description&fields=status");

            var credentials = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            Uri uri = new Uri(Apiurl.ToString());
            string ApiResponse = client1.GetStringAsync(uri).Result;
            var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);
            var issues = root.issues;

            foreach (var item in issues)
            {
                summary = (item.fields.summary).ToString();
                Console.WriteLine("in Jiraticket: status " + item.fields.status.name);

                if (item.fields.issuetype.name == "Bug")
                 {
                    if (item.fields.summary == bugSummary & item.fields.status.name == "Closed")
                     {
                        Console.WriteLine("This is the most important step : In Jiraticket Closed");
                        closedflag = true;
                        closedtktID = item.id;
                        closedtktkey = item.key;
                        break;
                     }
                   else if (item.fields.summary == bugSummary & item.fields.status.name == "Open")
                    {
                        if (bg.bugclosed)
                        {
                            Console.WriteLine("Bazooka : In Jiraticket BUG CLOSED AND REOPENED");
                            //continue;
                            //testing
                            opentktID = item.id;
                            opentktkey = item.key;
                            openedafterclosedflag = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Bazooka : In Jiraticket DIRECTLY into OPen");
                            tktID = item.id;
                            tktkey = item.key;
                            break;
                        }
                     }                  
                   }
                else
                 {
                    continue;
                 }
            }

            //if closed ticket workflow
            if (closedflag)
            {
                Console.WriteLine("Bazooka : In Jiraticket if closedflag");
                foreach (var item in issues)
                {
                    Console.WriteLine("In closed and in for loop");
                    if (item.fields.issuetype.name == "Bug")
                    {
                      if (item.fields.summary == bugSummary & item.fields.status.name == "Open")
                          {
                            Console.WriteLine("Bazooka : In Jiraticket if closedflag and now in open");
                  
                            opentktID = item.id;
                            opentktkey = item.key;
                            openedafterclosedflag = true;
                            break;
                          }
                      }
                    else
                    {
                        continue;
                    }
                }

            }



//Writing into the Feature file
      if (closedflag)
            {
                Console.WriteLine("Bazooka : In Jiraticket if closedflag writing intofeature");
                Console.WriteLine("Upali ClosedKey: " + closedtktkey);

                Text = File.ReadAllLines(featurpath).ToList();
                keyToInsert = "#" + closedtktkey + "  Closed ";
                trimmedText = keyToInsert.Remove(7);

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

      if (openedafterclosedflag)
            {
                Console.WriteLine("Bazooka : In Jiraticket if openedafterclosed writing intofeature");
                Text = File.ReadAllLines(featurpath).ToList();
                keyToInsert = "#" + opentktkey + " Opened ";
                Console.WriteLine("Text: " + Text);
                Console.WriteLine("keyToInsert: " + keyToInsert);
                trimmedText = keyToInsert.Remove(10);
                Console.WriteLine("trimmedText: " + trimmedText);
                if (Text.Contains(keyToInsert))
                {
                    Console.WriteLine("Key already exists");
                }
                else
                {
                    int length = scenarioName.Length;
                    int index = Text.FindIndex(x => x.Contains(scenarioName));
                    index = index + 2;
                    string a = Text[index];
                    if (a.Contains(trimmedText))
                    {
                        Text.Remove(a);
                        Text.Insert(index, keyToInsert);
                        System.IO.File.WriteAllLines(featurpath, Text);
                    }
                    else
                    {
                        Text.Insert(index, keyToInsert);
                        System.IO.File.WriteAllLines(featurpath, Text);
                    }
                }
            }
      else {

            Console.WriteLine("Bazooka : In Jiraticket if opened new writing intofeature");

            Text = File.ReadAllLines(featurpath).ToList();
            keyToInsert = "#" + tktkey +" Opened";
                Console.WriteLine("Text: " + Text);
                Console.WriteLine("keyToInsert: " + keyToInsert);
                trimmedText = keyToInsert.Remove(7);               
                Console.WriteLine("trimmedText: " + trimmedText);
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


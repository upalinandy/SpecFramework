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
        private string timestamp;

        public void getJiraTicketId(string featurpath, string bugSummary, string scenarioName, BugState bg)
        {
            Console.WriteLine("Bazooka : Entered JIRA TICKET ID");

            timestamp = DateTime.Now.ToString("dd-MM-yyyy, HH:mm");

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
 
                if (item.fields.issuetype.name == "Bug")
                {
                    if (item.fields.summary == bugSummary & item.fields.status.name == "Closed")
                    {
                        Console.WriteLine("This is the most important step : In Jiraticket Closed");
                        closedflag = true;
                        closedtktID = item.id;
                        closedtktkey = item.key;
                        bg.closedtkyKey = item.key;
                        bg.closedflag = true;
                        break;
                    }
                    else if (item.fields.summary == bugSummary & item.fields.status.name == "Open")
                    {
                        if (bg.bugclosed)
                        {
                            Console.WriteLine("Bazooka : In Jiraticket BUG CLOSED AND REOPENED");
                            opentktID = item.id;
                            opentktkey = item.key;
                            openedafterclosedflag = true;
                            bg.reopentktkey = item.key;
                            bg.openedafterclosedflag = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Bazooka : In Jiraticket DIRECTLY into OPen");
                            tktID = item.id;
                            tktkey = item.key;
                            bg.newopentktkey = item.key;
                            break;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            //if closed ticket workflow when a new bug ticket is created
            if (closedflag && (bg.nobugcreated == false))
            {
                Console.WriteLine("Bazooka : In Jiraticket if closedflag");
                Console.WriteLine("check the nobugcreated flag: " + bg.nobugcreated);
                foreach (var item in issues)
                {
                    
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

            //Get the creation/closed date of the bug
            FetchBugCreationResolutionDate bc = new FetchBugCreationResolutionDate();
  
  //Writing into the Feature file
   if (openedafterclosedflag)
            {
                Console.WriteLine("Bazooka : In Jiraticket if openedafterclosed writing intofeature");

                bg = bc.fetchBugCreatedClosedDate(bg);
                Text = File.ReadAllLines(featurpath).ToList();
                keyToInsert = "#" + opentktkey + " Opened on: " + bg.bugcreationdate;
                Console.WriteLine("Text: " + Text);
                Console.WriteLine("keyToInsert: " + keyToInsert);
                bg.reopentktkey = opentktkey;
                trimmedText = keyToInsert.Remove(10);
                Console.WriteLine("trimmedText: " + trimmedText);
                if (Text.Contains(keyToInsert))
                {
                    Console.WriteLine("Key already exists");
                }
                else
                {
                    Console.WriteLine("Key does not exist and will write into the feature file");
                    int length = scenarioName.Length;
                    int index = Text.FindIndex(x => x.Contains(scenarioName));
                    index = index + 1;

                    string a = Text[index];
                    Console.WriteLine("a contains: " + a);
                    Console.WriteLine("trimmedtext contains: " + trimmedText);
                    Console.WriteLine("index +1: " + index);
                    Text.RemoveAt(index);
                    Text.Insert(index, keyToInsert);
                    System.IO.File.WriteAllLines(featurpath, Text);

                }
            }

   if (closedflag)
            {
                Console.WriteLine("Bazooka : In Jiraticket if closedflag writing intofeature");
                Console.WriteLine("Upali ClosedKey: " + closedtktkey);

                bg = bc.fetchBugCreatedClosedDate(bg);
                Text = File.ReadAllLines(featurpath).ToList();
                keyToInsert = "#" + closedtktkey + " Closed on: " + bg.bugcloseddate;
                trimmedText = keyToInsert.Remove(7);

                int length = scenarioName.Length;
                int index = Text.FindIndex(x => x.Contains(scenarioName));
                Console.WriteLine("Index: " + index);
                index = index + 2;
                string a = Text[index];
                Console.WriteLine("Index +2: " + index);
                Console.WriteLine("In JIRA KEY TICKET, under closed Flag: a contains" + a);
                Console.WriteLine("In JIRA KEY TICKET, under closed Flag: Trimmed Text contains :" + trimmedText);
                if (a.Contains(trimmedText))
                {
                    if (bg.bugclosedcount == 0)
                    {
                        Console.WriteLine("Do Nothing");
                    }
             
                    //april 26 test
                    else if (bg.bugclosedcount >= 1)
                    {
                        if (bg.nobugcreated)
                        {
                            Console.WriteLine("Inside nobugcreated");
                            int newindex = index - 1;
                            string b = Text[newindex];
                            if (b.Contains("Opened"))
                            {
                                Console.WriteLine("Inside nobugcreated and if b contains Opened");
                                Console.WriteLine("Key to insert: " + keyToInsert);
                                Text.Remove(b);
                                Text.Insert(newindex, keyToInsert);
                                System.IO.File.WriteAllLines(featurpath, Text);
                            }

                        }
                        else
                        {
                            Console.WriteLine("old closed bug :" + bg.buglist[bg.bugclosedcount-1]);
                            Console.WriteLine("inside if a.contains(trimmedtext) and if bugcount > 1");

                            string closedbug = Text[index];
                            if (closedbug.Contains(closedtktkey))
                            {
                                Console.WriteLine("Do nothing");
                            }
                            else
                            {
                                Text.Insert(index, keyToInsert);
                                System.IO.File.WriteAllLines(featurpath, Text);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("inside else bugcount >1");

                        if (a.Contains(bg.buglist[0]))
                        {
                            Console.WriteLine("in if a.contains closed bug");
                            Text.Remove(a);
                            Text.Insert(index, keyToInsert);
                        }
                        else
                        {
                            Console.WriteLine("in else a.contains closed bug");
                            Text.Insert(index, keyToInsert);
                        }
                        System.IO.File.WriteAllLines(featurpath, Text);
                    }
                }
                else
                {
                    Console.WriteLine("inside else a.contains(trimmedtext)");
                    Text.Insert(index, keyToInsert);
                    System.IO.File.WriteAllLines(featurpath, Text);
                }
            }

  else if (bg.nobugcreated)
            {
                Console.WriteLine("When the test case passes and no bug is created,Do Nothing");
            }

  else
          {
              Console.WriteLine("Bazooka : In Jiraticket if opened new writing intofeature");
              Text = File.ReadAllLines(featurpath).ToList();
                bg = bc.fetchBugCreatedClosedDate(bg);
                keyToInsert = "#" + tktkey + " Opened on: " + bg.bugcreationdate;
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


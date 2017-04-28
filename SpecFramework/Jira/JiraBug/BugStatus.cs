using Newtonsoft.Json;
using SpecFramework.Jira.JiraApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpecFramework.Jira.JiraBug
{
   public class BugStatus
    {
     public BugState check(string featurpath,string bugsummary, string scenarioName, BugState bg)
      {
        string state = "";
        string issuetype = "";
        string closedtktID = null;
        string closedtktkey = null;
        bg.bugclosed = false;
        bg.bugexists = false;
        bg.bugopen = false;
        bool closedflag = false;
        List<string> Text = null;
        string keyToInsert = "";
        string trimmedText = "";
        string timestamp;
        timestamp = DateTime.Now.ToString("dd-MM-yyyy, HH:mm");

        HttpClient client2 = new HttpClient();
        string issueurl = ("https://spiderlogic.jira.com/rest/api/2/search?jql=project=SFLOW&fields=issues&fields=summary&fields=description&fields=status&fields=project&fields=issuetype");
        var credentials = Encoding.ASCII.GetBytes("rdoshi@spiderlogic.com:spiderqa");
        client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
        Uri uri = new Uri(issueurl.ToString());
        string ApiResponse = client2.GetStringAsync(uri).Result;
        var root = JsonConvert.DeserializeObject<RootObject>(ApiResponse);

        var Sumry = root.issues.Count;
      //Checking if the Bug already exists by iterating though the issue list in jira
        var issues = root.issues;

      foreach (var issue in issues)
      {
        var fields = issue.fields;
        var summary = (fields.summary).ToString();
        state = (fields.status.name).ToString();
        issuetype = (fields.issuetype.name).ToString();
        Console.WriteLine("issuetype: " + issuetype);
        Console.WriteLine("bugsummary: " + bugsummary);
        Console.WriteLine("State: " + state);


                if ((issuetype == "Bug") && summary.Equals(bugsummary) && (state == "Open"))
                {
                    //control may never come to this block
                    if (bg.bugclosed)
                    {
                        Console.WriteLine("Bazooka : In BugStatus BUG CLOSED AND REOPENED");
                        bg.openedafterclosedflag = true;
                        bg.reopentktkey = issue.key;
                        bg.openedafterclosedflag = true;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("In BugStatus : Bug exists");
                        bg.bugexists = true;
                        bg.newopentktkey = issue.key;
                        Console.WriteLine("Ticket Key: " + issue.key);
                        bg.bugopen = true;
                       // bg.openedafterclosedflag = true;
                    }
                }
                else if ((issuetype == "Bug") && summary.Equals(bugsummary) && (state == "Closed"))
                {
                 
                        Console.WriteLine("In BugStatus: Bug exists bug closed ");
                        Console.WriteLine("In Bug Status:This is the most important step ");
                        closedflag = true;
                        bg.bugexists = false;
                        closedtktID = issue.id;
                        closedtktkey = issue.key;
                        bg.bugclosed = true;
                        bg.nobugcreated = true;
                        bg.bugclosedcount = bg.bugclosedcount + 1;
                        bg.buglist.Add(closedtktkey);
                    
                }    
         }



  //Writing into the Feature file
            if (bg.openedafterclosedflag)
            {
                Console.WriteLine("Bazooka : In BugStatus if openedafterclosed writing intofeature");
                Console.WriteLine("bugclosed :" + bg.bugclosed);
                Console.WriteLine("bugexists :" + bg.bugexists);
                Console.WriteLine("bugopen :" + bg.bugopen);
                Console.WriteLine("nobugcreated :" + bg.nobugcreated);
                Console.WriteLine("reopentktkey :" + bg.reopentktkey);
                Console.WriteLine("newopentktkey :" + bg.newopentktkey);
                Console.WriteLine("bugclosedcount :" + bg.bugclosedcount);

                Text = File.ReadAllLines(featurpath).ToList();
                keyToInsert = "#" + bg.reopentktkey + " Opened " + timestamp;
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
                Console.WriteLine("bugclosed :" + bg.bugclosed);
                Console.WriteLine("bugexists :" + bg.bugexists);
                Console.WriteLine("bugopen :" + bg.bugopen);
                Console.WriteLine("nobugcreated :" + bg.nobugcreated);
                Console.WriteLine("reopentktkey :" + bg.reopentktkey);
                Console.WriteLine("newopentktkey :" + bg.newopentktkey);
                Console.WriteLine("bugclosedcount :" + bg.bugclosedcount);
                //april 27 testing
                string newclosedkey = bg.buglist[bg.bugclosedcount-1];

                Text = File.ReadAllLines(featurpath).ToList();

                //april 27
                keyToInsert = "#" + newclosedkey + " Closed " + timestamp;
                trimmedText = keyToInsert.Remove(7);

                int length = scenarioName.Length;
                int index = Text.FindIndex(x => x.Contains(scenarioName));
                //april 26
                // index = index + 2;
               int newindex = index + 1;
               string a = Text[newindex];
               Console.WriteLine("In JIRA KEY TICKET, under closed Flag: a contains" + a);
               Console.WriteLine("In JIRA KEY TICKET, under closed Flag: Trimmed Text contains :" + trimmedText);
                if (a.Contains(trimmedText))
                {
                    if (bg.bugclosedcount == 0)
                    {
                        Console.WriteLine("Do Nothing");
                    }

                    //april 26 test
                    else if ((bg.bugclosedcount >= 1))
                    {
                       if (bg.nobugcreated)
                        {
                          Console.WriteLine("Inside nobugcreated");
                            if ((a.Contains("Opened")) && (a.Contains(bg.newopentktkey)))
                            {
                                int closeindex = newindex + 1;
                                Text.RemoveAt(closeindex);
                                Text.Insert(closeindex, keyToInsert);

                            }
                            else 
                            {
                                Console.WriteLine("Inside nobugcreated and if b contains Opened");
                                Console.WriteLine("Key to insert: " + keyToInsert);
                                Text.Remove(a);
                                //    Text.RemoveAt(newindex);
                                Text.Insert(newindex, keyToInsert);
                                System.IO.File.WriteAllLines(featurpath, Text);
                            }
                        }
                        else
                        {
                            Console.WriteLine("old closed bug :" + bg.buglist[1]);
                            Console.WriteLine("inside if a.contains(trimmedtext) and if bugcount > 1");
                            Text.Insert(index, keyToInsert);
                            System.IO.File.WriteAllLines(featurpath, Text);
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

            return bg;
        }
    }
}

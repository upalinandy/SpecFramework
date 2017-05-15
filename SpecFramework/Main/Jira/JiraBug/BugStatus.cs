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
            Console.WriteLine("bg.closed: " + bg.closedflag);
            Console.WriteLine("closedflag : " + closedflag);
            Console.WriteLine("bg.open : " + bg.bugopen);

   foreach (var issue in issues)
      {
        var fields = issue.fields;
        var summary = (fields.summary).ToString();
        state = (fields.status.name).ToString();
        issuetype = (fields.issuetype.name).ToString();

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
                    }
                }

           else if ((issuetype == "Bug") && summary.Equals(bugsummary) && (state == "Closed"))
                   {

                           Console.WriteLine("In BugStatus: Bug exists bug closed ");
                           Console.WriteLine("In Bug Status:This is the most important step ");
                           closedflag = true;
                           bg.closedflag = true;
                           bg.bugexists = false;
                           closedtktID = issue.id;
                           closedtktkey = issue.key;
                           bg.closedtkyKey = issue.key;
                           bg.bugclosed = true;
                           bg.nobugcreated = true;
                           bg.bugclosedcount = bg.bugclosedcount + 1;
                           bg.buglist.Add(closedtktkey);                   
                   }
            }

            Console.WriteLine("closedflag: " + closedflag);
            Console.WriteLine("bg.closedflag: " + bg.closedflag);
            Console.WriteLine("closedtktkey: " + closedtktkey);
            Console.WriteLine("bg.bugopen: " + bg.bugopen);
            Console.WriteLine("bg.newopentktkey: " + bg.newopentktkey);

  


            //Get the creation/closed date of the bug
            FetchBugCreationResolutionDate bc = new FetchBugCreationResolutionDate();

            //Writing into the Feature file
            if (bg.openedafterclosedflag)
            {
                Console.WriteLine("Bazooka : In BugStatus if openedafterclosed writing intofeature");

                bg = bc.fetchBugCreatedClosedDate(bg);
                Text = File.ReadAllLines(featurpath).ToList();
                keyToInsert = "#" + bg.reopentktkey + " Opened on: " + bg.bugcreationdate;
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

         if (bg.bugopen)
            {
                Console.WriteLine("Bazooka : In BugStatus if bug already exists writing intofeature");

                Text = File.ReadAllLines(featurpath).ToList();
                bg = bc.fetchBugCreatedClosedDate(bg);
                keyToInsert = "#" + bg.newopentktkey + " Opened on: " + bg.bugcreationdate;
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

         if (closedflag)
            {
                Console.WriteLine("In Bugstatus: if closedflag writing intofeature");
                Console.WriteLine("Upali bugclosed: " + bg.bugclosedcount);
                string newclosedkey = bg.buglist[bg.bugclosedcount-1];
                bg.closedtkyKey = newclosedkey;
                Console.WriteLine("bg.closedtkyKey" + bg.closedtkyKey);
               Text = File.ReadAllLines(featurpath).ToList();
                bg = bc.fetchBugCreatedClosedDate(bg);
                keyToInsert = "#" + newclosedkey + " Closed on: " + bg.bugcloseddate;
                Console.WriteLine(" keyToInsert" + keyToInsert);
                trimmedText = keyToInsert.Remove(7);

                int length = scenarioName.Length;
                int index = Text.FindIndex(x => x.Contains(scenarioName));
               int newindex = index + 1;
               string a = Text[newindex];
                Console.WriteLine("a contains: " + a);
                Console.WriteLine("trimmedText contains: " + trimmedText);
                Console.WriteLine("bg.nobugcreated" + bg.nobugcreated);

                if (a.Contains(trimmedText))
                {
                    if (bg.bugclosedcount == 0)
                    {
                        Console.WriteLine("Do Nothing");
                    }

                    else if ((bg.bugclosedcount >= 1))
                    {
                      if (bg.nobugcreated)
                        {
                        if (a.Contains("Opened"))
                            {
                                Console.WriteLine("Inside Opened");
                                if (a.Contains(bg.newopentktkey))
                                    {
                                    Console.WriteLine("inside a contains bg.newopenkey");
                                      int closeindex = newindex + 1;
                                    Console.WriteLine("newindex: " + newindex);
                                    Console.WriteLine("closeindex: " + closeindex);

                                 //   Text.RemoveAt(closeindex);
                                    Text.Insert(closeindex, keyToInsert);
                                    System.IO.File.WriteAllLines(featurpath, Text);
                                }
                                else
                                   {
                                    Console.WriteLine("Inside nobugcreated and if a contains Opened, else newopentktkey");
                                    // when there are only closed keys 2nd may
                                    string updatedclosedkey = bg.buglist[0];
                                    bg.closedtkyKey = updatedclosedkey;
                                    bg = bc.fetchBugCreatedClosedDate(bg);
                                    keyToInsert = "#" + updatedclosedkey + " Closed on: " + bg.bugcloseddate;
                                    Console.WriteLine("Key to insert: " + keyToInsert);
                                   // Text.Remove(a);
                                      Text.RemoveAt(newindex);
                                    Text.Insert(newindex, keyToInsert);
                                    System.IO.File.WriteAllLines(featurpath, Text);
                                   }
                               }
                        else 
                             {
                                Console.WriteLine("Inside nobugcreated and else a contains Opened");
                                // when there are only closed keys 2nd may
                                string updatedclosedkey = bg.buglist[0];
                                bg.closedtkyKey = updatedclosedkey;
                                bg = bc.fetchBugCreatedClosedDate(bg);
                                keyToInsert = "#" + updatedclosedkey + " Closed on: " + bg.bugcloseddate;
                                Console.WriteLine("Key to insert: " + keyToInsert);
                               // Text.Remove(a);
                                Text.RemoveAt(newindex);
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
                    Console.WriteLine("index : " + index);
                    Console.WriteLine("newindex : " + newindex);
                    Text.Insert(newindex, keyToInsert);
                    System.IO.File.WriteAllLines(featurpath, Text);
                }
            }

        

            return bg;
        }
    }
}

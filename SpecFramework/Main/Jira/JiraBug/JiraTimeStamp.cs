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
    public class JiraTimeStamp
    {

        public void update(string featurpath, string bugSummary, string scenarioName, string lastex, bool bugcreateflag, BugState bg)
        {
            List<string> Text = File.ReadAllLines(featurpath).ToList();

            Console.WriteLine("Now Begins JIRA TIME STAMP");
            int length = scenarioName.Length;
            int index = Text.FindIndex(x => x.Contains(scenarioName));
            Console.WriteLine("firstindex: " + index);

              index = index + 2;
            Console.WriteLine("index + 2: " + index);
            Console.WriteLine("Then Text at index + 2 :" + Text[index]);
            
            string a = Text[index];
            Console.WriteLine("a: " + a+ " index: "+ index);
            if (a.Contains("#SFLOW"))
            {
                Console.WriteLine("now inside #SFLOW :" + index);
                Console.WriteLine("bugclosed :" + bg.bugclosed);
                Console.WriteLine("bugexists :" + bg.bugexists);
                Console.WriteLine("bugopen :" + bg.bugopen);
                Console.WriteLine("nobugcreated :" + bg.nobugcreated);
                Console.WriteLine("reopentktkey :" + bg.reopentktkey);
                Console.WriteLine("newopentktkey :" + bg.newopentktkey);
                Console.WriteLine("openedafterclosedflag :" + bg.openedafterclosedflag);
                Console.WriteLine("bugclosedcount :" + bg.bugclosedcount);
                int newindex = index + 1;
                Console.WriteLine("new index: " + newindex);
                string b = Text[newindex];
                Console.WriteLine("b contains: " + b);
             if (bg.bugclosedcount > 1)
                {
                 if (bg.nobugcreated)
                    {
                        Console.WriteLine("Inside no bug is created");
                        Console.WriteLine("JIra timestamp, under if sflow, if bugcount > 1, if nobugcreated");
                        //april 27
                        int indexofscenarioname = index - 2;
                        //open and closed tickets are there and the test case passes/fails
                        if ((bg.bugexists) || (bg.openedafterclosedflag || bg.bugopen))
                        {
                            int passfailindex = indexofscenarioname + 1 + bg.bugclosedcount + 1;
                            Console.WriteLine("indexofscenarioname :" + indexofscenarioname);
                            Console.WriteLine("bugcount :" + bg.bugclosedcount);
                            Console.WriteLine("passfailindex :" + passfailindex);
                            Console.WriteLine("Text at passfailindex : " + Text[passfailindex]);
                            Console.WriteLine("Text at: " + newindex + "is" + b);
                            Text.RemoveAt(passfailindex);
                            Text.Insert((passfailindex), lastex);
                        }
                        //only closed tickets are there and the test case passes/fails
                        else
                        {
                            int passfailindex = indexofscenarioname + 0 + bg.bugclosedcount + 1;
                            Console.WriteLine("indexofscenarioname :" + indexofscenarioname);
                            Console.WriteLine("bugcount :" + bg.bugclosedcount);
                            Console.WriteLine("passfailindex :" + passfailindex);
                            Console.WriteLine("Text at passfailindex : " + Text[passfailindex]);
                            Console.WriteLine("Text at: " + newindex + "is" + b);
                            Text.RemoveAt(passfailindex);
                            Text.Insert((passfailindex), lastex);

                        }
                    }
                else
                    {
                        Console.WriteLine("old closed bug :" + bg.buglist[1]);
                        if (b.Contains(bg.reopentktkey))
                        {
                            Console.WriteLine("newindex +1 : " + (newindex + 1));
                            Console.WriteLine("lastex : " + lastex);
                            Console.WriteLine("JIra timestamp, under if sflow, if bugcount >1, if b.contains reopenkey");
                            Text.Insert((newindex + 1), lastex);
                        }
                        else
                        {
                            Console.WriteLine("Inside ELSE no bug is created");
                            int indexofscenarioname = index - 2;
                            int passfailindex = indexofscenarioname + 1 + bg.bugclosedcount + 1;
                            Console.WriteLine("indexofscenarioname :" + indexofscenarioname);
                            Console.WriteLine("bugcount :" + bg.bugclosedcount);
                            Console.WriteLine("passfailindex :" + passfailindex);
                            Console.WriteLine("Text at passfailindex : " + Text[passfailindex]);
                            Console.WriteLine("Text at: " + newindex + "is" + b);
                            Text.RemoveAt(passfailindex);
                            Text.Insert((passfailindex), lastex);

                        }
                    }
                }
                else
                {
                    if (b.Contains("Given"))
                    {
                        Console.WriteLine("JIra timestamp, under if sflow, else bugcount > 1 and b contains Closed");
                        Console.WriteLine("Text at: " + newindex + "is" + b);
                        //may 11
                      //  Text.RemoveAt(newindex);
                        Text.Insert((newindex), lastex);
                    }
                    else
                    {
                        Console.WriteLine("JIra timestamp, under if sflow, else bugcount > 1");
                        Console.WriteLine("Text at: " + newindex + "is" + b);
                        Text.RemoveAt(newindex);
                        Text.Insert((newindex), lastex);
                    }
                }
            }

           else if (a.Contains("Given"))
            {
                int newindex = index - 1;
                string b = Text[newindex];
                Console.WriteLine("b contains: " + b);
                if (b.Contains("Passed"))
                {
                  Text.RemoveAt(newindex);
                  Text.Insert(newindex, lastex);
                }
                else
                {
                  Console.WriteLine("bugcreateflag :" + bugcreateflag);
                  Console.WriteLine("now inside given :" + index);
                  Text.Insert(index, lastex);
                }
               
            }
            else if (a.Contains("When"))
            {
                Console.WriteLine("now inside else if:" + index);
             
                Text.Insert((index-1), lastex);
            }
            else 
            {
                Console.WriteLine("now inside else:" +index);
                Text.RemoveAt(index);
                Text.Insert(index, lastex);
            }
                
                    System.IO.File.WriteAllLines(featurpath, Text);
                               
            }

        }

  

        }

 


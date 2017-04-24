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

        public void update(string featurpath, string bugSummary, string scenarioName, string lastex, bool bugcreateflag)
        {
            List<string> Text = File.ReadAllLines(featurpath).ToList();
            //    string keyToInsert = "#" + tktkey;
            //    string trimmedText = keyToInsert.Remove(7);
            /*     if (Text.Contains(keyToInsert))
                 {
                     Console.WriteLine("Key already exists");
                 }*/
            //    else
            //    {
            Console.WriteLine("Now Begins JIRA TIME STAMP");
            int length = scenarioName.Length;
            int index = Text.FindIndex(x => x.Contains(scenarioName));
            Console.WriteLine("firstindex: " + index);

              index = index + 2;
            Console.WriteLine("index + 2: " + index);
            Console.WriteLine("Then Text at index + 2 :" + Text[index]);
            
            string a = Text[index];
            Console.WriteLine("a: " + a+ "index"+ index);
            if (a.Contains("#SFLOW"))
            {
                Console.WriteLine("now inside #SFLOW :" + index);
                //Text.Remove(a);
                Console.WriteLine("Text at index+1 : " + Text[index + 1]);
                Text.RemoveAt(index + 1);
                Text.Insert((index+1), lastex);
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
                else {
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

 


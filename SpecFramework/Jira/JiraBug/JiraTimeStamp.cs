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

        public void update(string featurpath, string bugSummary, string scenarioName, string lastex, bool bugcreate, string lastexecflag)
        {
            /*
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
            */

            List<string> Text = File.ReadAllLines(featurpath).ToList();
            //    string keyToInsert = "#" + tktkey;
            //    string trimmedText = keyToInsert.Remove(7);
            /*     if (Text.Contains(keyToInsert))
                 {
                     Console.WriteLine("Key already exists");
                 }*/
            //    else
            //    {
            Console.WriteLine("JiraTime stamp here");
            int length = scenarioName.Length;
            int index = Text.FindIndex(x => x.Contains(scenarioName));

            if (bugcreate)
            { 
              index = index + 2;
            }
            else
            {
             index = index +1;
            }
                string a = Text[index];
           
                    Text.Insert(index, lastex);
          
                    System.IO.File.WriteAllLines(featurpath, Text);
                               
            }

        }

  

        }

 


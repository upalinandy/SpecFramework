using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraApi
{
    public class Fields
    {
        [JsonProperty("summary")]
        public string summary { get; set; }
        public IssueType issuetype { get; set; }
        public string description { get; set; }        
        public Project project { get; set; }
        public Status status { get; set; }


        public Fields()
        {
            project = new Project();
            issuetype = new IssueType();
            status = new Status();


        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraApi
{
   public class NewFields
    {
        [JsonProperty("summary")]
        public string summary { get; set; }
        public IssueType issuetype { get; set; }
        public string description { get; set; }
        public Project project { get; set; }



        public NewFields()
        {
            project = new Project();
            issuetype = new IssueType();
 


        }
    }
}

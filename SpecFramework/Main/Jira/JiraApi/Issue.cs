using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraApi
{
    public class Issue
    {
        [JsonProperty("expand")]
        public string expand { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("self")]
        public string self { get; set; }

        [JsonProperty("key")]
        public string key { get; set; }

        [JsonProperty("fields")]
        public Fields fields
        {
            get; set;
        }
        public Issue()
        {
            fields = new Fields();
        }
    }
}

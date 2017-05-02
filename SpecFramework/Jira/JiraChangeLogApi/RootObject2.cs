using Newtonsoft.Json;
using SpecFramework.JiraChangeLogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraChangeLogApi
{
    class RootObject2
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
        public Fields2 fields2 { get; set; }

        public RootObject2()
        {
            fields2 = new Fields2();
        }
        
    }
}

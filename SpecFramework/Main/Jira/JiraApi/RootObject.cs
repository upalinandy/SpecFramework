using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraApi
{
    class RootObject
    {
        [JsonProperty("expand")]
        public string expand { get; set; }

        [JsonProperty("startAt")]
        public int startAt { get; set; }

        [JsonProperty("maxResults")]
        public int maxResults { get; set; }

        [JsonProperty("total")]
        public int total { get; set; }

        [JsonProperty("issues")]
        public List<Issue> issues { get; set; }

    }
}

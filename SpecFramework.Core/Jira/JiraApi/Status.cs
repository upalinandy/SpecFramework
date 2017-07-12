using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Core.Jira.JiraApi
{
   public class Status
    {
        [JsonProperty("name")]
        public string name { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.JiraChangeLogApi
{
    public class Fields2
    {
        [JsonProperty("created")]
        public string created { get; set; }

        [JsonProperty("resolutiondate")]
        public string resolutiondate { get; set; }

    }
}

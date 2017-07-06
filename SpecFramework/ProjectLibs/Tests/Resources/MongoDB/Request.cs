using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.Tests.Resources.MongoDB
{
    public class Request
    {
        public int _id { get; set; }
        public string createdBy { get; set; }
        public string status { get; set; }
        public long createdOn { get; set; }
        public Creator creator { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public Claim[] claims { get; set; }
        public Statuschangehistory[] statusChangeHistory { get; set; }
        public long submittedOn { get; set; }
        public long approvedOn { get; set; }
        public int bankFileId { get; set; }
    }
}

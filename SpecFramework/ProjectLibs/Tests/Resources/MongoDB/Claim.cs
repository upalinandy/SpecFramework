using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.Tests.Resources.MongoDB
{
   public class Claim
    {
        public string type { get; set; }
        public int id { get; set; }
        public long createdOn { get; set; }
        public int billForMonth { get; set; }
        public float claimedAmount { get; set; }
        public long billDate { get; set; }
        public long billEndDate { get; set; }
        public long billStartDate { get; set; }
        public int billForYear { get; set; }
        public Attachment[] attachments { get; set; }
        public string status { get; set; }
        public float approvedAmount { get; set; }
    }
}

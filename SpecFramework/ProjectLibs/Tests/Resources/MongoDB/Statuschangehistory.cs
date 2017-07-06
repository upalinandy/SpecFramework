using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.Tests.Resources.MongoDB
{
    public class Statuschangehistory
    {
        public string oldStatus { get; set; }
        public string newStatus { get; set; }
        public string createdBy { get; set; }
        public long createdOn { get; set; }
    }
}

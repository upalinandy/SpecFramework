using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.ProjectLibs.Tests.Resources.MongoDB
{
    public class Attachment
    {
        public string fileName { get; set; }
        public string path { get; set; }
        public string type { get; set; }
        public int sizeInBytes { get; set; }
        public string uploadedBy { get; set; }
        public long uploadedOn { get; set; }
    }
}

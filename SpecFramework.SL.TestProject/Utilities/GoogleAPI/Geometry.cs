using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.SL.TestProject.Utilities.GoogleAPI
{
    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }
}

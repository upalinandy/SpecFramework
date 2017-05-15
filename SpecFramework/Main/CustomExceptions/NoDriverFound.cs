using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.CustomExceptions
{
   public class NoDriverFound : Exception
    {
        public NoDriverFound(string msg) : base(msg)
        {
            Console.WriteLine("exception");
        }
    }
}

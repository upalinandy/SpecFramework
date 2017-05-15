using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Jira.JiraApi
{
    public class BugState
    {
        public bool bugclosed = false;
        public bool bugexists = false;
        public bool bugopen = false;
        public bool nobugcreated = true;
      
        public string closedtkyKey = "";
        public string reopentktkey = "";
        public string newopentktkey = "nonewkey";      
        public bool openedafterclosedflag = false;
        public bool closedflag = false;
        public int bugclosedcount = 0;
        public List<string> buglist = new List<string>();
        public bool bugcreateflag = false;
        public string bugcreationdate = "";
        public string bugcloseddate = "";
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.FeatureFilePath
{
    public class FeatureFileBasePath
    {
        public string GetFeatureFilePath(string featureName)
        {
            string ProjFolderPath = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
            string pathForFeature = "\\SpecFramework\\FeatureFiles";
            string filename = featureName + ".feature";
            string[] filePaths = Directory.GetFiles(ProjFolderPath + pathForFeature, filename, SearchOption.AllDirectories);
            string featurePath = filePaths.First();
            return featurePath;
        }
    }
}

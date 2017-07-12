using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SpecFramework.Core.CommonUtils
{
    public static class XMLUtil
    {
        /// <summary>
        /// Fetches the Object Details from the respective xml object repository
        /// </summary>
        /// <param name="rootObjectName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Dictionary<string,Dictionary<string,string>> GetObjectDetails(string rootObjectName, string filePath)
        {
            Dictionary<string, Dictionary<string, string>> objectDetails = new Dictionary<string, Dictionary<string, string>>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Objects")[0].ChildNodes;
            
            foreach (XmlNode node in nodeList)
            {
                String objectName = node.Name;
                Dictionary<string,string> objectMetaData =  new Dictionary<string,string>();
                XmlAttributeCollection attributes = node.Attributes;
               
                foreach (XmlAttribute attr in attributes)
                {
                    if (attr.Name.Equals("name"))
                        objectName = attr.Value;
                    else
                        objectMetaData.Add(attr.Name, attr.Value);
                }
                objectDetails.Add(objectName, objectMetaData);
            }

            return objectDetails;
        }
    }
}

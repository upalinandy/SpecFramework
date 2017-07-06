using MongoDB.Driver;
using SpecFramework.ProjectLibs.Tests.Resources.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFramework.Main.CommonUtils
{
   public class MongoDBUtils
    {
        public static IMongoDatabase seradb;

        public void DeleteDoc(string connectionstring, string dbname, string key)
        {
            Console.WriteLine("key: " + key);
          //  connectionstring = "mongodb://sera:password@10.37.1.27:27017/sera";
            IMongoClient client = new MongoClient(connectionstring);
            seradb = client.GetDatabase("sera");
            var collection = seradb.GetCollection<Request>("requests");
            collection.DeleteOne(a => a._id == int.Parse(key));
        }
    }
}

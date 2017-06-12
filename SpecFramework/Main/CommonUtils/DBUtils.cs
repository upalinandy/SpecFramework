using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using SpecFramework.Config;

namespace SpecFramework.Main.CommonUtils
{
    class DBUtils
    {

        string connectionString = null;

        //Get the connection string from App.config file
        public DBUtils()
        {
            try
            {
                AppConfigReader appConfig = new AppConfigReader();
                connectionString = appConfig.GetDBConnectionString();
                Console.WriteLine("connection string=" + connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem while fetching connection string" + e);
            }
        }

        //Used for insert/update/delete operation and returns no. of rows affected by the operation
        public int ExecuteNonQuery(string sqlQuery)
        {
            SqlConnection connection = null;
            int rowsAffected = 0;
            try
            {
                SqlConnection.ClearAllPools();
                using (connection = new SqlConnection(connectionString))
                {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                        rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("No. Of Rows Affected = " + rowsAffected);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem while executing database query" + e);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed) connection.Close();
            }
            return rowsAffected;
        }

        //Used to fetch the records from database for select query. Returns a dictionary of each row of the table. 
        public Dictionary<int, Dictionary<string, object>> FetchRecords(string sqlQuery)
        {
            //Each record (row) would be saved. <RowNum,Row contents>  
            Dictionary<int, Dictionary<string, object>> result = new Dictionary<int, Dictionary<string, object>>();
            SqlConnection connection = null;
            try
            {
                SqlConnection.ClearAllPools();
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int i = 1;
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            //Each column of the row. <column header,column value>
                            Dictionary<string, object> row = new Dictionary<string, object>();

                            for (int j = 0; j < dr.FieldCount; j++)
                            {
                                row[dr.GetName(j)] = dr.GetValue(j);
                            }

                            result[i] = row;
                            i++;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem while reading" + e);
            }
            finally
            {
               if (connection != null && connection.State != System.Data.ConnectionState.Closed) connection.Close();
            }
            return result;
        }


    }
}
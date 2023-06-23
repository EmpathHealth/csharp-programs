using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Utilities
{
    public static class SqlFunctions
    {

        public static int ExeCmdReturnAffectedRows(SqlConnection connObject, string sqlText)
        {
            int rows = 0;

            using (connObject)
            {
                try
                {
                    connObject.Open();  //might be unnecessary 
                    SqlCommand cmd = connObject.CreateCommand();
                    cmd.CommandText = sqlText;
                    rows = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return rows;
            }

        }

        public static int ExeCmdReturnAffectedRows(SqlConnection connObject, string sqlText, Dictionary<string, object> keyValues)
        {
            int rows = 0;

            using (connObject)
            {
                try
                {
                    connObject.Open();
                    SqlCommand cmd = connObject.CreateCommand();
                    cmd.CommandText = sqlText;

                    foreach (var item in keyValues)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    rows = cmd.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);  
                }

                return rows;

            }
        }

        public static object ExeCmdReturnSingleValue(SqlConnection connObject, string sqlText)
        {
            object? returnObject = 0;

            using (connObject)
            {
                try
                {
                    connObject.Open();
                    SqlCommand cmd = connObject.CreateCommand();
                    cmd.CommandText = sqlText;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        returnObject = dataReader[0];
                    } 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return returnObject;
            }
        }
    }
}

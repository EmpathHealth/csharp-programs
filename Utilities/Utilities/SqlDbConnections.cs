using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Utilities
{
    public static class SqlDbConnections
    {
        public static string GetConnectionStringByName(string environmentType)
        {
            string connectionString;

            // Look for the name in the ConnectionString section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[environmentType];

            if (settings != null)
            {
                connectionString = settings.ConnectionString;
            }
            else
            {
                throw new Exception(environmentType + " does not exist in the config file");
            }

            return connectionString;

        }

        public static SqlConnection GetSqlConnectionByName(string environmentType)
        {
            SqlConnection sqlConnection;

            string constring = GetConnectionStringByName(environmentType);

            if (constring != null)
            {
                sqlConnection = new SqlConnection(constring);
            }
            else
            {
                throw new Exception(environmentType + " does not exist in the config file");
            }

            return sqlConnection;

        }
    }
}

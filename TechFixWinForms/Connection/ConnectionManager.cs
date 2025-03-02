using System;
using System.Data.SqlClient;
using System.Configuration;

namespace TechFixWinForms.Connection
{
    public class ConnectionManager
    {
        public static SqlConnection newCon;
        public static string Constr = ConfigurationManager.ConnectionStrings["ConString"]?.ConnectionString;

        public static SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(Constr))
            {
                throw new InvalidOperationException("Connection string 'ConString' is missing or invalid.");
            }

            newCon = new SqlConnection(Constr);
            return newCon;
        }
    }
}

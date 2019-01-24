using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Workflow
{
    public class DBConn
    {
        //The 'DBConnString' value still needs to be filled out for our DB in the Web.Config file
        string connStr = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
        SqlConnection conn;

        public void OpenConnection()
        {
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public void ExecuteQueries(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        public SqlDataReader DataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
    }
}
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

        public int ExecuteInsertCommand(SqlCommand cmd)
        {
            OpenConnection();
            //executes the insert statement provided in the CMD
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            cmd.Prepare();
            //gets the ID of the inserted ^
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int id = (int)cmd.ExecuteScalar();
            CloseConnection();
            return id;
        }

        public SqlDataReader ExecuteSelectCommand(SqlCommand cmd)
        {
            OpenConnection();
            //executes the select statement provided in the CMD
            cmd.Connection = conn;
            cmd.Prepare();
            SqlDataReader dr = cmd.ExecuteReader();
            CloseConnection();
            return dr;
        }
    }
}
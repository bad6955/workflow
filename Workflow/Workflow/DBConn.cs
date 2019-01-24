using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace Workflow
{
    public class DBConn
    {
        //The 'DBConnString' value still needs to be filled out for our DB in the Web.Config file
        string connStr = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
        MySqlConnection conn;

        public void OpenConnection()
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public void ExecuteInsertCommand(MySqlCommand cmd)
        {
            OpenConnection();
            //executes the insert statement provided in the CMD
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            cmd.Prepare();
            //gets the ID of the inserted ^
            //cmd.CommandText = "SELECT LAST_INSERT_ID()";
            //int id = (int)cmd.ExecuteScalar();
            //return id;
        }

        public MySqlDataReader ExecuteSelectCommand(MySqlCommand cmd)
        {
            OpenConnection();
            //executes the select statement provided in the CMD
            cmd.Connection = conn;
            cmd.Prepare();
            MySqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace Workflow.Utility
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

        public int ExecuteInsertCommand(MySqlCommand cmd)
        {
            int insertedId = -1;

            OpenConnection();
            //executes the insert statement provided in the CMD
            cmd.Connection = conn;

            if (cmd.CommandText.Contains("INSERT"))
            {
                cmd.CommandText += "; SELECT LAST_INSERT_ID();";
                //executes the insert statement provided in the CMD
                cmd.Connection = conn;
                cmd.Prepare();
                var id = cmd.ExecuteScalar();
                int.TryParse(id.ToString(), out insertedId);
            }
            else
            {
                cmd.Connection = conn;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            return insertedId;
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
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public class HelpSystemUtil
    {
        public static List<HelpSystem> GetHelp(int pageID, int roleID)
        {
            string query = "SELECT PageID, RoleID, Step, Intro from HelpSystem WHERE PageID=@pageID AND RoleID=@roleID";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@pageID", pageID);
            cmd.Parameters.AddWithValue("@roleID", roleID);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<HelpSystem> helpSystem = new List<HelpSystem>();
            while (dr.Read())
            {
                HelpSystem hs = new HelpSystem((int)dr["PageID"], (int)dr["RoleID"], (int)dr["Step"],(string)dr["Intro"]);
                helpSystem.Add(hs);
            }
            conn.CloseConnection();
            return helpSystem;
        }
    }
}
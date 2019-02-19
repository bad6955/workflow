using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public static class ComponentCompletionUtil
    {
        public static ComponentCompletion GetProCompletionStatus(int wfid, int projid)
        {
            string query = "SELECT WFComponentID, ProjectID, CompletionID from ComponentCompletion WHERE WFComponentID = @wfid AND ProjectID = @projid";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@wfid", wfid);
            cmd.Parameters.AddWithValue("@projid", projid);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            ComponentCompletion comp = null;
            while (dr.Read())
            {
                comp = new ComponentCompletion((int)dr["WFComponentID"], (int)dr["ProjectID"], (int)dr["CompletionID"]);
            }
            conn.CloseConnection();
            return comp;
        }
        public static List<ComponentCompletion> GetAllProCompletionStatus(int projid)
        {
            string query = "SELECT WFComponentID, ProjectID, CompletionID from ComponentCompletion WHERE ProjectID = @projid";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@projid", projid);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<ComponentCompletion> list = new List<ComponentCompletion>();
            while (dr.Read())
            { 
                ComponentCompletion comp = new ComponentCompletion((int)dr["WFComponentID"], (int)dr["ProjectID"], (int)dr["CompletionID"]);
                list.Add(comp);
            }
            conn.CloseConnection();
            return list;
        }
        public static List<ComponentCompletion> GetCompletedProCompletionStatus(int projid)
        {
            string query = "SELECT WFComponentID, ProjectID, CompletionID from ComponentCompletion WHERE ProjectID = @projid AND CompletionID = 2";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@projid", projid);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<ComponentCompletion> list = new List<ComponentCompletion>();
            while (dr.Read())
            {
                ComponentCompletion comp = new ComponentCompletion((int)dr["WFComponentID"], (int)dr["ProjectID"], (int)dr["CompletionID"]);
                list.Add(comp);
            }
            conn.CloseConnection();
            return list;
        }

    }
}
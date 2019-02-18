using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public class WorkflowComponentUtil
    {
        public static WorkflowComponent CreateWorkflowComponent(int wfid, int workflowID, string componenttitle, string componenttext)
        {
            WorkflowComponent w = new WorkflowComponent(wfid, workflowID, componenttitle, componenttext);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO WorkflowComponents (WFComponentID, WorkflowID, ComponentTitle, ComponentText) VALUES (@wfid, @workflowID, @componenttitle, @componenttext)");
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            cmd.Parameters.AddWithValue("@wfid", wfid);
            cmd.Parameters.AddWithValue("@componenttitle", componenttitle);
            cmd.Parameters.AddWithValue("@componenttext", componenttext);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            return w;
        }
        public static List<WorkflowComponent> GetWorkflowComponents(int workflowID)
        {
            string query = "SELECT WFComponentID, WorkflowID, ComponentTitle, ComponentText FROM WorkflowComponents WHERE WorkflowID = (@workflowID)";
            
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<WorkflowComponent> componentList = new List<WorkflowComponent>();
            while (dr.Read())
            {
                WorkflowComponent w = new WorkflowComponent((int)dr["WFComponentID"], (int)dr["WorkflowID"], (string)dr["ComponentTitle"], (string)dr["ComponentText"]);
                componentList.Add(w);
            }
            conn.CloseConnection();
            return componentList;
        }
    }
}
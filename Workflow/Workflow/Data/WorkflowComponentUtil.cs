using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Workflow.Models;

namespace Workflow.Data
{
    public class WorkflowComponentUtil
    {
        public static WorkflowComponent CreateWorkflowComponent(int workflowID, string componenttitle, string componenttext)
        {
            WorkflowComponent w = new WorkflowComponent(workflowID, componenttitle, componenttext);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO WorkflowComponents (WorkflowID, ComponentTitle, ComponentText) VALUES (@workflowID, @componenttitle, @componenttext)");
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            cmd.Parameters.AddWithValue("@componenttitle", componenttitle);
            cmd.Parameters.AddWithValue("@componenttext", componenttext);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            return w;
        }
        public static List<WorkflowComponent> GetWorkflowComponents(int workflowID)
        {
            string query = "SELECT ComponentTitle, ComponentText FROM WorkflowComponents WHERE WorkflowID = (@workflowID)";
            
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<WorkflowComponent> componentList = new List<WorkflowComponent>();
            while (dr.Read())
            {
                WorkflowComponent w = new WorkflowComponent((int)dr["WorkflowID"], (string)dr["ComponentTitle"], (string)dr["ComponentText"]);
                componentList.Add(w);
            }
            conn.CloseConnection();
            return componentList;
        }
    }
}
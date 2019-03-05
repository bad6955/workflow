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
        public static WorkflowComponent CreateWorkflowComponent(int workflowID)
        {
            WorkflowComponent w = new WorkflowComponent(workflowID);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO WorkflowComponents(WorkflowID, ComponentTitle, ComponentText) VALUES (@workflowID, '', '')");
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            DBConn conn = new DBConn();
            w.WFComponentID = conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return w;
        }

        public static WorkflowComponent CreateWorkflowComponent(int workflowID, string componenttitle, int formId)
        {
            WorkflowComponent w = new WorkflowComponent(workflowID, componenttitle, formId);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO WorkflowComponents(WorkflowID, ComponentTitle, FormID) VALUES (@workflowID, @componenttitle, @formId)");
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            cmd.Parameters.AddWithValue("@componenttitle", componenttitle);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            w.WFComponentID = conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return w;
        }

        public static List<WorkflowComponent> GetWorkflowComponents(int workflowID)
        {
            string query = "SELECT WFComponentID, WorkflowID, ComponentTitle, ComponentText, FormID FROM WorkflowComponents WHERE WorkflowID = @workflowID";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@workflowID", workflowID);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<WorkflowComponent> componentList = new List<WorkflowComponent>();
            WorkflowComponent w = null;
            try
            {
                WorkflowComponent w = new WorkflowComponent((int)dr["WFComponentID"], (int)dr["WorkflowID"], (string)dr["ComponentTitle"], (int)dr["FormID"]);
                componentList.Add(w);
            }
            catch (Exception e) { }
            conn.CloseConnection();
            return componentList;
        }

        public static void UpdateWorkflowComponent(int wfcId, string title, int formId)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE WorkflowComponents SET ComponentTitle=@title, FormID=@formId WHERE WFComponentID=@wfcId");
            cmd.Parameters.AddWithValue("@wfcId", wfcId);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
        }

        public static List<WorkflowComponent> GetFormWorkflowComponents(int formId)
        {
            string query = "SELECT WFComponentID, WorkflowID, ComponentTitle, ComponentText, FormID FROM WorkflowComponents WHERE FormID = @formId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<WorkflowComponent> componentList = new List<WorkflowComponent>();
            while (dr.Read())
            {
                WorkflowComponent w = new WorkflowComponent((int)dr["WFComponentID"], (int)dr["WorkflowID"], (string)dr["ComponentTitle"], (int)dr["FormID"]);
                componentList.Add(w);
            }
            conn.CloseConnection();
            return componentList;
        }

        public static void DeleteWorkflowComponent(int wfcId)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM WorkflowComponents WHERE WFComponentID=@wfcId");
            cmd.Parameters.AddWithValue("@wfcId", wfcId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
        }
    }
}

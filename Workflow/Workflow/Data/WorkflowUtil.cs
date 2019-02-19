using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public static class WorkflowUtil
    {
        public static WorkflowModel CreateWorkflow(string workflowName)
        {
            WorkflowModel w = new WorkflowModel(workflowName);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Workflows (WorkflowName) VALUES (@workflowName)");
            cmd.Parameters.AddWithValue("@workflowName", workflowName);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return w;
        }

        public static List<WorkflowModel> GetWorkflows()
        {
            string query = "SELECT WorkflowID, WorkflowName from Workflows";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<WorkflowModel> workflowList = new List<WorkflowModel>();
            while (dr.Read())
            {
                WorkflowModel w = new WorkflowModel((int)dr["WorkflowID"], (string)dr["WorkflowName"]);
                workflowList.Add(w);
            }
            conn.CloseConnection();
            return workflowList;
        }

        public static List<Form> GetWorkflowForms(int workflowId)
        {
            string query = "SELECT FormID, FormName, ApprovalRequiredID, ApprovalStatusID from Workflows where WorkflowID = @workflowId";
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@workflowId", workflowId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Form> formList = new List<Form>();
            while (dr.Read())
            {
                Form f = new Form((int)dr["FormID"], workflowId, (string)dr["FormName"], (int)dr["ApprovalRequiredID"], (int)dr["ApprovalStatusID"]);
                formList.Add(f);
            }
            conn.CloseConnection();
            return formList;
        }

        public static WorkflowModel GetWorkflow(int workflowId)
        {
            string query = "SELECT WorkflowID, WorkflowName from Workflows WHERE WorkflowID = @workflowId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@workflowId", workflowId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            WorkflowModel workflow = null;
            while (dr.Read())
            {
                workflow = new WorkflowModel((int)dr["WorkflowID"], (string)dr["WorkflowName"]);
            }
            conn.CloseConnection();
            return workflow;
        }
    }
}
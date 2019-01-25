using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Workflow.Models;

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
    }
}
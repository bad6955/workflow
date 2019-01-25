using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workflow.Models;

namespace Workflow.Data
{
    public static class ProjectUtil
    {
        public static Project CreateProject(string name, int workflowId, int companyId, int VCID, string notes)
        {
            Project p = new Project(workflowId, companyId, 0, VCID, name, notes);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Project (WorkflowID, CompanyID, StatusID, VCID, ProjectName, ProjectNotes) VALUES (@workflowId, @companyId, @statusId, @VCID, @name, @notes)");
            cmd.Parameters.AddWithValue("@workflowId", workflowId);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            cmd.Parameters.AddWithValue("@statusId", 0);
            cmd.Parameters.AddWithValue("@VCID", VCID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@notes", notes);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            return p;
        }

        public static List<Project> GetProjects()
        {
            string query = "SELECT ProjectID, WorkflowID, CompanyID, StatusID, VCID, ProjectName, ProjectNotes from Project";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Project> projectList = new List<Project>();
            while (dr.Read())
            {
                Project p = new Project((int)dr["ProjectID"], (int)dr["WorkflowID"], (int)dr["CompanyID"], (int)dr["StatusID"], (int)dr["VCID"], (string)dr["ProjectName"], (string)dr["ProjectNotes"]);
                projectList.Add(p);
            }
            conn.CloseConnection();
            return projectList;
        }
    }
}
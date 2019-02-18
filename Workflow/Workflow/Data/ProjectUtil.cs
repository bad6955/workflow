using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public static class ProjectUtil
    {
        public static Project CreateProject(string name, int workflowId, int companyId, int coachId, string notes)
        {
            Project p = new Project(workflowId, companyId, 0, coachId, name, notes);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Project (WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes) VALUES (@workflowId, @companyId, @statusId, @coachId, @name, @notes)");
            cmd.Parameters.AddWithValue("@workflowId", workflowId);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            cmd.Parameters.AddWithValue("@statusId", 0);
            cmd.Parameters.AddWithValue("@coachId", coachId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@notes", notes);
            DBConn conn = new DBConn();
            int id = conn.ExecuteInsertCommand(cmd);

            /*
            cmd = new MySqlCommand("SELECT LAST_INSERT_ID();");
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            while (dr.Read())
            {
                int projId = (int)dr["ProjectID"];
                FeedUtil.CreateProjectFeedItem("Added as a coach for " + name, coachId, projId);
            }
            */
            FeedUtil.CreateProjectFeedItem("Added as a coach for " + name, coachId, id);
            return p;
        }

        public static Project GetProject(int projectId)
        {
            string query = "SELECT ProjectID, WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes from Project where ProjectID = @projectId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@projectId", projectId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Project p = null;
            while (dr.Read())
            {
                p = new Project((int)dr["ProjectID"], (int)dr["WorkflowID"], (int)dr["CompanyID"], (int)dr["StatusID"], (int)dr["CoachID"], (string)dr["ProjectName"], (string)dr["ProjectNotes"]);
            }
            conn.CloseConnection();
            return p;
        }

        public static List<Project> GetProjects()
        {
            string query = "SELECT ProjectID, WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes from Project";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Project> projectList = new List<Project>();
            while (dr.Read())
            {
                Project p = new Project((int)dr["ProjectID"], (int)dr["WorkflowID"], (int)dr["CompanyID"], (int)dr["StatusID"], (int)dr["CoachID"], (string)dr["ProjectName"], (string)dr["ProjectNotes"]);
                projectList.Add(p);
            }
            conn.CloseConnection();
            return projectList;
        }

        public static List<Project> GetCoachProjects(int coachID)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT ProjectID, WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes from Project WHERE CoachID = @coachID");
            cmd.Parameters.AddWithValue("@coachID", coachID);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Project> projectList = new List<Project>();
            while (dr.Read())
            {
                Project p = new Project((int)dr["ProjectID"], (int)dr["WorkflowID"], (int)dr["CompanyID"], (int)dr["StatusID"], (int)dr["CoachID"], (string)dr["ProjectName"], (string)dr["ProjectNotes"]);
                projectList.Add(p);
            }
            conn.CloseConnection();
            return projectList;
        }
    }
}
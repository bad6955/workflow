﻿using MySql.Data.MySqlClient;
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
            p.ProjectId = conn.ExecuteInsertCommand(cmd);

            //creates notification for the coach
            FeedUtil.CreateProjectFeedItem("Added as a coach for " + name, coachId, p.ProjectId);

            List<WorkflowComponent> workflowComponents = WorkflowComponentUtil.GetWorkflowComponents(workflowId);
            foreach (WorkflowComponent wc in workflowComponents)
            {
                //create completion status and forms for the project
                if (wc.FormID != -1)
                {
                    Form f = FormUtil.CreateForm(wc.FormID, p.ProjectId);

                    //creates notifications for each member of the company and each form
                    List<User> clients = UserUtil.GetClients(companyId);
                    foreach (User client in clients)
                    {
                        FeedUtil.CreateProjectFormFeedItem("Form " + f.FormName + " needs completion for " + name, client.UserId, p.ProjectId, f.FormId);
                    }
                }
            }

            conn.CloseConnection();
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

        public static void DeleteProject(int projectId)
        {
            //delete associated forms
            List<Form> projectForms = FormUtil.GetProjectForms(projectId);
            foreach (Form f in projectForms)
            {
                FormUtil.DeleteForm(f.FormId);
            }

            string query = "DELETE from Project where ProjectID = @projectId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@projectId", projectId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
        }

        public static Project UpdateProject(int projectId, string projectName, int companyId, int coachId, string projectNotes)
        {
            string query = "UPDATE Project SET ProjectName=@projectName, CompanyID=@companyId, CoachID=@coachId, ProjectNotes=@projectNotes where ProjectID = @projectId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@projectName", projectName);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            cmd.Parameters.AddWithValue("@coachId", coachId);
            cmd.Parameters.AddWithValue("@projectNotes", projectNotes);
            cmd.Parameters.AddWithValue("@projectId", projectId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return GetProject(projectId);
        }

        public static List<Project> GetProjects()
        {
            string query = "SELECT ProjectID, WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes from Project ORDER BY ProjectID DESC";

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

        public static List<Project> GetWorkflowProjects(int workflowId)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT ProjectID, WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes from Project WHERE WorkflowID = @workflowId");
            cmd.Parameters.AddWithValue("@workflowId", workflowId);
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

        public static List<Project> GetCompanyProjects(int companyId)
        {
            string query = "SELECT ProjectID, WorkflowID, CompanyID, StatusID, CoachID, ProjectName, ProjectNotes from Project WHERE CompanyID = @companyId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@companyId", companyId);
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

        public static bool CheckProjectCompletion(int projectId)
        {
            bool complete = true;
            List<Form> forms = FormUtil.GetProjectForms(projectId);
            foreach (Form f in forms)
            {
                if (f.Approved.Contains("0"))
                {
                    complete = false;
                }
            }

            return complete;
        }
    }
}
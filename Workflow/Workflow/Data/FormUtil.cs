using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public static class FormUtil
    {
        public static Form CreateForm(int formId, int projId)
        {
            Form f = GetFormTemplate(formId);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Forms (FormTemplateID, ProjectID, FormName, FormData) VALUES (@formId, @projId, @formName, @formData)");
            cmd.Parameters.AddWithValue("@formId", formId);
            cmd.Parameters.AddWithValue("@projId", projId);
            cmd.Parameters.AddWithValue("@formName", f.FormName);
            cmd.Parameters.AddWithValue("@formData", f.FormData);
            DBConn conn = new DBConn();
            f.FormId = conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form CreateFormTemplate(string formName, string formData)
        {
            Form f = new Form(formName);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO FormTemplates (FormName, FormData) VALUES (@formName, @formData)");
            cmd.Parameters.AddWithValue("@formName", formName);
            cmd.Parameters.AddWithValue("@formData", formData);
            DBConn conn = new DBConn();
            f.FormId = conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form GetForm(int formId)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormTemplateID, FormName, FormData, ProjectID, ApprovalRequiredID, ApprovalStatusID, Submission, Approved, Denied, DenialReason FROM Forms WHERE FormID = @formId");
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Form f = null;
            while (dr.Read())
            {
                f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], (int)dr["ProjectID"], (int)dr["Submission"], (int)dr["Approved"], (int)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
            }
            conn.CloseConnection();
            return f;
        }

        public static Form GetProjectFormByTemplate(int formTemplateId, int projectId)
        {
            Form f = null;
            MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormTemplateID, FormName, FormData, ProjectID, ApprovalRequiredID, ApprovalStatusID, Submission, Approved, Denied, DenialReason FROM Forms WHERE ProjectId = @projId AND FormTemplateID = @formTemplateId");
            cmd.Parameters.AddWithValue("@projId", projectId);
            cmd.Parameters.AddWithValue("@formTemplateId", formTemplateId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            while (dr.Read())
            {
                f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], projectId, (int)dr["Submission"], (int)dr["Approved"], (int)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
            }
            conn.CloseConnection();
            return f;
        }

        public static List<Form> GetCompanyForms(int companyId)
        {
            List<Project> projects = ProjectUtil.GetCompanyProjects(companyId);
            List<Form> formList = new List<Form>();
            foreach (Project p in projects)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormTemplateID, FormName, FormData, ProjectID, ApprovalRequiredID, ApprovalStatusID, Submission, Approved, Denied, DenialReason FROM Forms WHERE ProjectId = @projId");
                cmd.Parameters.AddWithValue("@projId", p.ProjectId);
                DBConn conn = new DBConn();
                MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
                while (dr.Read())
                {
                    Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], p.ProjectId, (int)dr["Submission"], (int)dr["Approved"], (int)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
                    formList.Add(f);
                }
                conn.CloseConnection();
            }
            return formList;
        }

        public static List<Form> GetCoachForms(int coachId)
        {
            List<Project> projects = ProjectUtil.GetCoachProjects(coachId);
            List<Form> formList = new List<Form>();
            foreach (Project p in projects)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormTemplateID, FormName, FormData, ProjectID, ApprovalRequiredID, ApprovalStatusID, Submission, Approved, Denied, DenialReason FROM Forms WHERE ProjectId = @projId");
                cmd.Parameters.AddWithValue("@projId", p.ProjectId);
                DBConn conn = new DBConn();
                MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
                while (dr.Read())
                {
                    Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], p.ProjectId, (int)dr["Submission"], (int)dr["Approved"], (int)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
                    formList.Add(f);
                }
                conn.CloseConnection();
            }
            return formList;
        }

        public static Form GetFormTemplate(int formId)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT FormTemplateID, FormName, FormData FROM FormTemplates WHERE FormTemplateID = @formId");
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Form f = null;
            while (dr.Read())
            {
                f = new Form((int)dr["FormTemplateID"], (string)dr["FormName"], (string)dr["FormData"]);
            }
            conn.CloseConnection();
            return f;
        }

        public static Form UpdateFormTemplate(int formId, string formName, string formData)
        {
            Form f = new Form(formId, formName, formData);
            MySqlCommand cmd = new MySqlCommand("UPDATE FormTemplates SET FormName=@formName, FormData=@formData WHERE FormTemplateID=@formId");
            cmd.Parameters.AddWithValue("@formName", formName);
            cmd.Parameters.AddWithValue("@formData", formData);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form UpdateForm(int formId, string formName, string formData)
        {
            Form f = new Form(formId, formName, formData);
            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET FormName=@formName, FormData=@formData, Denied=0, Approved=0 WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@formName", formName);
            cmd.Parameters.AddWithValue("@formData", formData);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form SubmitForm(int formId, string formName, string formData)
        {
            Form f = GetForm(formId);
            f.FormName = formName;
            f.FormData = formData;
            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET FormName=@formName, FormData=@formData, Submission=1, Approved=0, Denied=0, DenialReason=\"\" WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@formName", formName);
            cmd.Parameters.AddWithValue("@formData", formData);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            Project p = ProjectUtil.GetProject(f.ProjectId);
            FeedUtil.CreateProjectFormFeedItem(p.Name + " has form " + formName + " ready for your approval", p.CoachId, p.ProjectId, formId);
            return f;
        }

        public static Form ApproveForm(int formId)
        {
            Form f = GetForm(formId);
            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET Approved=1 WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            Project p = ProjectUtil.GetProject(f.ProjectId);

            //notify all clients of the approval
            List<User> clients = UserUtil.GetClients(p.CompanyId);
            foreach (User client in clients)
            {
                FeedUtil.CreateProjectFormFeedItem(f.FormName + " was approved by " + UserUtil.GetCoachName(p.CoachId), client.UserId, p.ProjectId, formId);
            }
            return f;
        }

        public static Form DenyForm(int formId, string denialReason)
        {
            Form f = GetForm(formId);
            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET Denied=1, DenialReason=@denialReason, Submission=0 WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@denialReason", denialReason);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            Project p = ProjectUtil.GetProject(f.ProjectId);

            //notify all clients of the approval
            List<User> clients = UserUtil.GetClients(p.CompanyId);
            foreach (User client in clients)
            {
                FeedUtil.CreateProjectFormFeedItem(f.FormName + " was denied by " + UserUtil.GetCoachName(p.CoachId), client.UserId, p.ProjectId, formId);
            }
            return f;
        }

        public static List<Form> GetForms()
        {
            string query = "SELECT FormID, FormName from Forms WHERE FormID > 0";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Form> formList = new List<Form>();
            while (dr.Read())
            {
                Form f = new Form((int)dr["FormID"], (string)dr["FormName"]);
                formList.Add(f);
            }
            conn.CloseConnection();
            return formList;
        }

        //INCLUDES THINGS LIKE --SELECT FORM--
        public static List<Form> GetAllForms()
        {
            string query = "SELECT FormID, FormName from Forms";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Form> formList = new List<Form>();
            while (dr.Read())
            {
                Form f = new Form((int)dr["FormID"], (string)dr["FormName"]);
                formList.Add(f);
            }
            conn.CloseConnection();
            return formList;
        }

        public static List<Form> GetFormTemplates()
        {
            string query = "SELECT FormTemplateID, FormName, FormData from FormTemplates where FormTemplateID > 0";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Form> formList = new List<Form>();
            while (dr.Read())
            {
                Form f = new Form((int)dr["FormTemplateID"], (string)dr["FormName"], (string)dr["FormData"]);
                formList.Add(f);
            }
            conn.CloseConnection();
            return formList;
        }

        //INCLUDES THINGS LIKE --SELECT FORM--
        public static List<Form> GetAllFormTemplates()
        {
            string query = "SELECT FormTemplateID, FormName, FormData from FormTemplates";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Form> formList = new List<Form>();
            while (dr.Read())
            {
                Form f = new Form((int)dr["FormTemplateID"], (string)dr["FormName"], (string)dr["FormData"]);
                formList.Add(f);
            }
            conn.CloseConnection();
            return formList;
        }

        public static bool DeleteForm(int formId)
        {
            List<WorkflowComponent> comps = WorkflowComponentUtil.GetFormWorkflowComponents(formId);
            if (comps.Count == 0)
            {
                string query = "DELETE FROM FormTemplates WHERE FormTemplateID=@formId";
                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@formId", formId);
                DBConn conn = new DBConn();
                conn.ExecuteInsertCommand(cmd);
                conn.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
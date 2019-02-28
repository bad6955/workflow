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
            MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormName, FormData, ProjectId, ApprovalRequiredID, ApprovalStatusID FROM Forms WHERE FormID = @formId");
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Form f = null;
            while (dr.Read())
            {
                f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"]);
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
                MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormName, FormData, ProjectId, ApprovalRequiredID, ApprovalStatusID FROM Forms WHERE ProjectId = @projId");
                cmd.Parameters.AddWithValue("@projId", p.ProjectId);
                DBConn conn = new DBConn();
                MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
                while (dr.Read())
                {
                    Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], p.ProjectId);
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
                MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormName, FormData, ProjectId, ApprovalRequiredID, ApprovalStatusID FROM Forms WHERE ProjectId = @projId");
                cmd.Parameters.AddWithValue("@projId", p.ProjectId);
                DBConn conn = new DBConn();
                MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
                while (dr.Read())
                {
                    Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], p.ProjectId);
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
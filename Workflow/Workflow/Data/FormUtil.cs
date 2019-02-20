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
        public static Form CreateForm(string formName)
        {
            Form f = new Form(formName);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Forms (FormName) VALUES (@formName)");
            cmd.Parameters.AddWithValue("@formName", formName);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
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
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
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

        //field value and role id should be able to be null
        public static FormField CreateFormField(int formId, string fieldText, string fieldValue)
        {
            FormField ff = new FormField(formId, fieldText, fieldValue);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO FormFields (FormID, FieldText, FieldValue) VALUES (@formId, @fieldText, @fieldValue)");
            cmd.Parameters.AddWithValue("@formId", formId);
            cmd.Parameters.AddWithValue("@fieldText", fieldText);
            cmd.Parameters.AddWithValue("@fieldValue", fieldValue);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return ff;
        }

        public static List<Form> GetForms()
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

        public static List<FormField> GetFormFields(int formId)
        {
            string query = "SELECT FormFieldID, FieldValue, FieldText from FormFields where FormID = @formId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<FormField> formFieldList = new List<FormField>();
            while (dr.Read())
            {
                FormField ff = new FormField((int)dr["FormFieldID"], formId, (string)dr["FieldValue"], (string)dr["FieldText"]);
                formFieldList.Add(ff);
            }
            conn.CloseConnection();
            return formFieldList;
        }
    }
}
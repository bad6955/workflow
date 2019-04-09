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
            string defaultApproved = "0";
            string defaultDenied = "0";
            Form f = GetFormTemplate(formId);
            string[] approvalIDs = new string[0];
            if(f.ApproverIDs.Length > 0)
            {
                approvalIDs = f.ApproverIDs.Split(',');
            }

            if(approvalIDs.Length > 0)
            {
                int i = 0;
                foreach(string roleID in approvalIDs)
                {
                    if (i != 0)
                    {
                        defaultApproved += ",0";
                        defaultDenied += ",0";
                    }
                    i++;
                }
            }
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Forms (FormTemplateID, ProjectID, FormName, FormData, Approved, Denied) VALUES (@formId, @projId, @formName, @formData, @approval, @denial)");
            cmd.Parameters.AddWithValue("@formId", formId);
            cmd.Parameters.AddWithValue("@projId", projId);
            cmd.Parameters.AddWithValue("@formName", f.FormName);
            cmd.Parameters.AddWithValue("@formData", f.FormData);
            cmd.Parameters.AddWithValue("@approval", defaultApproved);
            cmd.Parameters.AddWithValue("@denial", defaultDenied);
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
            MySqlCommand cmd = new MySqlCommand("SELECT FormID, FormTemplateID, FormName, FormData, ProjectID, ApprovalRequiredID, ApprovalStatusID, Submission, Approved, Denied, DenialReason, FilePath, UploadedFileName FROM Forms WHERE FormID = @formId");
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Form f = null;
            try
            {
                while (dr.Read())
                {
                    f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], (int)dr["ProjectID"], (int)dr["Submission"], (string)dr["Approved"], (string)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"], (string)dr["FilePath"], (string)dr["UploadedFileName"]);
                }
            } catch (Exception e) { }
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
            try
            {
                while (dr.Read())
                {
                    f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], projectId, (int)dr["Submission"], (string)dr["Approved"], (string)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
                }
            } catch(Exception e) { }
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
                try
                {
                    while (dr.Read())
                    {
                        Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], p.ProjectId, (int)dr["Submission"], (string)dr["Approved"], (string)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
                        formList.Add(f);
                    }
                } catch (Exception e) { }
                conn.CloseConnection();
            }
            return formList;
        }

        public static List<Form> GetProjectForms(int projectId)
        {
            List<Form> formList = new List<Form>();
            Project p = ProjectUtil.GetProject(projectId);
            List<WorkflowComponent> workflowComponents = WorkflowComponentUtil.GetWorkflowComponents(p.WorkflowId);
            foreach (WorkflowComponent wc in workflowComponents)
            {
                if(wc.FormID != -1)
                {
                    formList.Add(GetProjectFormByTemplate(wc.FormID, projectId));
                }
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
                try
                {
                    while (dr.Read())
                    {
                        Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (string)dr["FormData"], p.ProjectId, (int)dr["Submission"], (string)dr["Approved"], (string)dr["Denied"], (string)dr["DenialReason"], (int)dr["FormTemplateID"]);
                        formList.Add(f);
                    }
                } catch (Exception e) { }
            conn.CloseConnection();
            }
            return formList;
        }

        public static Form GetFormTemplate(int formId)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT FormTemplateID, FormName, FormData, ApproverIDs FROM FormTemplates WHERE FormTemplateID = @formId");
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Form f = null;
            while (dr.Read())
            {
                f = new Form((int)dr["FormTemplateID"], (string)dr["FormName"], (string)dr["FormData"], (string)dr["ApproverIDs"]);
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

        public static Form UpdateFormTemplateApprovers(int formId, string approverIDs)
        {
            Form f = GetFormTemplate(formId);
            MySqlCommand cmd = new MySqlCommand("UPDATE FormTemplates SET ApproverIDs=@approverIDs WHERE FormTemplateID=@formId");
            cmd.Parameters.AddWithValue("@approverIDs", approverIDs);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form UpdateForm(int formId, string formName, string formData)
        {
            Form f = new Form(formId, formName, formData);
            Form f2 = GetForm(formId);
            string defaultApproved = "0";
            string defaultDenied = "0";
            Form ft = GetFormTemplate(f2.FormTemplateId);
            string[] approvalIDs = new string[0];
            if (ft.ApproverIDs.Length > 0)
            {
                approvalIDs = ft.ApproverIDs.Split(',');
            }

            if (approvalIDs.Length > 0)
            {
                int i = 0;
                foreach (string roleID in approvalIDs)
                {
                    if (i != 0)
                    {
                        defaultApproved += ",0";
                        defaultDenied += ",0";
                    }
                    i++;
                }
            }

            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET FormName=@formName, FormData=@formData, Denied=@denied, Approved=@approved WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@formName", formName);
            cmd.Parameters.AddWithValue("@formData", formData);
            cmd.Parameters.AddWithValue("@formId", formId);
            cmd.Parameters.AddWithValue("@denied", defaultDenied);
            cmd.Parameters.AddWithValue("@approved", defaultApproved);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form UpdateFormFile(Form f, string path, string localPath)
        {
            f.FilePath = path;
            f.LocalPath = localPath;
            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET FilePath=@path, UploadedFileName=@localPath WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@path", path);
            cmd.Parameters.AddWithValue("@localPath", localPath);
            cmd.Parameters.AddWithValue("@formId", f.FormId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            return f;
        }

        public static Form SubmitForm(int formId, string formName, string formData)
        {
            string defaultApproved = "0";
            string defaultDenied = "0";
            Form f = GetForm(formId);
            Form ft = GetFormTemplate(f.FormTemplateId);
            string[] approvalIDs = new string[0];
            if (ft.ApproverIDs.Length > 0)
            {
                approvalIDs = ft.ApproverIDs.Split(',');
            }

            if (approvalIDs.Length > 0)
            {
                int i = 0;
                foreach (string roleID in approvalIDs)
                {
                    if (i != 0)
                    {
                        defaultApproved += ",0";
                        defaultDenied += ",0";
                    }
                    i++;
                }
            }

            f.FormName = formName;
            f.FormData = formData;
            MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET FormName=@formName, FormData=@formData, Submission=1, Approved=@approved, Denied=@denied, DenialReason=\"\" WHERE FormID=@formId");
            cmd.Parameters.AddWithValue("@formName", formName);
            cmd.Parameters.AddWithValue("@formData", formData);
            cmd.Parameters.AddWithValue("@formId", formId);
            cmd.Parameters.AddWithValue("@denied", defaultDenied);
            cmd.Parameters.AddWithValue("@approved", defaultApproved);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            Project p = ProjectUtil.GetProject(f.ProjectId);
            FeedUtil.CreateProjectFormFeedItem(p.Name + " has form " + formName + " ready for your approval", p.CoachId, p.ProjectId, formId);
            return f;
        }

        public static Form ApproveForm(int formId, int roleId)
        {
            Form f = GetForm(formId);
            Form ft = GetFormTemplate(f.FormTemplateId);
            int approvalItemCt = -1;
            string[] approvalIDs = new string[0];
            if (ft.ApproverIDs.Length > 0)
            {
                approvalIDs = ft.ApproverIDs.Split(',');
            }

            if (approvalIDs.Length > 0)
            {
                int i = 0;
                foreach (string roleID in approvalIDs)
                {
                    int userRoleID = int.Parse(roleID);

                    //if they are one of the users who needs to approve the form
                    if(userRoleID == roleId)
                    {
                        approvalItemCt = i;
                    }
                    i++;
                }
            }

            if (approvalItemCt != -1)
            {
                string newApproval = "";
                string[] currentApproval = f.Approved.Split(',');
                if (currentApproval.Length > approvalItemCt)
                {
                    int i = 0;
                    foreach(string approval in currentApproval)
                    {
                        if( i == approvalItemCt)
                        {
                            newApproval += "1";
                        }
                        else
                        {
                            newApproval += currentApproval[i];
                        }

                        i++;

                        if(i != currentApproval.Length)
                        {
                            newApproval += ",";
                        }
                    }
                }

                MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET Approved=@approved WHERE FormID=@formId");
                cmd.Parameters.AddWithValue("@formId", formId);
                cmd.Parameters.AddWithValue("@approved", newApproval);
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
            }

            return f;
        }

        public static Form DenyForm(int formId, string denialReason, int roleId)
        {
            Form f = GetForm(formId);
            Form ft = GetFormTemplate(f.FormTemplateId);
            int denialItemCt = -1;
            string[] approvalIDs = new string[0];
            if (ft.ApproverIDs.Length > 0)
            {
                approvalIDs = ft.ApproverIDs.Split(',');
            }

            if (approvalIDs.Length > 0)
            {
                int i = 0;
                foreach (string roleID in approvalIDs)
                {
                    int userRoleID = int.Parse(roleID);

                    //if they are one of the users who needs to approve the form
                    if (userRoleID == roleId)
                    {
                        denialItemCt = i;
                    }
                    i++;
                }
            }

            if (denialItemCt != -1)
            {
                string newDenial = "";
                string[] currentDenial = f.Denied.Split(',');
                if (currentDenial.Length > denialItemCt)
                {
                    int i = 0;
                    foreach (string approval in currentDenial)
                    {
                        if (i == denialItemCt)
                        {
                            newDenial += "1";
                        }
                        else
                        {
                            newDenial += currentDenial[i];
                        }

                        i++;

                        if (i != currentDenial.Length)
                        {
                            newDenial += ",";
                        }
                    }
                }

                MySqlCommand cmd = new MySqlCommand("UPDATE Forms SET Denied=@denied, DenialReason=@denialReason, Submission=0 WHERE FormID=@formId");
                cmd.Parameters.AddWithValue("@denialReason", denialReason);
                cmd.Parameters.AddWithValue("@formId", formId);
                cmd.Parameters.AddWithValue("@denied", newDenial);
                DBConn conn = new DBConn();
                conn.ExecuteInsertCommand(cmd);
                conn.CloseConnection();
                Project p = ProjectUtil.GetProject(f.ProjectId);

                //notify all clients of the approval
                List<User> clients = UserUtil.GetClients(p.CompanyId);
                foreach (User client in clients)
                {
                    FeedUtil.CreateProjectFormFeedItem(f.FormName + " was denied by " + UserUtil.GetCoachName(p.CoachId) + " for " + denialReason, client.UserId, p.ProjectId, formId);
                }
            }

            return f;
        }

        public static List<Form> GetForms()
        {
            string query = "SELECT FormID, FormName, ProjectID from Forms WHERE FormID > 0";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Form> formList = new List<Form>();
            while (dr.Read())
            {
                Form f = new Form((int)dr["FormID"], (string)dr["FormName"], (int)dr["ProjectID"]);
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
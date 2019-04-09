using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using Workflow.Data;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow
{
    public partial class Forms : System.Web.UI.Page
    {
        int fieldCt { get; set; }
        int approvalCt { get; set; }
        private int count = 0;
        private String formNode = "";

        //prevents users from using back button to return to login protected pages
        protected override void OnInit(EventArgs e)
        {
            approvalCt = 1;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.FullName;

                if (user.RoleId == 1)
                {
                    CreateClientFormList(user.CompanyId);
                    CreateNewFormBtn.Visible = false;
                }
                else if (user.RoleId == 2)
                {
                    CreateFormList(user.UserId);
                    CreateNewFormBtn.Visible = false;
                }
                else if (user.RoleId == 4)
                {
                    AdminBtn.Visible = true;
                }
                if (user.RoleId == 4 || user.RoleId == 3)
                {
                    tabMenu.Visible = true;
                    FormTab.Visible = true;
                    TemplateTab.Visible = true;

                    //creates the template list and the full list of forms
                    if (Request.QueryString["templates"] != null)
                    {
                        CreateAdminFormList();
                        FormTab.CssClass = "item";
                        TemplateTab.CssClass = "item active";
                    }
                    else
                    {
                        CreateFullFormList();
                        FormTab.CssClass = "item active";
                        TemplateTab.CssClass = "item";
                    }
                }

                //loads the selected form if there is one
                if (Request.QueryString["fid"] != null)
                {
                    int formId = int.Parse(Request.QueryString["fid"]);
                    Form f = FormUtil.GetFormTemplate(formId);
                    FormNameLbl.Text = " <a href='Forms.aspx?templates=1'>Forms</a> > " + f.FormName;

                    //admin and trying to del
                    if (Request.QueryString["del"] != null && user.RoleId == 4)
                    {
                        if (FormUtil.DeleteForm(f.FormId))
                        {
                            Log.Info(user.Identity + " deleted a form template " + f.FormName);
                            ReloadSection();
                        }
                        else
                        {
                            FormError.Visible = true;
                            FormError.Text = "Unable to delete Form " + f.FormName;
                        }
                    }
                    else
                    {
                        //if they are trying to edit and they are admin, show form builder
                        if (Request.QueryString["edit"] != null && user.RoleId == 4)
                        {
                            ShowFormBuilder(f, user.RoleId);
                        }
                        //otherwise just show the form viewer
                        else
                        {
                            //disable the save/submit buttons for form templates
                            SubmitFormBtn.Visible = false;
                            SaveFormBtn.Visible = false;
                            ShowFormViewer(f, user.RoleId);
                        }
                    }
                }
                //if theyre a client trying to fill out a form
                else if (Request.QueryString["pfid"] != null)
                {
                    int formId = int.Parse(Request.QueryString["pfid"]);
                    Form f = FormUtil.GetForm(formId);
                    Project p = ProjectUtil.GetProject(f.ProjectId);
                    FormNameLbl.Text = "<a href='Forms.aspx'>Forms</a> > <a href='Projects.aspx?pid=" + f.ProjectId + "'>" + p.Name + "</a> > " + f.FormName;
                    ShowFormViewer(f, user.RoleId);
                }
                //if theyre an admin and trying to make a new form
                else if (Request.QueryString["edit"] != null && Request.QueryString["fid"] == null && user.RoleId == 4)
                {
                    formListing.Visible = false;
                    formBuilder.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void DashboardBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void ProjectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void FormBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        protected void WorkflowBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

        private void ShowFormBuilder(Form f, int roleId)
        {
            formListing.Visible = false;
            formBuilder.Visible = true;
            if (formBuilderData.Value.Length == 0 || formBuilderData.Value == "undefined")
            {
                formBuilderData.Value = f.FormData;
            }
            FormName.Text = f.FormName;

            string[] approverIDs = f.ApproverIDs.Split(',');

            //form approval selectors
            FormApproval1.DataSource = RoleUtil.GetRoles();
            FormApproval1.DataValueField = "RoleId";
            FormApproval1.DataTextField = "RoleName";
            FormApproval1.DataBind();
            if (f.ApproverIDs.Length > 0)
            {
                FormApproval1.SelectedIndex = int.Parse(approverIDs[0]);
                FormApproval2.Visible = true;
            }

            FormApproval2.DataSource = RoleUtil.GetRoles();
            FormApproval2.DataValueField = "RoleId";
            FormApproval2.DataTextField = "RoleName";
            FormApproval2.DataBind();
            if (approverIDs.Length > 1)
            {
                FormApproval2.SelectedIndex = int.Parse(approverIDs[1]);
                FormApproval3.Visible = true;
            }

            FormApproval3.DataSource = RoleUtil.GetRoles();
            FormApproval3.DataValueField = "RoleId";
            FormApproval3.DataTextField = "RoleName";
            FormApproval3.DataBind();
            if (approverIDs.Length > 2)
            {
                FormApproval3.SelectedIndex = int.Parse(approverIDs[2]);
                FormApproval4.Visible = true;
            }

            FormApproval4.DataSource = RoleUtil.GetRoles();
            FormApproval4.DataValueField = "RoleId";
            FormApproval4.DataTextField = "RoleName";
            FormApproval4.DataBind();
            if (approverIDs.Length > 3)
            {
                FormApproval4.SelectedIndex = int.Parse(approverIDs[3]);
            }

            CreateFormBtn.Text = "Update Form";
        }

        private void ShowFormViewer(Form f, int roleId)
        {
            FormResult2.Visible = false;
            formListing.Visible = false;
            formBuilder.Visible = false;
            formViewer.Visible = true;
            try
            {
                if (formViewerData.Value.Length == 0 || formViewerData.Value == "undefined")
                {
                    formViewerData.Value = f.FormData;
                }

                if (roleId == 1)
                {
                    if (f.FilePath.Length > 0 && f.LocalPath.Length > 0)
                    {
                        uploadedFiles.Visible = true;
                        UploadedName.Text = f.LocalPath;
                    }
                }
                else if (roleId > 1)
                {
                    if (Request.QueryString["pfid"] != null)
                    {
                        if (f.FilePath.Length > 0 && f.LocalPath.Length > 0)
                        {
                            coachUploadedFiles.Visible = true;
                            CoachUploadedName.Text = f.LocalPath;
                        }
                    }
                }

                string[] approvalIDs = null;
                if (Request.QueryString["pfid"] != null)
                {
                    Form ft = FormUtil.GetFormTemplate(f.FormTemplateId);
                    if (ft.ApproverIDs.Length > 0)
                    {
                        approvalIDs = ft.ApproverIDs.Split(',');
                        approvalLabel1.Text = RoleUtil.GetRoleName(int.Parse(approvalIDs[0]));
                        approvalLabel1.Visible = true;
                        if (approvalIDs.Length > 1)
                        {
                            approvalLabel2.Text = RoleUtil.GetRoleName(int.Parse(approvalIDs[1]));
                            approvalLabel2.Visible = true;
                        }
                        if (approvalIDs.Length > 2)
                        {
                            approvalLabel3.Text = RoleUtil.GetRoleName(int.Parse(approvalIDs[2]));
                            approvalLabel3.Visible = true;
                        }
                        if (approvalIDs.Length > 3)
                        {
                            approvalLabel4.Text = RoleUtil.GetRoleName(int.Parse(approvalIDs[3]));
                            approvalLabel4.Visible = true;
                        }
                    }
                }

                //the form has been submitted already
                //lock it all down and get rid of submit/save btns
                if (f.DenialReason.Length > 0)
                {
                    FormResult2.Visible = true;
                    FormResult2.CssClass = "error";
                    FormResult2.Text = "Denied :" + f.DenialReason;
                }

                if (f.Submission == 1)
                {
                    formLocking.Visible = true;
                    SubmitFormBtn.Visible = false;
                    SaveFormBtn.Visible = false;

                    //form has been approved
                    string[] approvals = f.Approved.Split(',');
                    string[] denials = f.Denied.Split(',');

                    FormResult2.Visible = true;
                    FormResult2.CssClass = "success";
                    FormResult2.Text = "Submitted: waiting for approval";

                    //if its a required approver viewing the form,
                    //show approve / deny buttons
                    foreach (string approvalRoleID in approvalIDs)
                    {
                        int appRoleID = int.Parse(approvalRoleID);
                        if (roleId == appRoleID)
                        {
                            ApproveFormBtn.Visible = true;
                            DenyFormBtn.Visible = true;
                            DenyReason.Visible = true;
                        }
                    }

                    if (approvals.Length > 0)
                    {
                        int i = 0;
                        foreach (string approval in approvals)
                        {
                            if (i == 0 && approval == "0")
                            {
                                approvalLabel1.Text += " - Waiting on Approval";
                            }
                            else if (i == 0 && approval == "1")
                            {
                                approvalLabel1.Text += " - Approved!";
                                approvalLabel1.CssClass = "success";
                            }

                            if (i == 1 && approval == "0")
                            {
                                approvalLabel2.Text += " - Waiting on Approval";
                            }
                            else if (i == 1 && approval == "1")
                            {
                                approvalLabel2.Text += " - Approved!";
                                approvalLabel2.CssClass = "success";
                            }

                            if (i == 2 && approval == "0")
                            {
                                approvalLabel3.Text += " - Waiting on Approval";
                            }
                            else if (i == 2 && approval == "1")
                            {
                                approvalLabel3.Text += " - Approved!";
                                approvalLabel3.CssClass = "success";
                            }

                            if (i == 3 && approval == "0")
                            {
                                approvalLabel4.Text += " - Waiting on Approval";
                            }
                            else if (i == 3 && approval == "1")
                            {
                                approvalLabel4.Text += " - Approved!";
                                approvalLabel4.CssClass = "success";
                            }

                            i++;
                        }
                    }
                    //form has been denied and the reason
                    else if (denials.Length > 0)
                    {
                        int i = 0;
                        foreach (string denial in denials)
                        {
                            if (i == 0 && denial == "1")
                            {
                                approvalLabel1.Text += " - Denied!";
                                approvalLabel1.CssClass = "error";
                            }

                            if (i == 1 && denial == "1")
                            {
                                approvalLabel2.Text += " - Denied!";
                                approvalLabel2.CssClass = "error";
                            }

                            if (i == 2 && denial == "1")
                            {
                                approvalLabel3.Text += " - Denied!";
                                approvalLabel3.CssClass = "error";
                            }

                            if (i == 3 && denial == "1")
                            {
                                approvalLabel4.Text += " - Denied!";
                                approvalLabel4.CssClass = "error";
                            }

                            i++;
                        }
                        //FormResult2.Visible = true;
                        //FormResult2.CssClass = "success";
                        //FormResult2.Text = "APPROVED";
                    }
                    /*
                    else if (f.Denied == 1)
                    {
                        FormResult2.Visible = true;
                        FormResult2.Text = "DENIED: " + f.DenialReason;
                    }
                    */
                }
            }
            catch (Exception e) { }
        }

        private void ReloadSection()
        {
            Response.Redirect("Forms.aspx");
        }

        private void ReloadCurrentPage()
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void TemplateBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx?templates=1");
        }

        private void CreateAdminFormList()
        {
            formNode = "";
            formList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Form> forms = FormUtil.GetFormTemplates();
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                MakeAdminText(forms, formNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateClientFormList(int companyId)
        {
            CreateNewFormBtn.Visible = false;
            formNode = "";
            formList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Form> forms = FormUtil.GetCompanyForms(companyId);
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                MakeText(forms, formNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateFullFormList()
        {
            CreateNewFormBtn.Visible = false;
            formNode = "";
            formList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Form> forms = FormUtil.GetForms();
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                MakeText(forms, formNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateFormList(int userId)
        {
            formNode = "";
            formList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Form> forms = FormUtil.GetCoachForms(userId);
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                MakeText(forms, formNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        protected void LoadMoreForms(object sender, EventArgs e)
        {
            ViewState["formcount"] = Convert.ToInt32(ViewState["formcount"]) + 1;
            int loaded = Convert.ToInt32(ViewState["formcount"]);

            List<Form> forms = new List<Form>();
            User user = (User)Session["User"];

            if (user.RoleId == 1)
            {
                forms = FormUtil.GetCompanyForms(user.CompanyId);
            }
            else if (user.RoleId == 2)
            {
                forms = FormUtil.GetCoachForms(user.UserId);
            }
            else if (user.RoleId == 4 || user.RoleId == 3)
            {
                if (Request.QueryString["templates"] != null)
                {
                    forms = FormUtil.GetFormTemplates();
                }
                else
                {
                    forms = FormUtil.GetForms();
                }
            }

            if (loaded == 1)
            {
                ViewState["formcount"] = Convert.ToInt32(ViewState["formcount"]) + 1;
                loaded = Convert.ToInt32(ViewState["formcount"]);
            }
            for (int i = 5; i < loaded * 5 && i < forms.Count; i++)
            {
                if (user.RoleId == 4)
                {
                    if (Request.QueryString["templates"] != null)
                    {
                        MakeAdminText(forms, formNode, i);
                    }
                    else
                    {
                        MakeText(forms, formNode, i);
                    }
                }
                else
                {
                    MakeText(forms, formNode, i);
                }
            }

            numberShowing.InnerHtml = "";
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void MakeAdminText(List<Form> forms, String formNode, int i)
        {
            var name = "";
            try
            {
                name = ProjectUtil.GetProject(forms[i].ProjectId).Name;
            }
            catch (Exception e) { }
            formNode = "<div class=\"item\"><div class=\"ui small image\"><i class=\"huge file icon\"/></i></div>";
            formNode += "<div class=\"content\"><a class=\"header\" href='Forms.aspx?fid=" + forms[i].FormId + "'>" + forms[i].FormName + "</a><div class=\"meta\">";
            formNode += "<span class=\"stay\"><a href='Forms.aspx?fid=" + forms[i].FormId + "&edit=1'>Edit Form</a>" + " | ";
            formNode += "<a href='Forms.aspx?fid=" + forms[i].FormId + "&del=1'>Delete Form</a>" + "</span></div></div></div>";
            formList.InnerHtml += formNode;
            count++;
        }

        private void MakeText(List<Form> forms, String formNode, int i)
        {
            var name = "";
            try
            {
                name = ProjectUtil.GetProject(forms[i].ProjectId).Name;
            }
            catch (Exception e) { }
            formNode = "<div class=\"item\"><div class=\"ui small image\"><i class=\"huge file icon\"/></i></div>";
            formNode += "<div class=\"content\"><a class=\"header\" href='Forms.aspx?pfid=" + forms[i].FormId + "'>" + forms[i].FormName + "</a> | " + name + "<div class=\"meta\">";
            formNode += "</div></div></div>";
            formList.InnerHtml += formNode;
            count++;
        }

        //admin / director creating form
        protected void CreateFormBtn_Click(object sender, EventArgs e)
        {
            FormResult.Visible = false;

            if (FormName.Text.Length > 0)
            {
                string formJson = formBuilderData.Value.ToString();

                if (formJson.Length > 0 && formJson != "undefined")
                {
                    User user = (User)Session["User"];
                    //updating a form not, creating it
                    if (Request.QueryString["fid"] != null)
                    {
                        int formId = int.Parse(Request.QueryString["fid"]);
                        FormUtil.UpdateFormTemplate(formId, FormName.Text, formJson);
                        Log.Info(user.Identity + " updated a form template " + FormName.Text + " with " + formJson);
                        FormResult.CssClass = "success";
                        FormResult.Text = "Updated form " + FormName.Text;
                        Response.Redirect("Forms.aspx?fid=" + formId);
                    }
                    else
                    {
                        Form f = FormUtil.CreateFormTemplate(FormName.Text, formJson);
                        Log.Info(user.Identity + " created a form template " + FormName.Text + " with " + formJson);
                        FormResult.CssClass = "success";
                        FormResult.Text = "Created form " + FormName.Text;
                        Response.Redirect("Forms.aspx?fid=" + f.FormId);
                    }
                }
                else
                {
                    FormResult.CssClass = "error";
                    FormResult.Text = "Please add elements to the form";
                }
            }
            else
            {
                FormResult.CssClass = "error";
                FormResult.Text = "Please enter a form name";
            }
            FormResult.Visible = true;
        }

        //client saving form
        protected void SaveFormBtn_Click(object sender, EventArgs e)
        {
            SaveForm();
            Response.Redirect(Request.RawUrl);
        }

        private void SaveForm()
        {
            FormResult.Visible = false;
            string formJson = formViewerData.Value.ToString();

            if (formJson.Length > 0 && formJson != "undefined")
            {
                //updating a form not, creating it
                if (Request.QueryString["pfid"] != null)
                {
                    int formId = int.Parse(Request.QueryString["pfid"]);
                    Form f = FormUtil.GetForm(formId);
                    Project p = ProjectUtil.GetProject(f.ProjectId);
                    FormUtil.UpdateForm(formId, f.FormName, formJson);
                    User user = (User)Session["User"];
                    Log.Info(user.Identity + " edited " + CompanyUtil.GetCompanyName(user.CompanyId) + " a form" + f.FormName + " from project " + p.Name + " with " + formJson);

                    if (fileInputName.Value.ToString().Length > 0)
                    {
                        string localName = fileUploadName.Value.ToString();
                        string fileType = localName.Split('.')[1];
                        string path = CompanyUtil.GetCompanyName(user.CompanyId) + "-" + p.Name + "-" + f.FormName + "." + fileType;
                        SaveFiles(path);
                        f = FormUtil.UpdateFormFile(f, path, localName);
                        Log.Info(user.Identity + " edited " + CompanyUtil.GetCompanyName(user.CompanyId) + " a form" + f.FormName + " from project " + p.Name + " added a file " + f.FilePath);
                    }
                    FormResult.CssClass = "success";
                    FormResult.Text = "Updated form " + f.FormName;
                }
            }
            else
            {
                FormResult.CssClass = "error";
                FormResult.Text = "Please fill out the form";
            }
            FormResult.Visible = true;
        }

        private void SaveFiles(string path)
        {
            string inputName = fileInputName.Value.ToString();
            if (inputName.Length > 0)
            {
                HttpPostedFile file = Request.Files[inputName];
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(path);
                }
            }
        }

        //client submitting form
        protected void SubmitFormBtn_Click(object sender, EventArgs e)
        {
            SaveForm();
            FormResult.Visible = false;
            string formJson = formViewerData.Value.ToString();

            if (formJson.Length > 0 && formJson != "undefined")
            {
                //updating a form not, creating it
                if (Request.QueryString["pfid"] != null)
                {
                    int formId = int.Parse(Request.QueryString["pfid"]);
                    Form f = FormUtil.GetForm(formId);
                    FormUtil.SubmitForm(formId, f.FormName, formJson);
                    User user = (User)Session["User"];
                    Log.Info(user.Identity + " submitted " + CompanyUtil.GetCompanyName(user.CompanyId) + "'s form " + f.FormName + " with " + formJson);
                    FormResult.CssClass = "success";
                    FormResult.Text = "Submitted form " + f.FormName;
                    Response.Redirect("Forms.aspx?pfid=" + formId);
                }
            }
            else
            {
                FormResult.CssClass = "error";
                FormResult.Text = "Please fill out the form";
            }
            FormResult.Visible = true;
        }

        protected void CreateNewFormBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx?edit=1");
        }

        protected void ApproveFormBtn_Click(object sender, EventArgs e)
        {
            FormResult.Visible = false;
            //updating a form not, creating it
            if (Request.QueryString["pfid"] != null)
            {
                int formId = int.Parse(Request.QueryString["pfid"]);
                Form f = FormUtil.GetForm(formId);
                Project p = ProjectUtil.GetProject(f.ProjectId);
                WorkflowModel w = WorkflowUtil.GetWorkflow(p.WorkflowId);

                User user = (User)Session["User"];
                FormUtil.ApproveForm(formId, user.RoleId);
                Log.Info(user.Identity + " approved " + CompanyUtil.GetCompanyName(p.CompanyId) + "'s form " + f.FormName + " - " + p.Name);
                FormResult.CssClass = "success";
                FormResult.Text = "Approved form " + f.FormName;
                FormResult.Visible = true;

                //pdf generation
                HtmlDocument doc = new HtmlDocument();
                string pdfName = string.Format("{0} - {1} - {2}", w.WorkflowName, f.FormName, CompanyUtil.GetCompanyName(p.CompanyId));
                string html = formViewerData.Value;
                if (html.Contains("user-data"))
                {
                    html = html.Replace("user-data", "value");
                }
                if (html.Contains("\""))
                {
                    html = html.Replace("\"", "'");
                }
                doc.LoadHtml(html);
                doc.Save("PDFGen/" + CompanyUtil.GetCompanyName(p.CompanyId) + "_" + f.FormName + "_" + p.Name + ".html");

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//input[@value]"))
                {
                    HtmlAttribute value = link.Attributes["value"];
                    if (link.Attributes.Contains("placeholder"))
                    {
                        link.Attributes.Remove("placeholder");
                    }
                    string val = value.Value;
                    link.InnerHtml = val;
                    link.Attributes.Remove("value");
                }

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//textarea[@value]"))
                {
                    HtmlAttribute value = link.Attributes["value"];
                    if (link.Attributes.Contains("placeholder"))
                    {
                        link.Attributes.Remove("placeholder");
                    }
                    string val = value.Value;
                    link.InnerHtml = val;
                    link.Attributes.Remove("value");
                }
                doc.Save("PDFGen/" + CompanyUtil.GetCompanyName(p.CompanyId) + "_" + f.FormName + "_" + p.Name + ".html");
                doc.Load("PDFGen/" + CompanyUtil.GetCompanyName(p.CompanyId) + "_" + f.FormName + "_" + p.Name + ".html");
                html = doc.Text;


                PDFGen.CreateHTMLPDF(html, pdfName);
                Response.Redirect("Forms.aspx?pfid=" + formId);
                PDFGen.CreateHTMLPDF(html, pdfName);
                Response.Redirect("Forms.aspx?pfid=" + formId);
            }
        }

        protected void DenyFormBtn_Click(object sender, EventArgs e)
        {
            FormResult.Visible = false;
            //updating a form not, creating it
            if (Request.QueryString["pfid"] != null)
            {
                string denyText = DenyReason.Text;
                int formId = int.Parse(Request.QueryString["pfid"]);
                Form f = FormUtil.GetForm(formId);
                Project p = ProjectUtil.GetProject(f.ProjectId);

                User user = (User)Session["User"];
                FormUtil.DenyForm(formId, denyText, user.RoleId);
                FormResult.CssClass = "success";
                FormResult.Text = "Denied form " + f.FormName;
                if (denyText.Length > 0)
                {
                    FormResult.Text += ": " + denyText;
                }
                else
                {
                    denyText = "None specified";
                }
                Log.Info(user.Identity + " denied " + CompanyUtil.GetCompanyName(p.CompanyId) + "'s form " + f.FormName + " - " + p.Name + " with reason: " + denyText);
                Response.Redirect("Forms.aspx?pfid=" + formId);
                FormResult.Visible = true;
            }
        }

        protected void TemplateTab_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["templates"] == null)
            {
                Response.Redirect("Forms.aspx?templates=1");
            }
        }

        protected void FormTab_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["templates"] != null)
            {
                Response.Redirect("Forms.aspx");
            }
        }

        protected void CoachDownloadBtn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["pfid"] != null)
            {
                int formId = int.Parse(Request.QueryString["pfid"]);
                Form f = FormUtil.GetForm(formId);
                Project p = ProjectUtil.GetProject(f.ProjectId);
                FormResult.CssClass = "success";
                FormResult.Text = "Denied form " + f.FormName;

                User user = (User)Session["User"];
                Log.Info(user.Identity + " downloaded files from " + CompanyUtil.GetCompanyName(p.CompanyId) + "'s form " + f.FormName + " - " + p.Name);
                SendFile(f.FilePath);
                FormResult.Visible = true;
            }
        }

        private void SendFile(string path)
        {
            FileInfo f = new FileInfo(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", path));
            Response.AddHeader("Content-Length", f.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(f.FullName);
            Response.End();
        }

        protected void FormApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["fid"] != null)
            {
                int formId = int.Parse(Request.QueryString["fid"]);
                Form f = FormUtil.GetForm(formId);

                string approvalIDs = "";
                if (SelectedApprover1.Value != "-1")
                {
                    FormApproval2.Visible = true;
                    approvalIDs += SelectedApprover1.Value;
                }
                if (SelectedApprover2.Value != "-1")
                {
                    FormApproval3.Visible = true;
                    approvalIDs += "," + SelectedApprover2.Value;
                }
                if (SelectedApprover3.Value != "-1")
                {
                    FormApproval4.Visible = true;
                    approvalIDs += "," + SelectedApprover3.Value;
                }
                if (SelectedApprover4.Value != "-1")
                {
                    approvalIDs += "," + SelectedApprover4.Value;
                }

                FormUtil.UpdateFormTemplateApprovers(formId, approvalIDs);
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow
{
    public partial class Forms : System.Web.UI.Page
    {
        int fieldCt { get; set; }
        int approvalCt { get; set; }

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
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.FullName;

                if (user.RoleId == 4)
                {
                    CreateAdminFormList();
                }
                else if (user.RoleId == 1)
                {
                    CreateClientFormList(user.CompanyId);
                }
                else
                {
                    CreateFormList();
                }

                //loads the selected form if there is one
                if (Request.QueryString["fid"] != null)
                {
                    int formId = int.Parse(Request.QueryString["fid"]);
                    Form f = FormUtil.GetFormTemplate(formId);

                    //admin and trying to del
                    if (Request.QueryString["del"] != null && user.RoleId == 4)
                    {
                        if (FormUtil.DeleteForm(f.FormId))
                        {
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
                            formListing.Visible = false;
                            formBuilder.Visible = true;
                            formBuilderData.Value = f.FormData;
                            FormName.Text = f.FormName;
                            CreateFormBtn.Text = "Update Form";
                        }
                        //otherwise just show the form viewer
                        else
                        {
                            ShowFormViewer(f);
                        }
                    }
                }
                //if theyre a client trying to fill out a form
                else if (Request.QueryString["pfid"] != null)
                {
                    int formId = int.Parse(Request.QueryString["pfid"]);
                    Form f = FormUtil.GetForm(formId);
                    ShowClientFormViewer(f);
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
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        private void ShowFormViewer(Form f)
        {
            formListing.Visible = false;
            formBuilder.Visible = false;
            formViewer.Visible = true;
            formViewerData.Value = f.FormData;
            FormNameLbl.Text = f.FormName;
        }

        private void ShowClientFormViewer(Form f)
        {
            ShowFormViewer(f);
            SaveFormBtn.Visible = false;
        }

        private void ReloadSection()
        {
            Response.Redirect("Forms.aspx");
        }

        private void ReloadCurrentPage()
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void DashboardBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void ProjectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void WorkflowBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        protected void FormBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        protected void LogoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        private void CreateAdminFormList()
        {
            var formNode = "";
            List<Form> forms = FormUtil.GetFormTemplates();
            var count = 0;
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                formNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/form.png\"/></div>";
                formNode += "<div class=\"content\"><a class=\"header\">" + forms[i].FormName + "</a><div class=\"meta\">";
                formNode += "<span class=\"stay\">" + "<a href='Forms.aspx?fid=" + forms[i].FormId + "'>View Form</a>" + " | ";
                formNode += "<a href='Forms.aspx?fid=" + forms[i].FormId + "&edit=1'>Edit Form</a>" + " | ";
                formNode += "<a href='Forms.aspx?fid=" + forms[i].FormId + "&del=1'>Delete Form</a>" + "</span></div></div></div>";
                formList.InnerHtml += formNode;
                count++;
            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateClientFormList(int companyId)
        {
            CreateNewFormBtn.Visible = false;
            var formNode = "";
            List<Form> forms = FormUtil.GetCompanyForms(companyId);
            var count = 0;
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                formNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/form.png\"/></div>";
                formNode += "<div class=\"content\"><a class=\"header\">" + forms[i].FormName + "</a> | " + ProjectUtil.GetProject(forms[i].ProjectId).Name + "<div class=\"meta\">";
                formNode += "<span class=\"stay\">" + "<a href='Forms.aspx?pfid=" + forms[i].FormId + "'>View Form</a>" + "</span></div></div></div>";
                formList.InnerHtml += formNode;
                count++;

            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateFormList()
        {
            var formNode = "";
            List<Form> forms = FormUtil.GetFormTemplates();
            var count = 0;
            for (int i = 0; i < 5 && i < forms.Count; i++)
            {
                formNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/form.png\"/></div>";
                formNode += "<div class=\"content\"><a class=\"header\">" + forms[i].FormName + "</a><div class=\"meta\">";
                formNode += "<span class=\"stay\">" + "<a href='Forms.aspx?fid=" + forms[i].FormId + "'>View Form</a>" + "</span></div></div></div>";
                formList.InnerHtml += formNode;
                count++;

            }
            var showing = "Showing 1 - " + count + " of " + forms.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        protected void CreateFormBtn_Click(object sender, EventArgs e)
        {
            FormResult.Visible = false;

            if(FormName.Text.Length > 0)
            {
                string formJson = formBuilderData.Value.ToString();

                if(formJson.Length > 0)
                {
                    //updating a form not, creating it
                    if (Request.QueryString["fid"] != null)
                    {
                        int formId = int.Parse(Request.QueryString["fid"]);
                        FormUtil.UpdateFormTemplate(formId, FormName.Text, formJson);
                        FormResult.CssClass = "success";
                        FormResult.Text = "Updated form " + FormName.Text;
                        Response.Redirect("Forms.aspx?fid="+ formId);
                    }
                    else
                    {
                        Form f = FormUtil.CreateFormTemplate(FormName.Text, formJson);
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

        protected void SaveFormBtn_Click(object sender, EventArgs e)
        {
            FormResult.Visible = false;

            if (FormName.Text.Length > 0)
            {
                string formJson = formBuilderData.Value.ToString();

                if (formJson.Length > 0)
                {
                    //updating a form not, creating it
                    if (Request.QueryString["pfid"] != null)
                    {
                        int formId = int.Parse(Request.QueryString["pfid"]);
                        FormUtil.UpdateFormTemplate(formId, FormName.Text, formJson);
                        FormResult.CssClass = "success";
                        FormResult.Text = "Updated form " + FormName.Text;
                        Response.Redirect("Forms.aspx?pfid=" + formId);
                    }
                    else
                    {
                        Form f = FormUtil.CreateFormTemplate(FormName.Text, formJson);
                        FormResult.CssClass = "success";
                        FormResult.Text = "Created form " + FormName.Text;
                        Response.Redirect("Forms.aspx?pfid=" + f.FormId);
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

        protected void SubmitFormBtn_Click(object sender, EventArgs e)
        {
            string formHtml = formBuilderData.Value.ToString();
            PDFGen.CreateHTMLPDF(formHtml, "tests");
        }

        protected void CreateNewFormBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx?edit=1");
        }
    }
}
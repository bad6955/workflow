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
                userLbl.Text = user.Email;

                CreateFormList();

                //loads the selected form if there is one
                if (Request.QueryString["fid"] != null)
                {
                    formListing.Visible = false;
                    int formId = int.Parse(Request.QueryString["fid"]);
                    Form f = FormUtil.GetFormTemplate(formId);

                    //if they are trying to edit and they are admin, show form builder
                    if (Request.QueryString["edit"] != null && user.RoleId == 4)
                    {
                        formBuilder.Visible = true;
                        formBuilderData.Value = f.FormData;
                    }
                    //otherwise just show the form viewer
                    else
                    {
                        formViewer.Visible = true;
                        formViewerData.Value = f.FormData;
                    }
                }
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
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

        protected void LogoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
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
                formNode += "<span class=\"stay\">" + "<a href='Forms.aspx?fid="+forms[i].FormId+"&edit=1'>Edit Form</a>" + " | " + "<a href='Forms.aspx?fid=" + forms[i].FormId + "'>View Form</a>" + "</span></div></div></div>";
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
                    FormUtil.CreateFormTemplate(FormName.Text, formJson);
                    FormResult.CssClass = "success";
                    FormResult.Text = "Created form " + FormName.Text;
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
            
        }

        protected void SubmitFormBtn_Click(object sender, EventArgs e)
        {
            string formHtml = formBuilderData.Value.ToString();
            PDFGen.CreateHTMLPDF(formHtml, "tests");
        }
    }
}
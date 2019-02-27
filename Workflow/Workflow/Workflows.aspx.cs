using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;

namespace Workflow
{
    public partial class Workflows : System.Web.UI.Page
    {
        //prevents users from using back button to return to login protected pages
        protected override void OnInit(EventArgs e)
        {
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
                    CreateAdminWorkflowList();
                }
                else
                {
                    CreateWorkflowList();
                }

                //loads the selected form if there is one
                if (Request.QueryString["wid"] != null)
                {
                    workflowListing.Visible = false;
                    int workflowId = int.Parse(Request.QueryString["wid"]);
                    WorkflowModel w = WorkflowUtil.GetWorkflow(workflowId);

                    //if they are trying to edit and they are admin, show form builder
                    if (Request.QueryString["edit"] != null && user.RoleId == 4)
                    {
                        workflowBuilder.Visible = true;
                        WorkflowName.Text = w.WorkflowName;
                        CreateWorkflowBtn.Text = "Update Workflow";
                    }
                    //otherwise just show the form viewer
                    else
                    {
                        workflowViewer.Visible = true;
                    }
                }
                //if theyre an admin and trying to make a new form
                else if (Request.QueryString["edit"] != null && Request.QueryString["wid"] == null && user.RoleId == 4)
                {
                    workflowListing.Visible = false;
                    workflowBuilder.Visible = true;
                }
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        private void CreateAdminWorkflowList()
        {
            var workflowNode = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetWorkflows();
            var count = 0;
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                workflowNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/workflow.png\"/></div>";
                workflowNode += "<div class=\"content\"><a class=\"header\">" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
                workflowNode += "<span class=\"stay\">" + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "&edit=1'>Edit Workflow</a>" + " | " + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>View Workflow</a>" + "</span></div></div></div>";
                workflowList.InnerHtml += workflowNode;
                count++;
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateWorkflowList()
        {
            var workflowNode = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetWorkflows();
            var count = 0;
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                workflowNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/workflow.png\"/></div>";
                workflowNode += "<div class=\"content\"><a class=\"header\">" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
                workflowNode += "<span class=\"stay\">" + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>View Workflow</a>" + "</span></div></div></div>";
                workflowList.InnerHtml += workflowNode;
                count++;
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
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

        protected void CreateWorkflowBtn_Click(object sender, EventArgs e)
        {
            if(WorkflowName.Text.Length > 0)
            {
                WorkflowUtil.CreateWorkflow(WorkflowName.Text);
            }
            else
            {
                //enter valid name
            }
        }

        protected void CreateNewWorkflowBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx?edit=1");
        }
    }
}
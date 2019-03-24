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
    public partial class NavMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.FullName;
                if (user.RoleId == 4)
                {
                    AdminBtn.Visible = true;
                }
            }
            //kicks them out if they arent
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

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}
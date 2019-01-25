using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Models;

namespace Workflow
{
    public partial class Projects : System.Web.UI.Page
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

                //checks user is an admin
                if (user.RoleId == 4)
                {
                    //sets up admin project creation form

                    //fills out company dropdown
                    CompanySelect.DataSource = Data.CompanyUtil.GetClientCompanies();
                    CompanySelect.DataTextField = "companyName";
                    CompanySelect.DataValueField = "companyId";
                    CompanySelect.DataBind();
                    CompanySelect.SelectedIndex = 0;

                    //fills out workflow dropdown
                    WorkflowSelect.DataSource = Data.WorkflowUtil.GetWorkflows();
                    WorkflowSelect.DataTextField = "workflowName";
                    WorkflowSelect.DataValueField = "workflowId";
                    WorkflowSelect.DataBind();
                    WorkflowSelect.SelectedIndex = 0;

                    //fills out company dropdown
                    CoachSelect.DataSource = Data.UserUtil.GetCoaches();
                    CoachSelect.DataTextField = "fullName";
                    CoachSelect.DataValueField = "userId";
                    CoachSelect.DataBind();
                    CoachSelect.SelectedIndex = 0;

                    adminDiv.Visible = true;
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

        protected void CreateProjectBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
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
                userLbl.Text = user.Email;

                CreateProjectList();

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

        private void CreateProjectList()
        {
            var projectNode = "";
            List<Project> projects = ProjectUtil.GetProjects();
            var count = 0;
            for (int i = 0; i < 5 && i < projects.Count; i++)
            {
                User coach = UserUtil.GetProjectCoach(projects[i].CoachId);
                WorkflowModel workflow = WorkflowUtil.GetSingleWorkflow(projects[i].WorkflowId);
                projectNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/project.png\"/></div>";
                projectNode += "<div class=\"content\"><a class=\"header\">" + projects[i].Name + "</a><div class=\"meta\">";
                projectNode += "<span class=\"stay\">" + coach.FullName + " | " + workflow.WorkflowName + "</span></div><div class=\"description\">";
                projectNode += projects[i].Notes + "</div></div></div>";
                projectList.InnerHtml += projectNode;
                count++;

            }
            var showing = "Showing 1 - "+ count +" of " + projects.Count + " Results";
            numberShowing.InnerHtml += showing;
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
            int companyId = int.Parse(SelectedCompany.Value);
            int workflowId = int.Parse(SelectedWorkflow.Value);
            int coachId = int.Parse(SelectedCoach.Value);
            string projectName = ProjectName.Text;
            string projectNotes = ProjectNotes.Text;

            if (projectName.Length > 0)
            {
                if(companyId != -1)
                {
                    if (workflowId != -1)
                    {
                        if (coachId != -1)
                        {
                            ProjectUtil.CreateProject(projectName, workflowId, companyId, coachId, projectNotes);
                        }
                    }
                }
            }
            else
            {
                //enter valid name
            }
        }
    }
}
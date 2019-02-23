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
        private int count = 0;
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

                //loads the selected form if there is one
                if (Request.QueryString["pid"] != null)
                {
                    projectListing.Visible = false;
                    int projId = int.Parse(Request.QueryString["pid"]);
                    Project p = ProjectUtil.GetProject(projId);


                    //if they are trying to edit and they are admin, show project builder
                    if (Request.QueryString["edit"] != null && user.RoleId == 4)
                    {
                        projectBuilder.Visible = true;
                        GenerateProjectDropdowns();
                        CreateProjectBtn.Text = "Update Project";
                        ProjectName.Text = p.Name;
                        CompanySelect.SelectedValue = p.CompanyId.ToString();
                        CoachSelect.SelectedValue = p.CoachId.ToString();
                        WorkflowSelect.SelectedValue = p.WorkflowId.ToString();
                        ProjectNotes.Text = p.Notes;
                    }
                    //otherwise just show the project viewer
                    else
                    {
                        projectViewer.Visible = true;
                        ProjectViewerName.Text = p.Name;
                        CompanyName.Text = CompanyUtil.GetCompanyName(p.CompanyId);
                        CoachName.Text = UserUtil.GetCoachName(p.CoachId);
                        WorkflowName.Text = WorkflowUtil.GetWorklowName(p.WorkflowId);
                        ProjectViewerNotes.Text = p.Notes;
                    }
                }
                //if theyre an admin and trying to make a new project
                else if (Request.QueryString["edit"] != null && Request.QueryString["pid"] == null && user.RoleId == 4)
                {
                    projectListing.Visible = false;
                    projectBuilder.Visible = true;
                    GenerateProjectDropdowns();
                }


                /*
                //checks user is an admin
                if (user.RoleId == 4)
                {
                    //sets up admin project creation form
                    GenerateProjectDropdowns();
                    projectBuilder.Visible = true;
                }
                */
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        private void GenerateProjectDropdowns()
        {
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
        }

        private void CreateProjectList()
        {
            var projectNode = "";
            numberShowing.InnerHtml = "";
            List<Project> projects = ProjectUtil.GetProjects();
            for (int i = 0; i < projects.Count; i++)
            {
                User coach = UserUtil.GetCoach(projects[i].CoachId);
                WorkflowModel workflow = WorkflowUtil.GetWorkflow(projects[i].WorkflowId);
                projectNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/project.png\"/></div>";
                projectNode += "<div class=\"content\"><a class=\"header\" href='Projects.aspx?pid="+projects[i].ProjectId+"'>" + projects[i].Name + "</a><div class=\"meta\">";
                projectNode += "<span class=\"stay\">" + coach.FullName + " | " + workflow.WorkflowName + "</span></div><div class=\"description\">";
                projectNode += projects[i].Notes + "</div></div></div>";
                projectList.InnerHtml += projectNode;
                projectNode = "";
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

        protected void ProjectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
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

        protected void CreateNewProjectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx?edit=1");
        }

        protected void LoadMoreProjects()
        {
            var moreProjects = "";
            var five = 0;
            List<Project> projects = ProjectUtil.GetProjects();
            for (int i = count; i < projects.Count && i < five; i++)
            {
                User coach = UserUtil.GetCoach(projects[i].CoachId);
                WorkflowModel workflow = WorkflowUtil.GetWorkflow(projects[i].WorkflowId);
                moreProjects = "<h1>"+count+"</h1><div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/project.png\"/></div>";
                moreProjects += "<div class=\"content\"><a class=\"header\" href='Projects.aspx?pid=" + projects[i].ProjectId + "'>" + projects[i].Name + "</a><div class=\"meta\">";
                moreProjects += "<span class=\"stay\">" + coach.FullName + " | " + workflow.WorkflowName + "</span></div><div class=\"description\">";
                moreProjects += projects[i].Notes + "</div></div></div>";
                projectList.InnerHtml += moreProjects;
                moreProjects = "";
                count++;
            }
            numberShowing.InnerHtml = "";
            var showing = "Showing 1 - " + count + " of " + projects.Count + " Results";
            numberShowing.InnerHtml += showing;
        }
    }
}
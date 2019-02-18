using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Models;
using Workflow.Data;

namespace Workflow
{
    public partial class Dashboard : System.Web.UI.Page
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
            List<Project> allProjects;
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.Email;
                if (user.RoleId == 2)
                {
                    allProjects = ProjectUtil.GetCoachProjects(user.UserId);
                    CreateProjectPanel(allProjects);
                }
                if (user.RoleId == 3 || user.RoleId == 4)
                {
                    allProjects = ProjectUtil.GetProjects();
                    CreateProjectPanel(allProjects);
                }
            }
                LoadActivityFeed(user.UserId);
            }
            //kicks them out if they arent
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void LoadActivityFeed(int userId)
        {
            List<FeedItem> feedList = FeedUtil.GetFeed(userId);
            foreach(FeedItem item in feedList)
            {
                notifications.Controls.AddAt(0, item);
            }
        }
       
        private void CreateProjectPanel(List<Project> proj)
        {
            foreach (Project project in proj)
            {
                // init String for adding HTML tags and info
                var projectNode = "";
                
                //Preparing Lists of the project's workflow components and their completion status
                List<WorkflowComponent> steps = WorkflowComponentUtil.GetWorkflowComponents(project.WorkflowId);
                List<ComponentCompletion> completionitems = ComponentCompletionUtil.GetCompletedProCompletionStatus(project.ProjectId);
                
                // Calculate percentage of steps completed using total steps and number completed 
                double totalSteps = steps.Count;
                double stepsCompleted = completionitems.Count;
                double completionPercent = (stepsCompleted/totalSteps)*100;
                projectNode += "<div class=\"item\"><div class=\"ui small image\">";
                projectNode += "<div class=\"ui orange progress\" data-percent=\"" + completionPercent + "\" id=\"project" + project.ProjectId + "\">";
                projectNode += "<div class=\"bar\"><div class=\"progress\"></div></div><div class=\"label\">Completion</div></div>";
                
                /*id for opening project?*/
                projectNode += "<button class=\"ui brown basic button\">View Full Project</button></div>";
                projectNode += "<div class=\"content\"><a class=\"header\">" + project.Name + "</a>";
                projectNode += "<div class=\"description\">" + project.Notes + "</div>";
                projectNode += "<div class=\"table\"><table class=\"ui celled table\"><thead><tr><th>Workflow Step</th><th>Status</th></tr></thead><tbody>";

                /* 3 Cases of project status for the table; completed, not completed/unknown, and needs modification. Check status, then add appropriate class.*/
                foreach (WorkflowComponent step in steps)
                {
                    ComponentCompletion compstatus = ComponentCompletionUtil.GetProCompletionStatus(step.WFComponentID, project.ProjectId);
                    var stat = compstatus.CompletionID;
                    if (stat == 0)
                        projectNode += "<tr class=\"disabled\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"icon checkmark\"></i>Not Started</td></tr>";
                    if (stat == 1)
                        projectNode += "<tr class=\"disabled\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"icon checkmark\"></i>In Progress</td></tr>";
                    if (stat == 2)
                        projectNode += "<tr class=\"positive\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"icon checkmark\"></i>Approved</td></tr>";
                    if (stat == 3)
                        projectNode += "<tr class=\"negative\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"icon checkmark\"></i>Needs Modification</td></tr>";
                    if (stat == 4)
                        projectNode += "<tr class=\"negative\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"icon checkmark\"></i>Denied</td></tr>";
                }

                // Complete table, add tab pages numbers
                projectNode += "</tbody></table></div></div></div><hr>";

                // Add to page
                projectParent.InnerHtml += projectNode;
            }
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

        protected void DismissBtn_Click(object sender, EventArgs e)
        {
            FeedUtil.DismissItem(1);
        }
    }
}
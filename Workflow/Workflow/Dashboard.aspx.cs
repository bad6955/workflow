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
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.FullName;

                //loads activity feeds
                LoadActivityFeed(user.UserId);

                List<Project> allProjects;
                //loads role specific items
                if (user.RoleId == 2)
                {
                    allProjects = ProjectUtil.GetCoachProjects(user.UserId);
                    CreateGraph(allProjects);
                    CreateProjectPanel(allProjects);
                }
                if (user.RoleId == 3 || user.RoleId == 4)
                {
                    allProjects = ProjectUtil.GetProjects();
                    CreateProjectPanel(allProjects);
                    CreateAdminGraph(allProjects);
                }
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
            foreach (FeedItem item in feedList)
            {
                activityFeed.Controls.AddAt(0, item);
            }
            if (feedList.Count == 0)
            {
                activityFeed.InnerHtml += "<div id=\"empty-feed\"><h4>No Recent Activity</h4><i class=\"big disabled coffee icon\"></i></div>";
            }
        }

        private void CreateGraph(List<Project> projects)
        {
            List<Project> allProjects = ProjectUtil.GetProjects();
            int waitingForApproval = 0;
            int waitingOnCompany = 0;
            foreach (Project proj in allProjects)
            {
                List<ComponentCompletion> completion = ComponentCompletionUtil.GetAllProCompletionStatus(proj.ProjectId);
                foreach (ComponentCompletion comp in completion)
                {
                    if (comp.CompletionID == 5)
                        waitingForApproval++;
                    if (comp.CompletionID == 1 || comp.CompletionID == 0)
                        waitingOnCompany++;
                }
            }
            var graphScript = "<canvas id=\"pie-chart\" width=\"800\" height=\"300\"></canvas>";
            graphScript += "<script>new Chart(document.getElementById(\"pie-chart\"), {type:'pie', data:{labels: [";
            graphScript += "\"My Waiting on Company\", \"My Projects\", \"My Pending Approval\"],";
            graphScript += "datasets: [{label: \"Total\", backgroundColor: [\"#dc7a32\", \"#04828F\", \"#5C3315\", \"#32CBDC\"],";
            graphScript += "data: [" + waitingOnCompany + ", " + projects.Count + ", " + waitingForApproval + "]}]}, options:{title:{display:true,text:'All Projects: "+allProjects.Count+"', position: 'bottom'}}});</script>";

            piechart.InnerHtml += graphScript;
        }

        private void CreateAdminGraph(List<Project> projects)
        {
            List<Project> allProjects = ProjectUtil.GetProjects();
            int closed = 0;
            int open = 0;
            int hold = 0;
            foreach (Project proj in allProjects)
            {
                if (proj.StatusId == 3)
                    closed++;
                if (proj.StatusId == 4)
                    hold++;
                else
                    open++;
            }
            var graphScript = "<canvas id=\"pie-chart\" width=\"800\" height=\"250\"></canvas>";
            graphScript += "<script>new Chart(document.getElementById(\"pie-chart\"), {type:'pie', data:{labels: [";
            graphScript += "\"All Projects\", \"Open\", \"Closed\", \"On Hold\"],";
            graphScript += "datasets: [{label: \"Total\", backgroundColor: [\"#dc7a32\", \"#04828F\", \"#5C3315\", \"#32CBDC\"],";
            graphScript += "data: [" + allProjects.Count + ", " + open + ", " + closed + ", " + hold + "]}]}});</script>";

            piechart.InnerHtml += graphScript;
        }

        private void CreateProjectPanel(List<Project> proj)
        {
            foreach (Project project in proj)
            {
                var projectNode = "";

                //Preparing Lists of the project's workflow components and their completion status
                List<WorkflowComponent> steps = WorkflowComponentUtil.GetWorkflowComponents(project.WorkflowId);
                List<ComponentCompletion> completionitems = ComponentCompletionUtil.GetCompletedProCompletionStatus(project.ProjectId);

                // Calculate percentage of steps completed using total steps and number completed 
                double totalSteps = steps.Count;
                double stepsCompleted = completionitems.Count;
                int percent = Convert.ToInt32((stepsCompleted / totalSteps) * 100);

                projectNode += "<div class=\"item\"><div class=\"ui small image\">";
                projectNode += "<div class=\"ui orange progress\" data-percent=" + percent + " id=\"project" + project.ProjectId + "\">";
                projectNode += "<div class=\"bar\"><div class=\"progress\"></div></div><div class=\"label\">Completion</div></div>";

                /*id for opening project?*/
                projectNode += "<button class=\"ui brown basic button\">View Full Project</button></div>";
                projectNode += "<div class=\"content\"><a class=\"header\" href='"+"Projects.aspx?pid="+project.ProjectId +"'>" + project.Name + "</a>";
                projectNode += "<div class=\"description\">" + project.Notes + "</div>";
                projectNode += "<div class=\"table\"><table class=\"ui celled table\"><thead><tr><th>Workflow Step</th><th>Status</th></tr></thead><tbody>";

                /* 3 Cases of project status for the table; completed, not completed/unknown, and needs modification. Check status, then add appropriate class.*/
                ComponentCompletion compstatus = null;
                foreach (WorkflowComponent step in steps)
                {
                    compstatus = ComponentCompletionUtil.GetProCompletionStatus(step.WFComponentID, project.ProjectId);
                    if (compstatus != null)
                    {
                        var stat = compstatus.CompletionID;
                        if (stat == 0)
                            projectNode += "<tr class=\"disabled\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"close icon\"></i>Not Started</td></tr>";
                        if (stat == 1)
                            projectNode += "<tr class=\"disabled\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"battery half icon\"></i>In Progress</td></tr>";
                        if (stat == 2)
                            projectNode += "<tr class=\"positive\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"icon checkmark\"></i>Approved</td></tr>";
                        if (stat == 3)
                            projectNode += "<tr class=\"negative\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"pencil alternate icon\"></i>Needs Modification</td></tr>";
                        if (stat == 4)
                            projectNode += "<tr class=\"negative\"><td>" + step.ComponentTitle + ": " + step.ComponentText + "</td><td><i class=\"close icon\"></i>Denied</td></tr>";
                    }
                }

                // Complete table, add tab pages numbers
                projectNode += "</tbody></table></div></div></div><script>$('#project" + project.ProjectId + "').progress();</script><hr>";
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
    }
}
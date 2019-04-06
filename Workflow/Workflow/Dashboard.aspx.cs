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
                if (user.RoleId == 4)
                {
                    AdminBtn.Visible = true;
                }
                //loads activity feeds
                LoadActivityFeed(user.UserId);

                List<Project> projectList = new List<Project>();
                List<Project> allProjects = ProjectUtil.GetProjects();
                //loads role specific items
                if (user.RoleId == 1)
                {
                    projectList = ProjectUtil.GetCompanyProjects(user.CompanyId);
                    CreateProjectPanel(projectList);
                }
                else if (user.RoleId == 2)
                {
                    projectList = ProjectUtil.GetCoachProjects(user.UserId);
                    CreateGraph(projectList);
                    CreateProjectPanel(projectList);
                }
                else if (user.RoleId == 3 || user.RoleId == 4)
                {
                    CreateAdminGraph(allProjects);
                    CreateProjectPanel(allProjects);
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

        private void LoadActivityFeed(int userId)
        {
            List<FeedItem> feedList = FeedUtil.GetFeed(userId);
            foreach (FeedItem item in feedList)
            {
                activityFeed.Controls.AddAt(0, item);
            }
            // if the feed is empty, fill with message and icon
            if (feedList.Count == 0)
            {
                activityFeed.InnerHtml += "<div id=\"empty-feed\"><h4>No Recent Activity</h4><i class=\"big disabled coffee icon\"></i></div>";
            }
        }

        private void CreateGraph(List<Project> projects)
        {
            int waitingForApproval = 0;
            int waitingOnCompany = 0;
            foreach (Project proj in projects)
            {
                List<Form> completion = FormUtil.GetCoachForms(proj.CoachId);
                foreach (Form comp in completion)
                {
                    if (comp.Approved.Contains("0") && comp.Submission == 1)
                        waitingForApproval++;
                    if (comp.Denied.Contains("1"))
                        waitingOnCompany++;
                }
            }
            var graphScript = "<canvas id=\"pie-chart\" width=\"800\" height=\"300\"></canvas>";
            graphScript += "<script>new Chart(document.getElementById(\"pie-chart\"), {type:'pie', data:{labels: [";
            graphScript += "\"Waiting on Company\", \"Pending My Approval\"],";
            graphScript += "datasets: [{label: \"Total\", backgroundColor: [\"#dc7a32\", \"#04828F\", \"#5C3315\", \"#32CBDC\"],";
            graphScript += "data: [" + waitingOnCompany + ", " + waitingForApproval + "]}]}, options:{title:{display:true,text:'My Projects: "+projects.Count+"', position: 'bottom'}}});</script>";

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
                if (proj.StatusId == 3) { closed++; }
                if (proj.StatusId == 4) { hold++; }
                if (proj.StatusId == 0 || proj.StatusId == 1) { open++; }
            }
            var graphScript = "<canvas id=\"pie-chart\" width=\"800\" height=\"250\"></canvas>";
            graphScript += "<script>new Chart(document.getElementById(\"pie-chart\"), {type:'pie', data:{labels: [";
            graphScript += "\"Open\", \"Closed\", \"On Hold\"],";
            graphScript += "datasets: [{label: \"Total\", backgroundColor: [\"#dc7a32\", \"#04828F\", \"#5C3315\", \"#32CBDC\"],";
            graphScript += "data: [" + open + ", " + closed + ", " + hold + "]}]}, options:{title:{display:true,text:'All Projects: " + projects.Count + "', position: 'bottom'}}});</script>";

            piechart.InnerHtml += graphScript;
        }

        private void CreateProjectPanel(List<Project> proj)
        {
            foreach (Project project in proj)
            {
                var projectNode = "";

                //Preparing Lists of the project's workflow components and their completion status
                List<WorkflowComponent> steps = WorkflowComponentUtil.GetWorkflowComponents(project.WorkflowId);

                double totalSteps = steps.Count;
                double stepsCompleted = 0;

                List<Form> formSteps = new List<Form>();
                try
                {
                    foreach (WorkflowComponent step in steps)
                    {
                        Form f = FormUtil.GetProjectFormByTemplate(step.FormID, project.ProjectId);
                        formSteps.Add(f);
                        if (!f.Approved.Contains("0"))
                        {
                            stepsCompleted++;
                        }
                    }
                } catch(Exception e) { }

                // Calculate percentage of steps completed using total steps and number completed 
                int percent = 0;
                if (stepsCompleted > 0)
                    Convert.ToInt32((stepsCompleted / totalSteps));

                projectNode += "<div class=\"item\"><div class=\"ui small image\">";
                projectNode += "<div class=\"ui orange progress\" data-percent=" + percent + " id=\"project" + project.ProjectId + "\">";
                projectNode += "<div class=\"bar\"><div class=\"progress\"></div></div><div class=\"label\">Completion</div></div>";

                /*id for opening project?*/
                projectNode += "<a class=\"header\" href='" + "Projects.aspx?pid=" + project.ProjectId + "'><span class=\"ui brown basic button\">View Full Project</span></a></div>";
                projectNode += "<div class=\"content\"><a class=\"header\" href='"+"Projects.aspx?pid="+project.ProjectId +"'>" + project.Name + "</a>";
                projectNode += "<div class=\"description\">" + project.Notes + "</div>";
                projectNode += "<div class=\"table\"><table class=\"ui celled table\"><thead><tr><th>Workflow Step</th><th>Status</th></tr></thead><tbody>";

                /* 3 Cases of project status for the table; completed, not completed/unknown, and needs modification. Check status, then add appropriate class.*/
                int i = 0;
                try
                {
                    foreach (WorkflowComponent step in steps)
                    {
                        Form f = formSteps[i];
                        if (!f.Approved.Contains("0"))
                        {
                            projectNode += "<tr class=\"positive\"><td><a href='Forms.aspx?pfid=" + f.FormId + "'>" + step.ComponentTitle + "</a></td><td><i class=\"icon checkmark\"></i>Approved</td></tr>";
                        }
                        else if (f.Denied.Contains("1") && f.DenialReason.Length > 0)
                        {
                            projectNode += "<tr class=\"negative\"><td><a href='Forms.aspx?pfid=" + f.FormId + "'>" + step.ComponentTitle + "</a></td><td><i class=\"pencil alternate icon\"></i>Needs Modification</td></tr>";
                        }
                        else if (f.Denied.Contains("1"))
                        {
                            projectNode += "<tr class=\"negative\"><td><a href='Forms.aspx?pfid=" + f.FormId + "'>" + step.ComponentTitle + "</a></td><td><i class=\"close icon\"></i>Denied</td></tr>";
                        }
                        else if (f.Submission == 1)
                        {
                            projectNode += "<tr class=\"disabled\"><td><a href='Forms.aspx?pfid=" + f.FormId + "'>" + step.ComponentTitle + "</a></td><td><i class=\"battery half icon\"></i>In Progress</td></tr>";
                        }
                        else
                        {
                            projectNode += "<tr class=\"disabled\"><td><a href='Forms.aspx?pfid=" + f.FormId + "'>" + step.ComponentTitle + "</a></td><td><i class=\"close icon\"></i>Not Started</td></tr>";
                        }
                        i++;
                    }
                } catch (Exception e) { }

                // Complete table, add tab pages numbers
                projectNode += "</tbody></table></div></div></div><script>$('#project" + project.ProjectId + "').progress();</script><hr>";
                // Add to page
                projectParent.InnerHtml += projectNode;
            }
        }
    }
}
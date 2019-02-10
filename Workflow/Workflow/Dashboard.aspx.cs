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
            AddFeedItem(1, "You were added as a coach on company's project: project name", new DateTime());
            List<Project> allProjects;
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.Email;
                //List<Project> projects = ProjectUtil.GetCoachProjects(user.UserId);
                allProjects = ProjectUtil.GetProjects();
                foreach(Project item in allProjects)
                {
                    CreateProjectPanel(item);
                }
            }
            //kicks them out if they arent
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void AddFeedItem(int id, string text, DateTime time)
        {
            DateTime current = DateTime.Now;
            var currentFeed = activityFeed.InnerHtml;
            var newItem = "<Asp:Panel runat=\"server\" class=\"item\" ID=\"FeedItem" + id + "\">";
            newItem += "<div class=\right floated content\">";
            newItem += "<Asp:Button runat=\"server\" ID=\"DismissBtn" + text + "\" OnClick=\"DismissBtn_Click\" CssClass=\"ui button\" Text=\"Dismiss\"></Asp:Button>";
            newItem += "</div>";
            newItem += "<i class=\"bell outline icon\"></i>";
            newItem += "<div class=\"content\">";
            newItem += "<a class=\"header\">" + text + "</a>";
            newItem += "<div class=\"description\">Updated" + "TIME" + "ago</div>";
            newItem += "</div>";
            newItem += "</Asp:Panel>";

            activityFeed.InnerHtml = newItem + currentFeed;

            /*
            <Asp:Panel runat="server" class="item">
                <div class="right floated content">
                    <Asp:Button runat="server" OnClick="DismissBtn_Click" CssClass="ui button" Text="Dismiss"></Asp:Button>
                </div>
                <i class="bell outline icon"></i>
                <div class="content">
                    <a class="header">You were added as a coach on company's project: project name</a>
                    <div class="description">Updated 10 mins ago</div>
                </div>
            </Asp:Panel>
            */
        }
       
        private void CreateProjectPanel(Project project)
        {
            var completionPercent = 30;
            var projectNode = "";
            projectNode += "<div class=\"item\"><div class=\"ui small image\">";
            projectNode += "<div class=\"ui orange progress\" data-percent=\"" + completionPercent + "\" id=\"project" + project.ProjectId + "\">";
            projectNode += "<div class=\"bar\"><div class=\"progress\"></div></div><div class=\"label\">Completion</div></div>";
            /*id for opening project?*/
            projectNode += "<button class=\"ui brown basic button\">View Full Project</button></div>";
            projectNode += "<div class=\"content\"><a class=\"header\">" + project.Name + "</a>";
            projectNode += "<div class=\"description\">" + project.Notes + "</div>";
            projectNode += "<table class=\"ui celled table\"><thead><tr><th> Workflow Step</th><th>Status</th></tr></thead><tbody>";
            /* 3 Cases of project status for the table; completed, not completed/unknown, and needs modification. Check status, then add appropriate class.*/
            // completed
            /*for ()
            /{
                if (stepCompleted)
                {
                    projectNode += "";
                }
                // needs modification
                else if (stepCompleted)
                {
                    projectNode += "";
                }
                // unknown or not yet completed
                else
                {
                    projectNode += "";
                }
            }*/
            /* TODO: managing the table pages */
            projectNode += "<tr class=\"positive\"><td>Step 1 - Worklflow Step Title</td ><td><i class=\"icon checkmark\"></i>Approved</td></tr>";


            projectNode += "</tbody><tfoot><tr><th colspan=\"3\"><div class=\"ui right floated pagination menu\">";
            projectNode += "<a class=\"icon item\"><i class=\"left chevron icon\"></i></a><a class=\"item\">1</a>";
            projectNode += "<a class=\"icon item\"><i class=\"right chevron icon\"></i></a></div></th></tr></tfoot></table></div></div>";
            projectParent.InnerHtml += projectNode;
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

        }
    }
}
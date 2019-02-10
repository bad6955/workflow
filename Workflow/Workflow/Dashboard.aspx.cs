using System;
using System.Web;
using Workflow.Models;
using System.Windows.Forms;

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
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.Email;
            }
            //kicks them out if they arent
            else
            {
                Response.Redirect("Login.aspx");
            }
            var doc = HtmlDocument.GetElementById("test").AppendChild("<h1>Hi</h1>");
        }

        private void AddFeedItem(int id, string text, DateTime time)
        {
            DateTime current = DateTime.Now;
            var currentFeed = activityFeed.InnerHtml;
            var newItem = "<Asp:Panel runat=\"server\" class=\"item\" ID=\"FeedItem"+id+"\">";
                newItem += "<div class=\right floated content\">";
                newItem += "<Asp:Button runat=\"server\" ID=\"DismissBtn"+text+"\" OnClick=\"DismissBtn_Click\" CssClass=\"ui button\" Text=\"Dismiss\"></Asp:Button>";
                newItem += "</div>";
                newItem += "<i class=\"bell outline icon\"></i>";
                newItem += "<div class=\"content\">";
                newItem += "<a class=\"header\">"+text+"</a>";
                newItem += "<div class=\"description\">Updated"+"TIME"+"ago</div>";
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
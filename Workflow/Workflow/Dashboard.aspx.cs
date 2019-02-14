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
            AddFeedItem(1, "Added as a couch on company's project", DateTime.Now);
            AddFeedItem(2, "Added as a coach on company's project", DateTime.Now);
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
        }

        private void AddFeedItem(int id, string text, DateTime time)
        {
            var feedItem = new FeedItem(id, text, time);
            notifications.Controls.AddAt(0, feedItem);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Models;

namespace Workflow
{
    public partial class Logs : System.Web.UI.Page
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
                if (user.RoleId == 5 || user.RoleId == 4)
                {
                    List<string> logLines = Workflow.Utility.Log.ReadLog();
                    foreach (string line in logLines)
                    {
                        LogText.Controls.Add(new LiteralControl(line));
                        LogText.Controls.Add(new LiteralControl("<br/>"));
                    }
                }
                else
                {
                    //kicks them out if they arent admin / auditor
                    Response.Redirect("Dashboard.aspx");
                }
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        protected void LogBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Logs.aspx");
        }

        protected void LogoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}
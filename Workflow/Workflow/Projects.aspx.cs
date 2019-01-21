using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Auth;

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
            if (Session["FirebaseUser"] != null)
            {
                User fbUser = (User)Session["FirebaseUser"];
            }
            //kicks them out if they arent
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
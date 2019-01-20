using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Auth;

namespace Workflow
{
    public partial class AccountSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //accessing the application variable set on the login code-behind and displaying it out to the user

            //validates that the user is logged in
            if (Session["FirebaseUser"] != null)
            {
                User fbUser = (User)Session["FirebaseUser"];
                //user.Text = "Welcome back, " + fbUser.Email;
            }
            //kicks them out if they arent
            else
            {
                Response.Redirect("Login.aspx");
            }
        }   
    }
}
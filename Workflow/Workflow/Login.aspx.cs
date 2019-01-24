using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Models;

namespace Workflow
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //checks if the user is logged in and redirects them to their dashboard
            if (Session["FirebaseUser"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        //Events in code-behind can be created using the OnClick attribute in an <ASP:*** /> tag
        //  you can either tie the page's event to an existing method in the code-behind, or 
        //  auto generate a new event with a name similar to the below
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string pass = Password.Text;

            //Check the user's infomation before logging in
            if (ValidateLogin(email, pass))
            {
                Response.Redirect("Dashboard.aspx"); //redirecting the user from Login.aspx to Dashboard.aspx
            }
            else
            {
                //Display bad login msg
                ErrorLabel.Text = "Invalid email or password";
            }

        }

        //Validate the user login
        protected bool ValidateLogin(string email, string pass)
        {
            //validates the user's credentials against Firebase
            User user = new User();
            user.firebaseUser = FirebaseUtil.LoginUser(email, pass);
            if (user != null)
            {
                Session["FirebaseUser"] = user;
                return true;
            }
            return false;

            //The following code will be use to determine if the user is
            // putting in the correct email and password. After 
            // 5 attempt, will lock out from any more attempt and forced to 
            // change their password.
            /*if(Email.Text == validatedUser)
            {
                if(Email.Text == validatedPass)
                {
                    return true;
                }
            }
            return false;*/
            //Delete this when testing security
        }

        //If user forgot password, run the following code
        protected void ForgotBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
}
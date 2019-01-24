using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Auth;

namespace Workflow
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        //Create a conntect to server here or get the conntection from login.aspx
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //Run the follow code when the user want to change their password.
        protected void ChangeBtn_Click(object sender, EventArgs e)
        {
            if (Email.Text.Length > 0)
            {
                if (FirebaseUtil.ForgotPassword(Email.Text))
                {
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Check your email for a password reset link!";
                    Email.Visible = false;
                    changeBtn.Visible = false;
                }
                else
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = Email.Text + " is not valid.";         
                }
            }
            else
            {
                ErrorLabel.Text = "Please enter your email!";
            }
        }
        //Return to login page
        protected void ReturnBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
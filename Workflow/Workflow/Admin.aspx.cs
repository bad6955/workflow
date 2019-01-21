using System;
using System.Web;
using System.Web.UI;
using Firebase.Auth;

namespace Workflow
{

    public partial class Admin : System.Web.UI.Page
    {
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

        //Register a new user in the system
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            String email = Email.Text;
            String pass = Password.Text;
            String pass2 = PasswordRepeat.Text;
            String displayName = "";
            bool verificationEmail = true;

            //Validate that the logged in user has permissions to do this
            //Validate the new user's information
            //Create the new user account
            //Send an email to the new user

            if (pass.Equals(pass2))
            {
                User user = Firebase.CreateNewUser(email, pass, displayName, verificationEmail);
                if (user != null)
                {
                    //display user created msg
                }
                else
                {
                    //display user failed to be created msg
                }
            }
            else
            {
                //throw error, passwords don't match
            }
        }
    }
}

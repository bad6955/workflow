using System;
using System.Web;
using System.Web.UI;
using Workflow.Data;
using Workflow.Models;

namespace Workflow
{

    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
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
            string email = Email.Text;
            string pass = Password.Text;
            string pass2 = PasswordRepeat.Text;
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string displayName = "";
            bool verificationEmail = true;

            //Validate that the logged in user has permissions to do this
            //Validate the new user's information
            //Create the new user account
            //Send an email to the new user

            if (pass.Equals(pass2))
            {
                User user = new User(email, firstName, lastName);
                user.setFirebaseUser(FirebaseUtil.CreateNewUser(email, pass, displayName, verificationEmail));
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

        protected void CompanyBtn_Click(object sender, EventArgs e)
        {
            string companyName = Company.Text;

            //Validate that the logged in user has permissions to do this

            if (companyName.Length > 0)
            {
                Company company = CompanyUtil.CreateCompany(companyName);
                if (company != null)
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

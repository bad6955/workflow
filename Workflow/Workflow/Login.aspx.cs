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

        String validatedUser = "User@email.com";
        String validatedPass = "pass123";

        //Create a conntect to server here
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Events in code-behind can be created using the OnClick attribute in an <ASP:*** /> tag
        //  you can either tie the page's event to an existing method in the code-behind, or 
        //  auto generate a new event with a name similar to the below
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            //Check the user's infomation before logging in
            if (vaildedLogIn() == true)
            {
                Session["userEmail"] = Email.Text; // setting an session variable so it can be accessed on other pages securely
                Project p = new Project(); //creating new model class instance
                Response.Redirect("Dashboard.aspx"); //redirecting the user from Login.aspx to Dashboard.aspx
            }
            else
            {
                //Display error here

            }

        }

        //Valid the user loging in
        protected bool vaildedLogIn()
        {
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
            return true;
        }

        //If user forgot password, run the following code
        protected void ForgotBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
            //Should the user be automactlly log in 
            // or do they need to enter their log in info again?
        }
    }
}
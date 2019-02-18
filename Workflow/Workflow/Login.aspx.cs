using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DEBUG testing
            //string html = JSONtoHTML.ConvertToHTML("[ { \"type\": \"header\", \"subtype\": \"h1\", \"label\": \"Header\" }, { \"type\": \"paragraph\", \"subtype\": \"p\", \"label\": \"Paragraph\" }, { \"type\": \"text\", \"label\": \"Text Field\", \"className\": \"form-control\", \"name\": \"text-1550510559308\", \"subtype\": \"text\" }, { \"type\": \"textarea\", \"label\": \"Text Area\", \"className\": \"form-control\", \"name\": \"textarea-1550510560317\", \"subtype\": \"textarea\" }, { \"type\": \"button\", \"label\": \"Button\", \"subtype\": \"button\", \"name\": \"button-1550510564127\" } ]");
            //PDFGen.CreateHTMLPDF(html, "testPDF");

            //checks if the user is logged in and redirects them to their dashboard
            if (Session["User"] != null)
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
            User user = UserUtil.GetUser(email);
            if (user != null)
            {
                if (user.InvalidLoginCt < 5)
                {
                    user.FirebaseUser = FirebaseUtil.LoginUser(email, pass);
                    if (user.FirebaseUser != null)
                    {
                        UserUtil.ValidLogin(user);
                        Session["User"] = user;
                        return true;
                    }
                    else
                    {
                        UserUtil.InvalidLogin(user);
                        ErrorLabel2.Text = (5-user.InvalidLoginCt+1) + " attempt(s) remaining until account is locked";
                    }
                }
                else
                {
                    //FirebaseUtil.ForgotPassword(user.Email);
                    //ErrorLabel2.Text = "Account locked, check your email for a password reset link";
                    ErrorLabel2.Text = "Account locked, contact a Venture Creations admin";
                }
            }
            return false;
        }

        //If user forgot password, run the following code
        protected void ForgotBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
}
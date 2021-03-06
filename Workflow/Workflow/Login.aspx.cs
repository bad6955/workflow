﻿using System;
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
            //checks if the user is logged in and redirects them to their dashboard
            User user = (User)Session["User"];
            if (Session["User"] != null)
            {
                if(user.RoleId == 5)
                {
                    Response.Redirect("Logs.aspx");
                }
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
                User user = UserUtil.GetUser(email);
                if (user.RoleId == 5)
                {
                    Response.Redirect("Logs.aspx");
                }
                else if (user.AdminPanel == 1)
                {
                    Response.Redirect("Admin.aspx");
                }
                Response.Redirect("Dashboard.aspx");
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
                        if((5-user.InvalidLoginCt+1) <= 3)
                        {
                            ErrorLabel2.Text = (5 - user.InvalidLoginCt + 1) + " attempt(s) remaining until account is locked";
                        }
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
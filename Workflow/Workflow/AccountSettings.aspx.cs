﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;

namespace Workflow
{
    public partial class AccountSettings : System.Web.UI.Page
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

                if (user.RoleId == 4)
                {
                    AdminBtn.Visible = true;
                    AdminPanelToggler.Visible = true;
                    if (!IsPostBack)
                    {
                        if (user.AdminPanel == 1)
                        {
                            AdminPanel.Checked = true;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void DashboardBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void ProjectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void FormBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        protected void WorkflowBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            PasswordChangeError.Visible = true;
            User user = (User)Session["User"];
            if (FirebaseUtil.ForgotPassword(user.Email))
            {
                PasswordChangeError.CssClass = "success";
                PasswordChangeError.Text = "Check your email for a password reset link";
            }
            else
            {
                PasswordChangeError.Text = "Couldn't send you a reset email";
            }
        }

        //makes sure password meets min requirements
        //returns a result code for missing reqs (and 0 for valid)
        private int ValidatePasswordSecurity(string pass)
        {
            bool containsUppercase = false;
            bool containsLowercase = false;
            bool containsNumber = false;

            foreach (char c in pass)
            {
                if (Char.IsDigit(c) || Char.IsSymbol(c))
                {
                    containsNumber = true;
                    continue;
                }

                if (Char.IsUpper(c))
                {
                    containsUppercase = true;
                    continue;
                }

                if (Char.IsLower(c))
                {
                    containsLowercase = true;
                    continue;
                }
            }

            if (containsNumber && containsLowercase && containsUppercase)
            {
                return 0;
            }
            else if (!containsUppercase)
            {
                return 1;
            }
            else if (!containsLowercase)
            {
                return 2;
            }
            else if (!containsNumber)
            {
                return 3;
            }
            return 4;
        }

        protected void AdminPanel_CheckedChanged(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            if (AdminPanel.Checked)
            {
                UserUtil.AdminPanelToggle(user, 1);
            }
            else
            {
                UserUtil.AdminPanelToggle(user, 0);
            }
        }
    }
}
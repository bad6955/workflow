﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Workflow.Data;
using Workflow.Utility;
using Workflow.Models;
using System.Configuration;

namespace Workflow
{

    public partial class Admin : System.Web.UI.Page
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
            string lockoutSetting = ConfigurationManager.AppSettings.Get("AdminPageLockout");
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];

                //checks user is an admin
                if (user.RoleId == 4)
                {
                    SetupAdminPage();
                }
                else
                {
                    //kicks them out if they arent
                    Response.Redirect("Login.aspx");
                }
            }
            else if (lockoutSetting.Equals("false"))
            {
                SetupAdminPage();
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        private void SetupAdminPage()
        {
            RoleSelect.DataSource = RoleUtil.GetRoles();
            RoleSelect.DataTextField = "roleName";
            RoleSelect.DataValueField = "roleId";
            RoleSelect.DataBind();
            RoleSelect.SelectedIndex = 0;

            UserRole.DataSource = RoleUtil.GetRoles();
            UserRole.DataTextField = "roleName";
            UserRole.DataValueField = "roleId";
            UserRole.DataBind();

            CompanySelect.DataSource = CompanyUtil.GetCompanies();
            CompanySelect.DataTextField = "companyName";
            CompanySelect.DataValueField = "companyId";
            CompanySelect.DataBind();
            CompanySelect.SelectedIndex = 0;

            LockedAccountSelect.DataSource = UserUtil.GetLockedUsers();
            LockedAccountSelect.DataTextField = "Identity";
            LockedAccountSelect.DataValueField = "UserId";
            LockedAccountSelect.DataBind();
            LockedAccountSelect.SelectedIndex = 0;

            MakeUserTable();
            MakeCompanyTable();
        }

        private void ClearFields()
        {
            Email.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Password.Text = "";
            PasswordRepeat.Text = "";
            Company.Text = "";
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

        //Register a new user in the system
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            UserCreateResult.Visible = false;
            EmailError.Visible = false;
            NameError.Visible = false;
            PasswordError.Visible = false;
            RoleCompanyError.Visible = false;

            string email = Email.Text;
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string pass = Password.Text;
            string pass2 = PasswordRepeat.Text;
            int roleId = int.Parse(SelectedRole.Value);
            int companyId = int.Parse(SelectedCompany.Value);
            string displayName = "";
            bool verificationEmail = true;

            //Validate that the logged in user has permissions to do this
            //Validate the new user's information
            //Create the new user account
            //Send an email to the new user

            //checks that a role was selected for the user

            if (!UserUtil.DoesUserExist(email))
            {
                if (firstName.Length > 0 && lastName.Length > 0)
                {
                    if (roleId != -1)
                    {
                        if (companyId != -1)
                        {
                            if (pass.Equals(pass2))
                            {
                                if (pass.Length > 7)
                                {
                                    int validPass = ValidatePasswordSecurity(pass);
                                    if (validPass == 0)
                                    {
                                        //creates the user in firebase
                                        Firebase.Auth.User fbUser = FirebaseUtil.CreateNewUser(email, pass, displayName, verificationEmail);

                                        //if the user already exists in firebase, try to log them in
                                        if (fbUser == null)
                                        {
                                            fbUser = FirebaseUtil.LoginUser(email, pass);
                                        }

                                        if (fbUser != null)
                                        {
                                            User u = UserUtil.CreateUser(roleId, companyId, email, firstName, lastName);
                                            User user = (User)Session["User"];
                                            if(user != null)
                                            {
                                                Log.Info(user.Identity + " created a new " + RoleUtil.GetRole(roleId).RoleName + " account under " + CompanyUtil.GetCompanyName(companyId) + " assigned to " + firstName + " " + lastName + " - " + email);
                                            }
                                            else
                                            {
                                                Log.Info("System created a new " + RoleUtil.GetRole(roleId).RoleName + " account under " + CompanyUtil.GetCompanyName(companyId) + " assigned to " + firstName + " " + lastName + " - " + email);
                                            }
                                            u.FirebaseUser = fbUser;
                                            //display user created msg
                                            UserCreateResult.Visible = true;
                                            UserCreateResult.Text = "Successfully created user " + u.Identity;
                                        }
                                        else
                                        {
                                            UserCreateResult.CssClass = "error";
                                            UserCreateResult.Visible = true;
                                            UserCreateResult.Text = "Error creating user in Firebase";
                                        }
                                    }
                                    else
                                    {
                                        PasswordError.Visible = true;
                                        if (validPass == 1)
                                        {
                                            PasswordError.Text = "Password must contain at least 1 uppercase";
                                        }
                                        else if (validPass == 2)
                                        {
                                            PasswordError.Text = "Password must contain at least 1 lowercase";
                                        }
                                        else if (validPass == 3)
                                        {
                                            PasswordError.Text = "Password must contain at least 1 number";
                                        }
                                        else
                                        {
                                            PasswordError.Text = "Unknown password error";
                                        }
                                    }
                                }
                                else
                                {
                                    //display user failed to be created msg
                                    PasswordError.Visible = true;
                                    PasswordError.Text = "Password must be at least 8 chars";
                                }
                            }
                            else
                            {
                                //throw error, passwords don't match
                                PasswordError.Visible = true;
                                PasswordError.Text = "Passwords don't match";
                            }
                        }
                        else
                        {
                            //throw error, please select company for user
                            RoleCompanyError.Visible = true;
                            RoleCompanyError.Text = "Please select a company";
                        }
                    }
                    else
                    {
                        //throw error, please select role for new user
                        RoleCompanyError.Visible = true;
                        RoleCompanyError.Text = "Please select a role";
                    }
                }
                else
                {
                    NameError.Visible = true;
                    NameError.Text = "Please enter a first and last name";
                }
            }
            else
            {
                EmailError.Visible = true;
                EmailError.Text = "Email already in use";
            }

            ClearFields();
        }

        protected void CompanyBtn_Click(object sender, EventArgs e)
        {
            CompanyResult.Visible = false;
            CompanyError.Visible = false;

            string companyName = Company.Text;
            Company.Text = "";

            //Validate that the logged in user has permissions to do this

            if (companyName.Length > 0)
            {
                Company company = CompanyUtil.CreateCompany(companyName);
                if (company != null)
                {
                    User user = (User)Session["User"];
                    if (user != null)
                    {
                        Log.Info(user.Identity + " created a new company " + companyName);
                    }
                    else
                    {
                        Log.Info("System created a new company " + companyName);
                    }                    
                    //display user created msg
                    CompanyResult.Visible = true;
                    CompanyResult.Text = "Created company " + companyName;
                }
            }
            else
            {
                CompanyError.Visible = true;
                CompanyError.Text = "Please enter a company name";
            }

            ClearFields();
        }

        protected void UnlockAccountBtn_Click(object sender, EventArgs e)
        {
            UnlockResult.Visible = false;
            UnlockError.Visible = false;

            int userId = int.Parse(SelectedAccount.Value);
            if (userId > 0)
            {
                User lockedUser = UserUtil.GetUser(userId);
                FirebaseUtil.ForgotPassword(lockedUser.Email);
                UserUtil.ValidLogin(lockedUser);
                User user = (User)Session["User"];
                if(user != null)
                {
                    Log.Info(user.Identity + " unlocked an account " + user.Identity);
                }
                else
                {
                    Log.Info("System unlocked an account " + user.Identity);
                }
                UnlockResult.Visible = true;
                UnlockResult.Text = "Unlocked account " + lockedUser.Identity;
            }
            else
            {
                UnlockError.Visible = true;
                UnlockError.Text = "Please select an account to unlock";
            }

            ClearFields();
        }

        protected void UpdateUser(object sender, EventArgs e)
        {
            int roleID = int.Parse(UserSelectedRole.Value);
            int userID = int.Parse(UserID.Value);
            String fname = UserFirstName.Value.ToString();
            String lname = UserLastName.Value.ToString();
            UserUtil.UpdateUser(userID, roleID, fname, lname);
            Response.Redirect("Admin.aspx");
        }
        protected void DeleteUser(object sender, EventArgs e)
        {
            UserUtil.DeleteUser();
        }

        protected void MakeUserTable()
        {
            List<User> users = UserUtil.GetUsers();
            UserTable.InnerHtml = "";
            var userTable = "";
            userTable += "<table class=\"ui orange table\"><thead><tr><th>Name</th><th>Email</th><th>Company</th><th>Role</th></tr></thead>";
            userTable += "<tbody>";
            foreach (User user in users)
            {
                if (user.UserId != -1 && user.UserId != -2)
                {
                    Company company = CompanyUtil.GetCompany(user.CompanyId);
                    Role role = RoleUtil.GetRole(user.RoleId);
                    userTable += "<tr onclick=\"EditUser('"+user.FirstName+"','"+user.LastName+"','"+role.RoleName+"',"+role.RoleId+","+user.UserId+")\">" +
                        "<td>" + user.FullName + "</td><td>" + user.Email + "</td><td>" + company.CompanyName + "</td><td>" + role.RoleName + "</td></tr>";
                }
            }
            userTable += "</tbody></table>";
            UserTable.InnerHtml += userTable;
        }

        protected void MakeCompanyTable()
        {
            List<Company> companies = CompanyUtil.GetCompanies();
            CompanyTable.InnerHtml = "";
            var companyTable = "";
            companyTable += "<table class=\"ui orange table\"><thead><tr><th>Company Name</th></tr></thead>";
            companyTable += "<tbody>";
            foreach (Company company in companies)
            {
                if (company.CompanyId != -1 && company.CompanyId != -2)
                    companyTable += "<tr><td>" + company.CompanyName+ "</td></tr>";
               
            }
            companyTable += "</tbody></table>";
            CompanyTable.InnerHtml += companyTable;
        }
    }
}

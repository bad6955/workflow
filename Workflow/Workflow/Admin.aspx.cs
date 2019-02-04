using System;
using System.Web;
using System.Web.UI;
using Workflow.Data;
using Workflow.Models;

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
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];

                //checks user is an admin
                if(user.RoleId == 4)
                {
                    //DEBUG move selectors from below, here
                }
                else
                {
                    //kicks them out if they arent
                    //DEBUG uncomment the below in production
                    //Response.Redirect("Login.aspx");
                }
            }
            else
            {
                //kicks them out if they arent
                //DEBUG uncomment the below in production
                //Response.Redirect("Login.aspx");
            }

            //DEBUG move into the nested if above
            //fills out role selector dropdown
            RoleSelect.DataSource = RoleUtil.GetRoles();
            RoleSelect.DataTextField = "roleName";
            RoleSelect.DataValueField = "roleId";
            RoleSelect.DataBind();
            RoleSelect.SelectedIndex = 0;

            //fills out company dropdown
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
            //DEBUG end move
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
        }

        protected void CompanyBtn_Click(object sender, EventArgs e)
        {
            CompanyResult.Visible = false;
            CompanyError.Visible = false;

            string companyName = Company.Text;

            //Validate that the logged in user has permissions to do this

            if (companyName.Length > 0)
            {
                Company company = CompanyUtil.CreateCompany(companyName);
                if (company != null)
                {
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
        }

        protected void UnlockAccountBtn_Click(object sender, EventArgs e)
        {
            UnlockResult.Visible = false;
            UnlockError.Visible = false;

            int userId = int.Parse(SelectedAccount.Value);
            if(userId > 0)
            {
                User lockedUser = UserUtil.GetUser(userId);
                FirebaseUtil.ForgotPassword(lockedUser.Email);
                UserUtil.ValidLogin(lockedUser);
                UnlockResult.Visible = true;
                UnlockResult.Text = "Unlocked account " + lockedUser.Identity;
            }
            else
            {
                UnlockError.Visible = true;
                UnlockError.Text = "Please select an account to unlock";
            }
        }
    }
}

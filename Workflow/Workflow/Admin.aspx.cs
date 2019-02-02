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

        //Register a new user in the system
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
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
            if(roleId != -1)
            {
                if (companyId != -1)
                {
                    if (pass.Equals(pass2))
                    {
                        //creates the user in firebase
                        Firebase.Auth.User fbUser = FirebaseUtil.CreateNewUser(email, pass, displayName, verificationEmail);
                        if (fbUser != null)
                        {
                            //creates the user in the DB
                            if (firstName.Length > 0 && lastName.Length > 0)
                            {
                                User u = UserUtil.CreateUser(roleId, companyId, email, firstName, lastName);
                                u.FirebaseUser = fbUser;
                            }

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
                else
                {
                    //throw error, please select company for user
                }
            }
            else
            {
                //throw error, please select role for new user
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

        protected void UnlockAccountBtn_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(SelectedAccount.Value);
            if(userId > 0)
            {
                User lockedUser = UserUtil.GetUser(userId);
                FirebaseUtil.ForgotPassword(lockedUser.Email);
                UserUtil.ValidLogin(lockedUser);
            }
        }
    }
}

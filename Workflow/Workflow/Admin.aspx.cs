using System;
using System.Web;
using System.Web.UI;
using Firebase.Auth;

namespace Workflow
{

    public partial class Admin : System.Web.UI.Page
    {
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
                CreateNewUser(email, pass, displayName, verificationEmail);
            }
            else
            {
                //throw error, passwords don't match
            }
        }

        private void CreateNewUser(String email, String pass, String displayName, bool verificationEmail)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAEa4tI_IPceDWsVkIf86AgyFozzmeC6tI"));
            authProvider.CreateUserWithEmailAndPasswordAsync(email, pass, displayName, verificationEmail);
        }
    }
}

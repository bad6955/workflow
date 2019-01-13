using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Workflow
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        //Create a conntect to server here or get the conntection from login.aspx
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Run the follow code when the user want to change their password.
        protected void changeBtn_Click(object sender, EventArgs e)
        {
            checkPassword();
        }
        //This method will check the newly created password 
        //Should we also check the server password to see if there is a match?
        protected void checkPassword()
        {
            if(password.Text == matchedPassword.Text)
            {
                //Change the password on the server using email provided

                //Redriect to login
                Response.Redirect("Login.aspx");                
            }else
            {
                //Display the error message
            }
        }
        
    }
}
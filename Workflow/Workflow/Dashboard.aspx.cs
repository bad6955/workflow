using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Workflow
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //accessing the application variable set on the login code-behind and displaying it out to the user
            //For testing purpose, added in user to prevent error, when need to test the server
            // remove the "User" and uncomment the rest of code
            user.Text = "Welcome back, User"/* + Session["userEmail"].ToString()*/;

        }
    }
}
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Events in code-behind can be created using the OnClick attribute in an <ASP:*** /> tag
        //  you can either tie the page's event to an existing method in the code-behind, or 
        //  auto generate a new event with a name similar to the below
        protected void loginBtn_Click(object sender, EventArgs e)
        {
            Application["userEmail"] = email.Text; // setting an Application variable so it can be accessed on other pages securely
            Project p = new Project(); //creating new model class instance
            Response.Redirect("Dashboard.aspx"); //redirecting the user from Login.aspx to Dashboard.aspx
        }
    }
}
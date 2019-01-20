<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountSettings.aspx.cs" Inherits="Workflow.AccountSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script runat="server">
        void Page_Load(Object sender, EventArgs e)
        {
            dashboard.Click += new EventHandler(this.dashboardBtn_Click);
            workflow.Click += new EventHandler(this.workflowBtn_Click);
            project.Click += new EventHandler(this.projectBtn_Click);
            logout.Click += new EventHandler(this.logoutBtn_Click);
        }

        void dashboardBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        void workflowBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        void projectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        void formBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        void logoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }
    </script>
    <title>Account Settings</title>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- this will be the nav for now --> 
        <div id="navigation">
            <div id="top-bar">
                <div id="account-dropdown">
				    <h1>Username</h1>
			    </div>
            </div>
            <div id="side-bar">
                <ul>
                    <li><img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="dashboard" OnClick="dashboardBtn_Click" Text="Dashboard"/></li>
                    <li><img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="workflowBtn_Click" Text="Workflows" /></li>
                    <li><img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="projectBtn_Click" Text="Projects" /></li>
                    <li><img src="assets/icons/form.png" /><asp:Button runat="server" ID ="form" OnClick="formBtn_Click" Text="Forms" /></li>
                    <li><asp:Button runat="server" ID="logout" Text="Log Out" OnClick="logoutBtn_Click" /></li>
                </ul>
                <div id="help"><img src="assets/icons/help.png" /></div>
            </div>
        </div>
    </form>
</body>
</html>

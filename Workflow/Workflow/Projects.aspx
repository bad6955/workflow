<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="Workflow.Projects" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    
    <link href="assets/css/styles.css" rel="stylesheet" />

    <%-- The script handle events from the buttons --%>
    <script runat="server">
        void Page_Load(Object sender, EventArgs e)
        {
            dashboard.Click += new EventHandler(this.dashboardBtn_Click);
            workflow.Click += new EventHandler(this.workflowBtn_Click);
            form.Click += new EventHandler(this.formBtn_Click);
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
    <title>Project</title>
</head>
<body>
    <%-- --%>
    <div id="Title"><h1>Projects</h1></div>
    
    <form id="form1" runat="server">
        <%-- this will be the nav for now --%>
        <div id="nav">
            <asp:Button runat="server" ID="dashboard" Text="Dashboard" OnClick="dashboardBtn_Click" />
            <asp:Button runat="server" ID="workflow" Text="Workflow" OnClick="workflowBtn_Click" />
            <asp:Label runat="server" ID="project">Project</asp:Label>
            <asp:Button runat="server" ID ="form" Text="Form" OnClick="formBtn_Click" />
            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="logoutBtn_Click" />
        </div>
        <div>
        </div>
    </form>
    <div id="footer"><h3>This is a footer</h3></div>

</body>
</html>

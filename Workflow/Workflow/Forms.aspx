<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Workflow.Forms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- The script handle events from the buttons --%>
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

        void logoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

    </script>
    <title>Forms</title>
</head>
<body>
    <div id="Title"><h1>Forms</h1></div>
    <form id="form1" runat="server">
        <%-- this will be the nav for now --%>
        <div id="nav">
            <asp:Button runat="server" ID="dashboard" Text="Dashboard" OnClick="dashboardBtn_Click" />
            <asp:Button runat="server" ID="workflow" Text="Workflow" OnClick="workflowBtn_Click" />
            <asp:Button runat="server" ID="project" Text="Project" OnClick="projectBtn_Click" />
            <asp:Label runat="server" ID="current">Forms</asp:Label>    
            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="logoutBtn_Click" />
        </div>
        <div>
        </div>
    </form>
    <div id="footer"><h3>This is the footer</h3></div>
</body>
</html>

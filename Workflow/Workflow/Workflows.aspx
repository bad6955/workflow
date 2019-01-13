<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="Workflow.Workflows" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- The script handle events from the buttons --%>
    <script runat="server">
        void Page_Load(Object sender, EventArgs e)
        {
            dashboard.Click += new EventHandler(this.dashboardBtn_Click);
            project.Click += new EventHandler(this.projectBtn_Click);
            form.Click += new EventHandler(this.formBtn_Click);
            logout.Click += new EventHandler(this.dashboardBtn_Click);
        }

        void dashboardBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
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
    <title>Workflow</title>
</head>
<body>
    <%--  --%>
    <div id="Title"><h1>Workflows</h1></div>
    <form id="form1" runat="server">
        <%-- this will be the nav for now --%>
        <div id="nav">
            <asp:Button runat="server" ID="dashboard" Text="Dashboard" OnClick="dashboardBtn_Click" />
            <asp:Label runat="server" ID="current">Workflows</asp:Label>    
            <asp:Button runat="server" ID="project" Text="Project" OnClick="projectBtn_Click" />
            <asp:Button runat="server" ID ="form" Text="Form" OnClick="formBtn_Click" />
            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="logoutBtn_Click" />
        </div>
        <div>
        </div>
    </form>
    <div id="footer"><h3>This is the footer</h3></div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- The script handle events from the buttons --%>
    <script runat="server">
        void Page_Load(Object sender, EventArgs e)
        {
            project.Click += new EventHandler(this.projectBtn_Click);
            workflow.Click += new EventHandler(this.workflowBtn_Click);
            form.Click += new EventHandler(this.formBtn_Click);
            logout.Click += new EventHandler(this.logoutBtn_Click);
        }

        void projectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        void formBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        void workflowBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        void logoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

    </script>
    <title>Dashboard</title>
</head>
<body>
    <div id="Title"><asp:Label runat="server" ID="user"></asp:Label></div>
    <form id="form1" runat="server">
        <%-- this will be the nav for now --%> 
        <div id="nav">
            <asp:Label runat="server" ID="current">Dashboard</asp:Label>
            <asp:Button runat="server" ID="workflow" Text="Workflow" OnClick="workflowBtn_Click" />
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

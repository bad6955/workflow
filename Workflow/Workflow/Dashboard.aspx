<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
</head>
<body>
    <div id="Title"><asp:Label runat="server" ID="user"></asp:Label></div>
    <form id="form1" runat="server">
        <!-- this will be the nav for now --> 
        <div id="nav">
            <asp:Label runat="server" ID="current">Dashboard</asp:Label>
            <asp:Button runat="server" ID="workflow" Text="Workflow" OnClick="WorkflowBtn_Click" />
            <asp:Button runat="server" ID="project" Text="Project" OnClick="ProjectBtn_Click" />
            <asp:Button runat="server" ID ="form" Text="Form" OnClick="FormBtn_Click" />
            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="LogoutBtn_Click" />
        </div>
    </form>
    <div id="footer"><h3>This is the footer</h3></div>
</body>
</html>

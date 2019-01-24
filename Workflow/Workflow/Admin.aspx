<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workflow.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creations Admin Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span>Create User</span>
            <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="FirstName" placeholder="First Name"></asp:TextBox>
            <asp:TextBox runat="server" ID="LastName" placeholder="Last Name"></asp:TextBox>
            <asp:TextBox runat="server" ID="Password" placeholder="Password"></asp:TextBox>
            <asp:TextBox runat="server" ID="PasswordRepeat" placeholder="Repeat Password"></asp:TextBox>
            <asp:Button runat="server" ID="RegisterBtn" Text="Register" OnClick="RegisterBtn_Click" />
        </div>
        <div>
            <span>Create Company</span>
            <asp:TextBox runat="server" ID="Company" placeholder="Company Name"></asp:TextBox>
            <asp:Button runat="server" ID="CompanyBtn" Text="Create Company" OnClick="CompanyBtn_Click" />
        </div>
    </form>
</body>
</html>

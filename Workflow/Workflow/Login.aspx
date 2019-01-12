<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Workflow.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="password" placeholder="Password"></asp:TextBox>
            <asp:Button runat="server" ID="loginBtn" Text="Login" OnClick="loginBtn_Click" />
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Workflow.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creation Log in</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="Password" placeholder="Password"></asp:TextBox>
            <asp:Button runat="server" ID="LoginBtn" Text="Login" OnClick="LoginBtn_Click" />
            <asp:Button runat="server" ID="ForgotBtn" Text="Forgot Password" OnClick="ForgotBtn_Click" />
        </div>
    </form>
</body>
</html>

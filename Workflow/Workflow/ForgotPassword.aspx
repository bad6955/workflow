<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Workflow.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
</head>
<body>
    <<form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="password" placeholder="Password"></asp:TextBox>
            <asp:TextBox runat="server" ID="matchedPassword" placeholder="Password"></asp:TextBox>
            <asp:Button runat="server" ID="changeBtn" Text="Change Password" OnClick="changeBtn_Click" />           
        </div>
    </form>
</body>
</html>

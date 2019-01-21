<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Workflow.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/gradient_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="forgot-password">
            <img src="assets/icons/rit_insignia.png" />
            <h1>RESET PASSWORD</h1>
            <asp:Label runat="server" ID="ErrorLabel" CssClass="error"></asp:Label>
            <asp:Label runat="server" ID="SuccessLabel" CssClass="success"></asp:Label>
            <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
            <asp:Button runat="server" ID="changeBtn" Text="Reset Password" OnClick="ChangeBtn_Click" />           
        </div>
    </form>
</body>
</html>

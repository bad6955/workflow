<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Workflow.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png"/>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/gradient_style.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="forgot-password">
            <img src="assets/icons/rit_insignia.png" />
            <h1>RESET PASSWORD</h1>
            <asp:Label runat="server" ID="ErrorLabel" CssClass="error"></asp:Label>
            <asp:Label runat="server" ID="SuccessLabel" CssClass="success"></asp:Label>
            <div class="ui left icon input">
                <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
                <i class="large envelope icon"></i>
            </div>
            <asp:Button runat="server" ID="changeBtn" Text="Reset Password" OnClick="ChangeBtn_Click" />
            <br />
            <asp:Button runat="server" ID="returnBtn" Text="Return to Login" OnClick="ReturnBtn_Click" />
        </div>
    </form>
</body>
</html>

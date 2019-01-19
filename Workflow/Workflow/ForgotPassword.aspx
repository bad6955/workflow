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
    <<form id="form1" runat="server">
        <div id="forgot-password">
            <img src="assets/icons/rit_insignia.png" />
            <h1>RESET PASSWORD</h1>
            <asp:TextBox runat="server" ID="email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="password" placeholder="Password"></asp:TextBox>
            <asp:TextBox runat="server" ID="matchedPassword" placeholder="Confirm Password"></asp:TextBox>
            <asp:Button runat="server" ID="changeBtn" Text="Change Password" OnClick="changeBtn_Click" />           
        </div>
    </form>
</body>
</html>

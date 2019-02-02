<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Workflow.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creation Log in</title>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/gradient_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <div id="login">
        <div id="login-right">
            <div class="login-text">
                <img src="assets/icons/rit_insignia.png" />
                <h1>LOG IN</h1>
                <form id="form1" runat="server">
                    <div>
                        <asp:Label runat="server" ID="ErrorLabel2" CssClass="error"></asp:Label><br />
                        <asp:Label runat="server" ID="ErrorLabel" CssClass="error"></asp:Label>
                        <div class="email-control">
                        <div class="login-email-icon"></div>
                        <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox><br />
                        </div>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Password"></asp:TextBox><br />
                        <asp:Button runat="server" ID="LoginBtn" Text="Login" OnClick="LoginBtn_Click" /><br />
                        <asp:Button runat="server" ID="ForgotBtn" Text="Forgot Password" OnClick="ForgotBtn_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>

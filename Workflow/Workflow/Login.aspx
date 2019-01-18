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
        <div id="login-left">
            <div class="login-text">
                <h1>Venture Creations</h1> <hr>
                <p><i>Where brilliant minds assemble and collaborate, where they pool together their individual talents across disciplines in service of big ideas and creative solutions.</i></p>
            </div>
        </div>
        <div id="login-right">
            <div class="login-text">
                <form id="form1" runat="server">
                    <div>
                        <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Password"></asp:TextBox>
                        <asp:Button runat="server" ID="LoginBtn" Text="Login" OnClick="LoginBtn_Click" />
                        <asp:Button runat="server" ID="ForgotBtn" Text="Forgot Password" OnClick="ForgotBtn_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div id="white-background"></div>
</body>
</html>

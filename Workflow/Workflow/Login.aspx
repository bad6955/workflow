﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Workflow.Login" %>

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
<script src="https://www.gstatic.com/firebasejs/5.7.3/firebase.js"></script>
<script>
      // Initialize Firebase
      var config = {
        apiKey: "AIzaSyAEa4tI_IPceDWsVkIf86AgyFozzmeC6tI",
        authDomain: "venturecreationsworkflow.firebaseapp.com",
        databaseURL: "https://venturecreationsworkflow.firebaseio.com",
        projectId: "venturecreationsworkflow",
        storageBucket: "venturecreationsworkflow.appspot.com",
        messagingSenderId: "75896481614"
      };
      firebase.initializeApp(config);
</script>
</html>

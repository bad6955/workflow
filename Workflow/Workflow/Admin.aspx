<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workflow.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creations Admin Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="Password" placeholder="Password"></asp:TextBox>
            <asp:TextBox runat="server" ID="PasswordRepeat" placeholder="Password"></asp:TextBox>
            <asp:Button runat="server" ID="RegisterBtn" Text="Register" OnClick="RegisterBtn_Click" />
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

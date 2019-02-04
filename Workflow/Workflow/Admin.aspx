<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workflow.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creations Admin Page</title>
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
    <!-- Bootstrap CSS -->
    <link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
</head>
<script>
    function saveSelection() {
        var roleEle = document.getElementById("RoleSelect");
        var role = roleEle.options[roleEle.selectedIndex].value;

        var companyEle = document.getElementById("CompanySelect");
        var company = companyEle.options[companyEle.selectedIndex].value;

        var accountEle = document.getElementById("LockedAccountSelect");
        var account = accountEle.options[accountEle.selectedIndex].value;

        document.getElementById("SelectedRole").value = role;
        document.getElementById("SelectedCompany").value = company;
        document.getElementById("SelectedAccount").value = account;
    }
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="UserCreateResult" CssClass="success" Visible="false"></asp:Label><br />
            <span>Create User</span><br />
            <asp:Label runat="server" ID="EmailError" CssClass="error" Visible="false"></asp:Label><br />
            <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox><br />
            <asp:Label runat="server" ID="NameError" CssClass="error" Visible="false"></asp:Label><br />
            <asp:TextBox runat="server" ID="FirstName" placeholder="First Name"></asp:TextBox>
            <asp:TextBox runat="server" ID="LastName" placeholder="Last Name"></asp:TextBox><br />
            <asp:Label runat="server" ID="PasswordError" CssClass="error" Visible="false"></asp:Label><br />
            <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Password"></asp:TextBox>
            <asp:TextBox runat="server" ID="PasswordRepeat" TextMode="Password" placeholder="Repeat Password"></asp:TextBox><br />
            <asp:Label runat="server" ID="RoleCompanyError" CssClass="error" Visible="false"></asp:Label><br />
            <asp:DropDownList runat="server" ID="RoleSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="CompanySelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList><br />
            <asp:Button runat="server" ID="RegisterBtn" Text="Register User" OnClick="RegisterBtn_Click" />
            <asp:HiddenField runat="server" ID="SelectedRole" />
            <asp:HiddenField runat="server" ID="SelectedCompany" />
        </div>
        <hr />
        <div>
            <asp:Label runat="server" ID="CompanyResult" CssClass="success" Visible="false"></asp:Label><br />
            <span>Create Company</span><br />
            <asp:Label runat="server" ID="CompanyError" CssClass="error" Visible="false"></asp:Label><br />
            <asp:TextBox runat="server" ID="Company" placeholder="Company Name"></asp:TextBox>
            <asp:Button runat="server" ID="CompanyBtn" Text="Create Company" OnClick="CompanyBtn_Click" />
        </div>
        <hr />
        <div>
            <asp:Label runat="server" ID="UnlockResult" CssClass="success" Visible="false"></asp:Label><br />
            <span>Unlock Accounts</span><br />
            <asp:Label runat="server" ID="UnlockError" CssClass="error" Visible="false"></asp:Label><br />
            <asp:DropDownList runat="server" ID="LockedAccountSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
            <asp:Button runat="server" ID="UnlockAccountBtn" Text="Unlock Account" OnClick="UnlockAccountBtn_Click"/>
            <asp:HiddenField runat="server" ID="SelectedAccount" />
        </div>
    </form>
</body>
</html>

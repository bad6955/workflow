<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workflow.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creations Admin Page</title>
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
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
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                </div>
            </div>
        </div>
        <div id="admin-panel" style="padding-top: 4em">
            <div class="ui secondary menu">
                <a class="item active" data-tab="first">User</a>
                <a class="item" data-tab="second">Company</a>
                <a class="item" data-tab="third">Password Reset</a>
            </div>
            <div class="ui tab segment active" data-tab="first">
                <span>Create User</span><br />
                <div class="ui input">
                    <asp:TextBox runat="server" ID="FirstName" placeholder="First Name"></asp:TextBox></div>
                <div class="ui input">
                    <asp:TextBox runat="server" ID="LastName" placeholder="Last Name"></asp:TextBox></div>
                <div class="ui input">
                    <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox></div>
                <div class="ui input">
                    <asp:TextBox runat="server" ID="Password" placeholder="Password"></asp:TextBox></div>
                <div class="ui input">
                    <asp:TextBox runat="server" ID="PasswordRepeat" placeholder="Repeat Password"></asp:TextBox></div>
                <asp:DropDownList runat="server" ID="RoleSelect" CssClass="ui dropdown" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="CompanySelect" CssClass="ui dropdown" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:Button runat="server" ID="RegisterBtn" CssClass="ui secondary button" Text="Register" OnClick="RegisterBtn_Click" />
                <asp:HiddenField runat="server" ID="SelectedRole" />
                <asp:HiddenField runat="server" ID="SelectedCompany" />
            </div>
            <hr />
            <div class="ui tab segment" data-tab="second">
                <span>Create Company</span><br />
                <div class="ui input">
                    <asp:TextBox runat="server" ID="Company" placeholder="Company Name"></asp:TextBox></div>
                <asp:Button runat="server" ID="CompanyBtn" CssClass="ui secondary button" Text="Create Company" OnClick="CompanyBtn_Click" />
            </div>
            <hr />
            <div class="ui tab segment" data-tab="third">
                <span>Unlock Accounts</span><br />
                <asp:DropDownList runat="server" ID="LockedAccountSelect" CssClass="ui dropdown" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:Button runat="server" ID="UnlockAccountBtn" CssClass="ui secondary button" Text="Unlock Account" OnClick="UnlockAccountBtn_Click" />
                <asp:HiddenField runat="server" ID="SelectedAccount" />
            </div>
        </div>
    </form>
    <script>
        $('#admin-panel .menu .item')
            .tab({
                // special keyword works same as above
                context: $('#admin-panel')
            })
            ;
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workflow.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creations Admin Page</title>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png"/>

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
    <form class="omb_loginForm" id="form1" runat="server">
        <h1>Administration</h1>
        <div class="ui top attached tabular menu">
            <a class="item active" data-tab="user">Users</a>
            <a class="item" data-tab="company">Companies</a>
            <a class="item" data-tab="unlock">Unlock Account</a>
        </div>
        <div class="ui bottom attached tab segment active" data-tab="user">
            <div class="ui placeholder segment">
                <div class="ui two column very relaxed stackable grid">
                    <div class="column">
                        <div class="ui form">
                            <h3>User Registration</h3>
                            <asp:Label runat="server" ID="UserCreateResult" CssClass="success" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="EmailError" CssClass="error" Visible="false"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox runat="server" class="form-control" name="newid" ID="Email" placeholder="Email"></asp:TextBox>
                            </div>
                            <span class="help-block"></span>

                            <asp:Label runat="server" ID="NameError" CssClass="error" Visible="false"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox runat="server" class="form-control" name="newid" ID="FirstName" placeholder="First Name"></asp:TextBox>
                                <asp:TextBox runat="server" class="form-control" name="newid" ID="LastName" placeholder="Last Name"></asp:TextBox>
                            </div>
                            <span class="help-block"></span>

                            <asp:Label runat="server" ID="PasswordError" CssClass="error" Visible="false"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox runat="server" TextMode="Password" class="form-control" name="newid" ID="Password" placeholder="Password"></asp:TextBox>
                                <asp:TextBox runat="server" TextMode="Password" class="form-control" name="newid" ID="PasswordRepeat" placeholder="Repeat Password"></asp:TextBox>
                            </div>

                            <asp:Label runat="server" ID="RoleCompanyError" CssClass="error" Visible="false"></asp:Label>
                            <div class="input-group">
                                <asp:DropDownList runat="server" class="form-control" name="newid" ID="RoleSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                                <asp:DropDownList runat="server" class="form-control" name="newid" ID="CompanySelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                            </div>

                            <div class="input-group">
                                <asp:Button runat="server" class="btn btn-dark" ID="RegisterBtn" Text="Register" OnClick="RegisterBtn_Click" />
                            </div>
                            <!-- HiddenFields don't need styles -->
                            <asp:HiddenField runat="server" ID="SelectedRole" />
                            <asp:HiddenField runat="server" ID="SelectedCompany" />
                        </div>
                    </div>
                    <div class="middle aligned column">
                        <h3>All Users</h3>
                        <div runat="server" class="middle aligned column" id="UserTable"></div>
                    </div>
                </div>
                <div class="ui vertical divider">
                    <i class="user circle icon"></i>
                </div>
            </div>
        </div>
        <div class="ui bottom attached tab segment" data-tab="company">
            <div class="ui placeholder segment">
                <div class="ui two column very relaxed stackable grid">
                    <div class="column">
                        <h3><span>Create Company</span></h3>
                        <br />
                        <asp:Label runat="server" ID="CompanyResult" CssClass="success" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="CompanyError" CssClass="error" Visible="false"></asp:Label><br />
                        <asp:TextBox runat="server" class="form-control" name="newid" ID="Company" placeholder="Company Name"></asp:TextBox>
                        <asp:Button runat="server" type="button" class="btn btn-success btn-block" ID="CompanyBtn" Text="Create Company" OnClick="CompanyBtn_Click" />
                    </div>
                    <div class="middle aligned column">
                        <h3>All Companies</h3>
                        <div runat="server" class="middle aligned column" id="CompanyTable"></div>
                    </div>
                </div>
                <div class="ui vertical divider">
                    <i class="address icon"></i>
                </div>
            </div>
        </div>
        <div class="ui bottom attached tab segment" data-tab="unlock">
            <div>
                <h3><span>Unlock Accounts</span><br />
                </h3>
                <asp:Label runat="server" ID="UnlockResult" CssClass="success" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="UnlockError" CssClass="error" Visible="false"></asp:Label><br />
                <asp:DropDownList runat="server" ID="LockedAccountSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:Button runat="server" type="button" class="btn btn-success " ID="UnlockAccountBtn" Text="Unlock Account" OnClick="UnlockAccountBtn_Click" />
                <asp:HiddenField runat="server" ID="SelectedAccount" />
            </div>
        </div>
    </form>
    <script>
        $('.menu .item')
            .tab()
            ;
    </script>
</body>
</html>

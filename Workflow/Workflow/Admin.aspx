<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workflow.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Venture Creations Admin Page</title>
</head>
<script>
    function saveSelection() {
        var roleEle = document.getElementById("RoleSelect");
        var role = roleEle.options[roleEle.selectedIndex].value;
        var companyEle = document.getElementById("CompanySelect");
        var company = companyEle.options[companyEle.selectedIndex].value;
        console.log("Role :" + role);
        console.log("Company : " + company);
        document.getElementById("SelectedRole").value = role;
        document.getElementById("SelectedCompany").value = company;
    }
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <span>Create User</span><br />
            <asp:TextBox runat="server" ID="Email" placeholder="Email"></asp:TextBox>
            <asp:TextBox runat="server" ID="FirstName" placeholder="First Name"></asp:TextBox>
            <asp:TextBox runat="server" ID="LastName" placeholder="Last Name"></asp:TextBox>
            <asp:TextBox runat="server" ID="Password" placeholder="Password"></asp:TextBox>
            <asp:TextBox runat="server" ID="PasswordRepeat" placeholder="Repeat Password"></asp:TextBox>
            <asp:DropDownList runat="server" ID="RoleSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="CompanySelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
            <asp:Button runat="server" ID="RegisterBtn" Text="Register" OnClick="RegisterBtn_Click" />
            <asp:HiddenField runat="server" ID="SelectedRole" />
            <asp:HiddenField runat="server" ID="SelectedCompany" />
        </div>
        <hr />
        <div>
            <span>Create Company</span><br />
            <asp:TextBox runat="server" ID="Company" placeholder="Company Name"></asp:TextBox>
            <asp:Button runat="server" ID="CompanyBtn" Text="Create Company" OnClick="CompanyBtn_Click" />
        </div>
    </form>
</body>
</html>

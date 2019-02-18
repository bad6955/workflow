<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Workflow.Forms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forms</title>
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="assets/js/form-builder.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
    <script>
        function approvalDialog() {
            console.log("Opening dialog");
            $("#ApprovalDialog").dialog({
                resizable: false,
                height: "auto",
                width: 400,
                modal: true,
                buttons: {
                    Cancel: function () {
                        document.getElementById("ApprovalDialogStatus").value = "0";
                        $(this).dialog("close");
                    }
                }
            });
        }

        function saveSelection() {
            var approvalEle = document.getElementById("ApprovalCt");
            var approvalCt = approvalEle.options[approvalEle.selectedIndex].value;
            document.getElementById("SelectedApprovalCt").value = approvalCt;

            if (approvalCt > 0) {
                var role1Ele = document.getElementById("ApprovalRole1");
                var role1 = role1Ele.options[role1Ele.selectedIndex].value;
                document.getElementById("SelectedApprovalRole1").value = role1;
            }
            else if (approvalCt > 1) {
                var role2Ele = document.getElementById("ApprovalRole2");
                var role2 = role2Ele.options[role2Ele.selectedIndex].value;
                document.getElementById("SelectedApprovalRole2").value = role2;
            }
            else if (approvalCt > 2) {
                var role3Ele = document.getElementById("ApprovalRole3");
                var role3 = role3Ele.options[role3Ele.selectedIndex].value;
                document.getElementById("SelectedApprovalRole3").value = role3;
            }
            else if (approvalCt > 3) {
                var role4Ele = document.getElementById("ApprovalRole4");
                var role4 = role4Ele.options[role4Ele.selectedIndex].value;
                document.getElementById("SelectedApprovalRole4").value = role4;
            }
        }
    </script>
</head>
<body>
    <%-- --%>
    <form id="form1" runat="server">
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                    <div id="account-dropdown">
                        <img src="assets/icons/person.png" />
                        <h1>
                            <asp:Label runat="server" ID="userLbl"></asp:Label></h1>
                        <div id="dropdown-content">
                            <a href="AccountSettings.aspx">
                                <h2>Account Settings</h2>
                            </a>
                            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="LogoutBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="side-bar">
                <div id="side-bar-top-content">
                    <ul>
                        <li>
                            <img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="dashboard" OnClick="DashboardBtn_Click" Text="Dashboard"></asp:Button></li>
                        <li>
                            <img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li>
                            <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li>
                            <img src="assets/icons/form.png" /><asp:Button runat="server" ID="current" Text="Forms" /></li>
                    </ul>
                </div>
                <div id="help">
                    <img src="assets/icons/help.png" /></div>
            </div>
        </div>
        <div id="content-body">
            <h1>Forms</h1>
            <div id="build-wrap"></div>
            <div runat="server" id="adminDiv" visible="false">
                <span>Form Editor Alpha</span><br />
                <asp:TextBox runat="server" ID="FormName" placeholder="Form Name"></asp:TextBox>
                <div class="form-editor" runat="server">
                    <fieldset runat="server" id="FormFieldset">
                        <div runat="server" id="FormDiv0" class="form-editor-field">
                            <asp:TextBox runat="server" ID="FormField0" placeholder="Form Field Text" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div runat="server" id="FormDiv1" class="form-editor-field">
                            <asp:CheckBox runat="server" ID="FormField1" Text="Approval Text"></asp:CheckBox>
                        </div>
                    </fieldset>
                    <asp:Button runat="server" ID="CreateTextFieldBtn" Text="Add Text Field to Form" OnClick="CreateTextFieldBtn_Click" />
                    <asp:Button runat="server" ID="CreateApprovalPopupBtn" Text="Add Approvals to Form" OnClick="CreateApprovalPopupBtn_Click" OnClientClick="approvalDialog();" />
                </div>
                <asp:Button runat="server" ID="CreateFormBtn" Text="Create Form" OnClick="CreateFormBtn_Click" />
            </div>
        </div>

        <asp:Panel ID="ApprovalDialog" title="Add Approvals to Form" runat="server" Visible="false">
            <div>
                <span class="ui-icon ui-icon-alert" style="float: left; margin: 12px 12px 20px 0;"></span>
                How many approvals would you like to add?
              <asp:DropDownList runat="server" ID="ApprovalCt" onchange="saveSelection()" OnSelectedIndexChanged="ApprovalCt_SelectedIndexChanged" AutoPostBack="false">
                  <asp:ListItem Text="1" Value="1"></asp:ListItem>
                  <asp:ListItem Text="2" Value="2"></asp:ListItem>
                  <asp:ListItem Text="3" Value="3"></asp:ListItem>
                  <asp:ListItem Text="4" Value="4"></asp:ListItem>
              </asp:DropDownList>
                <div id="ApprovalRoles" runat="server">
                    &nbsp;&nbsp;What role(s) need to approve it?<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ApprovalRole1" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ApprovalRole2" onchange="saveSelection()" AutoPostBack="false" Visible="false"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ApprovalRole3" onchange="saveSelection()" AutoPostBack="false" Visible="false"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ApprovalRole4" onchange="saveSelection()" AutoPostBack="false" Visible="false"></asp:DropDownList>
                </div>
                <asp:Button runat="server" ID="CreateApprovalBtn" Text="Add Approval Fields to Form" OnClick="CreateApprovalBtn_Click" />
                <asp:HiddenField runat="server" ID="ApprovalDialogStatus" Value="0" />
                <asp:HiddenField runat="server" ID="SelectedApprovalCt" />
                <asp:HiddenField runat="server" ID="SelectedApprovalRole1" />
                <asp:HiddenField runat="server" ID="SelectedApprovalRole2" />
                <asp:HiddenField runat="server" ID="SelectedApprovalRole3" />
                <asp:HiddenField runat="server" ID="SelectedApprovalRole4" />
            </div>
        </asp:Panel>
        <script>
            $('#build-wrap').formBuilder();
        </script>
    </form>
</body>
</html>

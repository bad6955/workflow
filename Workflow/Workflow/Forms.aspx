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
    <script type="text/javascript" src="assets/js/formToHtml.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
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
            <div runat="server" id="buildWrap"></div>
            <asp:Button runat="server" ID="CreateFormBtn" Text="Create Form" OnClientClick="SaveForm()" />
            <asp:HiddenField runat="server" ID="formBuilderData" />
            <!--
            <div runat="server" id="adminDiv" visible="false">
            </div>
            -->
        </div>
        <script>
            var options = {
                onSave: function (formData) {
                    //document.getElementById("formBuilderData").value = JSON.stringify(formData);
                    document.getElementById("formBuilderData").value = formBuilder.formData;
                },
            };
            var formBuilder = $('#buildWrap').formBuilder(options);

            function SaveForm() {
                jQuery{

                }
                //console.log(e("#buildWrap").formRender("html"))
                /*
                const data = formBuilder.formData;
                const markup = $("<div/>");
                markup.formRender({ data });
                document.getElementById("formBuilderData").value = markup.formRender("html")
                //document.getElementById("formBuilderData").value = formBuilder.formData;
                */
            }
        </script>
    </form>
</body>
</html>

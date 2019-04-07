<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="Workflow.Workflows" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workflow</title>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png" />
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>
    <script type="text/javascript" src="assets/js/script.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
    <!-- Bootstrap CSS -->
    <link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                    <div id="account-dropdown">
                        <i class="large user circle outline icon"></i>
                        <h1>
                            <asp:Label runat="server" ID="userLbl"></asp:Label></h1>
                        <div id="dropdown-content">
                            <asp:Button runat="server" ID="AdminBtn" Text="Admin Panel" OnClick="AdminBtn_Click" Visible="false" />
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
                        <li id="current-page">
                            <img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="current" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li>
                            <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li>
                            <img src="assets/icons/form.png" /><asp:Button runat="server" ID="form" OnClick="FormBtn_Click" Text="Forms" /></li>
                    </ul>
                    <a id="help" href="FAQ.aspx">
                        <img src="assets/icons/help.png" />
                    </a>
                </div>
            </div>
        </div>

        <div id="content-body">
            <asp:Label runat="server" ID="WorkflowError" Visible="false" CssClass="error"></asp:Label>
            <div runat="server" id="workflowListing">
                <h1>Workflows</h1>
                <asp:Button runat="server" ID="CreateNewWorkflowBtn" Text="Create New Workflow" OnClick="CreateNewWorkflowBtn_Click" CssClass="fluid ui button" />
                <div class="ui secondary segment">
                    <div class="ui floating dropdown labeled icon button">
                        <i class="filter icon"></i>
                        <span class="text">Sort Filter</span>
                        <div class="menu">
                            <div class="ui icon search input">
                                <i class="search icon"></i>
                                <input type="text" placeholder="Search..." />
                            </div>
                            <div class="divider"></div>
                            <div class="header">
                                <i class="tags icon"></i>
                                Sort Filter
                            </div>
                            <div class="scrolling menu">
                                <div class="item">
                                    <div class="ui orange empty circular label"></div>
                                    All
                                </div>
                                <div class="item">
                                    <div class="ui red empty circular label"></div>
                                    Open
                                </div>
                                <div class="item">
                                    <div class="ui blue empty circular label"></div>
                                    Closed
                                </div>
                            </div>
                        </div>
                    </div>
                    <p runat="server" id="numberShowing"></p>
                    <script>
                        $('.ui.dropdown')
                            .dropdown();
                    </script>
                </div>
                <div runat="server" class="ui items" id="workflowList">
                </div>
                <asp:Button runat="server" ID="Button2" Text="Show 5 More..." OnClick="LoadMoreWorkflows" CssClass="fluid ui button" />
            </div>

            <div runat="server" id="workflowBuilder" visible="false">
                <div id="formName">
                    <div class="ui left corner labeled input">
                        <asp:TextBox runat="server" ID="WorkflowName" placeholder="Workflow Name"></asp:TextBox>
                        <div class="ui teal left corner label">
                            <i class="white asterisk icon"></i>
                        </div>
                    </div>
                </div>
                <asp:PlaceHolder runat="server" ID="WorkflowSteps" />
                <div id="workflow-creation-buttons">
                    <asp:Button runat="server" ID="AddWorkflowComponentBtn" CssClass="ui inverted secondary button" Text="Add Workflow Step" OnClick="AddWorkflowComponentBtn_Click" />
                    <asp:Button runat="server" ID="CreateWorkflowBtn" CssClass="ui teal button" Text="Create Workflow" OnClick="CreateWorkflowBtn_Click" />
                </div>
            </div>

            <div runat="server" id="workflowViewer" visible="false">
            </div>
        </div>
    </form>
    <script src="assets/jquery/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>

    <script>
        $('.ui.selection.dropdown').dropdown();
    </script>

</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountSettings.aspx.cs" Inherits="Workflow.AccountSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Settings</title>
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- this will be the nav for now -->
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                    <div id="account-dropdown">
                        <i class="large user circle outline icon"></i>
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
                            <!-- TODO: gotta link this in the .cs -->
                            <img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="dashboard" OnClick="DashboardBtn_Click" Text="Dashboard"></asp:Button></li>
                        <li>
                            <img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li>
                            <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li>
                            <img src="assets/icons/form.png" /><asp:Button runat="server" ID="form" OnClick="FormBtn_Click" Text="Forms" /></li>

                    </ul>
                </div>
                <div id="help">
                    <img src="assets/icons/help.png" />
                </div>
            </div>
        </div>
    </form>
    <!-- everything has to be in content-body so it doesn't get cut off by the nav -->
    <div id="content-body">
        <h1>Account Settings</h1>
        <div class="ui raised very padded text container segment">
            <div class="ui form">
                <div class="two fields">
                    <div class="field">
                        Email:
                    </div>
                    <div class="field">
                        <input type="email" name="email" placeholder="INSERT USER EMAIL HERE" />
                    </div>
                </div>
                <div id="account-settings-password">
                    <div class="two fields">
                        <div class="field">
                            Password:
                        </div>
                        <div class="field">
                            <a href="">Change Password</a>
                        </div>
                    </div>
                </div>
                <div class="two fields">
                    <div class="field">
                        Email Alerts:
                    </div>
                    <select class="ui dropdown">
                        <option value="0">Changes to my projects</option>
                        <option value="1">Approved or denied</option>
                        <option value="2">Approved only</option>
                        <option value="3">Denied only</option>
                        <option value="4">No notifications</option>
                    </select>
                </div>
                <div id="button">
                    <button class="ui button" type="submit">Save</button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

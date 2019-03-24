<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logs.aspx.cs" Inherits="Workflow.Logs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log</title>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png"/>
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
                            <asp:Button runat="server" ID="AdminBtn" Text="Admin Panel" OnClick="AdminBtn_Click" Visible="false"/>
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
                        <li id="current-page">
                            <img src="assets/icons/form.png" /><asp:Button runat="server" ID="LogBtn" OnClick="LogBtn_Click" Text="Logs" />
                        </li>
                    </ul>
                </div>
                <div id="help">
                    <img src="assets/icons/help.png" />
                </div>
            </div>
        </div>
        <!-- everything has to be in content-body so it doesn't get cut off by the nav -->
        <div id="content-body">
            <div id="vc-coach-dashboard-top">
                <div class="ui placeholder segment">
                    <div class="ui two column stackable center aligned grid">
                        <div class="middle aligned row">
                            <asp:Placeholder runat="server" ID="LogText"></asp:Placeholder>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

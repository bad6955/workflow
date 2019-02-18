﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
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
                        <li id="current-page">
                            <img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="current" Text="Dashboard"></asp:Button></li>
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
        <!-- everything has to be in content-body so it doesn't get cut off by the nav -->
        <div id="content-body">
            <div><asp:Panel runat="server" ID="test"></asp:Panel></div>
            <h1>Dashboard</h1>
            <div id="vc-coach-dashboard-top">
                <div class="ui placeholder segment">
                    <div class="ui two column stackable center aligned grid">
                        <div class="ui vertical divider"></div>
                        <div class="middle aligned row">
                            <div class="column">
                                <h2>Activity Feed</h2>
                                <div runat="server" class="ui relaxed divided list" id="activityFeed"></div>
                            </div>
                            <div class="column">
                                <h2>Projects Overview</h2>
                                <!-- All open projects, my projects waiting on another, my projects 2 weeks without activity, my projects awaiting approval -->
                                <canvas id="pie-chart" width="800" height="250"></canvas>
                                <script>
                                    new Chart(document.getElementById("pie-chart"), {
                                        type: 'pie',
                                        data: {
                                            labels: ["All Open Projects", "My Waiting on Company", "2 Weeks Without Activity", "My Pending Approval"],
                                            datasets: [{
                                                label: "Total",
                                                backgroundColor: ["#dc7a32", "#04828F", "#5C3315", "#32CBDC"],
                                                data: [12, 3, 1, 2]
                                            }]
                                        }
                                    });
                                </script>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="vc-coach-dashboard-bottom">
                <h2>My Projects</h2>
                <div class="vc-coach-dashboard-project">
                </div>

                <div runat="server" class="ui items" id="projectParent">
                </div>
                <script>
                    $('#project-1').progress();
                    $('#project-2').progress();
                    $('#project-3').progress();
                    $('#project-4').progress();
                </script>
            </div>
        </div>
    </form>
</body>
</html>

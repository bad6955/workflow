﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
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
                        <li>
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
            <h1>Dashboard</h1>
            <div id="vc-coach-dashboard-top">
                <div class="ui placeholder segment">
                    <div class="ui two column stackable center aligned grid">
                        <div class="ui vertical divider"></div>
                        <div class="middle aligned row">
                            <div class="column">
                            </div>
                            <div class="column">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="vc-coach-dashboard-bottom">
                <h2>My Projects</h2>
                <div class="vc-coach-dashboard-project">
                </div>

                <div class="ui items">
                    <div class="item">
                        <!-- Progress bar stuff -->
                        <div class="ui small image">
                            <div class="ui orange progress">
                                <div class="bar">
                                    <div class="progress"></div>
                                </div>
                                <div class="label">Completion</div>
                            </div>
                            <button class="ui brown basic button">View Full Project</button>
                        </div>
                        <div class="content">
                            <!-- Project Name goes under .content -->
                            <a class="header">Project Name</a>
                            <!-- Project Description or current workflow step could go here -->
                            <div class="description">
                                Project Description or current workflow step could go here?
                            </div>
                            <!------------------------------- Table with Workflow steps ------------------------->
                            <table class="ui celled table">
                                <thead>
                                    <tr>
                                        <th>Workflow Step</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="positive">
                                        <td>Step 1 - Worklflow Step Title</td>
                                        <td><i class="icon checkmark"></i>Approved</td>
                                    </tr>
                                    <tr class="positive">
                                        <td>Step 2 - Workflow Step Title</td>
                                        <td><i class="icon checkmark"></i>Approved</td>
                                    </tr>
                                    <tr class="negative">
                                        <td>Step 3 - Workflow Step Title</td>
                                        <td>Denied - Needs Modification</td>
                                    </tr>
                                    <tr>
                                        <td>Step 4 - Workflow Step Title</td>
                                        <td>Unknown</td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan="3">
                                            <div class="ui right floated pagination menu">
                                                <a class="icon item">
                                                    <i class="left chevron icon"></i>
                                                </a>
                                                <a class="item">1</a>
                                                <a class="item">2</a>
                                                <a class="icon item">
                                                    <i class="right chevron icon"></i>
                                                </a>
                                            </div>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>

                    <div class="vc-coach-dashboard-project">
                    </div>
                    <div class="item">
                        <!-- Progress bar stuff -->
                        <div class="ui small image">
                            <div class="ui orange progress">
                                <div class="bar">
                                    <div class="progress"></div>
                                </div>
                                <div class="label">Completion</div>
                            </div>
                            <button class="ui brown basic button">View Full Project</button>
                        </div>
                        <div class="content">
                            <!-- Project Name goes under .content -->
                            <a class="header">Project Name</a>
                            <!-- Project Description or current workflow step could go here -->
                            <div class="description">
                                Project Description or current workflow step could go here?
                            </div>
                            <!------------------------------- Table with Workflow steps ------------------------->
                            <table class="ui celled table">
                                <thead>
                                    <tr>
                                        <th>Workflow Step</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="positive">
                                        <td>Step 1 - Worklflow Step Title</td>
                                        <td><i class="icon checkmark"></i>Approved</td>
                                    </tr>
                                    <tr class="positive">
                                        <td>Step 2 - Workflow Step Title</td>
                                        <td><i class="icon checkmark"></i>Approved</td>
                                    </tr>
                                    <tr class="negative">
                                        <td>Step 3 - Workflow Step Title</td>
                                        <td>Denied - Needs Modification</td>
                                    </tr>
                                    <tr>
                                        <td>Step 4 - Workflow Step Title</td>
                                        <td>Unknown</td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan="3">
                                            <div class="ui right floated pagination menu">
                                                <a class="icon item">
                                                    <i class="left chevron icon"></i>
                                                </a>
                                                <a class="item">1</a>
                                                <a class="item">2</a>
                                                <a class="icon item">
                                                    <i class="right chevron icon"></i>
                                                </a>
                                            </div>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

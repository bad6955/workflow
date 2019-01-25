<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/jquery.jqplot.js"></script>
    <script type="text/javascript" src="assets/js/jqplot.meterGaugeRenderer.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <link rel="stylesheet" type="text/css" href="assets/css/jquery.jqplot.css" />
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
                <!-- Some kind of graphs? like "how many active projects" -->
            </div>
            <div id="vc-coach-dashboard-bottom">
                <h2>My Projects</h2>
                <div class="vc-coach-dashboard-project">
                    <h3>Project Name</h3>
                    <div class="completion">
                        <div id="completion-bar">
                            <!-- only inline rn becuase I was lazy. Will move out later -->
                            <script>$(document).ready(function () {
                                    s1 = [50];

                                    plot4 = $.jqplot('completion-bar', [s1], {
                                        seriesDefaults: {
                                            renderer: $.jqplot.MeterGaugeRenderer,
                                            rendererOptions: {
                                                label: 'Completion',
                                                labelPosition: 'bottom',
                                                intervalOuterRadius: 50,
                                                ticks: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100],
                                                intervals: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100],
                                            }
                                        }
                                    });
                                });</script>
                        </div>
                    </div>
                </div>

                <div class="ui relaxed divided items">
                    <div class="item">
                        <div class="ui small image">
                            <img src="assets/images/wireframe/image.png">
                        </div>
                        <div class="content">
                            <a class="header">Content Header</a>
                            <div class="meta">
                                <a>Date</a>
                                <a>Category</a>
                            </div>
                            <div class="description">
                                A description which may flow for several lines and give context to the content.
                            </div>
                            <div class="extra">
                                <div class="ui right floated primary button">
                                    View Full
            <i class="right chevron icon"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="Workflow.Workflows" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workflow</title>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png" />
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
<body>
    <form id="form1" runat="server">
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                    <div id="account-dropdown">
                        <i class="large user circle outline icon"></i>
                        <h1>
                            <asp:Label runat="server" ID="userLbl"></asp:Label></h1>
                        <!-- not passing argument ID=user -->
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
                        <li id="current-page">
                            <img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="current" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li>
                            <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li>
                            <img src="assets/icons/form.png" /><asp:Button runat="server" ID="form" OnClick="FormBtn_Click" Text="Forms" /></li>
                    </ul>
                    <div id="help">
                        <img src="assets/icons/help.png" />
                    </div>
                </div>
            </div>
        </div>

        <div id="content-body">
            <div runat="server" id="workflowListing">
                <h1>Workflows</h1>
                <asp:Button runat="server" ID="CreateNewWorkflowBtn" Text="Create New Workflow" OnClick="CreateNewWorkflowBtn_Click" CssClass="fluid ui button" />
                <asp:Label runat="server" ID="WorkflowError" Visible="false" CssClass="error"></asp:Label>
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

            <div runat="server" id="test"></div>
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
            <!-- Forms-->



            <!-- Table 
            <hr />
            <h1>Steps:</h1>
            <div class="limiter">
                <div class="container-table100">
                    <div class="wrap-table100">
                        <div class="table100">
                            <table>
                                <thead>
                                    <tr class="table100-head">
                                        <th class="column1">Milestones</th>
                                        <th class="column2">Deliverables</th>
                                        <th class="column3">Participants</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="column1">2017-09-29 01:22</td>
                                        <td class="column2">200398</td>
                                        <td class="column3">iPhone X 64Gb Grey</td>
                                    </tr>
                                    <tr>
                                        <td class="column1">2017-09-28 05:57</td>
                                        <td class="column2">200397</td>
                                        <td class="column3">Samsung S8 Black</td>
                                    </tr>
                                    <tr>
                                        <td class="column1">2017-09-26 05:57</td>
                                        <td class="column2">200396</td>
                                        <td class="column3">Game Console Controller</td>
                                    </tr>
                                    <tr>
                                        <td class="column1">2017-09-22 05:57</td>
                                        <td class="column2">200389</td>
                                        <td class="column3">Macbook Pro Retina 2017</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>-->
        </div>
    </form>
    <script src="assets/jquery/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script>
        $('#carouselExample').on('slide.bs.carousel', function (e) {


            var $e = $(e.relatedTarget);
            var idx = $e.index();
            var itemsPerSlide = 4;
            var totalItems = $('.carousel-item').length;

            if (idx >= totalItems - (itemsPerSlide - 1)) {
                var it = itemsPerSlide - (totalItems - idx);
                for (var i = 0; i < it; i++) {
                    // append slides to end
                    if (e.direction == "left") {
                        $('.carousel-item').eq(i).appendTo('.carousel-inner');
                    }
                    else {
                        $('.carousel-item').eq(0).appendTo('.carousel-inner');
                    }
                }
            }
        });


        $('#carouselExample').carousel({
            interval: 2000
        });


        $(document).ready(function () {
            /* show lightbox when clicking a thumbnail */
            $('a.thumb').click(function (event) {
                event.preventDefault();
                var content = $('.modal-body');
                content.empty();
                var title = $(this).attr("title");
                $('.modal-title').html(title);
                content.html($(this).html());
                $(".modal-profile").modal({ show: true });
            });

        });
    </script>
    <script>
        $('.ui.selection.dropdown').dropdown();
    </script>

</body>
</html>

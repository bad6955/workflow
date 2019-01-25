﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
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
				        <h1><asp:Label runat="server" ID="userLbl"></asp:Label></h1>
                        <div id="dropdown-content">
                            <a href="AccountSettings.aspx"><h2>Account Settings</h2></a>
                            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="LogoutBtn_Click" />
                        </div>
			        </div>
                </div>
            </div>
            <div id="side-bar">
                <div id="side-bar-top-content">
                    <ul>
                        <li><img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="current" Text="Dashboard"></asp:Button></li>
                        <li><img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li><img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li><img src="assets/icons/form.png" /><asp:Button runat="server" ID ="form" OnClick="FormBtn_Click" Text="Forms"/></li>
                    
                    </ul>
                </div>
                <div id="help"><img src="assets/icons/help.png" /></div>
            </div>
        </div>
        <div id="content-body">
			<h1>Dashboard</h1>
			<div id="vc-coach-dashboard-top">
			<!-- Some kind of graphs? like "how many active projects" -->
			</div>
			<div id="vc-coach-dashboard-bottom">
				<h2>My Projects</h2> 
				<hr>
				<div class="vc-coach-dashboard-project">
					<h3>Project Name</h3>
					<div>
						<div class="completion">
							<div id="completion-bar"></div>
							<h4>Completion</h4>
						</div>
						<h4>Open Project</h4>
					</div>
				</div>
			</div>
		</div>
    </form>	
</body>
</html>

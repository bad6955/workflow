<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <div id="Title"><asp:Label runat="server" ID="user"></asp:Label></div>
    <form id="form1" runat="server">
        <!-- this will be the nav for now --> 
        <div id="navigation">
            <div id="top-bar">
                <div id="account-dropdown">
				    <h1>Username</h1>
			    </div>
            </div>
            <div id="side-bar">
                <ul>
                    <li><a class="active"><img src="assets/icons/dashboard.png" /><asp:Label runat="server" ID="current">Dashboard</asp:Label></a></li>
                    <li><a><img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="WorkflowBtn_Click" /><asp:Label runat="server" ID="Label1">Workflows</asp:Label></a></li>
                    <li><a><img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" /><asp:Label runat="server" ID="Label2">Projects</asp:Label></a></li>
                    <li><a><img src="assets/icons/form.png" /><asp:Button runat="server" ID ="form" OnClick="FormBtn_Click" /><asp:Label runat="server" ID="Label3">Forms</asp:Label></a></li>
                    <li><a><asp:Button runat="server" ID="logout" Text="Log Out" OnClick="LogoutBtn_Click" /></a></li>
                </ul>
                <div id="help"><img src="assets/icons/help.png" /></div>
            </div>
        </div>
    </form>
    <div id="vc-coach-dashboard">
			<h1>Dashboard</h1>
			<div id="vc-coach-dashboard-top">
			
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
</body>
</html>

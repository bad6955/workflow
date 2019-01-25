<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="Workflow.Projects" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project</title>
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<script>
    function saveSelection() {
        var companyEle = document.getElementById("CompanySelect");
        var company = companyEle.options[companyEle.selectedIndex].value;
        var workflowEle = document.getElementById("WorkflowSelect");
        var workflow = workflowEle.options[workflowEle.selectedIndex].value;
        var coachEle = document.getElementById("CoachSelect");
        var coach = coachEle.options[coachEle.selectedIndex].value;
        document.getElementById("SelectedCompany").value = company;
        document.getElementById("SelectedWorkflow").value = workflow;
        document.getElementById("SelectedCoach").value = coach;
    }
</script>
<body>
    <%-- --%>
    <form id="form1" runat="server">
        <%-- this will be the nav for now --%>
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
                        <li><img src="assets/icons/dashboard.png" /><asp:Label runat="server" ID="dashboard" OnClick="DashboardBtn_Click" Text="Dashboard"></asp:Label></li>
                        <li><img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li><img src="assets/icons/project.png" /><asp:Button runat="server" ID="current" Text="Projects" /></li>
                        <li><img src="assets/icons/form.png" /><asp:Button runat="server" ID ="form" OnClick="FormBtn_Click" Text="Forms"/></li>
                    </ul>
                </div>
                <div id="help"><img src="assets/icons/help.png" /></div>
            </div>
        </div>
        <div id="content-body">
			<h1>Projects</h1>
			<div runat="server" id="adminDiv" visible="false">
                <span>Create Project</span><br />
                <asp:TextBox runat="server" ID="ProjectName" placeholder="Project Name"></asp:TextBox>
                <asp:DropDownList runat="server" ID="CompanySelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="WorkflowSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:DropDownList runat="server" ID="CoachSelect" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList>
                <asp:TextBox runat="server" ID="ProjectNotes" placeholder="Project Notes (Optional)"></asp:TextBox>
                <asp:Button runat="server" ID="CreateProjectBtn" Text="Create Project" OnClick="CreateProjectBtn_Click"/>
                <asp:HiddenField runat="server" ID="SelectedCompany" />
                <asp:HiddenField runat="server" ID="SelectedWorkflow" />
                <asp:HiddenField runat="server" ID="SelectedCoach" />
			</div>
		</div>
    </form>
</body>
</html>

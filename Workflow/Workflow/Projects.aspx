<%@ Page Language="C#" MasterPageFile="~/NavMasterPage.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="Workflow.Projects" Title="Projects" %>


<asp:Content ID="MasterDashboard" ContentPlaceHolderID="MasterContentPlaceHolder" runat="server">
    <script>
        function saveSelection() {
            var companyEle = document.getElementById("MasterContentPlaceHolder_CompanySelect");
            var company = companyEle.options[companyEle.selectedIndex].value;
            var workflowEle = document.getElementById("MasterContentPlaceHolder_WorkflowSelect");
            var workflow = workflowEle.options[workflowEle.selectedIndex].value;
            var coachEle = document.getElementById("MasterContentPlaceHolder_CoachSelect");
            var coach = coachEle.options[coachEle.selectedIndex].value;
            document.getElementById("MasterContentPlaceHolder_SelectedCompany").value = company;
            document.getElementById("MasterContentPlaceHolder_SelectedWorkflow").value = workflow;
            document.getElementById("MasterContentPlaceHolder_SelectedCoach").value = coach;
        }
    </script>
    <div id="content-body">
        <div runat="server" id="projectListing">
            <h1>Projects</h1>
            <asp:Button runat="server" ID="CreateNewProjectBtn" Text="Create New Project" OnClick="CreateNewProjectBtn_Click" CssClass="fluid ui button" />
            <asp:Label runat="server" ID="ProjectError" Visible="false" CssClass="error"></asp:Label>
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
                            <div class="item">
                                <div class="ui black empty circular label"></div>
                                Assigned to Me
                            </div>
                            <div class="item">
                                <div class="ui purple empty circular label"></div>
                                A-Z Company Name
                            </div>
                            <div class="item">
                                <div class="ui yellow empty circular label"></div>
                                Z-A Company Name
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
            <div runat="server" class="ui items" id="projectList">
            </div>
            <asp:Button runat="server" ID="Button2" Text="Show 5 More..." OnClick="LoadMoreProjects" CssClass="fluid ui button" />
        </div>

        <div runat="server" id="projectBuilder" visible="false">
            <br />
            <div id="project-builder">
                <asp:Panel runat="server" ID="ProjectNamePanel" CssClass="ui left corner labeled input">
                    <asp:TextBox runat="server" ID="ProjectName" class="ui left icon input" placeholder="Project Name"></asp:TextBox>
                </asp:Panel>
                <br />
                <br />
                <asp:DropDownList runat="server" ID="CompanySelect" class="ui fluid selection dropdown" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList><br />
                <asp:DropDownList runat="server" ID="WorkflowSelect" class="ui fluid selection dropdown" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList><br />
                <asp:DropDownList runat="server" ID="CoachSelect" class="ui fluid selection dropdown" onchange="saveSelection()" AutoPostBack="false"></asp:DropDownList><br />
                <asp:TextBox runat="server" ID="ProjectNotes" class="ui form" Rows="6" placeholder="Project Notes (Optional)" TextMode="MultiLine"></asp:TextBox><br />
                <asp:Button runat="server" ID="CreateProjectBtn" Text="Create Project" CssClass="ui teal button" OnClick="CreateProjectBtn_Click" />
                <asp:HiddenField runat="server" ID="SelectedCompany" />
                <asp:HiddenField runat="server" ID="SelectedWorkflow" />
                <asp:HiddenField runat="server" ID="SelectedCoach" />
            </div>
        </div>

        <div runat="server" id="projectViewer" visible="false">
        </div>
        <asp:Button runat="server" ID="ProjectFileDownloader" CssClass="fluid ui button" Visible="false" Text="Download Project Files" OnClick="ProjectFileDownloader_Click" />
    </div>
</asp:Content>

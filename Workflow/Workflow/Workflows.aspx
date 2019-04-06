<%@ Page Language="C#" MasterPageFile="~/NavMasterPage.Master" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="Workflow.Workflows" Title="Workflows" %>

<asp:Content ID="MasterWorkflow" ContentPlaceHolderID="Content" runat="server">
    <div id="content-body" onload="WorkflowPageLoaded()">
        <asp:Label runat="server" ID="WorkflowError" Visible="false" CssClass="error"></asp:Label>
        <div runat="server" id="workflowListing">
            <h1>Workflows</h1>
            <asp:Button runat="server" ID="CreateNewWorkflowBtn" Text="Create New Workflow" OnClick="CreateNewWorkflowBtn_Click" CssClass="fluid ui button" />
            <div class="ui secondary segment">
                <div class="ui floating dropdown labeled icon button">
                    <i class="filter icon"></i>
                    <span class="text">Sort Filter</span>
                    <div class="menu">
                        <div class="ui icon search input">
                            <i class="search icon"></i>
                            <input type="text" placeholder="Search..." />
                            <div class="item">
                                <div class="ui red empty circular label"></div>
                                Open
                            </div>
                            <div class="item">
                                <div class="ui blue empty circular label"></div>
                                Closed
                            </div>
                            v>
                        </div>
                    </div>
            </div>
        <p runat="server" id="numberShowing"></p>
        </div>
    </div>
    <div runat="server" class="ui items" id="workflowList">
    </div>
    <asp:Button runat="server" ID="Button2" Text="Show 5 More..." OnClick="LoadMoreWorkflows" CssClass="fluid ui button" />
    </div>

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
</asp:Content>

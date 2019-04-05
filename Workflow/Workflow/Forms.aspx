<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Workflow.Forms" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forms</title>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png" />
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="assets/js/form-builder.min.js"></script>
    <script type="text/javascript" src="assets/js/form-render.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
</head>
<body>
    <%-- --%>
    <form id="form1" runat="server" enctype="multipart/form-data">
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
                        <li>
                            <img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="dashboard" OnClick="DashboardBtn_Click" Text="Dashboard"></asp:Button></li>
                        <li>
                            <img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li>
                            <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li id="current-page">
                            <img src="assets/icons/form.png" /><asp:Button runat="server" ID="current" OnClick="FormBtn_Click" Text="Forms" /></li>
                    </ul>
                </div>
                <div id="help">
                    <img src="assets/icons/help.png" />
                </div>
            </div>
        </div>
        <div id="content-body">

            <div runat="server" id="formListing">
                <h1>Forms</h1>
                <asp:Button runat="server" ID="CreateNewFormBtn" Text="Create New Template" OnClick="CreateNewFormBtn_Click" CssClass="fluid ui button" />
                <asp:Label runat="server" ID="FormError" Visible="false" CssClass="error"></asp:Label>
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
                        $('.menu .item')
                            .tab();
                    </script>
                </div>
                <div runat="server" id="tabMenu" visible="false" class="ui top attached tabular menu">
                    <asp:Button runat="server" ID="FormTab" Visible="false" class="item active" data-tab="forms" Text="Forms" OnClick="FormTab_Click"></asp:Button>
                    <asp:Button runat="server" ID="TemplateTab" Visible="false" class="item" data-tab="templates" Text="Templates" OnClick="TemplateTab_Click"></asp:Button>
                </div>

                <div runat="server" class="ui items" id="formList"></div>
                <asp:Button runat="server" ID="Button2" Text="Show 5 More..." OnClick="LoadMoreForms" CssClass="fluid ui button" />
            </div>

            <div runat="server" id="formBuilder" visible="false">
                <div>
                    <asp:Label runat="server" ID="FormResult" Visible="false"></asp:Label>
                    <div id="formName">
                        <div class="ui left corner labeled input">
                            <asp:TextBox runat="server" ID="FormName" Placeholder="Form Name..."></asp:TextBox>
                            <div class="ui teal left corner label">
                                <i class="white asterisk icon"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="buildWrap"></div>
                <div id="CreateFormBtnDiv">
                    <hr />
                    <h3>Form Required Approvals</h3>
                    <p>Select the role(s) required to approve this form</p><br />
                    <asp:DropDownList runat="server" ID="FormApproval1" onchange="saveSelection()" OnSelectedIndexChanged="FormApproval_SelectedIndexChanged" CssClass="form-selector-dropdown" /><br />
                    <asp:DropDownList runat="server" ID="FormApproval2" Visible="false" OnSelectedIndexChanged="FormApproval_SelectedIndexChanged" onchange="saveSelection()" CssClass="form-selector-dropdown" /><br />
                    <asp:DropDownList runat="server" ID="FormApproval3" Visible="false" OnSelectedIndexChanged="FormApproval_SelectedIndexChanged" onchange="saveSelection()" CssClass="form-selector-dropdown" /><br />
                    <asp:DropDownList runat="server" ID="FormApproval4" Visible="false" OnSelectedIndexChanged="FormApproval_SelectedIndexChanged" onchange="saveSelection()" CssClass="form-selector-dropdown" /><br /><br />
                    <asp:HiddenField runat="server" ID="SelectedApprover1" Value="-1" />
                    <asp:HiddenField runat="server" ID="SelectedApprover2" Value="-1"  />
                    <asp:HiddenField runat="server" ID="SelectedApprover3" Value="-1" />
                    <asp:HiddenField runat="server" ID="SelectedApprover4" Value="-1" />
                    <asp:Button runat="server" ID="CreateFormBtn" Text="Create Form" CssClass="ui teal button" OnClick="CreateFormBtn_Click" OnClientClick="SaveFormEditor()" />
                </div>
                <asp:HiddenField runat="server" ID="formBuilderData" />
                <script>
                    function saveSelection() {
                        console.log("Saving selectors");
                        var app1 = document.getElementById("FormApproval1");
                        var role1 = app1.options[app1.selectedIndex].value;
                        document.getElementById("SelectedApprover1").value = role1;

                        var app2 = document.getElementById("FormApproval2");
                        if (app2 != null) {
                            var role2 = app2.options[app2.selectedIndex].value;
                            document.getElementById("SelectedApprover2").value = role2;
                        }

                        var app3 = document.getElementById("FormApproval3");
                        if (app3 != null) {
                            var role3 = app3.options[app3.selectedIndex].value;
                            document.getElementById("SelectedApprover3").value = role3;
                        }


                        var app4 = document.getElementById("FormApproval4");
                        if (app4 != null) {
                            var role4 = app4.options[app4.selectedIndex].value;
                            document.getElementById("SelectedApprover4").value = role4;
                        }
                        __doPostBack("<%=FormApproval1.ClientID %>", '');
                    }

                    var builderOptions = {
                        dataType: 'json',
                        disabledActionButtons: ['data', 'save'],
                        controlPosition: 'left',
                        formData: document.getElementById("formBuilderData").value
                    };
                    var formBuilder = $('#buildWrap').formBuilder(builderOptions);
                    document.getElementById("formBuilderData").value = formBuilder.formData;
                    console.log("FormData: " + formBuilder.formData);

                    function SaveFormEditor() {
                        document.getElementById("formBuilderData").value = formBuilder.formData;
                    }
                </script>
            </div>

            <div runat="server" id="formViewer" visible="false">
                <div>
                    <h3>
                        <asp:Label runat="server" ID="FormNameLbl"></asp:Label>
                    </h3>
                    <asp:Label runat="server" ID="FormResult2" Visible="false"></asp:Label>
                </div>
                <fieldset class="formOutline">
                    <div runat="server" id="renderWrap"></div>

                    <div runat="server" id="uploadedFiles" visible="false">
                        <span class="error">NOTE: Previously uploaded files will be erased if a new file is uploaded:</span><br />
                            <ul>
                                <li><asp:Label runat="server" ID="UploadedName"></asp:Label></li>
                            </ul>
                    </div>
                    <div runat="server" id="coachUploadedFiles" visible="false">
                            Attached File:
                                <p><asp:Label runat="server" ID="CoachUploadedName"></asp:Label></p>
                                <asp:Button runat="server" ID="CoachDownloadBtn" OnClick="CoachDownloadBtn_Click" Text="Download File" CssClass="fluid ui button"/>
                    </div>
                </fieldset>
                <br />
                <hr />
                <h3>Form Required Approvals</h3>
                <asp:Label runat="server" ID="approvalLabel1" Visible="false"></asp:Label><br />
                <asp:Label runat="server" ID="approvalLabel2" Visible="false"></asp:Label><br />
                <asp:Label runat="server" ID="approvalLabel3" Visible="false"></asp:Label><br />
                <asp:Label runat="server" ID="approvalLabel4" Visible="false"></asp:Label><br /><br />

                <asp:Button runat="server" ID="SaveFormBtn" CssClass="fluid ui button" Text="Save Form" OnClick="SaveFormBtn_Click" OnClientClick="SaveFormViewer()" UseSubmitBehavior="false" /><br />
                <asp:Button runat="server" ID="SubmitFormBtn" CssClass="fluid ui button" Text="Submit Form" OnClick="SubmitFormBtn_Click" OnClientClick="SubmitForm()" UseSubmitBehavior="false" /><br />
                <asp:Button runat="server" ID="ApproveFormBtn"  CssClass="fluid ui button" Text="Approve Form" OnClick="ApproveFormBtn_Click" Visible="false" OnClientClick="ApproveForm()" /><br />
                <asp:Button runat="server" ID="DenyFormBtn" CssClass="fluid ui button" Text="Deny Form" OnClick="DenyFormBtn_Click" Visible="false" /><br />
                <asp:TextBox runat="server" ID="DenyReason" Placeholder="Reason for denial" TextMode="MultiLine" Visible="false" />
                <asp:HiddenField runat="server" ID="formViewerData" />
                <asp:HiddenField runat="server" ID="fileUploadName" />
                <asp:HiddenField runat="server" ID="fileInputName" />
                <script>
                    var viewerOptions = {
                        dataType: 'json',
                        formData: document.getElementById("formViewerData").value
                    };
                    var formViewer = $('#renderWrap').formRender(viewerOptions);
                    document.getElementById("formViewerData").value = formViewer.formData;

                    function SaveFormViewer() {
                        SaveUploadedFiles();
                        document.getElementById("formViewerData").value = JSON.stringify(formViewer.userData);
                    }

                    function SaveUploadedFiles() {
                        var file = $("input:file")[0].files[0];
                        var inputName = $("input:file")[0].name;

                        if (file) {
                            document.getElementById("fileUploadName").value = file.name;
                            document.getElementById("fileInputName").value = inputName;
                        }
                    }

                    function SubmitForm() {
                        SaveFormViewer();
                        jQuery(function () {
                            formRenderOpts = {
                                dataType: 'json',
                                formData: formViewer.formData
                            };

                            var renderedForm = $('<div>');
                            renderedForm.formRender(formRenderOpts);

                            console.log(renderedForm.html());
                            document.getElementById("formViewerData").value = renderedForm.html();
                        });
                    }

                    function ApproveForm() {
                        jQuery(function () {
                            formRenderOpts = {
                                dataType: 'json',
                                formData: formViewer.formData
                            };
                            var renderedForm = $('#renderWrap');
                            renderedForm.formRender(formRenderOpts);

                            console.log(renderedForm.html());
                            document.getElementById("formViewerData").value = renderedForm.html();
                        });
                    }
                </script>
                <div runat="server" id="formLocking" visible="false">
                    <script>
                        $(function () {
                            console.log("LOCKING FORM");
                            $("#renderWrap :input").attr("disabled", true);
                        })
                    </script>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

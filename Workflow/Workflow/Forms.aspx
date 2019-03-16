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
    <form id="form1" runat="server">
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
                <asp:Button runat="server" ID="CreateNewFormBtn" Text="Create New Form" OnClick="CreateNewFormBtn_Click" CssClass="fluid ui button" />
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
                    </script>
                </div>
                <div runat="server" class="ui items" id="formList">
                </div>
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
                    <asp:Button runat="server" ID="CreateFormBtn" Text="Create Form" CssClass="ui teal button" OnClick="CreateFormBtn_Click" OnClientClick="SaveFormEditor()" />
                </div>
                <asp:HiddenField runat="server" ID="formBuilderData" />
                <script>
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
                        <asp:Label runat="server" ID="FormNameLbl"></asp:Label></h3>
                    <asp:Label runat="server" ID="FormResult2" Visible="false"></asp:Label>
                </div>
                <div runat="server" id="renderWrap"></div>
                <asp:Button runat="server" ID="SaveFormBtn" Text="Save Form" OnClick="SaveFormBtn_Click" OnClientClick="SaveFormViewer()" />
                <asp:Button runat="server" ID="SubmitFormBtn" Text="Submit Form" OnClick="SubmitFormBtn_Click" OnClientClick="SubmitForm()" />
                <asp:Button runat="server" ID="ApproveFormBtn" Text="Approve Form" OnClick="ApproveFormBtn_Click" Visible="false" OnClientClick="ApproveForm()" />
                <asp:TextBox runat="server" ID="DenyReason" Placeholder="Reason for denial" Visible="false" />
                <asp:Button runat="server" ID="DenyFormBtn" Text="Deny Form" OnClick="DenyFormBtn_Click" Visible="false" />
                <asp:HiddenField runat="server" ID="formViewerData" />
                <script>
                    var viewerOptions = {
                        dataType: 'json',
                        formData: document.getElementById("formViewerData").value
                    };
                    var formViewer = $('#renderWrap').formRender(viewerOptions);
                    document.getElementById("formViewerData").value = formViewer.formData;

                    function SaveFormViewer() {
                        document.getElementById("formViewerData").value = JSON.stringify(formViewer.userData);
                    }

                    function SubmitForm() {
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
                            //document.getElementById("formViewerData").value = $('#renderWrap').formRender('html');
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
        </div>
    </form>
</body>
</html>

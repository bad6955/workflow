<%@ Page Language="C#" MasterPageFile="~/NavMasterPage.Master" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Workflow.Forms" ValidateRequest="false" Title="Forms" %>

<asp:Content ID="MasterForm" ContentPlaceHolderID="Content" runat="server">
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
                <asp:Button runat="server" ID="CreateFormBtn" Text="Create Form" CssClass="ui teal button" OnClick="CreateFormBtn_Click" OnClientClick="SaveFormEditor()"/>
            </div>
            <asp:HiddenField runat="server" ID="formBuilderData" />
            <script>
                var builderOptions = {
                    dataType: 'json',
                    disabledActionButtons: ['data', 'save'],
                    controlPosition: 'left',
                    formData: document.getElementById("Content_formBuilderData").value
                };
                var formBuilder = $('#buildWrap').formBuilder(builderOptions);
                document.getElementById("Content_formBuilderData").value = formBuilder.formData;
                console.log("FormData: " + formBuilder.formData);
            </script>
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
                            <li>
                                <asp:Label runat="server" ID="UploadedName"></asp:Label></li>
                        </ul>
                    </div>
                    <div runat="server" id="coachUploadedFiles" visible="false">
                        Attached File:
                                <p>
                                    <asp:Label runat="server" ID="CoachUploadedName"></asp:Label></p>
                        <asp:Button runat="server" ID="CoachDownloadBtn" OnClick="CoachDownloadBtn_Click" Text="Download File" CssClass="fluid ui button" />
                    </div>
                </fieldset>
                <br />

                <asp:Button runat="server" ID="SaveFormBtn" CssClass="fluid ui button" Text="Save Form" OnClick="SaveFormBtn_Click" OnClientClick="SaveFormViewer()" UseSubmitBehavior="false" /><br />
                <asp:Button runat="server" ID="SubmitFormBtn" CssClass="fluid ui button" Text="Submit Form" OnClick="SubmitFormBtn_Click" OnClientClick="SubmitForm()" UseSubmitBehavior="false" /><br />
                <asp:Button runat="server" ID="ApproveFormBtn" CssClass="fluid ui button" Text="Approve Form" OnClick="ApproveFormBtn_Click" Visible="false" OnClientClick="ApproveForm()" /><br />
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
                        console.log(renderedForm.html());
                        document.getElementById("formViewerData").value = renderedForm.html();
                        });
                    }
                </script>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/NavMasterPage.Master" AutoEventWireup="true" CodeBehind="AccountSettings.aspx.cs" Inherits="Workflow.AccountSettings" Title="Account Settings" %>

<asp:Content ID="MasterProject" ContentPlaceHolderID="Content" runat="server">
        <div id="content-body">
        <h1>Account Settings</h1>
        <div class="ui raised very padded text container segment">
            <div class="ui form">
                <div id="account-settings-password">
                    <asp:Label runat="server" ID="PasswordChangeError" Visible="false" CssClass="error"></asp:Label><br />
                    <div class="two fields">
                        <div class="field">
                            Change Password:
                        </div>
                        <div class="field">
                            <asp:Button runat="server" ID="ChangePassword" Text="Change Password" OnClick="ChangePassword_Click" />
                        </div>
                    </div>
                </div>
                <div runat="server" id="AdminPanelToggler" class="two fields" visible="false">
                    <div class="field">
                        Go to Admin Panel on login:
                    </div>
                    <div class="field">
                        <asp:CheckBox runat="server" ID="AdminPanel" Checked="false" OnCheckedChanged="AdminPanel_CheckedChanged" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
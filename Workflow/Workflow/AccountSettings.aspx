<%@ Page Language="C#" MasterPageFile="~/NavMasterPage.Master" AutoEventWireup="true" CodeBehind="AccountSettings.aspx.cs" Inherits="Workflow.AccountSettings" Title="Account Settings" %>

<asp:Content ID="MasterWorkflow" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="two fields">
                    <div class="field">
                        Email Alerts:
                    </div>
                    <select class="ui dropdown">
                        <option value="0">Changes to my projects</option>
                        <option value="1">Approved or denied</option>
                        <option value="2">Approved only</option>
                        <option value="3">Denied only</option>
                        <option value="4">No notifications</option>
                    </select>
                </div>
                <div id="button">
                    <button class="ui button" type="submit">Save</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/NavMasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Workflow.Dashboard" Title="Dashboard" %>

<asp:Content ID="MasterDashboard" ContentPlaceHolderID="MasterContentPlaceHolder" runat="server">
    <div id="content-body">
        <div id="vc-coach-dashboard-top">
            <div class="ui placeholder segment">
                <div class="ui two column stackable center aligned grid">
                    <div class="ui vertical divider">
                        <!--<i class="ellipsis horizontal icon"></i>-->
                    </div>
                    <div class="middle aligned row">
                        <div class="column" id="activity-feed-parent">
                            <h2>Activity Feed</h2>
                            <div runat="server" class="ui relaxed divided list" id="activityFeed"></div>
                        </div>
                        <div class="column">
                            <h2>Projects Overview</h2>
                            <!-- All open projects, my projects waiting on another, my projects 2 weeks without activity, my projects awaiting approval -->
                            <div runat="server" id="piechart"></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="vc-coach-dashboard-bottom">
            <h2 data-step="1" data-intro="This is a tooltip!">My Projects</h2>
            <div class="vc-coach-dashboard-project">
            </div>

            <div runat="server" class="ui items" id="projectParent">
            </div>
        </div>
    </div>
</asp:Content>

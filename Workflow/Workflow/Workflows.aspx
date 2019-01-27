<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="Workflow.Workflows" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workflows</title>

    <!-- Bootstrap core CSS -->
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    


    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />


    <script runat="server">
        void Page_Load(Object sender, EventArgs e)
        {
            dashboard.Click += new EventHandler(this.dashboardBtn_Click);
            project.Click += new EventHandler(this.projectBtn_Click);
            form.Click += new EventHandler(this.formBtn_Click);
            logout.Click += new EventHandler(this.dashboardBtn_Click);
        }

        void dashboardBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        void projectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        void formBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        void logoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- this will be the nav for now -->
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                    <div id="account-dropdown">
                        <img src="assets/icons/person.png" />
                        <h1>
                            <asp:Label runat="server" ID="user">Error here</asp:Label></h1>
                        <!-- not passing argument ID=user -->
                        <div id="dropdown-content">
                            <a href="AccountSettings.aspx">
                                <h2>Account Settings</h2>
                            </a>
                            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="logoutBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="side-bar">
                <ul>
                    <li>
                        <img src="assets/icons/dashboard.png" />

                        <asp:Button runat="server" ID="dashboard" Text="Dashboard" OnClick="dashboardBtn_Click" />


                    </li>
                    <li>
                        <img src="assets/icons/workflow.png" />
                        <asp:Label runat="server" ID="current">Workflows</asp:Label>
                    </li>
                    <li>
                        <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" Text="Project" OnClick="projectBtn_Click" /></li>
                    <li>
                        <img src="assets/icons/form.png" /><asp:Button runat="server" ID="form" Text="Form" OnClick="formBtn_Click" /></li>

                </ul>
                <div id="help">
                    <img src="assets/icons/help.png" />
                </div>
            </div>
        </div>
    </form>
    <div id="content-body">
        <h1>Workflow name:</h1>

        <div id="vc-coach-dashboard-bottom">
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="my-4">Information:</h2>
                </div>
                <div class="col-lg-4 col-sm-6 text-center mb-4">
                    <img class="rounded-circle img-fluid d-block mx-auto" src="http://placehold.it/200x200" alt="">
                    <h3>J.S.
                        <small>Job Title</small>
                    </h3>
                </div>
                <div class="col-lg-4 col-sm-6 text-center mb-4">
                    <img class="rounded-circle img-fluid d-block mx-auto" src="http://placehold.it/200x200" alt="">
                    <h3>J.S.
                        <small>Job Title</small>
                    </h3>
                </div>
                <div class="col-lg-4 col-sm-6 text-center mb-4">
                    <img class="rounded-circle img-fluid d-block mx-auto" src="http://placehold.it/200x200" alt="">
                    <h3>J.S.
                        <small>Job Title</small>
                    </h3>
                </div>
            </div>
            <!-- row -->

        </div>

    </div>
    
            

    <!-- Bootstrap core JavaScript -->
    <script src="assets/jquery/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/jquery/myquery.js"></script>
</body>
</html>

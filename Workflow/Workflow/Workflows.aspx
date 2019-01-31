<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflows.aspx.cs" Inherits="Workflow.Workflows" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workflow</title>



    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <script type="text/javascript" src="assets/js/Chart.js"></script>

    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />

    <!-- Bootstrap CSS -->
    <link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />




    <!-- Custom CSS -->
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />


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
                            <asp:Label runat="server" ID="userLbl">Username</asp:Label></h1>
                        <!-- not passing argument ID=user -->
                        <div id="dropdown-content">
                            <a href="AccountSettings.aspx">
                                <h2>Account Settings</h2>
                            </a>
                            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="LogoutBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="side-bar">
                <ul>
                    <li>
                        <img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="current" OnClick="DashboardBtn_Click" Text="Dashboard"></asp:Button></li>
                    <li>
                        <img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="workflow" Text="Workflows" /></li>
                    <li>
                        <img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                    <li>
                        <img src="assets/icons/form.png" /><asp:Button runat="server" ID="form" OnClick="FormBtn_Click" Text="Forms" /></li>
                </ul>
                <div id="help">
                    <img src="assets/icons/help.png" />
                </div>
            </div>
        </div>
        <div id="content-body">
            <h1>Workflows</h1>
            <div class="ui secondary segment">
                <div runat="server" id="adminDiv" visible="false">
                    <span>Create Workflow</span><br />
                    <asp:TextBox runat="server" ID="WorkflowName" placeholder="Workflow Name"></asp:TextBox>
                    <asp:Button runat="server" ID="CreateWorkflowBtn" Text="Create Workflow" OnClick="CreateWorkflowBtn_Click" />
                </div>
            </div>
        </div>
    </form>
    <div id="content-body">
        <h1>Workflow name:</h1>
        <hr />
        <!-- Team Members Row -->
        <div class="row">
            <div class="col-lg-12">
                <h2 class="my-4">Information:</h2>
            </div>
            <div class="col-lg-4 col-sm-6 text-center mb-4">
                <img class="rounded-circle img-fluid d-block mx-auto" src="http://placehold.it/200x200" alt="">
                <h3>Username</h3>
            </div>
            <div class="col-lg-4 col-sm-6 text-center mb-4">
                <img class="rounded-circle img-fluid d-block mx-auto" src="http://placehold.it/200x200" alt="">
                <h3>Coach Name</h3>
            </div>
            <div class="col-lg-4 col-sm-6 text-center mb-4">
                <img class="rounded-circle img-fluid d-block mx-auto" src="http://placehold.it/200x200" alt="">
                <h3>Funding Source Name</h3>
            </div>

        </div>
        <!-- row -->
        <hr />

        <!-- Forms -->



          <div class="responsive slick">
    <div>your content</div>
    <div>your content</div>
    <div>your content</div>
  </div>

  <script type="text/javascript" src="//code.jquery.com/jquery-1.11.0.min.js"></script>
  <script type="text/javascript" src="//code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
  <script type="text/javascript" src="slick/slick.min.js"></script>


    </div>





    <!-- Bootstrap core JavaScript -->
    <script src="assets/jquery/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
   <script>
       $('.responsive').slick({
  dots: true,
  infinite: false,
  speed: 300,
  slidesToShow: 4,
  slidesToScroll: 4,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});

   </script>
</body>
</html>

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
        <div id="navigation">
            <div id="top-bar">
                <div id="right">
                    <div id="account-dropdown">
                        <img src="assets/icons/person.png" />
                        <h1><asp:Label runat="server" ID="userLbl"></asp:Label></h1>
                        <!-- not passing argument ID=user -->
                        <div id="dropdown-content">
                            <a href="AccountSettings.aspx"><h2>Account Settings</h2></a>
                            <asp:Button runat="server" ID="logout" Text="Log Out" OnClick="LogoutBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="side-bar">
                <div id="side-bar-top-content">
                    <ul>
                        <li><img src="assets/icons/dashboard.png" /><asp:Button runat="server" ID="dashboard" OnClick="DashboardBtn_Click" Text="Dashboard"></asp:Button></li>
                        <li><img src="assets/icons/workflow.png" /><asp:Button runat="server" ID="current" OnClick="WorkflowBtn_Click" Text="Workflows" /></li>
                        <li><img src="assets/icons/project.png" /><asp:Button runat="server" ID="project" OnClick="ProjectBtn_Click" Text="Projects" /></li>
                        <li><img src="assets/icons/form.png" /><asp:Button runat="server" ID ="form" OnClick="FormBtn_Click" Text="Forms"/></li>
                    </ul>
                    <div id="help"><img src="assets/icons/help.png" /></div>
                </div>
            </div>
        </div>

        <div id="content-body">
            <h1>Workflows</h1>
                <div runat="server" id="workflowListing">
                    <asp:Button runat="server" ID="CreateNewWorkflowBtn" Text="Create New Workflow" OnClick="CreateNewWorkflowBtn_Click" CssClass="fluid ui button" />
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
                    <div runat="server" class="ui items" id="workflowList">
                    </div>
                    <button class="fluid ui button">Show 10 More...</button>
                </div>

                <div runat="server" id="workflowBuilder" visible="false">
                    <span>Create Workflow</span><br />
                    <asp:TextBox runat="server" ID="WorkflowName" placeholder="Workflow Name"></asp:TextBox>
                    <asp:Button runat="server" ID="CreateWorkflowBtn" Text="Create Workflow" OnClick="CreateWorkflowBtn_Click" />
                </div>


                <div runat="server" id="workflowViewer" visible="false">
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
                    <h1>Forms:</h1>
                    <div class="container-fluid">
                        <div id="carouselExample" class="carousel slide" data-ride="carousel" data-interval="9000">
                            <div class="carousel-inner row w-100 mx-auto" role="listbox">
                                <div class="carousel-item col-md-3  active">
                                   <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 1" class="thumb">
                                          <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text=Form 1" alt="slide 1"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                <div class="carousel-item col-md-3 ">
                                   <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 3" class="thumb">
                                         <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text= Form 2" alt="slide 2"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                <div class="carousel-item col-md-3 ">
                                   <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 4" class="thumb">
                                         <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text= Form 3" alt="slide 3"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                <div class="carousel-item col-md-3 ">
                                    <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 5" class="thumb">
                                         <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text=Form 4" alt="slide 4"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                <div class="carousel-item col-md-3 ">
                                  <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 6" class="thumb">
                                          <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text=Form 5" alt="slide 5"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                <div class="carousel-item col-md-3 ">
                                   <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 7" class="thumb">
                                          <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text=Form 6" alt="slide 6"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                <div class="carousel-item col-md-3 ">
                                   <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 8" class="thumb">
                                          <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text=Form 7" alt="slide 7"/>
                                        </a>
                                      </div>
                                    </div>
                                </div>
                                 <div class="carousel-item col-md-3  ">
                                    <div class="panel panel-default">
                                      <div class="panel-thumbnail">
                                        <a href="#" title="image 2" class="thumb">
                                         <img class="img-fluid mx-auto d-block" src="//via.placeholder.com/150x200?text=Form 8" alt="slide 8"/>
                                        </a>
                                      </div>
                  
                                    </div>
                                </div>
                            </div>
                            <a class="carousel-control-prev" href="#carouselExample" role="button" data-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="carousel-control-next text-faded" href="#carouselExample" role="button" data-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>
                    </div>
        

                    <!-- Table -->
                    <hr />
                    <h1>Steps:</h1>
                    <div class="limiter">
		                <div class="container-table100">
			                <div class="wrap-table100">
				                <div class="table100">
					                <table>
						            <thead>
							            <tr class="table100-head">
								            <th class="column1">Milestones</th>
								            <th class="column2">Deliverables</th>
								            <th class="column3">Participants</th>
							            </tr>
						            </thead>
						            <tbody>
								        <tr>
									        <td class="column1">2017-09-29 01:22</td>
									        <td class="column2">200398</td>
									        <td class="column3">iPhone X 64Gb Grey</td>
								        </tr>
								        <tr>
									        <td class="column1">2017-09-28 05:57</td>
									        <td class="column2">200397</td>
									        <td class="column3">Samsung S8 Black</td>
								        </tr>
								        <tr>
									        <td class="column1">2017-09-26 05:57</td>
									        <td class="column2">200396</td>
									        <td class="column3">Game Console Controller</td>
								        </tr>
								        <tr>
									        <td class="column1">2017-09-22 05:57</td>
									        <td class="column2">200389</td>
									        <td class="column3">Macbook Pro Retina 2017</td>
								        </tr>
						            </tbody>
					                </table>
				                </div>
			                </div>
		                </div>
	                </div>
                </div>
        </div>
    </form>




    <!-- Bootstrap core JavaScript -->
    <script src="assets/jquery/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script>
        $('#carouselExample').on('slide.bs.carousel', function (e) {

  
            var $e = $(e.relatedTarget);
            var idx = $e.index();
            var itemsPerSlide = 4;
            var totalItems = $('.carousel-item').length;
    
            if (idx >= totalItems-(itemsPerSlide-1)) {
                var it = itemsPerSlide - (totalItems - idx);
                for (var i=0; i<it; i++) {
                    // append slides to end
                    if (e.direction=="left") {
                        $('.carousel-item').eq(i).appendTo('.carousel-inner');
                    }
                    else {
                        $('.carousel-item').eq(0).appendTo('.carousel-inner');
                    }
                }
            }
        });


          $('#carouselExample').carousel({ 
                        interval: 2000
                });


          $(document).ready(function() {
        /* show lightbox when clicking a thumbnail */
            $('a.thumb').click(function(event){
              event.preventDefault();
              var content = $('.modal-body');
              content.empty();
                var title = $(this).attr("title");
                $('.modal-title').html(title);        
                content.html($(this).html());
                $(".modal-profile").modal({show:true});
            });

          });
    </script>
</body>
</html>

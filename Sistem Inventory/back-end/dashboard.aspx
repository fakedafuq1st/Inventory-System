<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dashboard.aspx.vb" Inherits="Sistem_Inventory.dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="en">
  <head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Inventory</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="../assets/vendors/flag-icon-css/css/flag-icon.min.css">
    <link rel="stylesheet" href="../assets/vendors/css/vendor.bundle.base.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="../assets/vendors/jquery-bar-rating/css-stars.css" />
    <link rel="stylesheet" href="../assets/vendors/font-awesome/css/font-awesome.min.css" />
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <!-- endinject -->
    <!-- Layout styles -->
    <link rel="stylesheet" href="../assets/css/demo_2/style.css" />

     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <!-- End layout styles -->
    <link rel="shortcut icon" href="../assets/images/favicon.png" />
  </head>
  <body>
      <form id="form1" runat="server">
    <div class="container-scroller">
      <!-- partial:partials/_horizontal-navbar.html -->
      <div class="horizontal-menu">
        <nav class="bottom-navbar" style="background-color: #4C3F91;">
          <div class="container">
            <ul class="nav page-navigation">
              <li class="nav-item">
                <a class="nav-link" href="dashboard.aspx">
                  <i class="mdi mdi-home menu-icon"></i>
                  <span class="menu-title">Dashboard</span>
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="inventory.aspx">
                  <i class="mdi mdi-folder menu-icon"></i>
                  <span class="menu-title">Data Barang</span>
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="stock.aspx">
                  <i class="mdi mdi-arrow-down menu-icon "></i>
                   <i class="mdi mdi-arrow-up menu-icon"></i>
                  <span class="menu-title">Laporan stok barang</span>
                </a>
              </li>
         <asp:PlaceHolder ID="placeholder_admin" runat="server" Visible="false">
            <li class="nav-item">
                <a class="nav-link" href="settings.aspx">
                  <i class="mdi mdi-settings menu-icon"></i>
                  <span class="menu-title">Settings</span>
                </a>
              </li>
                </asp:PlaceHolder>
                <li class="nav-item">
                <div class="nav-link d-flex">
                  <button class="btn btn-sm bg-danger text-white"><asp:Label ID="lblacc" runat="server" Text="Unknown"></asp:Label></button>
                  <div class="nav-item dropdown">
                    <a class="nav-link count-indicator dropdown-toggle text-white font-weight-semibold" id="profileDropdown" href="#" data-toggle="dropdown"><asp:Label ID="lblnamaakun" runat="server" Text="Missing Account Name"></asp:Label></a>
                    <div class="dropdown-menu dropdown-menu-right navbar-dropdown preview-list" aria-labelledby="profileDropdown">
                        <asp:PlaceHolder ID="PlaceHolderchangepass" Visible="false" runat="server">
                        <a class="dropdown-item" href="#" onclick="changepass" id="lnkpassword" runat="server">
                        <i class="mdi mdi-cached mr-2 text-success"></i> Change Password </a>
                       <div class="dropdown-divider"></div>
                       </asp:PlaceHolder>
                      <a class="dropdown-item" href="#" onclick="Logout" id="lnkLogout" runat="server">
                      <i class="mdi mdi-logout mr-2 text-primary"></i> Logout </a>
                    </div>
                  </div>
                </div>
              </li>
            </ul>
          </div>
        </nav>
      </div>
      <!-- partial -->
      <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
          <div class="content-wrapper pb-0">
            <div class="page-header flex-wrap">
              <div class="header-left">
                  <h5>Dashboard</h5>
              </div>
              <div class="header-right d-flex flex-wrap mt-md-2 mt-lg-0">
                <div class="d-flex align-items-center">
                  <a>
                    <p class="m-0 pr-3">Pusat Gelas</p>
                  </a>
                  <a class="pl-3 mr-4">
                      <asp:Label ID="lbltime" runat="server" Text="Time Error"></asp:Label>
                  </a>
                </div>
<%--                <button type="button" class="btn btn-primary mt-2 mt-sm-0 btn-icon-text">
                  <i class="mdi mdi-plus-circle"></i> Tambah Barang </button>--%>
              </div>
            </div>
            <!-- first row starts here -->
            <div class="row">
              <div class="col-xl-9 stretch-card grid-margin">
                <div class="card">
                  <div class="card-body">
                    <div class="d-flex justify-content-between flex-wrap">
                      <div>
                        <div class="card-title mb-0">Persediaan Barang</div>
                        <h3 class="font-weight-bold mb-0">Informasi</h3>
                      </div>
                      <div>
                        <div class="d-flex flex-wrap pt-2 justify-content-between sales-header-right">
                          <div class="d-flex mr-5">
                            <button type="button" class="btn btn-social-icon btn-outline-sales">
                              <i class="mdi mdi-inbox-arrow-down"></i>
                            </button>
                            <div class="pl-2">
                              <h4 class="mb-0 font-weight-semibold head-count"> <asp:Label ID="txtSumTotalStock" runat="server" Text="tidak ada"></asp:Label> Barang Keluar</h4>
                              <span class="font-10 font-weight-semibold text-muted">Total Stock Keluar</span>
                            </div>
                          </div>
                          <div class="d-flex mr-3 mt-2 mt-sm-0">
                            <button type="button" class="btn btn-social-icon btn-outline-sales profit">
                              <i class="mdi mdi-cash text-info"></i>
                            </button>
                            <div class="pl-2">
                              <h4 class="mb-0 font-weight-semibold head-count"> Rp.<asp:Label ID="lblProfit" runat="server" Text="Total Profit Error"></asp:Label>,- </h4>
                              <span class="font-10 font-weight-semibold text-muted">Total Profit</span>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <p class="text-muted font-13 mt-2 mt-sm-0"> Visualisasi Ketersediaan barang
                    </p>
                    <div class="chart-container" style="position: relative; left: -20px; height:100%; width:100%; margin:auto;">
                        <div id="chart" style="width: 100%; height: 100%;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-xl-3 stretch-card grid-margin">
                <div class="card color-card-wrapper">
                  <div class="card-body">
                    <img class="img-fluid card-top-img w-100" src="../assets/images/dashboard/img_5.jpg" alt="" />
                    <div class="d-flex flex-wrap justify-content-around color-card-outer">
                      <div class="col-6 p-0 mb-4">
                        <div class="color-card primary m-auto">
                          <i class="mdi mdi-archive"></i>
                          <p class="font-weight-semibold mb-0">Available</p>
                          <span class="small"><asp:Label ID="lbltersedia" runat="server" Text="0"></asp:Label> Barang</span>
                        </div>
                      </div>
                      <div class="col-6 p-0 mb-4">
                        <div class="color-card bg-danger m-auto">
                          <i class="mdi mdi-folder-remove"></i>
                          <p class="font-weight-semibold mb-0">Not Available</p>
                          <span class="small"><asp:Label ID="lbltidaktersedia" runat="server" Text="0"></asp:Label> Barang</span>
                        </div>
                      </div>
                       <div class="col-6 p-0">
                        <div class="color-card bg-info m-auto">
                          <i class="mdi mdi-folder-download"></i>
                          <p class="font-weight-semibold mb-0">Stock Masuk</p>
                          <span class="small"><asp:Label ID="lblmasuk" runat="server" Text="0"></asp:Label> Laporan</span>
                        </div>
                      </div>
                      <div class="col-6 p-0">
                        <div class="color-card bg-success m-auto">
                          <i class="mdi mdi-check-circle-outline"></i>
                          <p class="font-weight-semibold mb-0">Stock Keluar</p>
                          <span class="small"><asp:Label ID="lblkeluar" runat="server" Text="0"></asp:Label> Laporan</span>
                        </div>
                      </div>
                      <div class="col-6 p-0 mt-4">
                        <div class="color-card bg-danger m-auto">
                          <i class="mdi mdi-image-broken-variant"></i>
                          <p class="font-weight-semibold mb-0">Rusak</p>
                          <span class="small"><asp:Label ID="lblBroken" runat="server" Text="0"></asp:Label> Barang</span>
                        </div>
                      </div>
                      <div class="col-6 p-0 mt-4">
                        <div class="color-card bg-info m-auto">
                          <i class="mdi mdi-sale"></i>
                          <p class="font-weight-semibold mb-0">Discounted</p>
                          <span class="small"><asp:Label ID="lbldiscount" runat="server" Text="0"></asp:Label> Barang</span>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          <!-- content-wrapper ends -->
          <!-- partial:partials/_footer.html -->
          <footer class="footer">
            <div class="container">
              <div class="d-sm-flex justify-content-center justify-content-sm-between">
                <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">Copyright © 2022 Daffa Athallah. All rights reserved.</span>
                <span class="float-none float-sm-right d-block mt-1 mt-sm-0 text-center">Hand-crafted & made with <i class="mdi mdi-heart text-danger"></i><a href="https://www.bootstrapdash.com/" target="_blank">Bootstrapdash</a><i class="mdi mdi-heart text-danger"></i></span>
              </div>
            </div>
          </footer>
          <!-- partial -->
        </div>
        <!-- main-panel ends -->
      </div>
      <!-- page-body-wrapper ends -->
    </div>
    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src="../assets/vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <script src="../assets/vendors/jquery-bar-rating/jquery.barrating.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="../assets/vendors/flot/jquery.flot.js"></script>
    <script src="../assets/vendors/flot/jquery.flot.resize.js"></script>
    <script src="../assets/vendors/flot/jquery.flot.categories.js"></script>
    <script src="../assets/vendors/flot/jquery.flot.fillbetween.js"></script>
    <script src="../assets/vendors/flot/jquery.flot.stack.js"></script>
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src="../assets/js/off-canvas.js"></script>
    <script src="../assets/js/hoverable-collapse.js"></script>
    <script src="../assets/js/misc.js"></script>
    <script src="../assets/js/settings.js"></script>
    <script src="../assets/js/todolist.js"></script>
    <!-- endinject -->
    <!-- Custom js for this page -->
    <script src="../assets/js/dashboard.js"></script>
    <!-- End custom js for this page -->
          </form>


      <!--chart-->
       <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

        // Load the Visualization API and the corechart package.
        google.charts.load('current', { 'packages': ['corechart'] });

        // Set a callback to run when the Google Visualization API is loaded.
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Produk Barang',
                width: 810,
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
                isStacked: false
            };
            $.ajax({
                type: "POST",
                url: "dashboard.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.ColumnChart($("#chart")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
         </script>
  </body>
</html>
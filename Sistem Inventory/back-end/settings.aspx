<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="settings.aspx.vb" Inherits="Sistem_Inventory.settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Inventory</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../assets/vendors/mdi/css/materialdesignicons.min.css"/>
    <link rel="stylesheet" href="../assets/vendors/flag-icon-css/css/flag-icon.min.css"/>
    <link rel="stylesheet" href="../assets/vendors/css/vendor.bundle.base.css"/>
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="../assets/vendors/jquery-bar-rating/css-stars.css" />
    <link rel="stylesheet" href="../assets/vendors/font-awesome/css/font-awesome.min.css" />
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <!-- endinject -->
    <!-- Layout styles -->
    <link rel="stylesheet" href="../assets/css/demo_2/style.css" />
    <!-- End layout styles -->
    <link rel="shortcut icon" href="../assets/images/favicon.png" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous"/>
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
                <!-- Main -->
    <div class="container-fluid page-body-wrapper">
            <div class="main-panel">
          <div class="content-wrapper">
            <div class="page-header">
              <h3 class="page-title">Pengaturan</h3>
        <div class="float-left">
             <asp:PlaceHolder Visible="true" ID="PlaceHolderAkunBTN" runat="server">
        <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnAkun" runat="server">
            Account
        </button>
            </asp:PlaceHolder>
           <asp:PlaceHolder Visible="False" ID="PlaceHolderAkunBackBtn" runat="server">
        <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnAkunKembali" runat="server">
            Kembali
        </button>
            </asp:PlaceHolder>
    </div>
            </div>
            <div class="row">
              <div class="col-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                    <asp:PlaceHolder ID="plhList" runat="server" Visible="false">
                        <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 pad-profile">
                                        <div class="card rounded-0">
                                            <div class="card-body">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rptData" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-bordered" style="margin-bottom:0px;">
                                                                <tr>
                                                                    <th scope="row">
                                                                        Id user
                                                                    </th>
                                                                    <th scope="row">
                                                                        Username
                                                                    </th>
                                                                   <th scope="row">
                                                                        Nama Lengkap
                                                                    </th>
                                                                    <th scope="row">
                                                                        Access
                                                                    </th>
                                                                    <th scope="row" style="width:5px">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblIdUser" runat="server" Text='<%# Eval("id") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("username") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNamaLengkap" runat="server" Text='<%# Eval("nama") %>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAccess" runat="server" Text='<%# Eval("access") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Button CssClass="btn btn-info btn-sm" ID="btnUbahData" Text="Ubah" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="UbahData"/> | 
                                                                    <asp:Button CssClass="btn btn-danger btn-sm" ID="btnHapusData" Text="Hapus" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="HapusData"/>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>

                                                <table width="100%" border="0" class="mt-2">
                                                    <tr>
                                                        <td style="padding-left:15px">
                                                            <asp:Label ID="lblPaging" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left:15px;padding-bottom:20px;">
                                                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" Width="80px"></asp:Button>
                                                            <asp:Button ID="btnNext" runat="server" Text="Next" Width="80px"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </asp:PlaceHolder>
                      <asp:PlaceHolder ID="plhRinci" runat="server" Visible="false">
                        <div class="content-body">
                            <section>
                                <div class="row">
                                    <div class="col-md-12 pad-profile">
                                        <asp:Label ID="lblNomor" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="card rounded-0">
                                            <div class="card-body" aria-busy="True" aria-checked="undefined">
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderTitleEdit" runat="server"><h4>Edit Akun</h4></asp:PlaceHolder>
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderprofile" runat="server"><h4>Profile</h4></asp:PlaceHolder>
                                                <br />
                                                <div class="row">
                                                    <div class="form-group col-md-6" style="margin-bottom:12px;">
                                                        <label>Id User :</label><br />
                                                        <asp:TextBox ReadOnly="true" ID="txtIdUserRinci" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Username :</label><br />
                                                        <asp:TextBox ID="txtUsernameRinci" ReadOnly="true" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Nama Lengkap :</label><br />
                                                        <asp:TextBox ID="txtnama" ReadOnly="true" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <asp:PlaceHolder ID="PH_Access" Visible="false" runat="server">
                                                        <label>Account :</label><br />
                                                        <asp:DropDownList ID="DropDownAccess" CssClass="form-control form-control-line" runat="server" Enabled="False">
                                                            <asp:ListItem Text="Akun Belum Di Aktivasi" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="------------" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Supplier" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Karyawan" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PH_password" Visible="false" runat="server">
                                                        <label>Password :</label><br />
                                                        <asp:TextBox ID="pw" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Re-Type Password :</label><br />
                                                        <asp:TextBox ID="pw2" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        </asp:PlaceHolder>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer">
                                                <div class="float-right">
                                                    <button type="button" class="btn btn-warning mr-1" id="btnBatal" runat="server">
                                                        Batal
                                                    </button>
                                                    <button type="submit" class="btn btn-primary" id="btnSimpan" runat="server">
                                                        Simpan
                                                    </button>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </section>    
                        </div>
                    </asp:PlaceHolder>
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


    <script src="../assets/vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <script src="../assets/vendors/jquery-bar-rating/jquery.barrating.min.js"></script>
    <script src="../assets/vendors/chart.js/Chart.min.js"></script>
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
     </div>
    </form>
</body>
</html>

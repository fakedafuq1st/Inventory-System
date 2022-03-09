<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="inventory.aspx.vb" Inherits="Sistem_Inventory.inventory" %>

<html>
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
    <!-- End layout styles -->
    <link rel="shortcut icon" href="../assets/images/favicon.png" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
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
          <div class="content-wrapper">
            <div class="page-header">
              <h3 class="page-title">Menu Barang</h3>
        <div class="float-right">
             <asp:PlaceHolder Visible="true" ID="PlaceHolderCariBTN" runat="server">
        <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnCari" runat="server">
            Cari
        </button>
            </asp:PlaceHolder>
           <asp:PlaceHolder Visible="False" ID="PlaceHolderCariBackBtn" runat="server">
        <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnKembali" runat="server">
            Kembali
        </button>
            </asp:PlaceHolder>
    </div>
            </div>
            <div class="row">
              <div class="col-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                    <asp:PlaceHolder ID="plhList" runat="server" Visible="true">
                        <div class="content-body">
                            <section>
                                <div class="row">
                                    <div class="col-md-16 pad-profile">
                                        <div class="card rounded-0">
                                            <div class="card-body">
                       <asp:PlaceHolder Visible="false" ID="PlaceHolderCari" runat="server">
                           <section>
                                <div class="row">
                                    <div class="col-md-12 pad-profile">
                                        <div class="card rounded-0">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h5 class="content-header-title">Pencarian Barang</h5>
                                                        <hr />
                                                    </div>
                                                    <div class="col-md-6" style="margin-bottom:12px;">
                                                        <label>Cari dari Nama barang</label>
                                                        <input placeholder="Nama Barang" type="text" class="form-control form-control-line" id="txtCariID" runat="server">
                                                        <br />
                                                     <asp:Button ID="btnCariGo" Width="25%" OnClick="btnCariGo_Click" runat="server" Text="Cari" CssClass="btn btn-block btn-lg btn-info" Font-Size="Small" />
                                                    </div>
                                                    <div class="col-md-6" style="margin-bottom:12px;">
                                                        <label>Cari Berdasarkan</label>
                                                        <asp:Button ID="btnlessunit" OnClick="btnlessunit_Click" runat="server" Text="Barang tak tersedia" CssClass="btn btn-block btn-lg btn-info" Font-Size="Small" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer">
                                                <div class="float-left">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                 </div>
                            </section>
                      </asp:PlaceHolder>
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rptData" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-bordered" style="margin-bottom:0px;">
                                                                <tr>
                                                                    <th scope="row">
                                                                        Id Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Gambar Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Nama Barang
                                                                    </th>
                                                                   <th scope="row">
                                                                        Jenis Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Modal Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Harga Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Banyak Unit
                                                                    </th>
                                                                    <th scope="row" style="width:5px">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblIdBarang" runat="server" Text='<%# Eval("id") %>' />
                                                                </td>
                                                                
                                                                 <td>
                                                                    <asp:Image ID="imgbarang" runat="server" Width="100" Height="100" ImageUrl='<%# Eval("gambar") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNamaBarang" runat="server" Text='<%# Eval("nama") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblJenisBarang" runat="server" Text='<%#If(Eval("jenis").ToString() = "1", "Pecah Belah", "Kukuh Kuat") %>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblModalBarang" runat="server" Text='<%# Eval("modal") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblHargaBarang" runat="server" Text='<%# Eval("harga") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUnitBarang" runat="server" Text='<%# Eval("unit") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Button CssClass="btn btn-info btn-sm" ID="btnUbahRinci" Text="Ubah" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="UbahRinciCek"/> | 
                                                                    <asp:Button CssClass="btn btn-danger btn-sm" ID="btnHapusData" Text="Hapus" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="btnHapusData_Click"/>
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
                                                            <%--<asp:Label ID="lblPaging" runat="server" BackColor="Yellow"BorderColor="Yellow" Font-Bold="True"
                                                                ForeColor="#FF3300"></asp:Label> height="500" --%>
                                                            <asp:Label ID="lblPaging" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left:15px;padding-bottom:20px;">
                                                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" Width="80px"></asp:Button>
                                                            <asp:Button ID="btnNext" runat="server" Text="Next" Width="80px"></asp:Button>
                                                            <asp:PlaceHolder ID="PlaceHolderTambahAccess" runat="server" Visible="true">
                                                             <div class="float-right">
                                                                <button type="button" class="btn btn-success mr-1" id="btnTambah" runat="server">
                                                                    Tambah
                                                                </button>
                                                            </div>
                                                                </asp:PlaceHolder>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </section>    
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
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderTitleBaru" runat="server"><h4>Input barang baru</h4></asp:PlaceHolder>
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderTitleEdit" runat="server"><h4>Edit barang : "<asp:Label ID="lblNamaBarangEdit" runat="server" Text="Error Cannot Retrieve nama barang" Font-Bold="True" Font-Underline="False"></asp:Label>"</h4></asp:PlaceHolder>
                                                <br />
                                                <div class="row">
                                            <asp:Label Visible="false" ID="lblaccdebug" runat="server" Text="No status detected"></asp:Label>
                                                    <div class="form-group col-md-6" style="margin-bottom:12px;">
                                                        <label>Id Barang :</label><br />
                                                        <asp:TextBox ReadOnly="true" ID="txtIdBarangRinci" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Nama Barang :</label><br />
                                                        <asp:TextBox ID="txtNamaBarangRinci" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Jenis Barang :</label><br />
                                                        <asp:DropDownList ID="DropDownJenis" CssClass="form-control form-control-line" runat="server">
                                                            <asp:ListItem Text="Pilih Jenis" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="------------" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Pecah Belah" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="kukuh Kuat" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                        <label>Harga Barang :</label><br />
                                                        <asp:TextBox ID="txtHargaBarangRinci" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                       <label>Modal Barang :</label><br />
                                                       <asp:TextBox ID="txtModalBarangRinci" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Gambar Barang :</label><br />
                                                        <asp:Label ID="lblExtension" runat="server" Text="(png, jpg, jpeg)"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblimageuploaded" runat="server" visible="false" Text=""></asp:Label>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
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
    <!-- container-scroller -->
    <!-- plugins:js -->
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
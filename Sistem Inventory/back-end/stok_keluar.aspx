<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="stok_keluar.aspx.vb" Inherits="Sistem_Inventory.stok_keluar" %>

<!DOCTYPE html>
<html lang="en">
  <head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Plus Admin</title>
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
  </head>
  <body>
      <form id="form1" runat="server">
    <div class="container-scroller">
      <!-- partial:partials/_horizontal-navbar.html -->
      <div class="horizontal-menu">
        <nav class="navbar top-navbar col-lg-12 col-12 p-0">
          <div class="container">
            <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-center">
               <a class="navbar-brand brand-logo">
                  <span class="font-19 d-block font-weight-light">Placeholder Nama Perusahaan</span>
              </a>
              <a class="navbar-brand brand-logo-mini" href="index.html"><img src="../assets/images/logo-mini.svg" alt="logo" /></a>
            </div>
            <div class="navbar-menu-wrapper d-flex align-items-center justify-content-end">
              <ul class="navbar-nav mr-lg-2">
                <li class="nav-item nav-search d-none d-lg-block">
                </li>
              </ul>
              <ul class="navbar-nav navbar-nav-right">
                <li class="nav-item nav-profile dropdown">
                  <a class="nav-link" id="profileDropdown" href="#" data-toggle="dropdown" aria-expanded="false">
                    <div class="nav-profile-text">
                      <p class="text-black font-weight-semibold m-0"><asp:Label ID="lblnamaakun" runat="server" Text="Missing Account Name"></asp:Label></p>
                      <span class="font-13 online-color"><asp:Label ID="lblacc" runat="server" Text=""></asp:Label><i class="mdi mdi-chevron-down"></i></span>
                    </div>
                  </a>
                  <div class="dropdown-menu navbar-dropdown" aria-labelledby="profileDropdown">
                       <asp:PlaceHolder ID="PlaceHolderchangepass" Visible="false" runat="server">
                        <a class="dropdown-item" href="#" onclick="changepass" id="lnkpassword" runat="server">
                        <i class="mdi mdi-cached mr-2 text-success"></i> Change Password </a>
                       <div class="dropdown-divider"></div>
                       </asp:PlaceHolder>
                    <a class="dropdown-item" href="#" onclick="Logout" id="lnkLogout" runat="server">
                      <i class="mdi mdi-logout mr-2 text-primary"></i> Signout </a>
                  </div>
                </li>
              </ul>
              <button class="navbar-toggler navbar-toggler-right d-lg-none align-self-center" type="button" data-toggle="horizontal-menu-toggle">
                <span class="mdi mdi-menu"></span>
              </button>
            </div>
          </div>
        </nav>
        <nav class="bottom-navbar">
          <div class="container">
            <ul class="nav page-navigation">
              <li class="nav-item">
                <a class="nav-link" href="Dashboard.aspx">
                  <i class="mdi mdi-home menu-icon"></i>
                  <span class="menu-title">Dashboard</span>
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="Inventory.aspx">
                  <i class="mdi mdi-folder menu-icon"></i>
                  <span class="menu-title">Barang</span>
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="Stok_Masuk.aspx">
                  <i class="mdi mdi-arrow-down menu-icon"></i>
                  <span class="menu-title">Laporan stok masuk</span>
                </a>
              </li>
                <li class="nav-item">
                <a class="nav-link" href="Stok_Keluar.aspx">
                  <i class="mdi mdi-arrow-up menu-icon"></i>
                  <span class="menu-title">Laporan stok keluar</span>
                </a>
              </li>
                <li class="nav-item">
                <div class="nav-link d-flex">
                  <button class="btn btn-sm bg-danger text-white"> Trailing </button>
                  <div class="nav-item dropdown">
                    <a class="nav-link count-indicator dropdown-toggle text-white font-weight-semibold" id="notificationDropdown" href="#" data-toggle="dropdown"> Indonesia </a>
                    <div class="dropdown-menu dropdown-menu-right navbar-dropdown preview-list" aria-labelledby="notificationDropdown">
                      <a class="dropdown-item" href="#">
                        <i class="mr-3"></i> English </a>
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
              <h3 class="page-title">Menu Laporan Stok | Stok Keluar</h3>
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
                                    <div class="col-md-12 pad-profile">
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
                                                        <label>Cari dari ID stok</label>
                                                        <input placeholder="ID stok" type="text" class="form-control form-control-line" id="txtCariID" runat="server">
                                                    </div>
<%--                                                    <div class="col-md-6" style="margin-bottom:12px;">
                                                        <label>Cari dari Nama Barang</label>
                                                        <input placeholder="Nama Barang" type="text" class="form-control form-control-line" id="txtCariNAMA" runat="server">
                                                    </div>--%>
                                                </div>
                                            </div>
                                            <div class="card-footer">
                                                <div class="float-left">
                                                    <asp:Button ID="btnCariGo" OnClick="btnCariGo_Click" runat="server" Text="Cari" CssClass="btn btn-block btn-lg btn-info" Font-Size="Small" />
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
                                                                        Tanggal Laporan
                                                                    </th>
                                                                    <th scope="row">
                                                                        Id Stok
                                                                    </th>
                                                                    <th scope="row">
                                                                        Id Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Unit
                                                                    </th>
                                                                    <th scope="row">
                                                                        ID User
                                                                    </th>
                                                                    <th scope="row">
                                                                        Keterangan Lengkap
                                                                    </th>
                                                                    <th scope="row" style="width:5px">
                                                            
                                                                    </th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTanggal" runat="server" Text='<%# Eval("tanggal") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblIdStok" runat="server" Text='<%# Eval("id_stok") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblIdBarang" runat="server" Text='<%# Eval("id_barang") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("unit") %>' />
                                                                </td>
                                                                 <td>
                                                                    <asp:Label ID="lblNamaAkun" runat="server" Text='<%# Eval("nama_user") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblKeterangan" runat="server" Text='<%# Eval("keterangan") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:PlaceHolder ID="PlaceHolderEditDataAccess" Visible="true" runat="server">
                                                                    <asp:Button ID="btnLihatDetail" Text="Lihat Detail" runat="server" CommandArgument='<%#Eval("id_stok") %>' OnClick="LihatDetail"/> |
                                                                    <asp:Button ID="btnUbahRinci" Text="Ubah" runat="server" CommandArgument='<%#Eval("id_stok") %>' OnClick="UbahRinci"/> |
                                                                    <asp:Button ID="btnHapusData" Text="Hapus" runat="server" CommandArgument='<%#Eval("id_stok") %>' OnClick="btnHapusData_Click"/>
                                                                    </asp:PlaceHolder>
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
                                                                ForeColor="#FF3300"></asp:Label>--%>
                                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left:15px;padding-bottom:20px;">
                                                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" Width="80px"></asp:Button>
                                                            <asp:Button ID="btnNext" runat="server" Text="Next" Width="80px"></asp:Button>
                                                            <asp:PlaceHolder ID="PlaceHolderTambahAccess" runat="server" Visible="false">
                                                             <div class="float-right">
                                                                <button type="button" class="btn btn-warning mr-1" id="btnTambah" runat="server">
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
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderTitleBaru" runat="server"><h3>Input Laporan Stok Masuk</h3></asp:PlaceHolder>
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderTitleEdit" runat="server"><h3>Edit Laporan Nomor : "<asp:Label ID="lblIdStok" runat="server" Text="Error Cannot Retrieve Id Laporan" Font-Bold="True" Font-Underline="False"></asp:Label>"</h3></asp:PlaceHolder>
                                                <br />
                                                <div class="row">
                                            <%--<asp:Label Visible="false" ID="lblaccdebug" runat="server" Text="No status detected"></asp:Label>--%>
                                                    <div class="form-group col-md-6" style="margin-bottom:12px;">
                                                        <label>Id Stok :</label><br />
                                                        <asp:TextBox ReadOnly="true" ID="txtIdStokRinci" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <label>Id Barang :</label><br />
                                                        <input runat="server" onkeypress="return isNumberKey(event)" id="txtIdBarangRinci" type="text">
                                                        <br />
                                                        <label>Unit :</label><br />
                                                        <input runat="server" id="txtUnitRinci" onkeypress="return isNumberKey(event)" type="text" >
                                                        <asp:PlaceHolder ID="PlaceHolderNama" Visible="True" runat="server">
                                                        <br />
                                                        <br /> 
                                                        <label>Dibuat Oleh :</label><br />
                                                        <input runat="server" id="txtNamaLengkapRinci" type="text" readonly="yes"></asp:PlaceHolder>
                                                        <br />
                                                        <br />
                                                        <label>Keterangan :</label><br />
                                                       <textarea id="txtKeteranganRinci" runat="server"  cols="50" rows="5"></textarea>
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
                      <asp:PlaceHolder ID="PlaceHolderDetails" runat="server" Visible="false">
                        <div class="content-body">
                            <section>
                                <div class="row">
                                    <div class="col-md-12 pad-profile">
                                        <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="card rounded-0">
                                            <div class="card-body" aria-busy="True" aria-checked="undefined">
                                                <asp:PlaceHolder Visible="false" ID="PlaceHolderDetailTitle" runat="server"><h3>Details Laporan Nomor : "<asp:Label ID="lblIdLaporanDetailjudul" runat="server" Text="Error Cannot Retrieve Id Laporan" Font-Bold="True" Font-Underline="False"></asp:Label>"</h3></asp:PlaceHolder>
                                                <br />
                                                <div class="row">
                                            <asp:Label Visible="false" ID="Label3" runat="server" Text="No status detected"></asp:Label>
                                                    <div class="form-group col-md-6" style="margin-bottom:12px;">
                                                        <label>Id Stok :</label><br />
                                                        <asp:TextBox ReadOnly="true" ID="txtIdStokDetail" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <label>Id Barang :</label><br />
                                                        <asp:TextBox ID="txtIdBarangDetail" ReadOnly="true" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <label>Unit :</label><br />
                                                        <input runat="server" id="txtUnitDetail" readonly="yes" onkeypress="return isNumberKey(event)" type="text" >
                                                        <br />
                                                        <br />
                                                        <label>Dibuat Oleh :</label><br />
                                                        <input runat="server" id="txtNamaPembuatdetail" type="text" readonly="yes">
                                                        <br />
                                                        <br />
                                                        <label>Keterangan :</label><br />
                                                        <textarea id="txtKeteranganDetail" readonly="readonly" runat="server"  cols="50" rows="5"></textarea>
<%--                                                        <br />
                                                        <br />
                                                       <label>Total :</label><br />
                                                       <input runat="server" id="txtTotalDetail" readonly="yes" onkeypress="return isNumberKey(event)" type="text" >--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer">
                                                <div class="float-right">
                                                    <button type="button" class="btn btn-warning mr-1" id="btnDetailKembali" runat="server">
                                                        kembali
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
          <footer class="footer">
            <div class="container">
              <div class="d-sm-flex justify-content-center justify-content-sm-between">
                <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">Copyright © 2021 Daffa Athallah. All rights reserved.</span>
                <span class="float-none float-sm-right d-block mt-1 mt-sm-0 text-center">Hand-crafted & made with <i class="mdi mdi-heart text-danger"></i><a href="https://www.bootstrapdash.com/" target="_blank">Bootstrapdash</a><i class="mdi mdi-heart text-danger"></i></span>
              </div>
            </div>
          </footer>
        </div>
       </div>
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
          </form>
  </body>
</html>
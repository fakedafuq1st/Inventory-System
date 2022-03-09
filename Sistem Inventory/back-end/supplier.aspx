 <%@ Page Language="vb" AutoEventWireup="false" CodeBehind="supplier.aspx.vb" Inherits="Sistem_Inventory.supplier" %>

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
                <a class="nav-link" href="dashboard2.aspx">
                  <i class="mdi mdi-home menu-icon"></i>
                  <span class="menu-title">Dashboard</span>
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="supplier.aspx">
                  <i class="mdi mdi-folder menu-icon"></i>
                  <span class="menu-title">Data</span>
                </a>
              </li>
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
                  <h5>Menu - Supplier</h5>
                  <br />
            <asp:PlaceHolder Visible="False" ID="ph_back" runat="server">
            <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnback" runat="server">
                Kembali
            </button>
                </asp:PlaceHolder>
                  <asp:PlaceHolder Visible="true" ID="ph_menu" runat="server">
                <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnBarang" runat="server">
                    Barang
                </button>
                <button type="button" class="btn btn-info btn-rounded btn-fw" id="btnRekap" runat="server">
                    Rekap Data
                </button>
            </asp:PlaceHolder>
              </div>
              <div class="header-right d-flex flex-wrap mt-md-2 mt-lg-0">
              </div>
            </div>
            <!-- first row starts here -->
            <div class="row">
              <div class="col-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                    <asp:PlaceHolder ID="ph_barang" runat="server" Visible="false">
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
                                                                        id Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Nama Barang
                                                                    </th>
                                                                   <th scope="row">
                                                                        Jenis Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Tipe Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Ukuran Barang
                                                                    </th>
                                                                    <th scope="row">
                                                                        Banyak Barang
                                                                    </th>
                                                                    <th scope="row" style="width:5px">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblIdbarang" runat="server" Text='<%# Eval("id") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNamaBarang" runat="server" Text='<%# Eval("nama") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblJenisBarang" runat="server" Text='<%# Eval("jenis") %>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTipe" runat="server" Text='<%# Eval("tipe") %>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("size") %>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:Button CssClass="btn btn-info btn-sm" ID="btnUbahData" Text="Ubah" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="Ubah_barang" /> | 
                                                                    <asp:Button CssClass="btn btn-danger btn-sm" ID="btnHapusData" Text="Hapus" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="Hapus_barang" />
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
                                                            <div class="float-right">
                                                                <button type="button" class="btn btn-success mr-1" id="btnTambah" runat="server">
                                                                    Tambah Data
                                                                </button>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </asp:PlaceHolder>
                      <asp:PlaceHolder ID="ph_rekap" runat="server" Visible="true">
                        <div class="content-body">
                            <section>
                                <div class="row">
                                    <div class="col-md-12 pad-profile">
                                        <div class="card rounded-0">
                                            <div class="card-body">
                                               <asp:PlaceHolder Visible="true" ID="PlaceHolderCari" runat="server">
                                                   <section>
                                                        <div class="row">
                                                            <div class="col-md-12 pad-profile">
                                                                <div class="card rounded-0">
                                                                    <div class="card-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h5 class="content-header-title">Rekap Data</h5>
                                                                                <hr />
                                                                            </div>
                                                                            <div class="col-md-6" style="margin-bottom:12px;">
                                                                                <label>Bulan :</label>
                                                                                <asp:DropDownList ID="DD_bulan" runat="server">
                                                                                    <asp:ListItem Text="Pilih Bulan" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="----========-----" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Januari" Value="01"></asp:ListItem>
                                                                                    <asp:ListItem Text="Februari" Value="02"></asp:ListItem>
                                                                                    <asp:ListItem Text="Maret" Value="03"></asp:ListItem>
                                                                                    <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                                                                    <asp:ListItem Text="Mei" Value="05"></asp:ListItem>
                                                                                    <asp:ListItem Text="Juni" Value="06"></asp:ListItem>
                                                                                    <asp:ListItem Text="Juli" Value="07"></asp:ListItem>
                                                                                    <asp:ListItem Text="Agustus" Value="08"></asp:ListItem>
                                                                                    <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                                                                    <asp:ListItem Text="Oktober" Value="10"></asp:ListItem>
                                                                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                                                    <asp:ListItem Text="Desember" Value="12"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <label>Tahun :</label>
                                                                                <asp:DropDownList ID="DD_tahun" runat="server">
                                                                                    <asp:ListItem Text="Pilih Tahun" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="----========-----" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                                                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                                                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <asp:Button ID="btnCariGo" runat="server" Text="Ambil Data" Width="25%" CssClass="btn btn-block btn-lg btn-info" Font-Size="Small" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-footer">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                         </div>
                                                    </section>
                                              </asp:PlaceHolder>
                                                <div class="table-responsive">
                                                    <table class="table table-striped table-bordered" style="margin-bottom:0px;">
                                                        <tr>
                                                            <th scope="row">
                                                                Bulan
                                                            </th>
                                                            <th scope="row">
                                                                Tahun
                                                            </th>
                                                            <th scope="row">
                                                                Barang Masuk
                                                            </th>
                                                            <th scope="row">
                                                                Barang Keluar
                                                            </th>
                                                            <th scope="row">
                                                                Barang Rusak
                                                            </th>
                                                            <th scope="row">
                                                                Barang Expired
                                                            </th>
                                                            <th scope="row">
                                                                Barang dipesan
                                                            </th>
                                                        </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBulan" runat="server" Text="Pilih Bulan"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTahun" runat="server" Text="Pilih Tahun"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMasuk" runat="server" Text="Tidak ada Data"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblKeluar" runat="server" Text="Tidak ada Data"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRusak" runat="server" Text="Tidak ada Data"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblExpired" runat="server" Text="Tidak ada Data"></asp:Label>
                                                        </td>
                                                            <td>
                                                            <asp:Label ID="lblOrder" runat="server" Text="Tidak ada data"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </section>    
                        </div>
                    </asp:PlaceHolder>
                      <asp:PlaceHolder ID="ph_detail" runat="server" Visible="false">
                        <div class="content-body">
                            <section>
                                <div class="row">
                                    <div class="col-md-12 pad-profile">
                                        <asp:Label ID="lblNomor" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="card rounded-0">
                                            <div class="card-body" aria-busy="True" aria-checked="undefined">
                                                <br />
                                                <div class="row">
                                                    <div class="form-group col-md-6" style="margin-bottom:12px;">
                                                        <asp:PlaceHolder ID="ph_id" visible="false" runat="server">
                                                        <label>Id :</label><br />
                                                        <asp:TextBox ReadOnly="true" ID="txtIdBarang" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        </asp:PlaceHolder>
                                                        <label>Nama Barang :</label><br />
                                                        <asp:TextBox ID="txtNamaBarang" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Jenis Barang :</label><br />
                                                        <asp:TextBox ID="txtJenis" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Tipe Barang :</label><br />
                                                        <asp:TextBox ID="txtTipe" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Ukuran Barang :</label><br />
                                                        <asp:TextBox ID="txtSize" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Banyak Barang :</label><br />
                                                        <asp:TextBox ID="txtQuantity" runat="server" TextMode="number" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <label>Supplier :</label><br />
                                                        <asp:TextBox ReadOnly="true" ID="txtUser" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                                                        <br />
                                                        <br />
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


      <!--chart-->
       <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  </body>
</html>
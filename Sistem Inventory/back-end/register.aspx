<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="register.aspx.vb" Inherits="Sistem_Inventory.register1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daftar Supplier Baru</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="//cdn.muicss.com/mui-0.10.3/css/mui.min.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.muicss.com/mui-0.10.3/js/mui.min.js"></script>
    <style>
    @import "bower_components/mui/src/sass/mui";
    body {
      font-family: Arial, Helvetica, sans-serif;
      background-color: dimgray ;
    }

    * {
      box-sizing: border-box;
    }
    /* Add padding to containers */
    /* Keseluruhan form */
    .container {
        padding: 600px;
        background-color: white;
        max-width: 800px;
        max-height: 1000px;
        margin: auto;
        padding: 10px;
        margin-top: 50px;
        border-radius: 5px;
    }

    /* Full-width input fields */
    input[type=text], input[type=password] {
      width: 70%;
      padding: 26px;
      margin: 0px 0 0px 0;
      display: inline-block;
      position: relative;
      left: 15%;
    }
        
    label{
        margin: 10px 0 0px 0;
        left: 17%;
    }

    input[type=text]:focus, input[type=password]:focus {
      background-color: #ddd;
      outline: none;
    }

    /* Overwrite default styles of hr */
    hr {
      border: 1px solid #f1f1f1;
      margin-bottom: 25px;
    }

    /* Set a style for the submit button */
    .registerbtn {
      background-color: #4CAF50;
      color: white;
      padding: 10px 20px;
      margin: 8px 0;
      border: none;
      cursor: pointer;
      width: 25%;
      opacity: 0.9;
      border-radius: 5px;
      position: relative;
      left: 40%;
      font-size: 15px;
    }

    .registerbtn:hover {
      opacity: 1;
    }

    .backbtn {
    text-decoration: none;
    background-color: red;
    color: white;
    padding: 10px 70px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 25%;
    opacity: 0.9;
    border-radius: 5px;
    position: relative;
    left: 10%;
    font-size: 15px;
    }

    .backbtn:hover {
      opacity: 1;
    }
    .savebtn {
      background-color: #4CAF50;
      color: white;
      padding: 10px 20px;
      margin: 8px 0;
      border: none;
      cursor: pointer;
      width: 25%;
      opacity: 0.9;
      border-radius: 5px;
      position: relative;
      left: 40%;
      font-size: 15px;
    }

    .savebtn:hover {
      opacity: 1;
    }

    .Bypassbtn {
      background-color: red;
      color: white;
      padding: 16px 20px;
      margin: 8px 1px;
      border: none;
      cursor: pointer;
      width: 48%;
      opacity: 0.9;
    }

    .Bypassbtn:hover {
      opacity: 1;
    }

    .Bypassbtn2 {
      background-color: red;
      color: white;
      padding: 16px 20px;
      margin: 8px 5px;
      border: none;
      cursor: pointer;
      width: 49%;
      opacity: 0.9;
    }

    .Bypassbtn2:hover {
      opacity: 1;
    }

    /* Add a blue text color to links */
    a {
      color: dodgerblue;
    }

    /* Set a grey background color and center the text of the "sign in" section */
    .signin {
      background-color: #f1f1f1;
      text-align: center;
    }
    .imgcontainer {
      text-align: center;
      margin: 24px 0 12px 0;
    }

    img.avatar {
      width: 40%;
      border-radius: 50%;
    }
    </style>
    <link rel="shortcut icon" href="assets\images\favicon.png" />
</head>
<body>
    <form id="form1" class="mui-form" runat="server">
    <asp:PlaceHolder ID="phmain" Visible="true" runat="server">
        <div class="container">
        <h3 style="text-align:center;">Daftar Supplier Baru</h3>
        <hr>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="" type="text" name="Nama Supplier" required="" id="txtName" runat="server">
        <label>Nama Supplier</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="address" required="" id="txtAlamat" runat="server">
        <label>Alamat Lengkap</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="Kota" required="" id="txtKota" runat="server">
        <label>Kota</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="Negara" required="" id="txtNegara" runat="server">
        <label>Negara</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="Provinsi" required="" id="txtProvinsi" runat="server">
        <label>Provinsi</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="Kode Pos" required="" id="txtKodePos" runat="server">
        <label>Kode Pos</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="Telepon" required="" id="txtTelepon" runat="server">
        <label>Telepon</label>
        </div>
        <br/>
        <br/>
        <a href="#" onclick="Logout" runat="server" id="lnkLogout" class="backbtn">Logout</a>
        <button class="registerbtn" runat="server" id="btnsubmit" type="submit">Buat Data</button>
        <br/>
        <br/>
      </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="ph_final" Visible="false" runat="server">
        <div class="container">
            <h2 style="text-align:center;">Harap Cek kembali Data.</h2>
            <hr>
            <label>Nama Supplier :</label>
            <asp:TextBox ReadOnly="true" ID="txtNamaCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <label>Alamat Lengkap :</label>
            <asp:TextBox ReadOnly="true" ID="txtAlamatCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <label>Kota :</label>
            <asp:TextBox ReadOnly="true" ID="txtKotaCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <label>Negara :</label>
            <asp:TextBox ReadOnly="true" ID="txtNegaraCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <label>Provinsi :</label>
            <asp:TextBox ReadOnly="true" ID="txtProvinsiCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <label>Kode Pos :</label>
            <asp:TextBox ReadOnly="true" ID="txtKodePosCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <label>Telepon :</label>
            <asp:TextBox ReadOnly="true" ID="txtTeleponCek" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
            <br />
            <br />
            <a href="#" onclick="Back" runat="server" id="back" class="backbtn">Kembali</a>
            <button class="registerbtn" runat="server" id="btnsave" type="submit">Data Sudah Benar</button>
            <br/>
            <br/>
        </div>
        </asp:PlaceHolder>
    </form>
</body>
</html>

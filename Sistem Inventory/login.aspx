<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Sistem_Inventory.Index" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
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
        max-width: 500px;
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
      padding: 10px 15px;
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
      background-color: yellow;
      color: black;
      padding: 10px 35px;
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
    <asp:PlaceHolder ID="phlogin" Visible="true" runat="server">
        <div class="container">
         <img src="assets\images\DA.svg" class="rounded mx-auto d-block" alt="...">
        <h4 style="text-align:center">Login</h4>
        <hr>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="text" name="username" required="" id="txtUsername" runat="server">
        <label>Username</label>
        </div>
        <div class="mui-textfield mui-textfield--float-label">
        <input style="content" type="password" name="psw" required="" id="txtPassword" runat="server">
        <label>Password</label>
        </div>
        <br />
        <br />
        <a href="index.html" class="backbtn">Kembali</a>
        <button class="registerbtn" runat="server" id="btnsubmit" type="submit">Masuk</button>
        <br />
        <br />
       <!--<button class="registerbtn" runat="server" id="btnback">Kembali</button> -->
      <%-- <button class="Bypassbtn" runat="server" id="Btnbypass" type="submit">Bypass Level 1</button><button class="Bypassbtn2" runat="server" id="Btnbypass2" type="submit">Bypass Level 2</button>--%>
      </div>
    </asp:PlaceHolder>
        <asp:PlaceHolder ID="phmain" Visible="false" runat="server">
         <div class="container">
        <h1 style="text-align:center">Inventory System</h1>
        <h3 style="text-align:center">Main Menu</h3>
        <hr>
         <button class="registerbtn" runat="server" id="btnlogin">Login</button>
         <button class="registerbtn" runat="server" id="btnsetting">Pengaturan</button>
        </div>
        </asp:PlaceHolder>
        
        <asp:PlaceHolder ID="phsetting" Visible="false" runat="server">
        <div class="container">
        <h1 style="text-align:center">Inventory System</h1>
        <h3 style="text-align:center">Pengaturan</h3>
        <hr>
         <a>Server: <asp:Label ID="lblserver" runat="server" Text="tidak terhubung dengan server manapun"></asp:Label></a>
         <button class="registerbtn" runat="server" id="btnserver">Atur Server</button>
         <button class="registerbtn" runat="server" id="btnback2">Kembali</button>
        </div>
        </asp:PlaceHolder>
         
        <asp:PlaceHolder ID="phserver" Visible="false" runat="server">
        <div class="container">
        <h1 style="text-align:center">Inventory System</h1>
        <h3 style="text-align:center">Pengaturan Server</h3>
        <hr>
         <h3>List Server :</h3>
         <button class="registerbtn" runat="server" id="But">Tambah Server</button>
         <button class="registerbtn" runat="server" id="Button1">Hapus Server</button>
         <button class="registerbtn" runat="server" id="Button2">Kembali</button>
        </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phserverrinci" Visible="false" runat="server">
        <div class="container">
        <h1 style="text-align:center">Inventory System</h1>
        <h3 style="text-align:center">Server : <asp:Label ID="lblserverjudul" runat="server" Text="Error No Server"></asp:Label></h3>
        <hr>
         <h3>List Server :</h3>
         <button class="registerbtn" runat="server" id="Button3">Tambah Server</button>
         <button class="registerbtn" runat="server" id="Button4">Hapus Server</button>
         <button class="registerbtn" runat="server" id="Button5">Kembali</button>
        </div>
        </asp:PlaceHolder>
    </form>
</body>
</html>

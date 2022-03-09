<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="register.aspx.vb" Inherits="Sistem_Inventory.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Daftar Akun</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <style>
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
        margin-top: 250px;
        border-radius: 5px;
    }

    /* Full-width input fields */
    input[type=text], input[type=password] {
      color:black;
      width: 70%;
      padding: 15px;
      margin: 10px 0 5px 0;
      display: inline-block;
      border: none;
      background: #f1f1f1;
      border-radius: 5px;
      position: relative;
      left: 15%;
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
      background-color: white;
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
    <form id="form1" runat="server">
  <div class="container">
        <h3 style="color:black; text-align:center">Daftar Akun Baru</h3>
        <hr/>
        <input style="content" type="text" placeholder="Username" name="username" required="" id="txtUsername" runat="server"/>
        <br />
        <input style="content" type="text" placeholder="Nama Lengkap" name="namaLengkap" required="" id="txtNama" runat="server"/>
        <br />
        <input type="password" placeholder="Password" name="psw" required="" id="txtPassword" runat="server"/>
        <br />
        <input type="password" placeholder="Re-Type Password" name="pswval" required="" id="txtPasswordVal" runat="server"/>
        <br />
        <br />
        <a href="index.html" class="backbtn">Kembali</a>
        <button class="registerbtn" runat="server" id="btnsubmit" type="submit">Daftar</button>
        <br />
        <br />
      </div>
    </form>
</body>
</html>

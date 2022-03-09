Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Configuration
Imports System.Web.UI.DataVisualization.Charting
Imports System.Web.Services
Imports System.Web.Script.Services


Public Class register1
    Inherits System.Web.UI.Page
    Dim myConnection As SqlConnection
    Dim sqlString As String
    Dim myCommand As SqlCommand
    Dim myReader As SqlDataReader
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        If Not Page.IsPostBack Then
            If Session("BlnLoggedIn") = False Then
                Response.Write("<script> window.open('../index.html', '_self'); </script>")
            End If
        End If
    End Sub


    Private Sub Messagebox(ByVal Message As String)
        Dim lblMessageBox As New Label()
        lblMessageBox.Text = "<script language='javascript'>" + Environment.NewLine & "window.alert('" & Message & "')</script>"
        Page.Controls.Add(lblMessageBox)
    End Sub

    Private Sub btnsubmit_ServerClick(sender As Object, e As EventArgs) Handles btnsubmit.ServerClick
        'check if the input was empty or not clear.
        If Len(Trim(txtName.Value)) = 0 Then
            Messagebox("Kolom nama masih kosong!")
            txtName.Focus()
            Return
        End If
        If Len(Trim(txtAlamat.Value)) = 0 Then
            Messagebox("Kolom alamat Belum Diisi !")
            txtAlamat.Focus()
            Return
        End If
        If Len(Trim(txtKota.Value)) = 0 Then
            Messagebox("Kolom Kota Belum Diisi !")
            txtKota.Focus()
            Return
        End If
        If Len(Trim(txtNegara.Value)) = 0 Then
            Messagebox("Kolom negara belum diisi!")
            txtNegara.Focus()
            Return
        End If
        If Len(Trim(txtKodePos.Value)) = 0 Then
            Messagebox("Kolom Kode Pos Belum Diisi ")
            txtKodePos.Focus()
            Return
        End If
        If Len(Trim(txtTelepon.Value)) = 0 Then
            Messagebox("Kolom Telepon Belum Diisi !!")
            txtTelepon.Focus()
            Return
        End If
        'if these valid, then continue to add the input to the check.

        txtNamaCek.Text = txtName.Value
        txtAlamatCek.Text = txtAlamat.Value
        txtKotaCek.Text = txtKota.Value
        txtNegaraCek.Text = txtNegara.Value
        txtProvinsiCek.Text = txtProvinsi.Value
        txtKodePosCek.Text = txtKodePos.Value
        txtTeleponCek.Text = txtTelepon.Value

        ph_final.Visible = True
        phmain.Visible = False
    End Sub

    Private Sub lnkLogout_ServerClick(sender As Object, e As System.EventArgs) Handles lnkLogout.ServerClick
        Session("BlnLoggedIn") = False
        Session("UserAuthentication") = ""
        Response.Write("<script> window.open('../index.html', '_self'); </script>")
    End Sub

    Private Sub Back_ServerClick(sender As Object, e As System.EventArgs) Handles back.ServerClick
        ph_final.Visible = False
        phmain.Visible = True
        'readding for editing purposes.
        txtName.Value = txtNamaCek.Text
        txtAlamat.Value = txtAlamatCek.Text
        txtKota.Value = txtKotaCek.Text
        txtNegara.Value = txtNegaraCek.Text
        txtKodePos.Value = txtKodePosCek.Text
        txtTelepon.Value = txtTeleponCek.Text
    End Sub

    Private Sub btnsave_ServerClick(sender As Object, e As EventArgs) Handles btnsave.ServerClick
        'dim for useless bit
        Dim id_barang, id As Integer
        Dim nama_barang As String
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "Select * from supplier" & Chr(10)
        sqlString = sqlString & "where id_user = '" & Session("id") & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            ID = myReader("id")
            id_barang = myReader("id_barang")
            nama_barang = myReader("nama_Barang")
        End If
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_supplier", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", ID)
            cmd.Parameters.AddWithValue("@f_nama_supplier", txtNamaCek.Text)
            cmd.Parameters.AddWithValue("@f_alamat", txtAlamatCek.Text)
            cmd.Parameters.AddWithValue("@f_kota", txtKotaCek.Text)
            cmd.Parameters.AddWithValue("@f_negara", txtNegaraCek.Text)
            cmd.Parameters.AddWithValue("@f_provinsi", txtProvinsiCek.Text)
            cmd.Parameters.AddWithValue("@f_kode_pos", txtKodePosCek.Text)
            cmd.Parameters.AddWithValue("@f_telepon", txtTeleponCek.Text)
            cmd.Parameters.AddWithValue("@f_register", 1)
            cmd.Parameters.AddWithValue("@f_id_barang", id_barang)
            cmd.Parameters.AddWithValue("@f_nama_Barang", nama_barang)
            cmd.Parameters.AddWithValue("@f_id_user", Session("id"))
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try

        Response.Write("<script>alert('Pendaftaran Berhasil.')</script>")
        Response.Write("<script> window.open('../back-end/dashboard2.aspx', '_self'); </script>")

    End Sub




End Class
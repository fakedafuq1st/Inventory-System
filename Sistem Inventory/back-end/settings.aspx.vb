Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.IO
Public Class settings
    Inherits System.Web.UI.Page
    Dim myConnection As SqlConnection
    Dim sqlString As String
    Dim myCommand As SqlCommand
    Dim myReader As SqlDataReader
    Dim myReader2 As SqlDataReader
    Dim sqlString2 As String
    Dim myCommand2 As SqlCommand
    Dim myConnection2 As SqlConnection
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        If Not Page.IsPostBack Then
            If Session("BlnLoggedIn") = False Then
                Response.Write("<script> window.open('../index.html', '_self'); </script>")

            Else
                Call InfoUser()
                Call BindData()
            End If
        End If
    End Sub
    Protected Sub InfoUser()
        Dim access As String
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "Select * from account" & Chr(10)
        sqlString = sqlString & "where username = '" & Session("UserAuthentication") & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lblnamaakun.Text = myReader("nama").ToString
            access = myReader("access").ToString

            If access = "1" Then
                lblacc.Text = "Admin"
                placeholder_admin.Visible = "true"
            ElseIf access = "2" Then
                lblacc.Text = "Supervisor"
                placeholder_admin.Visible = "true"
            ElseIf access = "3" Then
                lblacc.Text = "Karyawan"
            Else
                lblacc.Text = "ERROR"
            End If
        End If
    End Sub
    Private Sub lnkLogout_ServerClick(sender As Object, e As System.EventArgs) Handles lnkLogout.ServerClick
        Session("BlnLoggedIn") = False
        Session("UserAuthentication") = ""
        Response.Write("<script> window.open('../index.html', '_self'); </script>")
    End Sub


    Private Sub BindData()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from account" & Chr(10)
            sqlString = sqlString & "order by id desc"
            Using cmd As New SqlCommand(sqlString)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataSet
                        sda.Fill(dt)
                        Dim Pds1 As New PagedDataSource()
                        Pds1.DataSource = dt.Tables(0).DefaultView
                        Pds1.AllowPaging = True
                        Pds1.PageSize = 10
                        Pds1.CurrentPageIndex = CurrentPage
                        lblPaging.Text = "Halaman: " & (CurrentPage + 1).ToString() & " dari " & Pds1.PageCount.ToString()
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage

                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub


    Public Property CurrentPage() As Integer
        Get
            Dim s1 As Object = Me.ViewState("CurrentPage")
            If s1 Is Nothing Then
                Return 0
            Else
                Return CInt(s1)
            End If
        End Get
        Set(ByVal value As Integer)
            Me.ViewState("CurrentPage") = value
        End Set
    End Property

    Private Sub btnNext_Click1(sender As Object, e As System.EventArgs) Handles btnNext.Click
        CurrentPage += 1
        BindData()
    End Sub

    Private Sub btnPrevious_Click1(sender As Object, e As System.EventArgs) Handles btnPrevious.Click
        CurrentPage -= 1
        BindData()
    End Sub
    Private Sub Messagebox(ByVal Message As String)
        Dim lblMessageBox As New Label()

        lblMessageBox.Text = "<script language='javascript'>" + Environment.NewLine & "window.alert('" & Message & "')</script>"
        Page.Controls.Add(lblMessageBox)
    End Sub

    Private Sub btnAkun_ServerClick(sender As Object, e As EventArgs) Handles btnAkun.ServerClick
        PlaceHolderAkunBTN.Visible = False
        PlaceHolderAkunBackBtn.Visible = True
        plhList.Visible = True
    End Sub
    Private Sub btnAkunKembali_ServerClick(sender As Object, e As EventArgs) Handles btnAkunKembali.ServerClick
        PlaceHolderAkunBTN.Visible = True
        PlaceHolderAkunBackBtn.Visible = False
        plhList.Visible = False
    End Sub
    Private Sub btnBatal_ServerClick(sender As Object, e As System.EventArgs) Handles btnBatal.ServerClick
        plhList.Visible = True
        plhRinci.Visible = False
    End Sub
    Private Sub btnSimpan_ServerClick(sender As Object, e As System.EventArgs) Handles btnSimpan.ServerClick


        If txtIdUserRinci.Text = "" Then
            Messagebox("Isi Nama Barang!")
            txtIdUserRinci.Focus()
            Return
        End If
        If txtUsernameRinci.Text = "" Then
            Messagebox("Isi Harga Barang !")
            txtUsernameRinci.Focus()
            Return
        End If
        If txtnama.Text = "" Then
            Messagebox("Isi Modal Barang!")
            txtnama.Focus()
            Return
        End If

        Call UbahData()
        plhList.Visible = True
        plhRinci.Visible = False
        PlaceHolderAkunBackBtn.Visible = True
        Call BindData()


    End Sub
    Private Sub UbahData()
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from account" & Chr(10)
        sqlString = sqlString & "where id = '" & txtIdUserRinci.Text & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_akun", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", txtIdUserRinci.Text)
            cmd.Parameters.AddWithValue("@f_username", txtUsernameRinci.Text)
            cmd.Parameters.AddWithValue("@f_nama", txtnama.Text)
            cmd.Parameters.AddWithValue("@f_access", DropDownAccess.SelectedValue)
            cmd.Parameters.AddWithValue("@f_password", pw2.Text)
            cmd.Parameters.AddWithValue("@f_activation", "1")
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try

        If DropDownAccess.SelectedValue = 2 Then
            Try
                Dim cmd As SqlCommand = New SqlCommand("up_apl_insert_supplier", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@f_nama_supplier", "New Account")
                cmd.Parameters.AddWithValue("@f_alamat", "New Account")
                cmd.Parameters.AddWithValue("@f_kota", "New Account")
                cmd.Parameters.AddWithValue("@f_negara", "New Account")
                cmd.Parameters.AddWithValue("@f_provinsi", "New Account")
                cmd.Parameters.AddWithValue("@f_kode_pos", "New Account")
                cmd.Parameters.AddWithValue("@f_telepon", "New Account")
                cmd.Parameters.AddWithValue("@f_register", 0)
                cmd.Parameters.AddWithValue("@f_id_barang", 0)
                cmd.Parameters.AddWithValue("@f_nama_barang", "NEW ACCOUNT")
                cmd.Parameters.AddWithValue("@f_id_user", txtIdUserRinci.Text)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Catch ex As Exception
                Console.WriteLine("An Error Occurred : '{0}'", ex)
            End Try
        End If
        Response.Write("<script>alert('Data Telah Diubah')</script>")
    End Sub


    Protected Sub btnHapusData(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String
        lblnomoragrument = commandArgument
        Dim cmd As SqlCommand = New SqlCommand("up_apl_delete_account", con)
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandTimeout = 0
        cmd.Parameters.AddWithValue("@f_id", lblnomoragrument)
        cmd.ExecuteNonQuery()
        Messagebox("Data Telah Dihapus.")
        cmd.Dispose()

        Call BindData()
    End Sub

    Protected Sub UbahData(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim lblnomoragrument As String = nomor.CommandArgument
        lblNomor.Text = "1"

        PlaceHolderTitleEdit.Visible = True
        PlaceHolderAkunBackBtn.Visible = False
        PH_Access.Visible = True

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from account" & Chr(10)
        sqlString = sqlString & "where id = '" & lblnomoragrument & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then

            plhList.Visible = False
            plhRinci.Visible = True
            txtIdUserRinci.Text = myReader("id").ToString
            txtUsernameRinci.Text = myReader("username").ToString
            txtnama.Text = myReader("nama").ToString
            If myReader("access") = 0 Then
                DropDownAccess.SelectedValue = 0
            ElseIf myReader("access") = 2 Then
                DropDownAccess.SelectedValue = 2
            ElseIf myReader("access") = 3 Then
                DropDownAccess.SelectedValue = 2
            End If
            pw.Text = myReader("pass").ToString
            pw2.Text = myReader("pass").ToString
            If myReader("activation") = 0 Then
                DropDownAccess.Enabled = True
            Else
                DropDownAccess.Enabled = False
            End If
        End If

    End Sub



    Protected Sub HapusData(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim status2 As String
        Dim lblnomoragrument As String

        lblnomoragrument = commandArgument

        myConnection2 = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection2.Open()

        sqlString2 = "select * from account" & Chr(10)
        sqlString2 = sqlString2 & "where username = '" & Session("UserAuthentication") & "'"

        myCommand2 = New SqlCommand(sqlString2, myConnection2)
        myCommand2.CommandTimeout = 0

        myReader2 = myCommand2.ExecuteReader()
        If myReader2.Read = True Then

            status2 = myReader2("access").ToString
            If status2 = "1" Then
                Call hapusbarang(lblnomoragrument)
            ElseIf status2 = "2" Then
                Call hapusbarang(lblnomoragrument)
            Else
                Response.Write("<script>alert('Anda Harus Menjadi admin untuk melakukan hal itu')</script>")
            End If
        End If

    End Sub


    Protected Sub hapusbarang(lblnomoragrument)

        Dim cmd As SqlCommand = New SqlCommand("up_apl_delete_akun", con)
        cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandTimeout = 0

            cmd.Parameters.AddWithValue("@f_id", lblnomoragrument)
            cmd.ExecuteNonQuery()
            Messagebox("Data Telah Dihapus.")
            cmd.Dispose()

        Call BindData()
    End Sub
End Class
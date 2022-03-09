Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.IO
Public Class inventory
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
            sqlString = "select * from barang" & Chr(10)
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


    Protected Sub UbahRincicek(ByVal sender As Object, ByVal e As EventArgs)
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
            lblaccdebug.Text = status2
            If status2 = "1" Then
                Call UbahRinci(lblnomoragrument)
            ElseIf status2 = "2" Then
                Call UbahRinci(lblnomoragrument)
            Else
                Response.Write("<script>alert('Anda Harus Menjadi admin untuk melakukan hal itu')</script>")
            End If
        End If

    End Sub

    Protected Sub UbahRinci(lblnomoragrument)
        Dim lblid As String

        lblid = lblnomoragrument
        lblNomor.Text = "1"
        lblNamaBarangEdit.Text = txtNamaBarangRinci.Text

        PlaceHolderTitleEdit.Visible = True
        PlaceHolderTitleBaru.Visible = False
        PlaceHolderCariBTN.Visible = False
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where id = '" & lblid & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then

            plhList.Visible = False
            plhRinci.Visible = True
            txtIdBarangRinci.Text = myReader("id").ToString
            txtNamaBarangRinci.Text = myReader("nama").ToString
            If myReader("jenis") = 1 Then
                DropDownJenis.SelectedValue = 1
            ElseIf myReader("jenis") = 2 Then
                DropDownJenis.SelectedValue = 2
            End If
            txtHargaBarangRinci.Text = myReader("harga").ToString
            txtModalBarangRinci.Text = myReader("modal").ToString
            lblNamaBarangEdit.Text = txtNamaBarangRinci.Text
        End If

    End Sub


    Private Sub btnTambah_ServerClick(sender As Object, e As System.EventArgs) Handles btnTambah.ServerClick
        PlaceHolderCariBTN.Visible = False
        PlaceHolderTitleEdit.Visible = False
        PlaceHolderTitleBaru.Visible = True
        lblNomor.Text = "0"
        plhList.Visible = False
        plhRinci.Visible = True
        txtNamaBarangRinci.Focus()
        txtIdBarangRinci.Text = "ID Barang Baru"
        txtNamaBarangRinci.Text = String.Empty
        txtHargaBarangRinci.Text = String.Empty
        lblimageuploaded.Text = "0"
    End Sub

    Private Sub btnBatal_ServerClick(sender As Object, e As System.EventArgs) Handles btnBatal.ServerClick
        plhList.Visible = True
        plhRinci.Visible = False
        PlaceHolderCariBTN.Visible = True
    End Sub

    Private Sub Messagebox(ByVal Message As String)
        Dim lblMessageBox As New Label()

        lblMessageBox.Text = "<script language='javascript'>" + Environment.NewLine & "window.alert('" & Message & "')</script>"
        Page.Controls.Add(lblMessageBox)
    End Sub

    Private Sub btnSimpan_ServerClick(sender As Object, e As System.EventArgs) Handles btnSimpan.ServerClick


        If txtNamaBarangRinci.Text = "" Then
            Messagebox("Isi Nama Barang!")
            txtNamaBarangRinci.Focus()
            Return
        End If
        If txtHargaBarangRinci.Text = "" Then
            Messagebox("Isi Harga Barang !")
            txtHargaBarangRinci.Focus()
            Return
        End If
        If txtModalBarangRinci.Text = "" Then
            Messagebox("Isi Modal Barang!")
            txtModalBarangRinci.Focus()
            Return
        End If
        If DropDownJenis.SelectedValue = 0 Then
            Messagebox("Pilih Jenis barang!")
            DropDownJenis.Focus()
            Return
        End If
        Call cekgambar()
        If lblimageuploaded.text = "0" Then
            Messagebox("Gambar belum Di upload!")
            FileUpload1.Focus()
            Return
        End If

        'this section for uploading image and insert it into variable so u can upload it onto the database


        If lblNomor.Text = "0" Then
            Call SimpanData()
        Else
            Call UbahData()
        End If
        plhList.Visible = True
        plhRinci.Visible = False

        Call BindData()


    End Sub

    Private Sub cekgambar()
        Dim gambar As String
        'Upload File to folder
        Dim folderPath As String = Server.MapPath("~/uploads/")
        'Check whether Directory (Folder) exists.
        If Not Directory.Exists(folderPath) Then
            'If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath)
        End If
        Dim strpath As String = System.IO.Path.GetExtension(FileUpload1.FileName)
        If strpath <> ".jpg" AndAlso strpath <> ".jpeg" AndAlso strpath <> ".gif" AndAlso strpath <> ".png" Then
            lblExtension.ForeColor = System.Drawing.Color.Red
            lblExtension.Text = "Only image formats (jpg, png, gif) are accepted"
            lblimageuploaded.Text = "0"
            Return
        Else
            lblimageuploaded.Text = "1"
            FileUpload1.SaveAs(folderPath & "(1)" + Path.GetFileName(FileUpload1.FileName))
        End If
    End Sub

    Private Sub SimpanData()
        'uploadgambar
        Dim gambar As String
        'Upload File to folder
        Dim folderPath As String = Server.MapPath("~/uploads/")
        'Check whether Directory (Folder) exists.
        If Not Directory.Exists(folderPath) Then
            'If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath)
        End If
        Dim strpath As String = System.IO.Path.GetExtension(FileUpload1.FileName)
        gambar = "~/uploads/(1)" + FileUpload1.FileName
        FileUpload1.SaveAs(folderPath & "(1)" + Path.GetFileName(FileUpload1.FileName))
        Dim unit As Integer
        unit = 0
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_insert_barang", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_nama", txtNamaBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_jenis", DropDownJenis.SelectedValue)
            cmd.Parameters.AddWithValue("@f_harga", txtHargaBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_modal", txtModalBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_unit", unit)
            cmd.Parameters.AddWithValue("@f_gambar", gambar)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
        Response.Write("<script>alert('Data Telah Ditambah')</script>")
    End Sub

    Private Sub deleteimage()
        Dim idgambar As String
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where id = '" & txtIdBarangRinci.Text & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            idgambar = myReader("gambar")
            File.Delete(Server.MapPath(String.Format(idgambar)))
        End If
    End Sub

    Private Sub UbahData()
        Call deleteimage()
        Dim gambar As String
        'Upload File to folder
        Dim folderPath As String = Server.MapPath("~/uploads/")
        'Check whether Directory (Folder) exists.
        If Not Directory.Exists(folderPath) Then
            'If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath)
        End If
        Dim strpath As String = System.IO.Path.GetExtension(FileUpload1.FileName)
        gambar = "~/uploads/(1)" + FileUpload1.FileName
        FileUpload1.SaveAs(folderPath & "(1)" + Path.GetFileName(FileUpload1.FileName))
        Dim unit As Integer
        unit = 0
        lblimageuploaded.Text = "0"
        Dim setunit As Integer
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where id = '" & txtIdBarangRinci.Text & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            setunit = myReader("unit")
        End If

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_barang", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", txtIdBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_nama", txtNamaBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_jenis", DropDownJenis.SelectedValue)
            cmd.Parameters.AddWithValue("@f_harga", txtHargaBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_modal", txtModalBarangRinci.Text)
            cmd.Parameters.AddWithValue("@f_unit", setunit)
            cmd.Parameters.AddWithValue("@f_gambar", gambar)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
        Response.Write("<script>alert('Data Telah Diubah')</script>")
    End Sub


    Protected Sub btnHapusData_Click(ByVal sender As Object, ByVal e As EventArgs)
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
            lblaccdebug.Text = status2
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
        Dim lblid As String
        Call deleteimage()
        lblid = lblnomoragrument
        Dim setunit As Integer
        'check if ada unit
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where id = '" & lblnomoragrument & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            setunit = myReader("unit")
        End If
        If setunit <= 0 Then
            Dim cmd As SqlCommand = New SqlCommand("up_apl_delete_barang", con)
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandTimeout = 0

            cmd.Parameters.AddWithValue("@f_id", lblnomoragrument)
            cmd.ExecuteNonQuery()
            Messagebox("Data Telah Dihapus.")
            cmd.Dispose()

            Call BindData()
        End If
        If setunit >= 1 Then
            Response.Write("<script>alert('Data Barang ini masih mempunyai unit maka tidak bisa dihapus. mohon hapus laporan terlebih dahulu.')</script>")
        End If

    End Sub

    Protected Sub btnCariGo_Click(sender As Object, e As EventArgs)
        Call CariData()
    End Sub
    Protected Sub btnlessunit_Click(sender As Object, e As EventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from barang" & Chr(10)
            sqlString = sqlString & "where unit < 101" & Chr(10)
            sqlString = sqlString & "order by id asc"
            Using cmd As New SqlCommand(sqlString)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataSet
                        sda.Fill(dt)
                        Dim Pds1 As New PagedDataSource()
                        Pds1.DataSource = dt.Tables(0).DefaultView
                        Pds1.AllowPaging = True
                        Pds1.PageSize = 999
                        Pds1.CurrentPageIndex = CurrentPage
                        lblPaging.Text = "Showing : Unit Less than 100"
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage
                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub CariData()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from barang" & Chr(10)
            sqlString = sqlString & "where id is not null" & Chr(10)
            sqlString = sqlString & "and nama like '%" & txtCariID.Value & "%'" & Chr(10)
            sqlString = sqlString & "order by id asc"
            Using cmd As New SqlCommand(sqlString)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataSet
                        sda.Fill(dt)
                        Dim Pds1 As New PagedDataSource()
                        Pds1.DataSource = dt.Tables(0).DefaultView
                        Pds1.AllowPaging = True
                        Pds1.PageSize = 999
                        Pds1.CurrentPageIndex = CurrentPage
                        lblPaging.Text = "Showing Barang dengan nama : " & txtCariID.Value
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage

                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnCari_ServerClick(sender As Object, e As EventArgs) Handles btnCari.ServerClick
        PlaceHolderCari.Visible = True
        PlaceHolderCariBTN.Visible = False
        PlaceHolderCariBackBtn.Visible = True
    End Sub

    Private Sub btnKembali_ServerClick(sender As Object, e As EventArgs) Handles btnKembali.ServerClick
        PlaceHolderCari.Visible = False
        PlaceHolderCariBTN.Visible = True
        PlaceHolderCariBackBtn.Visible = False
        Call BindData()
    End Sub


End Class
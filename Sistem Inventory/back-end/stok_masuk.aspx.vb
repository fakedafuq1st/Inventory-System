Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.IO
Public Class stok_masuk
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
                Response.Redirect("../index.html")
            Else
                Call InfoUser()
                Call BindData()

            End If
        End If
    End Sub


    Protected Sub InfoUser()
        Dim status As String
        Dim ID As Integer

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "Select * from tbl_account" & Chr(10)
        sqlString = sqlString & "where username = '" & Session("UserAuthentication") & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            status = myReader("status").ToString

            If status = "1" Then
                lblacc.Text = "ADMIN"
                PlaceHolderTambahAccess.Visible = True



            ElseIf status = "2" Then
                lblacc.Text = "KARYAWAN"
                PlaceHolderTambahAccess.Visible = True


            ElseIf status = "3" Then
                lblacc.Text = "KARYAWAN"
                PlaceHolderTambahAccess.Visible = False


            Else
                lblacc.Text = "ERROR"

            End If
        End If


    End Sub


    Private Sub BindData()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stok_in" & Chr(10)
            sqlString = sqlString & "order by id_stok asc"
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
                        lblPaging.Text = "Showing Page: " & (CurrentPage + 1).ToString() & " of " & Pds1.PageCount.ToString()
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


    Private Sub lnkLogout_ServerClick(sender As Object, e As System.EventArgs) Handles lnkLogout.ServerClick
        Session("BlnLoggedIn") = False
        Session("UserAuthentication") = ""
        Response.Write("<script> window.open('../index.html', '_self'); </script>")
    End Sub

    Protected Sub UbahRinci(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String
        Dim nama As String
        'Dim totaltemp As Integer

        lblnomoragrument = commandArgument
        If lblNomor.Text = "2" Then
            Response.Write("<script>alert('Silahkan tutup menu Detail untuk melakukan mengeditan data.')</script>")
        Else
            lblNomor.Text = "1"
            PlaceHolderTitleEdit.Visible = True
            PlaceHolderTitleBaru.Visible = False

            myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
            myConnection.Open()

            sqlString = "select * from stok_in" & Chr(10)
            sqlString = sqlString & "where id_stok = '" & lblnomoragrument & "'"

            myCommand = New SqlCommand(sqlString, myConnection)
            myCommand.CommandTimeout = 0

            myReader = myCommand.ExecuteReader()
            If myReader.Read = True Then
                lblIdStok.Text = myReader("id_stok").ToString
                plhList.Visible = False
                plhRinci.Visible = True
                PlaceHolderDetails.Visible = False
                txtIdStokRinci.Text = myReader("id_stok").ToString
                txtIdBarangRinci.Value = myReader("id_barang").ToString
                txtUnitRinci.Value = myReader("unit").ToString
                txtNamaLengkapRinci.Value = myReader("username").ToString
                txtKeteranganRinci.Value = myReader("keterangan").ToString
                'Call totalEQ(totaltemp)
                'txtTotalRinci.Value = totaltemp
            End If
        End If

    End Sub


    Private Sub btnTambah_ServerClick(sender As Object, e As System.EventArgs) Handles btnTambah.ServerClick
        PlaceHolderTitleEdit.Visible = False
        PlaceHolderTitleBaru.Visible = True
        PlaceHolderDetails.Visible = False
        lblNomor.Text = "0"
        plhList.Visible = False
        plhRinci.Visible = True
        PlaceHolderNama.Visible = False
        'txtNamaBarangRinci.Focus()
        txtIdStokRinci.Text = "ID stok Baru"
        txtIdBarangRinci.Value = String.Empty
        txtUnitRinci.Value = String.Empty
        txtKeteranganRinci.Value = String.Empty
    End Sub

    Private Sub btnBatal_ServerClick(sender As Object, e As System.EventArgs) Handles btnBatal.ServerClick
        plhList.Visible = True
        plhRinci.Visible = False
    End Sub

    Private Sub Messagebox(ByVal Message As String)
        Dim lblMessageBox As New Label()

        lblMessageBox.Text = "<script language='javascript'>" + Environment.NewLine & "window.alert('" & Message & "')</script>"
        Page.Controls.Add(lblMessageBox)
    End Sub

    Protected Sub LihatDetail(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String
        Dim nama As String
        'Dim totaltemp As Integer
        lblnomoragrument = commandArgument

        PlaceHolderDetailTitle.Visible = True
        PlaceHolderTambahAccess.Visible = False

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from stok_in" & Chr(10)
        sqlString = sqlString & "where id_stok = '" & lblnomoragrument & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lblNomor.Text = "2"
            plhRinci.Visible = False
            PlaceHolderDetails.Visible = True
            lblIdLaporanDetailjudul.Text = myReader("id_stok").ToString
            txtIdStokDetail.Text = myReader("id_stok").ToString
            txtIdBarangDetail.Text = myReader("id_barang").ToString
            txtUnitDetail.Value = myReader("unit").ToString
            txtKeteranganDetail.Value = myReader("keterangan").ToString
            txtNamaPembuatdetail.Value = myReader("nama_user").ToString

        End If

    End Sub

    Private Sub btnSimpan_ServerClick(sender As Object, e As System.EventArgs) Handles btnSimpan.ServerClick


        If txtIdBarangRinci.Value = "" Then
            Messagebox("Isi ID Barang!")
            txtIdBarangRinci.Focus()
            Return
        End If
        If txtUnitRinci.Value = "" Then
            Messagebox("Isi Unit Barang !")
            txtUnitRinci.Focus()
            Return
        End If

        If txtKeteranganRinci.Value = "" Then
            Messagebox("Isi Keterangan!")
            txtKeteranganRinci.Focus()
            Return
        End If

        If lblNomor.Text = "0" Then
            Call SimpanData()
        Else
            Call UbahData()
        End If
        plhList.Visible = True
        plhRinci.Visible = False

        Call BindData()

    End Sub

    Private Sub SimpanData()
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_insert_stok_in", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_tanggal", DateTime.Now.ToString("ddd dd/MM/yyyy HH:mm:ss"))
            cmd.Parameters.AddWithValue("@f_id_barang", txtIdBarangRinci.Value)
            cmd.Parameters.AddWithValue("@f_unit", txtUnitRinci.Value)
            cmd.Parameters.AddWithValue("@f_keterangan", txtKeteranganRinci.Value)

            Call InfoUser()
            'cmd.Parameters.AddWithValue("@f_nama", lblNamaUser.Text)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
    End Sub

    Private Sub UbahData()

        Dim datecurrent = DateTime.Now.ToString("ddd dd/MM/yyyy HH:mm:ss")

        Dim v As String = "Updated : " + datecurrent
        txtKeteranganRinci.Value += v

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_stok_in", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id_barang", txtIdBarangRinci.Value)
            cmd.Parameters.AddWithValue("@f_unit", txtUnitRinci.Value)
            cmd.Parameters.AddWithValue("@f_keterangan", txtKeteranganRinci.Value)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
    End Sub


    Protected Sub btnHapusData_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String

        lblnomoragrument = commandArgument

        Dim lblid As String

        lblid = lblnomoragrument
        Dim cmd As SqlCommand = New SqlCommand("up_apl_delete_stok_in", con)
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandTimeout = 0

        cmd.Parameters.AddWithValue("@f_id_stok", lblnomoragrument)
        cmd.ExecuteNonQuery()
        Messagebox("Data Telah Dihapus.")
        cmd.Dispose()

        Call BindData()

    End Sub



    Protected Sub btnCariGo_Click(sender As Object, e As EventArgs)
        Call CariData()
    End Sub

    Private Sub CariData()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stok_in" & Chr(10)
            sqlString = sqlString & "where id_stok is not null" & Chr(10)

            If txtCariID.Value <> "" Then
                sqlString = sqlString & "and id_stok = '" & txtCariID.Value & "'" & Chr(10)
            End If

            sqlString = sqlString & "order by id_stok asc"
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
                        lblPaging.Text = "Showing Page: " & (CurrentPage + 1).ToString() & " of " & Pds1.PageCount.ToString()
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



    Private Sub btnDetailKembali_ServerClick(sender As Object, e As EventArgs) Handles btnDetailKembali.ServerClick
        PlaceHolderDetailTitle.Visible = False
        PlaceHolderTambahAccess.Visible = True
        plhRinci.Visible = False
        PlaceHolderDetails.Visible = False
        lblNomor.Text = 0
        Call BindData()
    End Sub
End Class
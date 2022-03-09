Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Configuration
Imports System.Web.UI.DataVisualization.Charting
Imports System.Web.Services
Imports System.Web.Script.Services

Public Class supplier
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
            Else
                Call InfoUser()
                Call BindData()
            End If
        End If

        'check season
        If Session("page") = 0 Then
            'barang and rekap both disabled

            ''page part
            ph_barang.Visible = False
            ph_rekap.Visible = False
            ph_detail.Visible = False
            ''end page part

            ''button part
            ph_back.Visible = False
            ph_menu.Visible = True
            ''end button part


        ElseIf Session("page") = 1 Then

            'barang enabled
            'rekap disabled

            ''page part
            ph_barang.Visible = True
            ph_rekap.Visible = False
            ph_detail.Visible = False
            ''end of page part

            'button part
            ph_back.Visible = True
            ph_menu.Visible = False
            'end button part

        ElseIf Session("page") = 2 Then
            'barang disabled
            'rekap enabled

            ''page part
            ph_barang.Visible = False
            ph_rekap.Visible = True
            ph_detail.Visible = False
            ''end of page part

            ph_back.Visible = True
            ph_menu.Visible = False
        Else
            'both disabled on default.

            ''page part
            ph_barang.Visible = False
            ph_rekap.Visible = False
            ph_detail.Visible = False
            ''end page part

            ''button part
            ph_back.Visible = False
            ph_menu.Visible = True
            ''end button part

        End If
    End Sub


    Private Sub Messagebox(ByVal Message As String)
        Dim lblMessageBox As New Label()

        lblMessageBox.Text = "<script language='javascript'>" + Environment.NewLine & "window.alert('" & Message & "')</script>"
        Page.Controls.Add(lblMessageBox)
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
            Session("id") = myReader("id")
            If access = "1" Then
                lblacc.Text = "Admin"
            ElseIf access = "2" Then
                lblacc.Text = "Supplier"
            ElseIf access = "3" Then
                lblacc.Text = "Karyawan"
            Else
                lblacc.Text = "ERROR"
            End If
        End If
    End Sub
    '---------========== BUTTON FUNCTION ==========---------'
    Private Sub btnBarang_ServerClick(sender As Object, e As EventArgs) Handles btnBarang.ServerClick
        Session("page") = 1
        ph_back.Visible = True
        ph_menu.Visible = False
        ph_barang.Visible = True
        ph_rekap.Visible = False
        ph_detail.Visible = False
    End Sub

    Private Sub btnRekap_ServerClick(sender As Object, e As EventArgs) Handles btnRekap.ServerClick
        Session("page") = 2
        ph_back.Visible = True
        ph_menu.Visible = False
        ph_barang.Visible = False
        ph_rekap.Visible = True
        ph_detail.Visible = False
    End Sub

    Private Sub btnback_ServerClick(sender As Object, e As EventArgs) Handles btnback.ServerClick
        Session("page") = 0
        ph_back.Visible = False
        ph_menu.Visible = True
        ph_barang.Visible = False
        ph_rekap.Visible = False
        ph_detail.Visible = False
    End Sub
    '---------========== END OF BUTTON FUNCTION ==========---------'

    Private Sub BindData()
        'Barang
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from barang_2" & Chr(10)
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
        'Rekap

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

    Public Property CurrentPage2() As Integer
        Get
            Dim s2 As Object = Me.ViewState("CurrentPage2")
            If s2 Is Nothing Then
                Return 0
            Else
                Return CInt(s2)
            End If
        End Get
        Set(ByVal value As Integer)
            Me.ViewState("CurrentPage2") = value
        End Set
    End Property

    Private Sub btnNext2_Click1(sender As Object, e As System.EventArgs) Handles btnNext.Click
        CurrentPage2 += 1
        BindData()
    End Sub

    Private Sub btnPrevious2_Click1(sender As Object, e As System.EventArgs) Handles btnPrevious.Click
        CurrentPage2 -= 1
        BindData()
    End Sub

    Private Sub btnTambah_ServerClick(sender As Object, e As System.EventArgs) Handles btnTambah.ServerClick
        ''page part
        ph_barang.Visible = False
        ph_rekap.Visible = False
        ph_detail.Visible = True
        ''end page part

        ''button part
        ph_back.Visible = False
        ph_menu.Visible = False
        ''end button part

        lblNomor.Text = "0"

        'default input
        txtIdBarang.Text = "Id Barang Baru"
        txtNamaBarang.Text = ""
        txtJenis.Text = ""
        txtTipe.Text = ""
        txtSize.Text = ""
        txtQuantity.Text = ""

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from supplier" & Chr(10)
        sqlString = sqlString & "where id_user = '" & Session("id") & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            txtUser.Text = myReader("nama_supplier")
        End If
    End Sub
    Protected Sub Ubah_barang(ByVal sender As Object, ByVal e As EventArgs)
        Dim nama As String
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String

        lblnomoragrument = commandArgument
        Dim lblid As String

        lblid = lblnomoragrument
        lblNomor.Text = "1"

        ph_detail.Visible = True
        ph_back.Visible = False
        ph_barang.Visible = False

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from barang_2" & Chr(10)
        sqlString = sqlString & "where id = '" & lblid & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            txtIdBarang.Text = myReader("id").ToString
            txtNamaBarang.Text = myReader("nama").ToString
            txtJenis.Text = myReader("jenis").ToString
            txtTipe.Text = myReader("tipe").ToString
            txtSize.Text = myReader("size").ToString
            txtQuantity.Text = myReader("quantity")
            txtUser.Text = myReader("from_user")
        End If
    End Sub

    Private Sub btnSimpan_ServerClick(sender As Object, e As System.EventArgs) Handles btnSimpan.ServerClick


        If txtNamaBarang.Text = "" Then
            Messagebox("Isi Nama Barang!")
            txtNamaBarang.Focus()
            Return
        End If
        If txtJenis.Text = "" Then
            Messagebox("Isi Jenis Barang !")
            txtJenis.Focus()
            Return
        End If
        If txtTipe.Text = "" Then
            Messagebox("Isi Tipe Barang!")
            txtTipe.Focus()
            Return
        End If
        If txtSize.Text = "" Then
            Messagebox("Isi Ukuran barang!")
            txtSize.Focus()
            Return
        End If
        If txtQuantity.Text = 0 Then
            Messagebox("Isi Banyak Barang!")
            txtQuantity.Focus()
            Return
        End If

        'this section for uploading image and insert it into variable so u can upload it onto the database


        If lblNomor.Text = "0" Then
            Call SimpanData()
        Else
            Call UbahData()
        End If
        ph_detail.Visible = False
        ph_back.Visible = True
        ph_barang.Visible = True

        Call BindData()

    End Sub

    Private Sub btnBatal_ServerClick(sender As Object, e As EventArgs) Handles btnBatal.ServerClick
        ph_detail.Visible = False
        ph_back.Visible = True
        ph_barang.Visible = True
    End Sub
    Private Sub SimpanData()
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_insert_barang2", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_nama", txtNamaBarang.Text)
            cmd.Parameters.AddWithValue("@f_jenis", txtJenis.Text)
            cmd.Parameters.AddWithValue("@f_tipe", txtTipe.Text)
            cmd.Parameters.AddWithValue("@f_size", txtSize.Text)
            cmd.Parameters.AddWithValue("@f_quantity", txtQuantity.Text)
            cmd.Parameters.AddWithValue("@f_user", txtUser.Text)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
        Response.Write("<script>alert('Data Telah Ditambah')</script>")
    End Sub

    Protected Sub hapus_barang(ByVal sender As Object, ByVal e As EventArgs)
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String

        lblnomoragrument = commandArgument

        Dim lblid As String
        lblid = lblnomoragrument
        Dim cmd As SqlCommand = New SqlCommand("up_apl_delete_barang2", con)
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandTimeout = 0

        cmd.Parameters.AddWithValue("@f_id", lblnomoragrument)
        cmd.ExecuteNonQuery()
        Messagebox("Data Telah Dihapus.")
        cmd.Dispose()

        Call BindData()
    End Sub

    Private Sub UbahData()
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_barang2", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", txtIdBarang.Text)
            cmd.Parameters.AddWithValue("@f_nama", txtNamaBarang.Text)
            cmd.Parameters.AddWithValue("@f_jenis", txtJenis.Text)
            cmd.Parameters.AddWithValue("@f_tipe", txtTipe.Text)
            cmd.Parameters.AddWithValue("@f_size", txtSize.Text)
            cmd.Parameters.AddWithValue("@f_quantity", txtQuantity.Text)
            cmd.Parameters.AddWithValue("@f_user", txtUser.Text)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
        Response.Write("<script>alert('Data Telah Diubah')</script>")
    End Sub

    Private Sub btnCariGo_Click(sender As Object, e As EventArgs) Handles btnCariGo.Click
        'dim bulan and tanggal
        Dim bulan, tahun, akhir_bulan As Integer
        Dim bulan_table As String
        Dim masuk, keluar, rusak, expired, dipesan As Integer
        bulan = Convert.ToInt32(DD_bulan.SelectedValue)

        If bulan = 1 Then
            bulan_table = "January"
            akhir_bulan = 31
        ElseIf bulan = 2 Then
            bulan_table = "Februari"
            akhir_bulan = 28
        ElseIf bulan = 3 Then
            bulan_table = "Maret"
            akhir_bulan = 31
        ElseIf bulan = 4 Then
            bulan_table = "April"
            akhir_bulan = 30
        ElseIf bulan = 5 Then
            bulan_table = "Mei"
            akhir_bulan = 31
        ElseIf bulan = 6 Then
            bulan_table = "Juni"
            akhir_bulan = 30
        ElseIf bulan = 7 Then
            bulan_table = "Juli"
            akhir_bulan = 31
        ElseIf bulan = 8 Then
            bulan_table = "Agustus"
            akhir_bulan = 31
        ElseIf bulan = 9 Then
            bulan_table = "September"
            akhir_bulan = 30
        ElseIf bulan = 10 Then
            bulan_table = "Oktober"
            akhir_bulan = 31
        ElseIf bulan = 11 Then
            bulan_table = "November"
            akhir_bulan = 30
        ElseIf bulan = 12 Then
            bulan_table = "Desember"
            akhir_bulan = 31
        Else
            bulan_table = "Pilih Bulan"
        End If

        tahun = Convert.ToInt32(DD_tahun.SelectedValue)

        ''Get Data

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select sum(unit) as unit_total from stock" & Chr(10)
        sqlString = sqlString & "where Jenis = 'Barang Masuk' and Tanggal BETWEEN '" & tahun & "-" & bulan & "-01' AND '" & tahun & "-" & bulan & "-" & akhir_bulan & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            If myReader("unit_total") Is DBNull.Value Then
                masuk = 0
            Else
                masuk = myReader("unit_total")
            End If

        End If

        sqlString = ""
        sqlString = "select sum(unit) as unit_total from stock" & Chr(10)
        sqlString = sqlString & "where Jenis = 'Barang Keluar' and Tanggal BETWEEN '" & tahun & "-" & bulan & "-01' AND '" & tahun & "-" & bulan & "-" & akhir_bulan & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then

            If myReader("unit_total") Is DBNull.Value Then
                keluar = 0
            Else
                keluar = myReader("unit_total")
            End If

        End If

        sqlString = ""
        sqlString = "select sum(unit) as unit_total from stock" & Chr(10)
        sqlString = sqlString & "where Jenis = 'Barang Tak Layak' and Tanggal BETWEEN '" & tahun & "-" & bulan & "-01' AND '" & tahun & "-" & bulan & "-" & akhir_bulan & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            If myReader("unit_total") Is DBNull.Value Then
                rusak = 0
            Else
                rusak = myReader("unit_total")
            End If

        End If

        sqlString = ""
        sqlString = "select sum(unit) as unit_total from stock" & Chr(10)
        sqlString = sqlString & "where Jenis = 'Barang Kadaluarsa' and Tanggal BETWEEN '" & tahun & "-" & bulan & "-01' AND '" & tahun & "-" & bulan & "-" & akhir_bulan & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then

            If myReader("unit_total") Is DBNull.Value Then
                expired = 0
            Else
                expired = myReader("unit_total")
            End If

        End If

        sqlString = ""
        sqlString = "select sum(unit) as unit_total from stock" & Chr(10)
        sqlString = sqlString & "where Jenis = 'Barang Sedang Dipesan' and Tanggal BETWEEN '" & tahun & "-" & bulan & "-01' AND '" & tahun & "-" & bulan & "-" & akhir_bulan & "'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()

        If myReader.Read = True Then
            If myReader("unit_total") Is DBNull.Value Then
                dipesan = 0
            Else
                dipesan = myReader("unit_total")
            End If
        End If


        'masuk kedalam table
        lblBulan.Text = bulan_table
        lblTahun.Text = tahun


        If masuk > 0 Then
            lblMasuk.Text = masuk
        Else
            lblMasuk.Text = "Tidak Ada Data"
        End If

        If keluar > 0 Then
            lblKeluar.Text = keluar
        Else
            lblKeluar.Text = "Tidak Ada Data"
        End If

        If rusak > 0 Then
            lblRusak.Text = rusak
        Else
            lblRusak.Text = "Tidak Ada Data"
        End If

        If expired > 0 Then
            lblExpired.Text = expired
        Else
            lblExpired.Text = "Tidak ada Data"
        End If

        If dipesan > 0 Then
            lblOrder.Text = dipesan
        Else
            lblOrder.Text = "Tidak Ada Data"
        End If
        '' END
    End Sub

    Private Sub lnkLogout_ServerClick(sender As Object, e As System.EventArgs) Handles lnkLogout.ServerClick
        Session("BlnLoggedIn") = False
        Session("UserAuthentication") = ""
        Response.Write("<script> window.open('../index.html', '_self'); </script>")
    End Sub

End Class
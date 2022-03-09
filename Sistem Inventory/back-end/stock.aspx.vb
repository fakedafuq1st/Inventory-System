Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.IO
Public Class stock
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

    Protected Sub OnCheckChanged(ByVal sender As Object, ByVal e As EventArgs)
        paneldiskon.Visible = EnableDiskon.Checked
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

    Private Sub BindData()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "order by id_stock desc"
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
                        lblPaging.Text = "Halaman: " & (CurrentPage + 1).ToString() & " Dari " & Pds1.PageCount.ToString()
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
        'Dim totaltemp As Integer

        lblnomoragrument = commandArgument
        If lblNomor.Text = "2" Then
            Response.Write("<script>alert('Silahkan tutup menu Detail untuk melakukan mengeditan data.')</script>")
        Else
            myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
            myConnection.Open()
            sqlString = "select * from barang" & Chr(10)
            myCommand = New SqlCommand(sqlString, myConnection)
            myCommand.CommandTimeout = 0

            DDDataBarang.DataSource = myCommand.ExecuteReader()
            DDDataBarang.DataTextField = "nama"
            DDDataBarang.DataValueField = "nama"
            DDDataBarang.DataBind()
            DDDataBarang.Items.Insert(0, New ListItem("--Pilih Barang--", "0"))
            DDDataBarang.Items.Insert(1, New ListItem("----------------", "0"))

            lblNomor.Text = "1"
            PlaceHolderTitleEdit.Visible = True
            PlaceHolderTitleBaru.Visible = False
            PlaceHolderCari.Visible = False
            PlaceHolderCariBTN.Visible = False
            PlaceHolderCariBackBtn.Visible = False
            DropDownJenis.Enabled = "False"

            myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
            myConnection.Open()

            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where id_stock = '" & lblnomoragrument & "'"

            myCommand = New SqlCommand(sqlString, myConnection)
            myCommand.CommandTimeout = 0

            myReader = myCommand.ExecuteReader()
            If myReader.Read = True Then
                lblIdStok.Text = myReader("id_stock").ToString
                plhList.Visible = False
                plhRinci.Visible = True
                PlaceHolderDetails.Visible = False
                txtDate.Text = myReader("tanggal").ToString
                txtIdStokRinci.Text = myReader("id_stock").ToString
                txtNamaLengkap.Text = myReader("nama_user")
                DDDataBarang.SelectedValue = myReader("nama_barang").ToString
                DropDownJenis.SelectedValue = myReader("jenis").ToString
                txtUnit.Text = myReader("unit").ToString
                txtKet.Text = myReader("keterangan").ToString
                If myReader("diskon") = True Then
                    EnableDiskon.Checked = True
                    txtDiskon.Text = myReader("diskon_ammount")
                ElseIf Myreader("diskon") = False Then
                    EnableDiskon.Checked = False
                    txtDiskon.Text = 0
                End If
            End If
            End If

    End Sub

    Private Sub btnTambah_ServerClick(sender As Object, e As System.EventArgs) Handles btnTambah.ServerClick
        PlaceHolderTitleEdit.Visible = False
        PlaceHolderTitleBaru.Visible = True
        PlaceHolderDetails.Visible = False
        PlaceHolderCariBTN.Visible = True
        lblNomor.Text = "0"
        txtUnit.Text = "0"
        txtDiskon.Text = "0"
        EnableDiskon.Checked = False
        plhList.Visible = False
        plhRinci.Visible = True
        DropDownJenis.Enabled = "true"
        txtNamaLengkap.Text = lblnamaakun.Text
        txtIdStokRinci.Text = "Laporan Baru"
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        sqlString = "select * from barang" & Chr(10)
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        DDDataBarang.DataSource = myCommand.ExecuteReader()
        DDDataBarang.DataTextField = "nama"
        DDDataBarang.DataValueField = "nama"
        DDDataBarang.DataBind()
        DDDataBarang.Items.Insert(0, New ListItem("--Pilih Barang--", "0"))
        DDDataBarang.Items.Insert(1, New ListItem("----------------", "0"))

        DropDownJenis.SelectedValue = 0
        txtUnit.Text = String.Empty
        txtKet.Text = String.Empty
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

    Protected Sub LihatDetail(ByVal sender As Object, ByVal e As EventArgs)
        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String
        'Dim totaltemp As Integer
        lblnomoragrument = commandArgument

        PlaceHolderDetailTitle.Visible = True
        PlaceHolderTambahAccess.Visible = False
        plhList.Visible = False
        PlaceHolderCariBTN.Visible = False
        PlaceHolderCariBackBtn.Visible = False

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from stock" & Chr(10)
        sqlString = sqlString & "where id_stock = '" & lblnomoragrument & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lblNomor.Text = "2"
            plhRinci.Visible = False
            PlaceHolderDetails.Visible = True
            lblIdLaporanDetailjudul.Text = myReader("id_stock").ToString
            txtIdDetail.Text = myReader("id_stock").ToString
            txtNamaUserDetail.Text = myReader("nama_user").ToString
            txtNamaBarangDetail.Text = myReader("nama_barang").ToString
            txtJenisDetail.Text = myReader("jenis").ToString
            txtUnitD.Text = myReader("unit").ToString
            txtKetDetail.Text = myReader("keterangan").ToString
            If myReader("diskon") = "true" Then
                txtDiskonDetail.Text = myReader("diskon_ammount").ToString
            Else
                txtDiskonDetail.Text = "Tidak ada / Tidak memakai Diskon"
            End If

        End If

    End Sub

    Private Sub checkunit(unitpass)
        If DropDownJenis.SelectedValue = "Barang Keluar" Then
            Dim namabarang, diskon1 As String
            Dim unitbarang As Integer
            Dim unit, database, hasil, profit, gross, profitdatabase, grossdatabase, diskon2 As Integer
            unit = Integer.Parse(txtUnit.Text)
            Dim id, jenis, harga, modal As Integer
            Dim nama, gambar As String

            namabarang = DDDataBarang.SelectedValue
            unitbarang = Convert.ToInt32(txtUnit.Text)
            'update barang
            ''ngambil data unit dari database barang
            unit = unitbarang
            myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
            myConnection.Open()
            sqlString = "select * from barang" & Chr(10)
            sqlString = sqlString & "where nama = '" & namabarang & "'"
            myCommand = New SqlCommand(sqlString, myConnection)
            myCommand.CommandTimeout = 0

            myReader = myCommand.ExecuteReader()
            If myReader.Read = True Then
                id = myReader("id")
                nama = myReader("nama")
                jenis = myReader("jenis")
                modal = myReader("modal")
                harga = myReader("harga")
                database = Integer.Parse(myReader("unit"))
                gambar = myReader("gambar")
            End If

            hasil = database - unit
            If hasil < 0 Then
                Messagebox("Unit Barang tidak cukup!")
                txtUnit.Focus()
                unitpass = "tidak"
                Return
            End If
        End If

    End Sub

    Private Sub btnSimpan_ServerClick(sender As Object, e As System.EventArgs) Handles btnSimpan.ServerClick
        Dim unitpass As String
        unitpass = "check"
        Dim database, unit As Integer
        unit = Convert.ToInt32(txtUnit.Text)
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where nama = '" & DDDataBarang.SelectedValue & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            database = Integer.Parse(myReader("unit"))
            If DropDownJenis.SelectedValue = "Barang Keluar" Then

                If database < unit Then
                    Messagebox("Unit Barang tidak cukup!")
                    Return
                End If
            End If
        End If

        'Cek Apakah Unit Tersedia untuk laporan keluar


        If DDDataBarang.SelectedValue = "" Then
            Messagebox("Isi Barang!")
            DDDataBarang.Focus()
            Return
        End If

        If DropDownJenis.SelectedValue = "" Then
            Messagebox("Pilih Jenis Laporan!")
            DropDownJenis.Focus()
            Return
        End If

        If txtUnit.Text = "" Then
            Messagebox("Isi Unit dengan benar!")
            txtUnit.Focus()
            Return
        End If

        If txtKet.Text = "" Then
            Messagebox("Isi Keterangan !")
            txtKet.Focus()
            Return
        End If
        Call checkunit(unitpass)
        If unitpass = "tidak" Then
            Messagebox("Unit Barang tidak cukup!")
            txtUnit.Focus()
            Return
        End If

        'check discount
        Dim diskon As Integer
        diskon = Convert.ToInt32(txtDiskon.Text)

        If diskon > 100 Or diskon < 0 Then
            Messagebox("Nilai diskon tidak sesuai.")
            txtDiskon.Focus()
            Return
        End If

        If lblNomor.Text = "0" Then
            Call SimpanData()
        Else
            Call UbahData()
        End If
        plhList.Visible = True
        plhRinci.Visible = False
        PlaceHolderCariBTN.Visible = True
        PlaceHolderCariBackBtn.Visible = False

        Call BindData()

    End Sub

    Private Sub SimpanData()
        'simpendata buat di parsing
        Dim namabarang, diskon1 As String
        Dim unitbarang As Integer
        Dim unit, database, hasil, profit, gross, profitdatabase, grossdatabase, diskon2 As Integer
        unit = Integer.Parse(txtUnit.Text)
        Dim id, jenis, harga, modal As Integer
        Dim nama, gambar As String

        namabarang = DDDataBarang.SelectedValue
        unitbarang = Convert.ToInt32(txtUnit.Text)
        'update barang
        ''ngambil data unit dari database barang
        unit = unitbarang
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where nama = '" & namabarang & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            id = myReader("id")
            nama = myReader("nama")
            jenis = myReader("jenis")
            modal = myReader("modal")
            harga = myReader("harga")
            database = Integer.Parse(myReader("unit"))
            gambar = myReader("gambar")
        End If

        'diskon
        If EnableDiskon.Checked = True Then
            diskon1 = "True"
            diskon2 = Convert.ToInt32(txtDiskon.Text)
        Else
            diskon1 = "False"
            diskon2 = 0
        End If
        'Simpan Data Laporan
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_insert_stock", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_tanggal", txtDate.Text)
            cmd.Parameters.AddWithValue("@f_barang", DDDataBarang.SelectedValue)
            cmd.Parameters.AddWithValue("@f_user", txtNamaLengkap.Text)
            cmd.Parameters.AddWithValue("@f_jenis", DropDownJenis.SelectedValue)
            cmd.Parameters.AddWithValue("@f_unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@f_keterangan", txtKet.Text)
            cmd.Parameters.AddWithValue("@f_diskon1", diskon1)
            cmd.Parameters.AddWithValue("@f_diskon2", diskon2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try


        'updating database barang

        If DropDownJenis.SelectedValue = "Barang Masuk" Then
            hasil = database + unit

        End If
        If DropDownJenis.SelectedValue = "Barang Keluar" Then
            hasil = database - unit
            If hasil < 0 Then
                Messagebox("Unit Barang tidak cukup!")
                txtUnit.Focus()
                Return
            End If
            profit = (harga * unit) - (modal * unit)
            gross = harga * unit
            myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
            myConnection.Open()
            sqlString = "select * from data" & Chr(10)
            sqlString = sqlString & "where id = '1'"
            myCommand = New SqlCommand(sqlString, myConnection)
            myCommand.CommandTimeout = 0
            myReader = myCommand.ExecuteReader()
            If myReader.Read = True Then
                profitdatabase = Integer.Parse(myReader("profit"))
                grossdatabase = Integer.Parse(myReader("gross_profit"))
            End If
            'check if diskon is enabled
            If EnableDiskon.Checked = True Then
                Dim diskonhasil, profitdiskon, grossdiskon, hasilprofitdiskon, hasilgrossdiskon As Integer
                diskonhasil = diskon2 * 0.01
                profitdiskon = profit * diskonhasil
                grossdiskon = gross * diskonhasil
                hasilprofitdiskon = profit - profitdiskon
                hasilgrossdiskon = gross - grossdiskon

                'put inside the database again
                profit = hasilprofitdiskon
                gross = hasilgrossdiskon
            End If

            profitdatabase += profit
            grossdatabase += gross
            Try
                Dim cmd As SqlCommand = New SqlCommand("up_apl_update_data", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@f_profit", profitdatabase)
                cmd.Parameters.AddWithValue("@f_gross_profit", grossdatabase)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Catch ex As Exception
                Console.WriteLine("An Error Occurred : '{0}'", ex)
            End Try
        End If

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_barang", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", id)
            cmd.Parameters.AddWithValue("@f_nama", nama)
            cmd.Parameters.AddWithValue("@f_jenis", jenis)
            cmd.Parameters.AddWithValue("@f_harga", harga)
            cmd.Parameters.AddWithValue("@f_modal", modal)
            cmd.Parameters.AddWithValue("@f_unit", hasil)
            cmd.Parameters.AddWithValue("@f_gambar", gambar)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try


        Messagebox("Data Telah Ditambah.")
    End Sub

    Private Sub resetdatabarang(lblnomoragrument)
        'get data laporan
        Dim unit As Integer
        Dim nama, jenis As String
        Dim unit_barang As Integer
        Dim reset As Integer
        Dim profit, gross, profitdatabase, grossdatabase, modal, harga As Integer
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        sqlString = "select * from stock" & Chr(10)
        sqlString = sqlString & "where id_stock = '" & lblnomoragrument & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            nama = myReader("nama_barang")
            unit = Convert.ToInt32(myReader("unit"))
            jenis = myReader("jenis")
        End If
        Dim id, jenis_barang As Integer
        Dim gambar As String
        'get data barang
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where nama = '" & nama & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            ID = myReader("id")
            nama = myReader("nama")
            jenis_barang = myReader("jenis")
            gambar = myReader("gambar")
            unit_barang = Convert.ToInt32(myReader("unit"))
            modal = Convert.ToInt32(myReader("modal"))
            harga = Convert.ToInt32(myReader("harga"))
            profit = (harga * unit) - (modal * unit)
            gross = harga * unit
        End If

        'get data data
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        sqlString = "select * from data" & Chr(10)
        sqlString = sqlString & "where id = '1'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            profitdatabase = Integer.Parse(myReader("profit"))
            grossdatabase = Integer.Parse(myReader("gross_profit"))
        End If

        'check data
        If jenis = "Barang Masuk" Then
            reset = unit_barang - unit
        ElseIf jenis = "Barang Keluar" Then
            reset = unit_barang + unit
            profitdatabase -= profit
            grossdatabase -= gross
            'reset profit
        End If

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_data", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_profit", profitdatabase)
            cmd.Parameters.AddWithValue("@f_gross_profit", grossdatabase)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_barang", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", id)
            cmd.Parameters.AddWithValue("@f_nama", nama)
            cmd.Parameters.AddWithValue("@f_jenis", jenis_barang)
            cmd.Parameters.AddWithValue("@f_harga", harga)
            cmd.Parameters.AddWithValue("@f_modal", modal)
            cmd.Parameters.AddWithValue("@f_unit", reset)
            cmd.Parameters.AddWithValue("@f_gambar", gambar)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
    End Sub
    Private Sub UbahData()
        Dim lblnomoragrument, diskon1 As String
        lblnomoragrument = Integer.Parse(txtIdStokRinci.Text)
        Dim unit, database, hasil, profit, gross, profitdatabase, grossdatabase, harga, modal, diskon2 As Integer
        Call resetdatabarang(lblnomoragrument)

        If EnableDiskon.Checked = True Then
            diskon1 = "True"
            diskon2 = Convert.ToInt32(txtDiskon.Text)
        Else
            diskon1 = "False"
            diskon2 = 0
        End If

        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_stock", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", txtIdStokRinci.Text)
            cmd.Parameters.AddWithValue("@f_tanggal", txtDate.Text)
            cmd.Parameters.AddWithValue("@f_barang", DDDataBarang.SelectedValue)
            cmd.Parameters.AddWithValue("@f_user", txtNamaLengkap.Text)
            cmd.Parameters.AddWithValue("@f_jenis", DropDownJenis.SelectedValue)
            cmd.Parameters.AddWithValue("@f_unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@f_keterangan", txtKet.Text)
            cmd.Parameters.AddWithValue("@f_diskon1", diskon1)
            cmd.Parameters.AddWithValue("@f_diskon2", diskon2)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try

        'update barang
        ''ngambil data unit dari database barang
        Dim nama As String
        unit = Convert.ToInt32(txtUnit.Text)
        nama = DDDataBarang.SelectedValue
        Dim id As Integer
        Dim gambar As String
        Dim jenis As Integer
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        sqlString = "select * from barang" & Chr(10)
        sqlString = sqlString & "where nama = '" & nama & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            ID = myReader("id")
            nama = myReader("nama")
            jenis = myReader("jenis")
            gambar = myReader("gambar")
            database = myReader("unit")
            'Hitung Unit
            modal = Integer.Parse(myReader("modal"))
            harga = Integer.Parse(myReader("harga"))
        End If

        If DropDownJenis.SelectedValue = "Barang Masuk" Then
            hasil = database + unit
        End If
        If DropDownJenis.SelectedValue = "Barang Keluar" Then
            hasil = database - unit
            If hasil < 0 Then
                Messagebox("Unit Barang tidak cukup!")
                txtUnit.Focus()
                Return
            End If
            profit = (harga * unit) - (modal * unit)
            gross = harga * unit
            myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
            myConnection.Open()
            sqlString = "select * from data" & Chr(10)
            sqlString = sqlString & "where id = '1'"
            myCommand = New SqlCommand(sqlString, myConnection)
            myCommand.CommandTimeout = 0
            myReader = myCommand.ExecuteReader()
            If myReader.Read = True Then
                profitdatabase = Integer.Parse(myReader("profit"))
                grossdatabase = Integer.Parse(myReader("gross_profit"))
            End If

            If EnableDiskon.Checked = True Then
                Dim diskonhasil, profitdiskon, grossdiskon, hasilprofitdiskon, hasilgrossdiskon As Integer
                diskonhasil = diskon2 * 0.01
                profitdiskon = profit * diskonhasil
                grossdiskon = gross * diskonhasil
                hasilprofitdiskon = profit - profitdiskon
                hasilgrossdiskon = gross - grossdiskon

                'put inside the database again
                profit = hasilprofitdiskon
                gross = hasilgrossdiskon
            End If

            profitdatabase += profit
            grossdatabase += gross
            Try
                Dim cmd As SqlCommand = New SqlCommand("up_apl_update_data", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@f_profit", profitdatabase)
                cmd.Parameters.AddWithValue("@f_gross_profit", grossdatabase)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Catch ex As Exception
                Console.WriteLine("An Error Occurred : '{0}'", ex)
            End Try

        End If
        Try
            Dim cmd As SqlCommand = New SqlCommand("up_apl_update_barang", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@f_id", ID)
            cmd.Parameters.AddWithValue("@f_nama", nama)
            cmd.Parameters.AddWithValue("@f_jenis", jenis)
            cmd.Parameters.AddWithValue("@f_harga", harga)
            cmd.Parameters.AddWithValue("@f_modal", modal)
            cmd.Parameters.AddWithValue("@f_unit", hasil)
            cmd.Parameters.AddWithValue("@f_gambar", gambar)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            Console.WriteLine("An Error Occurred : '{0}'", ex)
        End Try
        Messagebox("Data telah diubah.")
    End Sub



    Protected Sub btnHapusData_Click(ByVal sender As Object, ByVal e As EventArgs)

        'Get the reference of the clicked button.
        Dim nomor As Button = CType(sender, Button)
        'Get the command argument
        Dim commandArgument As String = nomor.CommandArgument
        Dim lblnomoragrument As String

        lblnomoragrument = commandArgument

        Call resetdatabarang(lblnomoragrument)

        Dim lblid As String

        lblid = lblnomoragrument
        Dim cmd As SqlCommand = New SqlCommand("up_apl_delete_stock", con)
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandTimeout = 0

        cmd.Parameters.AddWithValue("@f_id", lblnomoragrument)
        cmd.ExecuteNonQuery()
        Messagebox("Data Telah Dihapus.")
        cmd.Dispose()

        Call BindData()

    End Sub



    Protected Sub btnCariGo_Click(sender As Object, e As EventArgs)
        Call CariData()
    End Sub
    Protected Sub btnLaporanMasuk_Click(sender As Object, e As EventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where jenis = 'Barang Masuk'" & Chr(10)
            sqlString = sqlString & "order by id_stock asc"
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
                        lblPaging.Text = "Showing : Laporan Barang Masuk "
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage

                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub btnLaporanKeluar_Click(sender As Object, e As EventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where jenis = 'Barang Keluar'" & Chr(10)
            sqlString = sqlString & "order by id_stock asc"
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
                        lblPaging.Text = "Showing : Laporan Barang Keluar "
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage

                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub btnLaporanRusak_Click(sender As Object, e As EventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where jenis = 'Barang Tak Layak'" & Chr(10)
            sqlString = sqlString & "order by id_stock asc"
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
                        lblPaging.Text = "Showing : Laporan Barang Tak Layak "
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage

                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub btnLaporanExpired_Click(sender As Object, e As EventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where jenis = 'Barang Kadaluarsa'" & Chr(10)
            sqlString = sqlString & "order by id_stock asc"
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
                        lblPaging.Text = "Showing : Barang Kadaluarsa "
                        btnPrevious.Enabled = Not Pds1.IsFirstPage
                        btnNext.Enabled = Not Pds1.IsLastPage

                        rptData.DataSource = Pds1
                        rptData.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub btnLaporanOrder_Click(sender As Object, e As EventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Dim sqlString As String
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where jenis = 'Barang Sedang Dipesan'" & Chr(10)
            sqlString = sqlString & "order by id_stock asc"
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
                        lblPaging.Text = "Showing : Laporan Barang Sedang Dipesan "
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
            sqlString = "select * from stock" & Chr(10)
            sqlString = sqlString & "where id_stock is not null" & Chr(10)

            If txtCariID.Value <> "" Then
                sqlString = sqlString & "and id_stock = '" & txtCariID.Value & "'" & Chr(10)
            End If

            sqlString = sqlString & "order by id_stock asc"
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
        plhList.Visible = True
        lblNomor.Text = 0
        PlaceHolderCariBTN.Visible = True
        PlaceHolderCariBackBtn.Visible = False
        Call BindData()
    End Sub


End Class
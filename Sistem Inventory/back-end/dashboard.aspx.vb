Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Configuration
Imports System.Web.UI.DataVisualization.Charting
Imports System.Web.Services
Imports System.Web.Script.Services
Public Class dashboard
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
                Call binddata()
            End If
        End If
        lbltime.Text = DateTime.Today.ToString("ddd dd/MM/yyyy")
    End Sub

    <WebMethod()>
    Public Shared Function GetChartData() As List(Of Object)
        Dim query As String = "SELECT nama, unit"
        query += " FROM barang"
        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim chartData As New List(Of Object)()
        chartData.Add(New Object() {"nama", "unit"})
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        chartData.Add(New Object() {sdr("nama"), sdr("unit")})
                    End While
                End Using
                con.Close()
                Return chartData
            End Using
        End Using
    End Function


    Protected Sub binddata()
        'bind koneksi
        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()
        'untuk total unit yang sudah keluar
        sqlString = "Select sum(unit) as unitkeluar from stock" & Chr(10)
        sqlString = sqlString & "where jenis = 'Barang Keluar'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            txtSumTotalStock.Text = myReader("unitkeluar").ToString
        End If

        'untuk binding profit
        sqlString = "Select * from data" & Chr(10)
        sqlString = sqlString & "where id = '1'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lblProfit.Text = myReader("profit").ToString
        End If

        'untuk barang tersedia
        sqlString = "Select count(nama) as barangtersedia from barang" & Chr(10)
        sqlString = sqlString & "where unit > '100'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lbltersedia.Text = myReader("barangtersedia").ToString
        End If
        'untuk barang tidak tersedia
        sqlString = "Select count(nama) as barangtidaktersedia from barang" & Chr(10)
        sqlString = sqlString & "where unit < '100'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lbltidaktersedia.Text = myReader("barangtidaktersedia").ToString
        End If
        'untuk stock masuk
        sqlString = "Select count(jenis) as unitmasuk from stock" & Chr(10)
        sqlString = sqlString & "where jenis = 'Barang Masuk'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lblmasuk.Text = myReader("unitmasuk").ToString
        End If
        'Untuk Stock Keluar
        sqlString = "Select count(jenis) as unitkeluar from stock" & Chr(10)
        sqlString = sqlString & "where jenis = 'Barang Keluar'"

        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0

        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lblkeluar.Text = myReader("unitkeluar").ToString
        End If
        'untuk diskon
        sqlString = "select count(diskon) As discount from stock where diskon = 'true'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            lbldiscount.Text = myReader("discount")
        End If

        'untuk broken
        Dim taklayak, expired, combine As Integer
        sqlString = "select count(jenis) As taklayak from stock where jenis = 'Barang Tak Layak'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            taklayak = myReader("taklayak")
        End If
        sqlString = "select count(jenis) As expired from stock where jenis = 'Barang Kadaluarsa'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myCommand.CommandTimeout = 0
        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            expired = myReader("expired")
        End If
        combine = taklayak + expired
        lblBroken.Text = combine

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



End Class
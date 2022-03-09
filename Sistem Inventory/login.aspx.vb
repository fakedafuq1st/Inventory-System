Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Security.Cryptography

Public Class Index
    Inherits System.Web.UI.Page
    Dim myConnection As SqlConnection
    Dim sqlString As String
    Dim myCommand As SqlCommand
    Dim myReader As SqlDataReader
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
    Dim IdUser As String
    Dim Pass As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
    End Sub

    Private Sub Messagebox(ByVal Message As String)
        Dim lblMessageBox As New Label()

        lblMessageBox.Text = "<script language='javascript'>" + Environment.NewLine & "window.alert('" & Message & "')</script>"
        Page.Controls.Add(lblMessageBox)
    End Sub

    Private Sub btnsubmit_ServerClick(sender As Object, e As EventArgs) Handles btnsubmit.ServerClick
        If Len(Trim(txtUsername.Value)) = 0 Then
            Messagebox("Kolom Id masih kosong!")
            txtUsername.Focus()
            Return
        End If

        If Len(Trim(txtPassword.Value)) = 0 Then
            Messagebox("Kolom Kata Sandi Belum Diisi !")
            txtPassword.Focus()
            Return
        End If

        IdUser = txtUsername.Value
        Pass = txtPassword.Value
        Dim id As Integer
        Dim Pw As String
        Dim status As String

        myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
        myConnection.Open()

        sqlString = "select pass, access, id from account where username = '" & IdUser & "'"
        myCommand = New SqlCommand(sqlString, myConnection)
        myReader = myCommand.ExecuteReader()
        If myReader.Read = True Then
            id = myReader("id")
            Pw = myReader("pass").ToString
            status = myReader("access").ToString
        Else
            Pass = ""
            status = ""
        End If
        myConnection.Close()

        If Pass = "" Then
            Session("UserAuthentication") = ""
            Response.Write("<script>alert('Username / Password salah!.')</script>")
            Return
        ElseIf status = "0" Then
            Session("UserAuthentication") = ""
            Response.Write("<script>alert('Akun tidak mempunyai hak untuk login. silahkan menghubungi administrator untuk informasi lebih lanjut')</script>")
            Return
        ElseIf Pass = Pw Then
            Session("id") = id
            Session("UserAuthentication") = IdUser
            Session("BlnLoggedIn") = True
            If status = 2 Then
                Response.Write("<script>alert('Berhasil Login')</script>")
                Response.Write("<script> window.open('../back-end/dashboard2.aspx', '_self'); </script>")
            End If
            Response.Write("<script>alert('Berhasil Login')</script>")
            Response.Write("<script> window.open('../back-end/dashboard.aspx', '_self'); </script>")
            Return
        Else
            Session("UserAuthentication") = ""
            Response.Write("<script>alert('Login Anda tidak berhasil. Coba lagi!')</script>")
            Return
        End If
    End Sub

    'Private Sub Btnbypass_Serverclick(sender As Object, e As EventArgs) Handles Btnbypass.ServerClick

    '    namauser = "admin"
    '    pwd = "admin"

    '    Dim hasilPass As String
    '    Dim status As String

    '    myConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("conn").ConnectionString)
    '    myConnection.Open()

    '    sqlString = "select password, status from tbl_account where username = '" & namauser & "'"
    '    myCommand = New SqlCommand(sqlString, myConnection)
    '    myReader = myCommand.ExecuteReader()
    '    If myReader.Read = True Then
    '        hasilPass = myReader("password").ToString
    '        status = myReader("status").ToString
    '    Else
    '        hasilPass = ""
    '        status = ""
    '    End If
    '    myConnection.Close()

    '    If hasilPass = "" Then
    '        Session("UserAuthentication") = ""
    '        Response.Write("<script>alert('Username / Password salah!.')</script>")
    '        Return
    '    ElseIf status = "0" Then
    '        Session("UserAuthentication") = ""
    '        Response.Write("<script>alert('Akun tidak mempunyai hak untuk login. silahkan menghubungi administrator untuk informasi lebih lanjut')</script>")
    '        Return
    '    ElseIf hasilPass = pwd Then
    '        Session("UserAuthentication") = namauser
    '        Session("BlnLoggedIn") = True
    '        Response.Write("<script>alert('Berhasil Login')</script>")
    '        Response.Write("<script> window.open('../back-end/Dashboard.aspx', '_self'); </script>")
    '        Return
    '    Else
    '        Session("UserAuthentication") = ""
    '        Response.Write("<script>alert('Login Anda tidak berhasil. Coba lagi!')</script>")
    '        Return
    '    End If
    'End Sub

End Class
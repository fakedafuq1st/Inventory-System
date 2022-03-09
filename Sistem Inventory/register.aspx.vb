Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Security.Cryptography

Public Class register
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
            Messagebox("Kolom Username masih kosong!")
            txtUsername.Focus()
            Return
        End If

        If Len(Trim(txtNama.Value)) = 0 Then
            Messagebox("Kolom Nama Belum Diisi !")
            txtNama.Focus()
            Return
        End If
        If Len(Trim(txtPassword.Value)) = 0 Then
            Messagebox("Kolom Kata Sandi Belum Diisi !")
            txtPassword.Focus()
            Return
        End If
        If Len(Trim(txtPasswordVal.Value)) = 0 Then
            Messagebox("Ketik kata sandi yang sama lagi!")
            txtPasswordVal.Focus()
            Return
        End If
        If txtPasswordVal.Value = txtPassword.Value Then
            Dim username, nama, password As String
            Dim access As Integer
            username = txtUsername.Value
            nama = txtNama.Value
            password = txtPasswordVal.Value
            access = 0
            Try
                Dim cmd As SqlCommand = New SqlCommand("up_apl_insert_akun", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@f_username", username)
                cmd.Parameters.AddWithValue("@f_nama", nama)
                cmd.Parameters.AddWithValue("@f_password", password)
                cmd.Parameters.AddWithValue("@f_access", access)
                cmd.Parameters.AddWithValue("@f_activation", 0)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Catch ex As Exception
                Console.WriteLine("An Error Occurred : '{0}'", ex)
            End Try
            Response.Write("<script>alert('Registrasi Berhasil, Silahkan menunggu akun diaktivasi oleh admin. terima kasih!')</script>")
            Response.Write("<script> window.open('../login.aspx', '_self'); </script>")
        End If
        Messagebox("Password tidak sama. !")
        txtPassword.Focus()
        Return
    End Sub
End Class
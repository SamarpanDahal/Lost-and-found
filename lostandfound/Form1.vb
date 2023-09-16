Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Public Class Form1

    Private Sub Buttonregister_Click(sender As Object, e As EventArgs) Handles Buttonregister.Click
        '// declared for email validation //
        Dim regex As New Regex("^[A-Za-z0-9]+(.|_)+[A-Za-z0-9]+@+kristujayanti.com$")
        Dim allowedemail As Boolean = regex.IsMatch(Email.Text.Trim)

        '// declare for password validation //
        Dim minLength As Integer = 8
        Dim maxLength As Integer = 16
        Dim containsUpperCase As Boolean = True
        Dim containsLowerCase As Boolean = True
        Dim containsNumber As Boolean = True
        Dim containsSymbol As Boolean = True
        Dim symbolRegex As String = "[!@#$%^&*()_+={}|;':"",.<>?/\[\]\\\-]"
        Dim errorMessage As String = ErrorProvider1.GetError(Password)


        ' Clear any existing errors
        ErrorProvider1.SetError(Password, "")

        ' Check password length
        If Password.Text.Length < minLength Or Password.Text.Length > maxLength Then
            ErrorProvider1.SetError(Password, "must be 8-16 character")
            Return
        End If

        ' Check if password contains upper case letters
        If containsUpperCase And Not Password.Text.Any(Function(c) Char.IsUpper(c)) Then
            ErrorProvider1.SetError(Password, "Must contain uppercase")
            Return
        End If

        ' Check if password contains lower case letters
        If containsLowerCase And Not Password.Text.Any(Function(c) Char.IsLower(c)) Then
            ErrorProvider1.SetError(Password, "Must contain Lowercase")
            Return
        End If

        ' Check if password contains numbers
        If containsNumber And Not Password.Text.Any(Function(c) Char.IsDigit(c)) Then
            ErrorProvider1.SetError(Password, "Must contain digit")
            Return
        End If

        ' Check if password contains symbols
        If containsSymbol And Not Regex.IsMatch(Password.Text, symbolRegex) Then
            ErrorProvider1.SetError(Password, "Must contain symbol")
            Return
        End If

        If Password.Text <> Retype.Text Then
            ErrorProvider2.SetError(Retype, "")
            ErrorProvider3.SetIconPadding(Retype, 6)
            ErrorProvider3.SetError(Retype, "Passwords don't match.")
            Return
        End If

        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dr As SqlDataReader
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True"
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Register where Regno='" & Regno.Text & "'"
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Regno already registered", MsgBoxStyle.Critical)
            Regno.Clear()
            Fname.Clear()
            Email.Clear()
            Password.Clear()
            Retype.Clear()
            ErrorProvider2.SetError(Retype, "")
            ErrorProvider3.SetError(Retype, "")
            con.Close()
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("Insert Into Register values('" & Fname.Text & "','" & Regno.Text & "','" & Email.Text & "','" & Password.Text & "')", con)



            If (Fname.Text = "" And Regno.Text = "" And Email.Text = "" And Password.Text = "") Then
                MessageBox.Show("please enter the details")
            ElseIf (Fname.Text = "" Or Regno.Text = "" Or Email.Text = "" Or Password.Text = "") Then
                MessageBox.Show("Fill all the details")

            ElseIf Not allowedemail Then
                MessageBox.Show("Please Enter College Email.")
                Email.Clear()

            Else
                cmd.ExecuteNonQuery()
                MsgBox("successfully registered.", MsgBoxStyle.Information, "Success")
                GunaPanel3.Hide()
                GunaPanel4.Show()

                Fname.Clear()
                Regno.Clear()
                Email.Clear()
                Password.Clear()
                ErrorProvider1.Clear()

            End If
            con.Close()
        End If
        con.Close()
    End Sub



    Private Sub Retype_TextChanged(sender As Object, e As EventArgs) Handles Retype.TextChanged
        If Password.Text.Length <> Retype.Text.Length Then
            ' Passwords don't match, show error provider icon
            ErrorProvider2.SetError(Retype, "")
            ErrorProvider3.SetError(Retype, "Passwords don't match.")
            ErrorProvider3.SetIconPadding(Retype, 6)
        Else
            ' Check each character
            Dim match As Boolean = True
            For i As Integer = 0 To Password.Text.Length - 1
                If Password.Text(i) <> Retype.Text(i) Then
                    match = False
                    Exit For
                End If
            Next
            If match Then
                ' Passwords match, show green tick icon
                ErrorProvider3.SetError(Retype, "")
                ErrorProvider2.SetError(Retype, "Passwords match.")
                ErrorProvider2.SetIconPadding(Retype, 6)

            Else
                ' Passwords don't match, show error provider icon
                ErrorProvider2.SetError(Retype, "")
                ErrorProvider3.SetError(Retype, "Passwords don't match.")
                ErrorProvider3.SetIconPadding(Retype, 6)


            End If
        End If
    End Sub




    '// Name validation //
    Private Sub Fname_keyPress(sender As Object, e As KeyPressEventArgs) Handles Fname.KeyPress
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedname As String = "abcdefghijklmnopqrstuvwxyz "
            If Not allowedname.Contains(e.KeyChar.ToString.ToLower) Then
                ErrorProvider1.SetError(Fname, "Enter Alphabets")
                e.KeyChar = ChrW(0)
                e.Handled = True
            Else
                ErrorProvider1.SetError(Fname, "")
            End If
        End If
    End Sub


    '// Regno validation //
    Private Sub Regno_keyPress(sender As Object, e As KeyPressEventArgs) Handles Regno.KeyPress
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedregno As String = "abcdefghijklmnopqrstuvwxyz1234567890"
            If Not allowedregno.Contains(e.KeyChar.ToString.ToLower) Then
                ErrorProvider1.SetError(Regno, "Enter Correct Regno")
                e.KeyChar = ChrW(0)
                e.Handled = True

            Else
                ErrorProvider1.SetError(Regno, "")
            End If
        End If
    End Sub


    '// login page//
    Private Sub Buttonlogin_Click_1(sender As Object, e As EventArgs) Handles Buttonlogin.Click
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True"
        Dim objcon As SqlConnection = Nothing
        Dim objcmd As SqlCommand = Nothing
        objcon = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")

        objcon.Open()
        Dim stmt As String = " select * from Register where Regno='" & Lregno.Text & "' and Password='" & Lpassword.Text & "' "
        objcmd = New SqlCommand(stmt, objcon)
        Dim reader As SqlDataReader = objcmd.ExecuteReader
        If reader.Read Then


            Me.Hide()
            Form2.Show()
            Lregno.Clear()
            Lpassword.Clear()

        Else
            MessageBox.Show("Invalid login ")
            Lregno.Clear()
            Lpassword.Clear()
        End If
    End Sub



    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        GunaPanel3.Show()
        GunaPanel4.Hide()



    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        GunaPanel3.Hide()
        GunaPanel4.Show()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GunaPanel2.BackColor = Color.FromArgb(110, 0, 0, 0)
        GunaPanel1.BackColor = Color.FromArgb(100, 0, 0, 0)

    End Sub


End Class














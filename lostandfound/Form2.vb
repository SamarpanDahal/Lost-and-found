
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text


Public Class Form2
    Public user As String



    Private Sub Form2Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.Show()
    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        user = Form1.Lregno.Text
        Todaydate.Text = Date.Today.ToString("dd-MMMM-yyyy")
        Ctodaydate.Text = Date.Today.ToString("dd-MMMM-yyyy")
        GunaPanel6.BackColor = Color.FromArgb(100, 0, 0, 0)
        GunaPanel5.BackColor = Color.FromArgb(100, 0, 0, 0)
        GunaPanel7.BackColor = Color.FromArgb(70, 0, 0, 0)
        GunaPanel8.BackColor = Color.FromArgb(70, 0, 0, 0)
        userinfopanel.Hide()
        GunaPanel13.Hide()
        GunaPanel9.Hide()
        GunaPanel5.Hide()
        GunaPanel12.Hide()
        GunaPanel11.Hide()
        Buttonfound.BaseColor = Color.Lime

        '// admin login//
        Dim admin1 As String = "21BCAA54"
        Dim admin2 As String = "1"


        If user <> admin1 AndAlso user <> admin2 Then
            Report.Hide()
        End If

        Dim con As New SqlConnection
        con.ConnectionString = "data source=(localdb)\mssqllocaldb;attachdbfilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;integrated security=true"

        con.Open()
        Dim stmt As String
        stmt = "select fname,regno,email from register where regno='" & user & "'"

        Dim cmd As New SqlCommand(stmt, con)
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader
        If reader.Read() Then
            Label8.Text = reader("fname")
            Fname.Text = reader("fname")
            Regno.Text = reader("regno")
            Email.Text = reader("email")
            Cname.Text = reader("fname")
            Cregno.Text = reader("regno")
            Cemail.Text = reader("email")
        Else
            MessageBox.Show("error")
        End If
    End Sub


    Private Sub Report_Click(sender As Object, e As EventArgs) Handles Report.Click
        GunaDataGridView1.RowTemplate.Height = 30
        GunaLabel4.Text = "Report"
        GunaDataGridView1.Hide()
        GunaPanel11.Show()
        userinfopanel.Hide()
        foundpanel.Hide()
        lostpanel.Hide()
        GunaPanel10.Hide()
        Buttonfound.BaseColor = Color.Cyan
        Buttonlost.BaseColor = Color.Cyan
        Auction.BaseColor = Color.Cyan
        Report.BaseColor = Color.Lime
        Today.BaseColor = Color.FromArgb(100, 88, 255)
        Thismonth.BaseColor = Color.FromArgb(100, 88, 255)


    End Sub


    Private Sub Buttonfound_Click(sender As Object, e As EventArgs) Handles Buttonfound.Click
        Buttonfound.BaseColor = Color.Lime
        Buttonlost.BaseColor = Color.Cyan
        Auction.BaseColor = Color.Cyan
        Report.BaseColor = Color.Cyan
        foundpanel.Show()
        userinfopanel.Hide()
        lostpanel.Hide()
        GunaPanel11.Hide()
    End Sub

    Private Sub Buttonlost_Click(sender As Object, e As EventArgs) Handles Buttonlost.Click
        Buttonfound.BaseColor = Color.Cyan
        Buttonlost.BaseColor = Color.Lime
        Auction.BaseColor = Color.Cyan
        Report.BaseColor = Color.Cyan
        lostpanel.Show()
        foundpanel.Hide()
        userinfopanel.Hide()
        GunaPanel11.Hide()
    End Sub

    Private Sub Phoneno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Phoneno.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True 'Ignore the key press if it's not a digit or control character
            ErrorProvider1.SetError(Phoneno, "Enter a valid phone number")
        Else
            ErrorProvider1.SetError(Phoneno, "") 'Clear the error message if the input is valid
        End If
    End Sub



    Private Sub Buttonselectimage_Click(sender As Object, e As EventArgs) Handles Buttonselectimage.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Fitemphoto.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub


    '// Button submit of Found items//
    Private Sub Buttonsubmit_Click(sender As Object, e As EventArgs) Handles Buttonsubmit.Click

        Dim con As New SqlConnection
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True"

        ' Dim cmd As New SqlCommand("INSERT INTO Found (Fitemname, Fitemcolour, Fbrandname, Founddate, Fkeydesc, placefound, Fitemcondition, Fitemphoto, Fname, Regno, Email, Phoneno, Sem, Course, Section, Todaydate) VALUES (@Fitemname, @Fitemcolour, @Fbrandname, @Founddate, @Fkeydesc, @placefound, @Fitemcondition, @ph, @Fname, @Regno, @Email, @Phoneno, @Sem, @Course, @Section, @Todaydate)", con)


        Dim cmd As New SqlCommand("Insert Into Found values('" & Fitemname.Text & "','" & Fitemcolour.Text & "','" & Fbrandname.Text & "','" & Founddate.Text & "','" & Fkeydesc.Text & "','" & placefound.Text & "','" & Fitemcondition.Text & "',@ph,'" & Fname.Text & "','" & Regno.Text & "','" & Email.Text & "','" & Phoneno.Text & "','" & Sem.Text & "','" & Course.Text & "','" & Section.Text & "','" & Todaydate.Text & "')", con)
        If (Fitemphoto.Image IsNot Nothing) Then

            Dim ms As New MemoryStream
            Dim img As Image = Fitemphoto.Image
            Dim bmpimage As New Bitmap(img)
            bmpimage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim data As Byte() = ms.GetBuffer
            Dim p As New SqlParameter("@ph", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
        End If

        con.Open()

        If (Fitemname.Text = "" Or Fitemcolour.Text = "" Or Fbrandname.Text = "" Or Founddate.Text = "" Or Fkeydesc.Text = "" Or placefound.Text = "" Or Fitemcondition.Text = "" Or Fname.Text = "" Or Regno.Text = "" Or Email.Text = "" Or Phoneno.Text = "" Or Sem.Text = "" Or Course.Text = "" Or Section.Text = "") Then
            MessageBox.Show("Fill the details")

        ElseIf ErrorProvider1.GetError(Founddate) <> "" Then
            MessageBox.Show("Please fill in all required fields correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)


        ElseIf Fitemphoto.Image Is Nothing Then
            MessageBox.Show("Please upload image")
        ElseIf Phoneno.Text.Length < 10 Then
            ErrorProvider1.SetError(Phoneno, "Enter a valid phone number")


        Else cmd.ExecuteNonQuery()
            MessageBox.Show("added")
            Fitemcolour.Text = ""
            Fitemname.Text = ""
            Fbrandname.Text = ""
            Founddate.Text = ""
            Fkeydesc.Text = ""
            placefound.Text = ""
            Fitemcondition.Text = ""
            Fitemphoto.Image = Nothing
            Phoneno.Text = ""
            Sem.SelectedItem = Nothing
            Course.Text = ""
            Section.Text = ""
        End If
        con.Close()


    End Sub

    ' //Calculate the Levenshtein Distance between two strings//
    Function LevenshteinDistance(s1 As String, s2 As String) As Integer
        Dim d(s1.Length, s2.Length) As Integer
        For i As Integer = 0 To s1.Length
            d(i, 0) = i
        Next
        For j As Integer = 0 To s2.Length
            d(0, j) = j
        Next
        For i As Integer = 1 To s1.Length
            For j As Integer = 1 To s2.Length
                Dim cost As Integer = If(s1(i - 1) = s2(j - 1), 0, 1)
                d(i, j) = Math.Min(Math.Min(d(i - 1, j) + 1, d(i, j - 1) + 1), d(i - 1, j - 1) + cost)
            Next
        Next
        Return d(s1.Length, s2.Length)
    End Function
    '// Button submit of Lost Items//
    Private idValue As Integer
    Private citemname As String
    Private colour As String
    Private brand As String
    Private Sub GunaButton1_Click(sender As Object, e As EventArgs) Handles GunaButton1.Click
        If (Litemname.Text = "" Or Litemcolour.Text = "" Or Lbrandname.Text = "" Or Lostdate.Text = "" Or Lkeydesc.Text = "" Or Placelost.Text = "" Or Litemcondition.Text = "") Then
            MessageBox.Show("Fill the details")
        ElseIf ErrorProvider1.GetError(Lostdate) <> "" Then
            MessageBox.Show("Please fill in all required fields correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")
            con.Open()
            ' //Select all items found after the specified lost date, including the image data//
            Dim cmd As New SqlCommand(" SELECT * FROM found WHERE founddate >= @lostdate AND id NOT IN ( SELECT id FROM Claims  )", con)
            '   Dim cmd As New SqlCommand("SELECT * FROM found WHERE founddate >= @lostdate", con)
            cmd.Parameters.AddWithValue("@lostdate", Lostdate.Value)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ' //Loop through all found items and add the matching items to the list//
            Dim matchingItems As New StringBuilder()
            Dim itemsforclaim As New StringBuilder()
            While reader.Read()
                ' //Check if the item name matches the search query//
                Dim itemName As String = reader("Fitemname")
                If LevenshteinDistance(Litemname.Text, itemName) <= itemName.Length * 0.6 Then
                    ' //Check if any two values match fitemcolour, fbrandname, and placefound//
                    If (Litemcolour.Text.ToLower() = reader("Fitemcolour").ToString().ToLower() AndAlso Lbrandname.Text.ToLower() = reader("Fbrandname").ToString().ToLower()) _
                        OrElse (Litemcolour.Text.ToLower() = reader("Fitemcolour").ToString().ToLower() AndAlso Placelost.Text.ToLower() = reader("Placefound").ToString().ToLower()) _
                        OrElse (Lbrandname.Text.ToLower() = reader("Fbrandname").ToString().ToLower() AndAlso Placelost.Text.ToLower() = reader("Placefound").ToString().ToLower()) Then

                        Dim imageData As Byte() = TryCast(reader("Fitemphoto"), Byte())
                        If imageData IsNot Nothing Then
                            Using stream As New MemoryStream(imageData)
                                GunaPictureBox2.Image = Image.FromStream(stream)
                            End Using
                        End If

                        ' //Add the matching item details to the StringBuilder//
                        itemsforclaim.AppendLine($"Fitemname: {reader("Fitemname")}")
                        itemsforclaim.AppendLine($"Fitemcolour: {reader("Fitemcolour")}")
                        itemsforclaim.AppendLine($"Fbrandname: {reader("Fbrandname")}")
                        matchingItems.AppendLine($"ID: {reader("Id")}")
                        matchingItems.AppendLine($"Place found: {reader("Placefound")}")
                        matchingItems.AppendLine($"Person Name: {reader("Fname")}")
                        matchingItems.AppendLine($"Person Regno: {reader("Regno")}")
                        matchingItems.AppendLine($"Person Email: {reader("Email")}")
                        matchingItems.AppendLine($"Person Phoneno: {reader("Phoneno")}")
                        matchingItems.AppendLine($"Person Sem: {reader("Sem")}")
                        matchingItems.AppendLine($"Person Course: {reader("Course")}")
                        matchingItems.AppendLine($"Person Section: {reader("Section")}")
                        matchingItems.AppendLine() ' Add a blank line between each item
                        ' //Set the idValue variable to the ID value//
                        idValue = reader("Id")
                        citemname = reader("Fitemname")
                        colour = reader("Fitemcolour")
                        brand = reader("Fbrandname")
                    End If
                End If
            End While
            '// show result of seatch items//
            Dim finalmatch = matchingItems.ToString()
            If finalmatch.Trim() = String.Empty Then
                GunaPanel5.Show()
                GunaPanel9.Show()
                Label30.Text = "No Items Found"
                RichTextBox2.Text = "No matching items found."
                GunaPictureBox2.Image = Nothing
                Buttonyes.Hide()
                Buttonno.Hide()
            Else
                GunaPanel5.Show()
                RichTextBox1.Text = "Matching items found:" & vbCrLf & finalmatch.ToString()

            End If
        End If
    End Sub
    '// button yes click to confirm lost item//
    Private Sub Buttonyes_Click(sender As Object, e As EventArgs) Handles Buttonyes.Click
        If GunaPictureBox2.Image IsNot Nothing Then
            GunaPanel13.Show()
        End If
        GunaPanel9.Hide()
    End Sub

    '// button claim click to store data //
    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        ' Create a new SqlConnection object with the connection string
        Dim con As New SqlConnection
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True"

        ' Create a new SqlCommand object with the insert query
        Dim cmd As New SqlCommand("Insert Into Claims values('" & idValue & "','" & citemname & "','" & colour & "','" & brand & "','" & Cname.Text & "','" & Cregno.Text & "','" & Cemail.Text & "','" & Cphoneno.Text & "','" & Csem.Text & "','" & Ccourse.Text & "','" & Csection.Text & "','" & Ctodaydate.Text & "')", con)
        con.Open()
        ' Check if the required fields are filled
        If (Cphoneno.Text = "" Or Csem.Text = "" Or Ccourse.Text = "" Or Csection.Text = "") Then
            MessageBox.Show("Fill the details")
        Else
            ' Execute the insert query and display a success message
            cmd.ExecuteNonQuery()
            MessageBox.Show("Added")

            ' Clear the input fields
            Cphoneno.Text = ""
            Csem.Text = ""
            Ccourse.Text = ""
            Csection.Text = ""
            Litemcolour.Text = ""
            Litemname.Text = ""
            Lbrandname.Text = ""
            Lostdate.Text = ""
            Lkeydesc.Text = ""
            Placelost.Text = ""
            Litemcondition.Text = ""
            GunaPictureBox2.Image = Nothing
            GunaPanel12.Hide()
            GunaPanel5.Hide()
        End If
        con.Close()
    End Sub





    '// Button no click to confirm shown item is not yours //
    Private Sub Buttonno_Click(sender As Object, e As EventArgs) Handles Buttonno.Click
        GunaPanel13.Hide()
        If GunaPictureBox2.Image IsNot Nothing Then
            GunaPanel9.Show()
            RichTextBox2.Text = "No matching items found."
        End If

        GunaPictureBox2.Image = Nothing
    End Sub
    '// clear form on ok button click after lost search result//
    Private Sub GunaButton2_Click(sender As Object, e As EventArgs) Handles GunaButton2.Click
        GunaPanel5.Hide()
        Litemcolour.Text = ""
        Litemname.Text = ""
        Lbrandname.Text = ""
        Lostdate.Text = ""
        Lkeydesc.Text = ""
        Placelost.Text = ""
        Litemcondition.Text = ""
        GunaPictureBox2.Image = Nothing
    End Sub

    '//Lostdate validation//
    Private Sub Lostdate_ValueChanged(sender As Object, e As EventArgs) Handles Lostdate.ValueChanged

        Dim selectedDate As Date = Lostdate.Value.Date
        If selectedDate > Date.Today Then
            ErrorProvider1.SetError(Lostdate, "Selected date cannot be in the future.")
            Return
        End If
        ErrorProvider1.SetError(Lostdate, "")
    End Sub
    '//Founddate validation//
    Private Sub Founddate_ValueChanged(sender As Object, e As EventArgs) Handles Founddate.ValueChanged
        Dim selectedDate As Date = Founddate.Value.Date
        If selectedDate > Date.Today Then
            ErrorProvider1.SetError(Founddate, "Selected date cannot be in the future.")
            Return
        End If
        ErrorProvider1.SetError(Founddate, "")
    End Sub





    Private Sub Today_Click(sender As Object, e As EventArgs) Handles Today.Click
        GunaDataGridView1.Show()
        GunaDataGridView1.DataSource = Nothing
        GunaLabel4.Text = "Today's Report"
        GunaPanel10.Show()
        userinfopanel.Show()
        todayfound.Show()
        todayclaimed.Show()
        monthlyfound.Hide()
        monthlyclaimed.Hide()
        Today.BaseColor = Color.Lime
        Thismonth.BaseColor = Color.FromArgb(100, 88, 255)
        todayfound.BaseColor = Color.FromArgb(100, 88, 255)
        todayclaimed.BaseColor = Color.FromArgb(100, 88, 255)
        monthlyfound.BaseColor = Color.FromArgb(100, 88, 255)
        monthlyclaimed.BaseColor = Color.FromArgb(100, 88, 255)

    End Sub
    Private Sub todayfound_Click(sender As Object, e As EventArgs) Handles todayfound.Click
        todayfound.BaseColor = Color.Lime
        todayclaimed.BaseColor = Color.FromArgb(100, 88, 255)
        Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")
        con.Open()

        Dim cmd As New SqlCommand("SELECT Fitemname, Fitemcolour, Fbrandname, Founddate, placefound, Fname AS [Founders name], Regno FROM Found WHERE Todaydate = CONVERT(date, GETDATE())", con)

        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)
        GunaDataGridView1.DataSource = table

        GunaDataGridView1.Columns("Fitemname").HeaderText = "Item Name"
        GunaDataGridView1.Columns("Fitemcolour").HeaderText = "Item Colour"
        GunaDataGridView1.Columns("Fbrandname").HeaderText = "Brand Name"
        GunaDataGridView1.Columns("Founddate").HeaderText = "Date Found"
        GunaDataGridView1.Columns("placefound").HeaderText = "Place Found"
        GunaDataGridView1.Columns("Founders name").HeaderText = "Founders name"
        GunaDataGridView1.Columns("Regno").HeaderText = "Registration No."

        If table.Rows.Count > 0 Then
            GunaDataGridView1.DataSource = table

        End If

        con.Close()
    End Sub

    Private Sub todayclaimed_Click(sender As Object, e As EventArgs) Handles todayclaimed.Click
        todayclaimed.BaseColor = Color.Lime
        todayfound.BaseColor = Color.FromArgb(100, 88, 255)

        Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")
        con.Open()
        Dim cmd As New SqlCommand("SELECT Itemname, Colour, Brandname, Cname AS [claimer's name],Todaydate, Regno , Phoneno FROM Claims WHERE Todaydate = CONVERT(date, GETDATE())", con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)
        GunaDataGridView1.DataSource = table

        GunaDataGridView1.Columns("Itemname").HeaderText = "Item Name"
        GunaDataGridView1.Columns("Colour").HeaderText = "Item Colour"
        GunaDataGridView1.Columns("Brandname").HeaderText = "Brand Name"
        GunaDataGridView1.Columns("Todaydate").HeaderText = "Date Claimed"
        GunaDataGridView1.Columns("claimer's name").HeaderText = "claimer's name"
        GunaDataGridView1.Columns("Regno").HeaderText = "Registration No."
        GunaDataGridView1.Columns("Phoneno").HeaderText = "Phoneno."

        If table.Rows.Count > 0 Then
            GunaDataGridView1.DataSource = table

        End If

        con.Close()
    End Sub


    Private Sub Thismonth_Click(sender As Object, e As EventArgs) Handles Thismonth.Click
        GunaLabel4.Text = "Monthly Report"
        GunaDataGridView1.Show()
        GunaDataGridView1.DataSource = Nothing
        GunaPanel10.Show()
        userinfopanel.Show()
        todayfound.Hide()
        todayclaimed.Hide()
        monthlyfound.Show()
        monthlyclaimed.Show()
        Thismonth.BaseColor = Color.Lime
        Today.BaseColor = Color.FromArgb(100, 88, 255)
        todayfound.BaseColor = Color.FromArgb(100, 88, 255)
        todayclaimed.BaseColor = Color.FromArgb(100, 88, 255)
        monthlyfound.BaseColor = Color.FromArgb(100, 88, 255)
        monthlyclaimed.BaseColor = Color.FromArgb(100, 88, 255)
    End Sub
    Private Sub monthlyfound_Click(sender As Object, e As EventArgs) Handles monthlyfound.Click
        monthlyfound.BaseColor = Color.Lime
        monthlyclaimed.BaseColor = Color.FromArgb(100, 88, 255)

        Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")
        con.Open()
        Dim cmd As New SqlCommand("SELECT Fitemname, Fitemcolour, Fbrandname, Founddate, placefound, Fname AS [Founders name], Regno FROM Found WHERE Todaydate BETWEEN DATEADD(day, -30, GETDATE()) AND GETDATE()", con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)

        GunaDataGridView1.DataSource = table

        GunaDataGridView1.Columns("Fitemname").HeaderText = "Item Name"
        GunaDataGridView1.Columns("Fitemcolour").HeaderText = "Item Colour"
        GunaDataGridView1.Columns("Fbrandname").HeaderText = "Brand Name"
        GunaDataGridView1.Columns("Founddate").HeaderText = "Date Found"
        GunaDataGridView1.Columns("placefound").HeaderText = "Place Found"
        GunaDataGridView1.Columns("Founders name").HeaderText = "Founders name"
        GunaDataGridView1.Columns("Regno").HeaderText = "Registration No."


        If table.Rows.Count > 0 Then
            GunaDataGridView1.DataSource = table
        End If
        con.Close()
    End Sub

    Private Sub monthlyclaimed_Click(sender As Object, e As EventArgs) Handles monthlyclaimed.Click
        monthlyclaimed.BaseColor = Color.Lime
        monthlyfound.BaseColor = Color.FromArgb(100, 88, 255)

        Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")
        con.Open()
        Dim cmd As New SqlCommand("SELECT Itemname, Colour, Brandname, Cname AS [claimer's name],Todaydate, Regno , Phoneno FROM Claims WHERE Todaydate BETWEEN DATEADD(day, -30, GETDATE()) AND GETDATE()", con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)
        GunaDataGridView1.DataSource = table

        GunaDataGridView1.Columns("Itemname").HeaderText = "Item Name"
        GunaDataGridView1.Columns("Colour").HeaderText = "Item Colour"
        GunaDataGridView1.Columns("Brandname").HeaderText = "Brand Name"
        GunaDataGridView1.Columns("Todaydate").HeaderText = "Date Claimed"
        GunaDataGridView1.Columns("claimer's name").HeaderText = "claimer's name"
        GunaDataGridView1.Columns("Regno").HeaderText = "Registration No."
        GunaDataGridView1.Columns("Phoneno").HeaderText = "Phoneno."

        If table.Rows.Count > 0 Then
            GunaDataGridView1.DataSource = table

        End If

        con.Close()
    End Sub


    Private Sub Auction_Click(sender As Object, e As EventArgs) Handles Auction.Click

        Buttonfound.BaseColor = Color.Cyan
        Buttonlost.BaseColor = Color.Cyan
        Auction.BaseColor = Color.Lime
        Report.BaseColor = Color.Cyan
        Thismonth.BaseColor = Color.FromArgb(100, 88, 255)
        Today.BaseColor = Color.FromArgb(100, 88, 255)
        GunaLabel4.Text = "Welcome To Auction"
        GunaPanel10.Hide()
        userinfopanel.Show()

        foundpanel.Hide()
        lostpanel.Hide()
        GunaPanel11.Hide()
        GunaDataGridView1.Show()



        Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\samar\source\repos\lostandfound\lostandfound\registration.mdf;Integrated Security=True")
        con.Open()
        Dim cmd As New SqlCommand("SELECT Fitemname, Fitemcolour, Fbrandname, Founddate, placefound, Fitemphoto FROM Found WHERE Todaydate < DATEADD(day, -50, GETDATE()) AND id NOT IN (SELECT id FROM Claims)", con)
        ' Dim cmd As New SqlCommand("SELECT Fitemname, Fitemcolour, Fbrandname, Founddate, placefound, Fitemphoto FROM Found WHERE Todaydate < DATEADD(day, -50, GETDATE())", con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)

        GunaDataGridView1.DataSource = table

        GunaDataGridView1.Columns("Fitemname").HeaderText = "Item Name"
        GunaDataGridView1.Columns("Fitemcolour").HeaderText = "Item Colour"
        GunaDataGridView1.Columns("Fbrandname").HeaderText = "Brand Name"
        GunaDataGridView1.Columns("Founddate").HeaderText = "Date Found"
        GunaDataGridView1.Columns("placefound").HeaderText = "Place Found"
        GunaDataGridView1.Columns("Fitemphoto").HeaderText = "image"
        ' Set the cell style of the "Fitemphoto" column to a DataGridViewImageCell with Zoom layout
        Dim imageCellStyle As New DataGridViewCellStyle()
        imageCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        imageCellStyle.NullValue = Nothing
        imageCellStyle.Padding = New Padding(2)
        Dim imageCellTemplate As New DataGridViewImageCell()
        imageCellTemplate.ImageLayout = DataGridViewImageCellLayout.Zoom
        imageCellStyle.Font = New Font("Arial", 9)
        imageCellStyle.BackColor = Color.White
        imageCellStyle.SelectionBackColor = Color.FromArgb(128, 128, 255)
        imageCellStyle.SelectionForeColor = Color.Black
        imageCellStyle.WrapMode = DataGridViewTriState.[True]
        GunaDataGridView1.Columns("Fitemphoto").DefaultCellStyle = imageCellStyle
        GunaDataGridView1.Columns("Fitemphoto").CellTemplate = imageCellTemplate
        GunaDataGridView1.Columns("Fitemphoto").Width = 150
        GunaDataGridView1.RowTemplate.Height = 90
        If table.Rows.Count > 0 Then
            GunaDataGridView1.DataSource = table
        End If
        con.Close()
    End Sub




    Private Sub GunaButton4_Click(sender As Object, e As EventArgs) Handles GunaButton4.Click
        GunaPanel12.Show()
        GunaPanel5.Hide()

    End Sub


End Class











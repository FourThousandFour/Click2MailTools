'This file is part of Click2Mail Tool.
'
'Click2Mail is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'Click2Mail Tool is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with Click2Mail Too.  If not, see <http://www.gnu.org/licenses/>.

Imports System.Data.OleDb
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Public Class frm_sendSingleDocument
    Private XMLDOC As System.Xml.XmlDocument
    Friend _filename As String = String.Empty




    Private Sub readExcel()
        Try

        
        If Not Me.OpenFileDialog1.FileName = "" Then
            Dim fileName As String = OpenFileDialog1.FileName
            Dim connectionString As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName)
            Dim adapter As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" & Me.ListBox1.SelectedItem & "]", connectionString)
            Dim ds As New DataSet()

            adapter.Fill(ds, "mytable")
            Dim dt As DataTable = ds.Tables("mytable")

                Me.DataGridView1.DataSource = dt
                Me.ListBox2.Items.Clear()
                For Each c As DataColumn In dt.Columns
                    Me.ListBox2.Items.Add(c.ColumnName)
                Next
        End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function ListSheetInExcel(ByVal filePath As String) As List(Of String)
        Dim sbConnection As New OleDbConnectionStringBuilder()
        Dim strExtendedProperties As [String] = [String].Empty
        sbConnection.DataSource = filePath
        If Path.GetExtension(filePath).Equals(".xls") Then
            'for 97-03 Excel file

            sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0"
            'HDR=ColumnHeader,IMEX=InterMixed
            strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1"
        ElseIf Path.GetExtension(filePath).Equals(".xlsx") Then
            'for 2007 Excel file
retry:
            sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0"
            strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1"
        End If
        sbConnection.Add("Extended Properties", strExtendedProperties)
        Dim listSheet As New List(Of String)()
        Using conn As New OleDbConnection(sbConnection.ToString())
            Try
                conn.Open()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                GoTo retry
            End Try
            Dim dtSheet As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            For Each drSheet As DataRow In dtSheet.Rows
                If drSheet("TABLE_NAME").ToString().Contains("$") Then
                    'checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    listSheet.Add(drSheet("TABLE_NAME").ToString())
                End If
            Next
        End Using
        Return listSheet
    End Function

    Private Sub frm_sendSingleDocument_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _filename = "" Then

            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Excel 97-2000(*.xls)|*.xls|Excel 2003-2012(*.xlsx)|*.xlsx"
            OpenFileDialog1.ShowDialog()
            _filename = OpenFileDialog1.FileName
        End If
        If Not _filename = "" Then
            OpenFileDialog1.FileName = _filename
            Dim listsheet As List(Of String)
            listsheet = ListSheetInExcel(OpenFileDialog1.FileName)
            Me.ListBox1.DataSource = listsheet
            Me.ListBox1.SelectedIndex = 0
            readExcel()
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        readExcel()
    End Sub


    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim dt As DataTable = DataGridView1.DataSource
        Me.TextBox2.Text = Me.TextBox1.Text
        For Each c As DataColumn In dt.Columns
            Try
                If Not dt.Rows(1)(c) Is DBNull.Value Then
                    Me.TextBox2.Text = Replace(Me.TextBox2.Text, "{" & c.ColumnName & "}", dt.Rows(0)(c))
                Else
                    Me.TextBox2.Text = Replace(Me.TextBox2.Text, "{" & c.ColumnName & "}", "")
                End If

            Catch
            End Try
        Next
        TextBox2.Text = Regex.Replace(TextBox2.Text, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
    End Sub


    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim gr As New DataGridView
        gr = sender
        Select e.ColumnIndex
            Case Is > -1
                '  MsgBox(gr.Columns(e.ColumnIndex).Name)

        End Select

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If Me.ListBox2.SelectedItems.Count = 0 Then
            MsgBox("Please select a column from the list first")
            Return
        End If
        Me.TextBox1.Text = Me.TextBox1.Text & "{" & Me.ListBox2.SelectedItem & "}"

    End Sub



    Private Sub ListBox2_DoubleClick(sender As Object, e As EventArgs) Handles ListBox2.DoubleClick
        If Me.ListBox2.SelectedItems.Count = 0 Then
            MsgBox("Please select a column from the list first")
            Return
        End If
        Me.TextBox1.Text = Me.TextBox1.Text & "{" & Me.ListBox2.SelectedItem & "}"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.TextBox1.Text = ""
        Me.Close()
    End Sub
End Class
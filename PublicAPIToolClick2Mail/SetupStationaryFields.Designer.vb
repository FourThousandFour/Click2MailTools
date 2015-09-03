<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetupStationaryFields
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.btn_upload = New System.Windows.Forms.Button()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lb_SavedTemplates = New System.Windows.Forms.ListBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.lbl_Live = New System.Windows.Forms.Label()
        Me.lbl_CurrentTemplate = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_pages = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(736, 157)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(470, 442)
        Me.DataGridView2.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Window
        Me.Label2.Location = New System.Drawing.Point(12, 634)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1025, 67)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Page 1 of 1"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button5.Location = New System.Drawing.Point(515, 608)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(138, 23)
        Me.Button5.TabIndex = 21
        Me.Button5.Text = "Select File"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(227, 608)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(138, 23)
        Me.Button4.TabIndex = 20
        Me.Button4.Text = "PREVIOUS PAGE"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(371, 608)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(138, 23)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "Next Page"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 704)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(605, 137)
        Me.DataGridView1.TabIndex = 17
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.Location = New System.Drawing.Point(1056, 769)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(127, 23)
        Me.Button6.TabIndex = 25
        Me.Button6.Text = "VERIFY IT WORKS"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(15, 847)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(602, 23)
        Me.Button2.TabIndex = 24
        Me.Button2.Text = "Close Application"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(1056, 664)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 23)
        Me.Button1.TabIndex = 23
        Me.Button1.Text = "Save Template"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.Location = New System.Drawing.Point(1056, 605)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(129, 53)
        Me.Button7.TabIndex = 26
        Me.Button7.Text = "Click 2 Mail Template Settings"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'btn_upload
        '
        Me.btn_upload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_upload.Location = New System.Drawing.Point(1054, 798)
        Me.btn_upload.Name = "btn_upload"
        Me.btn_upload.Size = New System.Drawing.Size(127, 23)
        Me.btn_upload.TabIndex = 27
        Me.btn_upload.Text = "Upload Document"
        Me.btn_upload.UseVisualStyleBackColor = True
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(623, 704)
        Me.Label3.MinimumSize = New System.Drawing.Size(414, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(414, 39)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Select a Template"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lb_SavedTemplates
        '
        Me.lb_SavedTemplates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lb_SavedTemplates.FormattingEnabled = True
        Me.lb_SavedTemplates.Location = New System.Drawing.Point(623, 769)
        Me.lb_SavedTemplates.Name = "lb_SavedTemplates"
        Me.lb_SavedTemplates.Size = New System.Drawing.Size(414, 69)
        Me.lb_SavedTemplates.TabIndex = 29
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.Location = New System.Drawing.Point(738, 58)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(465, 93)
        Me.RichTextBox1.TabIndex = 28
        Me.RichTextBox1.Text = ""
        '
        'lbl_Live
        '
        Me.lbl_Live.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Live.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Live.Location = New System.Drawing.Point(736, 9)
        Me.lbl_Live.Name = "lbl_Live"
        Me.lbl_Live.Size = New System.Drawing.Size(465, 46)
        Me.lbl_Live.TabIndex = 31
        Me.lbl_Live.Text = "Processing Notes"
        Me.lbl_Live.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_CurrentTemplate
        '
        Me.lbl_CurrentTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_CurrentTemplate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CurrentTemplate.Location = New System.Drawing.Point(617, 841)
        Me.lbl_CurrentTemplate.Name = "lbl_CurrentTemplate"
        Me.lbl_CurrentTemplate.Size = New System.Drawing.Size(420, 42)
        Me.lbl_CurrentTemplate.TabIndex = 32
        Me.lbl_CurrentTemplate.Text = "Current Template:"
        Me.lbl_CurrentTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(1053, 832)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(88, 13)
        Me.LinkLabel1.TabIndex = 33
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Reload Template"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(1053, 847)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(78, 13)
        Me.LinkLabel2.TabIndex = 34
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Clear Template"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 618)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Page 1 of 1"
        '
        'lbl_pages
        '
        Me.lbl_pages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_pages.AutoSize = True
        Me.lbl_pages.Location = New System.Drawing.Point(3, 598)
        Me.lbl_pages.Name = "lbl_pages"
        Me.lbl_pages.Size = New System.Drawing.Size(62, 13)
        Me.lbl_pages.TabIndex = 36
        Me.lbl_pages.Text = "Page 1 of 1"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(0, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(730, 516)
        Me.Panel1.TabIndex = 16
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(352, 226)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'TrackBar1
        '
        Me.TrackBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar1.LargeChange = 20
        Me.TrackBar1.Location = New System.Drawing.Point(6, 542)
        Me.TrackBar1.Maximum = 300
        Me.TrackBar1.Minimum = 1
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(724, 45)
        Me.TrackBar1.SmallChange = 20
        Me.TrackBar1.TabIndex = 37
        Me.TrackBar1.Value = 100
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button8.Location = New System.Drawing.Point(659, 608)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(295, 23)
        Me.Button8.TabIndex = 38
        Me.Button8.Text = "Send Entire Document To Multiple Receipients"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(691, 747)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(346, 20)
        Me.TextBox1.TabIndex = 39
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(623, 743)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 26)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Filter"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel3
        '
        Me.LinkLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(1053, 860)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(85, 13)
        Me.LinkLabel3.TabIndex = 41
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Delete Template"
        '
        'SetupStationaryFields
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 878)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.lbl_pages)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lbl_CurrentTemplate)
        Me.Controls.Add(Me.lbl_Live)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lb_SavedTemplates)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.btn_upload)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.MinimumSize = New System.Drawing.Size(1149, 669)
        Me.Name = "SetupStationaryFields"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents btn_upload As System.Windows.Forms.Button
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lb_SavedTemplates As System.Windows.Forms.ListBox
    Public WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents lbl_Live As System.Windows.Forms.Label
    Friend WithEvents lbl_CurrentTemplate As System.Windows.Forms.Label
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents lbl_pages As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Public Sub New(Optional ByVal TemplatePath As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not TemplatePath = String.Empty Then
            Dim fileName As String = TemplatePath
            fileName = fileName.TrimEnd(New Char() {"/", "\"})
            _path = TemplatePath
        Else
            Dim mypath = "defaults.xml"
            If System.IO.File.Exists(mypath) Then
                ds1.Clear()
                ds1.ReadXml(mypath)
                Dim _dtt1 As DataTable
                _dtt1 = ds1.Tables(0)

                If _dtt1.Select("setting = true and fieldname = 'templatePath'").Count > 0 Then
                    _path = _dtt1.Select("setting = true and fieldname = 'templatePath'")(0)("misc")
                End If
            End If
        End If
    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_settings
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
        Me.tb_username = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tb_password = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cb_layout = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_productiontime = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cb_envelope = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_color = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cb_papertype = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cb_printoption = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cb_documentclass = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tb_raName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tb_raOrganization = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Tb_raAddress1 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tb_raAddress2 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tb_raCity = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tb_raState = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tb_PostalCode = New System.Windows.Forms.TextBox()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_mailclass = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Chkbox_NonStandardized = New System.Windows.Forms.CheckBox()
        Me.Chkbox_NonValidated = New System.Windows.Forms.CheckBox()
        Me.chk_OmitUSPSWarning = New System.Windows.Forms.CheckBox()
        Me.chk_TEST = New System.Windows.Forms.CheckBox()
        Me.tb_templatePath = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tb_username
        '
        Me.tb_username.Location = New System.Drawing.Point(144, 12)
        Me.tb_username.Name = "tb_username"
        Me.tb_username.Size = New System.Drawing.Size(239, 20)
        Me.tb_username.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Click2Mail Username:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Click2Mail Password:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_password
        '
        Me.tb_password.Location = New System.Drawing.Point(144, 38)
        Me.tb_password.Name = "tb_password"
        Me.tb_password.Size = New System.Drawing.Size(239, 20)
        Me.tb_password.TabIndex = 2
        Me.tb_password.UseSystemPasswordChar = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Layout"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_layout
        '
        Me.cb_layout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_layout.FormattingEnabled = True
        Me.cb_layout.Items.AddRange(New Object() {"Address on Separate Page", "Address on First Page", "Picture and Address First Page", "Address Back Page", "Same as a Stamp", "Single Sided Postcard", "Double Sided Postcard", "Double Sided Vertical Front Postcard", "Picture Postcard", "Vertical Split Postcard", "Self Mailer", "Self Mailer with Message Area on Address Panel"})
        Me.cb_layout.Location = New System.Drawing.Point(144, 88)
        Me.cb_layout.Name = "cb_layout"
        Me.cb_layout.Size = New System.Drawing.Size(239, 21)
        Me.cb_layout.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 23)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Production Time"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_productiontime
        '
        Me.cb_productiontime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_productiontime.FormattingEnabled = True
        Me.cb_productiontime.Items.AddRange(New Object() {"Next Day", "Within 3 Days", "Within 7 Days"})
        Me.cb_productiontime.Location = New System.Drawing.Point(144, 111)
        Me.cb_productiontime.Name = "cb_productiontime"
        Me.cb_productiontime.Size = New System.Drawing.Size(239, 21)
        Me.cb_productiontime.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 23)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Envelope"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_envelope
        '
        Me.cb_envelope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_envelope.FormattingEnabled = True
        Me.cb_envelope.Items.AddRange(New Object() {"#10 Double Window", "Flat Envelope", "#10 Open Window Envelope"})
        Me.cb_envelope.Location = New System.Drawing.Point(144, 135)
        Me.cb_envelope.Name = "cb_envelope"
        Me.cb_envelope.Size = New System.Drawing.Size(239, 21)
        Me.cb_envelope.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 158)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(126, 23)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Color"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_color
        '
        Me.cb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_color.FormattingEnabled = True
        Me.cb_color.Items.AddRange(New Object() {"Black and White", "Full Color"})
        Me.cb_color.Location = New System.Drawing.Point(144, 158)
        Me.cb_color.Name = "cb_color"
        Me.cb_color.Size = New System.Drawing.Size(239, 21)
        Me.cb_color.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(12, 181)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 23)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Paper Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_papertype
        '
        Me.cb_papertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_papertype.FormattingEnabled = True
        Me.cb_papertype.Items.AddRange(New Object() {"White 24#", "Off-White 28#", "Canary 24#", "High Quality Paper", "White Matte", "White Matte with Gloss UV Finish - MS"})
        Me.cb_papertype.Location = New System.Drawing.Point(144, 181)
        Me.cb_papertype.Name = "cb_papertype"
        Me.cb_papertype.Size = New System.Drawing.Size(239, 21)
        Me.cb_papertype.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(12, 204)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(126, 23)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Print Option"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_printoption
        '
        Me.cb_printoption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_printoption.FormattingEnabled = True
        Me.cb_printoption.Items.AddRange(New Object() {"Printing One side", "Printing both sides"})
        Me.cb_printoption.Location = New System.Drawing.Point(144, 204)
        Me.cb_printoption.Name = "cb_printoption"
        Me.cb_printoption.Size = New System.Drawing.Size(239, 21)
        Me.cb_printoption.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(12, 61)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(126, 23)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Document Class"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_documentclass
        '
        Me.cb_documentclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_documentclass.FormattingEnabled = True
        Me.cb_documentclass.Items.AddRange(New Object() {"Letter 8.5 x 11", "Letter 8.5 x 14", "Postcard 3.5 x 5", "Postcard 4.25 x 6", "Postcard 6 x 11", "Flyer 8.5 x 11", "Brochure 11 x 8.5"})
        Me.cb_documentclass.Location = New System.Drawing.Point(144, 61)
        Me.cb_documentclass.Name = "cb_documentclass"
        Me.cb_documentclass.Size = New System.Drawing.Size(239, 21)
        Me.cb_documentclass.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(425, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(189, 23)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Return Address Name"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_raName
        '
        Me.tb_raName.Location = New System.Drawing.Point(620, 21)
        Me.tb_raName.Name = "tb_raName"
        Me.tb_raName.Size = New System.Drawing.Size(239, 20)
        Me.tb_raName.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(425, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(189, 23)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Return Address Organization"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_raOrganization
        '
        Me.tb_raOrganization.Location = New System.Drawing.Point(620, 47)
        Me.tb_raOrganization.Name = "tb_raOrganization"
        Me.tb_raOrganization.Size = New System.Drawing.Size(239, 20)
        Me.tb_raOrganization.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(425, 70)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(189, 23)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Return Address - Address 1"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Tb_raAddress1
        '
        Me.Tb_raAddress1.Location = New System.Drawing.Point(620, 73)
        Me.Tb_raAddress1.Name = "Tb_raAddress1"
        Me.Tb_raAddress1.Size = New System.Drawing.Size(239, 20)
        Me.Tb_raAddress1.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(425, 93)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(189, 23)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Return Address - Address 2"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_raAddress2
        '
        Me.tb_raAddress2.Location = New System.Drawing.Point(620, 96)
        Me.tb_raAddress2.Name = "tb_raAddress2"
        Me.tb_raAddress2.Size = New System.Drawing.Size(239, 20)
        Me.tb_raAddress2.TabIndex = 26
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(425, 116)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(189, 23)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Return Address City"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_raCity
        '
        Me.tb_raCity.Location = New System.Drawing.Point(620, 122)
        Me.tb_raCity.Name = "tb_raCity"
        Me.tb_raCity.Size = New System.Drawing.Size(239, 20)
        Me.tb_raCity.TabIndex = 28
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(425, 142)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(189, 23)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Return Address State"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_raState
        '
        Me.tb_raState.Location = New System.Drawing.Point(620, 145)
        Me.tb_raState.Name = "tb_raState"
        Me.tb_raState.Size = New System.Drawing.Size(26, 20)
        Me.tb_raState.TabIndex = 30
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(425, 165)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(189, 23)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Return Address Postal Code"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_PostalCode
        '
        Me.tb_PostalCode.Location = New System.Drawing.Point(620, 168)
        Me.tb_PostalCode.Name = "tb_PostalCode"
        Me.tb_PostalCode.Size = New System.Drawing.Size(83, 20)
        Me.tb_PostalCode.TabIndex = 32
        '
        'btn_save
        '
        Me.btn_save.Location = New System.Drawing.Point(56, 277)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(316, 23)
        Me.btn_save.TabIndex = 34
        Me.btn_save.Text = "Update Template"
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 227)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 23)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Document Class"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cb_mailclass
        '
        Me.cb_mailclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_mailclass.FormattingEnabled = True
        Me.cb_mailclass.Items.AddRange(New Object() {"First Class", "Standard"})
        Me.cb_mailclass.Location = New System.Drawing.Point(144, 227)
        Me.cb_mailclass.Name = "cb_mailclass"
        Me.cb_mailclass.Size = New System.Drawing.Size(239, 21)
        Me.cb_mailclass.TabIndex = 36
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(477, 280)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(316, 23)
        Me.Button1.TabIndex = 37
        Me.Button1.Text = "Save as Program Defaults"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Chkbox_NonStandardized
        '
        Me.Chkbox_NonStandardized.AutoSize = True
        Me.Chkbox_NonStandardized.Location = New System.Drawing.Point(484, 210)
        Me.Chkbox_NonStandardized.Name = "Chkbox_NonStandardized"
        Me.Chkbox_NonStandardized.Size = New System.Drawing.Size(219, 17)
        Me.Chkbox_NonStandardized.TabIndex = 39
        Me.Chkbox_NonStandardized.Text = "Omit Non USPS Standardized Addresses"
        Me.Chkbox_NonStandardized.UseVisualStyleBackColor = True
        '
        'Chkbox_NonValidated
        '
        Me.Chkbox_NonValidated.AutoSize = True
        Me.Chkbox_NonValidated.Checked = True
        Me.Chkbox_NonValidated.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Chkbox_NonValidated.Location = New System.Drawing.Point(484, 226)
        Me.Chkbox_NonValidated.Name = "Chkbox_NonValidated"
        Me.Chkbox_NonValidated.Size = New System.Drawing.Size(309, 17)
        Me.Chkbox_NonValidated.TabIndex = 40
        Me.Chkbox_NonValidated.Text = "Omit Non Validated Addresses(Addresses with missing parts)"
        Me.Chkbox_NonValidated.UseVisualStyleBackColor = True
        '
        'chk_OmitUSPSWarning
        '
        Me.chk_OmitUSPSWarning.AutoSize = True
        Me.chk_OmitUSPSWarning.Location = New System.Drawing.Point(484, 194)
        Me.chk_OmitUSPSWarning.Name = "chk_OmitUSPSWarning"
        Me.chk_OmitUSPSWarning.Size = New System.Drawing.Size(180, 17)
        Me.chk_OmitUSPSWarning.TabIndex = 41
        Me.chk_OmitUSPSWarning.Text = "Omit USPS Warning Addresses?"
        Me.chk_OmitUSPSWarning.UseVisualStyleBackColor = True
        '
        'chk_TEST
        '
        Me.chk_TEST.AutoSize = True
        Me.chk_TEST.Checked = True
        Me.chk_TEST.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_TEST.Location = New System.Drawing.Point(56, 253)
        Me.chk_TEST.Name = "chk_TEST"
        Me.chk_TEST.Size = New System.Drawing.Size(195, 17)
        Me.chk_TEST.TabIndex = 42
        Me.chk_TEST.Text = "Use Test Mode with Staging server."
        Me.chk_TEST.UseVisualStyleBackColor = True
        '
        'tb_templatePath
        '
        Me.tb_templatePath.Location = New System.Drawing.Point(620, 254)
        Me.tb_templatePath.Name = "tb_templatePath"
        Me.tb_templatePath.Size = New System.Drawing.Size(239, 20)
        Me.tb_templatePath.TabIndex = 43
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(481, 252)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(126, 23)
        Me.Label18.TabIndex = 44
        Me.Label18.Text = "Default Template Path"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(425, 303)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(412, 23)
        Me.Label19.TabIndex = 45
        Me.Label19.Text = "Note, saving as defaults will populate these values automatically for new Templat" & _
    "es"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frm_settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 325)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.tb_templatePath)
        Me.Controls.Add(Me.chk_TEST)
        Me.Controls.Add(Me.chk_OmitUSPSWarning)
        Me.Controls.Add(Me.Chkbox_NonValidated)
        Me.Controls.Add(Me.Chkbox_NonStandardized)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cb_mailclass)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.tb_PostalCode)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.tb_raState)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.tb_raCity)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.tb_raAddress2)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Tb_raAddress1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.tb_raOrganization)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.tb_raName)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cb_documentclass)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cb_printoption)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cb_papertype)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cb_color)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cb_envelope)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cb_productiontime)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cb_layout)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_password)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tb_username)
        Me.Name = "frm_settings"
        Me.Text = "frm_settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb_username As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tb_password As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cb_layout As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_productiontime As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cb_envelope As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cb_color As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cb_papertype As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cb_printoption As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cb_documentclass As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tb_raName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tb_raOrganization As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Tb_raAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tb_raAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tb_raCity As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tb_raState As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tb_PostalCode As System.Windows.Forms.TextBox
    Friend WithEvents btn_save As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_mailclass As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Chkbox_NonStandardized As System.Windows.Forms.CheckBox
    Friend WithEvents Chkbox_NonValidated As System.Windows.Forms.CheckBox
    Friend WithEvents chk_OmitUSPSWarning As System.Windows.Forms.CheckBox
    Friend WithEvents chk_TEST As System.Windows.Forms.CheckBox
    Friend WithEvents tb_templatePath As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class

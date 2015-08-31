'This file is part of Click2Mail API Tool.
'
'Click2Mail is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'Click2Mail API Tool is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with Click2Mail Too.  If not, see <http://www.gnu.org/licenses/>.

Public Class frm_settings
    Private ds As New DataSet("StationaryDataset")
    Private _TemplatePath As String = ""
    Private _dtt As DataTable
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If Me.tb_templatePath.Text <> _TemplatePath Then
            Dim y As MsgBoxResult = MsgBox("You have changed the Default Template Path and have not saved it.  Do you want to save this new Template Path?  A Template path is used mostly if you are storing on a network location.  Note this will overwrite all defaults to this, if this is not what you want click NO then close this and click CLEAR TEMPLATE to reload defaults", MsgBoxStyle.YesNo)
            If y = MsgBoxResult.Yes Then
                saveDefaults()
            End If
        End If
        MsgBox("This template will UPDATE for this run, click SAVE TEMPLATE to actually save it for future uses when this closes.")
        Me.Close()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        saveDefaults()
    End Sub
    Private Sub savexml()
        If ds.Tables.Count = 0 Then
            ds.Tables.Add(_dtt)
        End If
        Me._dtt.WriteXml("defaults.xml")
        MsgBox("Defaults Saved")
    End Sub
    Private Sub frm_settings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mypath = "defaults.xml"

        If System.IO.File.Exists(mypath) Then
            ds.ReadXml(mypath)
            _dtt = ds.Tables(0)
            If tb_password.Text = "" And tb_username.Text = "" And cb_layout.SelectedItem = "" And Me.cb_productiontime.SelectedItem = "" And Me.cb_envelope.SelectedItem = "" And Me.cb_color.SelectedItem = "" And Me.cb_papertype.SelectedItem = "" And Me.cb_papertype.SelectedItem = "" And Me.tb_raName.Text = "" And Me.tb_raName.Text = "" Then
                Me.tb_username.Text = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")
                Me.tb_password.Text = _dtt.Select("setting = true and fieldname = 'password'")(0)("misc")
                Me.cb_documentclass.SelectedItem = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")
                Me.cb_layout.SelectedItem = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
                Me.cb_productiontime.SelectedItem = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
                Me.cb_envelope.SelectedItem = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")
                Me.cb_color.SelectedItem = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
                Me.cb_papertype.SelectedItem = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
                Me.cb_printoption.SelectedItem = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
                Me.cb_mailclass.SelectedItem = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
                Me.tb_raName.Text = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
                Me.tb_raOrganization.Text = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
                Me.Tb_raAddress1.Text = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
                Me.tb_raAddress2.Text = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
                Me.tb_raCity.Text = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
                Me.tb_raState.Text = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
                Me.tb_PostalCode.Text = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
                Me.Chkbox_NonStandardized.Checked = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
                Me.Chkbox_NonValidated.Checked = _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc")
                Me.chk_OmitUSPSWarning.Checked = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")
                Me.chk_TEST.Checked = _dtt.Select("setting = true and fieldname = 'testMode'")(0)("misc")
                If _dtt.Select("setting = true and fieldname = 'templatePath'").Count > 0 Then
                    Me.tb_templatePath.Text = _dtt.Select("setting = true and fieldname = 'templatePath'")(0)("misc")
                End If
            Else
                Me.tb_templatePath.Visible = False
                Me.Label18.Visible = False
                If _dtt.Select("setting = true and fieldname = 'templatePath'").Count > 0 Then
                    Me.tb_templatePath.Text = _dtt.Select("setting = true and fieldname = 'templatePath'")(0)("misc")
                End If
            End If
        End If
    End Sub
    Private Sub saveDefaults()
        _dtt = New DataTable("FixedFields")


        Dim c As DataColumn
        c = New DataColumn("fieldname", System.Type.GetType("System.String"))
        _dtt.Columns.Add(c)
        c.DefaultValue = 0
        c = New DataColumn("x", System.Type.GetType("System.Int32"))
        c.DefaultValue = 0
        _dtt.Columns.Add(c)
        c = New DataColumn("y", System.Type.GetType("System.Int32"))
        c.DefaultValue = 0
        _dtt.Columns.Add(c)
        c = New DataColumn("width", System.Type.GetType("System.Int32"))
        c.DefaultValue = 0
        _dtt.Columns.Add(c)

        c = New DataColumn("height", System.Type.GetType("System.Int32"))
        c.DefaultValue = 0
        _dtt.Columns.Add(c)
        c = New DataColumn("misc", System.Type.GetType("System.String"))
        c.DefaultValue = ""
        _dtt.Columns.Add(c)
        c = New DataColumn("rowid", System.Type.GetType("System.Int32"))
        c.DefaultValue = 0
        _dtt.Columns.Add(c)
        c = New DataColumn("Visible", System.Type.GetType("System.Boolean"))
        c.DefaultValue = False
        _dtt.Columns.Add(c)
        c = New DataColumn("Setting", System.Type.GetType("System.Boolean"))
        c.DefaultValue = False
        _dtt.Columns.Add(c)
        Dim i As Integer = 0

        Dim dr As DataRow = _dtt.NewRow()

        dr("fieldname") = "username"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1
        dr = _dtt.NewRow()
        dr("fieldname") = "password"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()
        dr("fieldname") = "appSignature"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "poDocumentClass"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "poLayout"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "poProductionTime"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "poEnvelope"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "poColor"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "poPaperType"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1


        dr = _dtt.NewRow()

        dr("fieldname") = "poPrintOption"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1


        dr = _dtt.NewRow()

        dr("fieldname") = "poMailClass"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "raName"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1
        dr = _dtt.NewRow()

        dr("fieldname") = "raOrganization"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1
        dr = _dtt.NewRow()

        dr("fieldname") = "raAddress1"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "raAddress2"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "raCity"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "raState"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "raPostalCode"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        _dtt.Rows.Add(dr)


        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "omitNonStandard"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True

        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "omitNonStandardWarning"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True

        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "omitNonValidated"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True

        _dtt.Rows.Add(dr)

        dr = _dtt.NewRow()

        dr("fieldname") = "testMode"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True

        _dtt.Rows.Add(dr)


        dr = _dtt.NewRow()

        dr("fieldname") = "templatePath"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True

        _dtt.Rows.Add(dr)

        _dtt.Select("setting = true and fieldname = 'username'")(0)("misc") = Me.tb_username.Text
        _dtt.Select("setting = true and fieldname = 'password'")(0)("misc") = Me.tb_password.Text
        _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc") = Me.cb_documentclass.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc") = Me.cb_layout.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc") = Me.cb_productiontime.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc") = Me.cb_envelope.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc") = Me.cb_color.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc") = Me.cb_papertype.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") = Me.cb_printoption.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc") = Me.cb_mailclass.SelectedItem
        _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = Me.tb_raName.Text
        _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = Me.tb_raOrganization.Text
        _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc") = Me.Tb_raAddress1.Text
        _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc") = Me.tb_raAddress2.Text
        _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc") = Me.tb_raCity.Text
        _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc") = Me.tb_raState.Text
        _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc") = Me.tb_PostalCode.Text
        _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc") = Me.Chkbox_NonStandardized.Checked
        _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc") = Me.Chkbox_NonValidated.Checked
        _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc") = Me.chk_OmitUSPSWarning.Checked
        _dtt.Select("setting = true and fieldname = 'testMode'")(0)("misc") = Me.chk_TEST.Checked

        Dim fileName As String = Me.tb_templatePath.Text
        fileName = fileName.TrimEnd(New Char() {"/", "\"})
        _dtt.Select("setting = true and fieldname = 'templatePath'")(0)("misc") = fileName
        savexml()
    End Sub
End Class
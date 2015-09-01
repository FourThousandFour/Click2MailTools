
'# Click2MailTools
'
'This file is part of Click2Mail API Tool, Developed by Vincent D. Senese.
'
'Click2Mail API Tool is free software: you can redistribute it and/or modify
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


Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.IO
Imports System.Xml
Imports System
Imports Ghostscript.NET
Imports Ghostscript.NET.Rasterizer
Imports System.Collections
Imports System.Net
Imports System.Text
Imports System.Web
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices

Public Class SetupStationaryFields
    Private _keepopen As Boolean = False
    Private _xlsfilename As String = String.Empty
    Private _xtemplate As String = String.Empty
    Private _md As DateTime
    Private startover As Integer = 1
    Private _dtt As DataTable
    Private _mode As Integer = 1
    Public mode As frm_clicktomail.mode
    Public sentXML As String
    Private oversized As String = "Flat Envelope"
    Private gvi As GhostscriptVersionInfo
    Private rasterizer As GhostscriptRasterizer
    Private Rect As Rectangle
    Private RectValidate As Rectangle
    Private _DT As New DataTable
    Private Keeplast As Boolean = False
    Private zoom As Decimal = 1.0
    Private ds As New DataSet("StationaryDataset")
    Private ds1 As New DataSet("StationaryDataset")
    Private ww, hh, tt, ll As Decimal
    Dim mouseb As Boolean = False
    Private loadedbool As Boolean = False
    Private _CurrentCount = 0
    Public XMLDOC As System.Xml.XmlDocument
    Private _validatetext As String = ""
    Public _sourcefilename As String = ""
    Private _CurrentTemplate As String = ""
    Private badaddress As String = ""
    Private _path As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "\"
    Private Delegate Sub updatetext(ByVal t As String)
    Private Delegate Sub updatert(ByVal t As XmlDocument, ba As String)
    Private Delegate Sub updatecomplete()
    Private Delegate Sub updatedatagrid(aic As addresscollection)
    Private sDLLPath As String = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "gsdll32.dll")
    Private sDLLPath64 As String = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "gsdll64.dll")
    Private _originalimage As Image
    Private _EncryptionKey As String = "kmjfds(#1231SDSA()#rt32geswfkjFJDSKFJDSFd"
    Private _aiSingle As addressitem
    Public _hideform As Boolean = False
    Public iscomplete As Boolean = False
    Public merror As String = ""
    Private img As System.Drawing.Image
    Private CurrentPage As Integer = 0
    Private bimg As System.Drawing.Bitmap
    Private _file As String = ""
    Private _startuploadwhendone = False
    Public Property keepopen As Boolean
        Get
            Return _keepopen
        End Get
        Set(value As Boolean)
            _keepopen = value
        End Set
    End Property


    Public ReadOnly Property dt() As DataTable
        Get
            Return _DT
        End Get
    End Property
    Public ReadOnly Property CurrentTemplateFile As String
        Get
            Dim f As New FileInfo(_CurrentTemplate)
            Return f.Name
        End Get
    End Property
#Region "Secure Password Storage"
    Private Function Encrypt(clearText As String) As String


        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(_EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function
    Private Function Decrypt(cipherText As String) As String

        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(_EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function
#End Region
#Region "Image Manipulation"
    Private Sub TrackBar1_Scroll_1(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Me.PictureBox1.Image = ZoomImage(Me.TrackBar1.Value / 100)
    End Sub
    Public Function ZoomImage(ByVal ZoomFactor As Double) As Image
        Return New Bitmap(_originalimage, Int(_originalimage.Width * ZoomFactor), Int(_originalimage.Height * ZoomFactor))
    End Function
    Private Function getImageFromFile(filename As String, page As Integer, dpi As Integer) As System.Drawing.Image

        Dim dpi_x As Integer = dpi
        Dim dpi_y As Integer = dpi
        Dim i As Integer = 1

        Dim img As Image = rasterizer.GetPage(dpi_x, dpi_y, page + 1)
        _originalimage = img
        ' System.Drawing.Image obtained. Now it can be used at will.
        ' Simply save it to storage as an example.
        Return img


    End Function
#End Region
#Region "Returning Thread Calls"
    Private Sub updatedatagridonMail(aic As addresscollection)
        Me.DataGridView2.DataSource = aic
        updategrid(DataGridView2)
    End Sub
    Private Sub updatecompleted()
        Me.Button1.Enabled = True
        Me.Button2.Enabled = True
        Me.Button3.Enabled = True
        Me.Button4.Enabled = True
        Me.Button5.Enabled = True
        Me.Button6.Enabled = True
        Me.Button7.Enabled = True
        Me.btn_upload.Enabled = True
        Me.ControlBox = True
        If _hideform = True Then
            Dim ai As addressitem = CType(DataGridView2.DataSource, addresscollection).Item(0)
            If ai.ommitted = True Then
                MsgBox("Correct the address on the right")
                Me.WindowState = FormWindowState.Normal
                'Me.TopMost = True
                rasterizer.Close()

                rasterizer.Open(_sourcefilename, gvi, False)
                Return
            End If
        End If
        If _startuploadwhendone = True Then

            _startuploadwhendone = False
            startupload()
        Else
            MsgBox("Completed")
        End If

    End Sub
    Private Sub updatelabel1text(t As String)
        Me.Label2.Text = t
    End Sub
    Private Sub updaterichtext(xml As XmlDocument, badaddresses As String)
        Me.RichTextBox1.Text = badaddresses '& Beautify(xml)
        XMLDOC = xml
    End Sub
#End Region
#Region "Drawing and Mapping Mouse Click"
    Private Sub drawrectangles()
        If _dtt.Rows(0)("width") > 0 Then

            Rect.Location = New System.Drawing.Point(_dtt.Rows(0)("x"), Math.Abs(PictureBox1.Height - _dtt.Rows(0)("y") - _dtt.Rows(0)("height")))
            Rect.Size = New Size(_dtt.Rows(0)("width"), _dtt.Rows(0)("height"))
            mouseb = True
            Me.PictureBox1.Invalidate()
        End If
        If _dtt.Rows(1)("width") > 0 Then

            RectValidate.Location = New System.Drawing.Point(_dtt.Rows(1)("x"), Math.Abs(PictureBox1.Height - _dtt.Rows(1)("y") - _dtt.Rows(1)("height")))
            RectValidate.Size = New Size(_dtt.Rows(1)("width"), _dtt.Rows(1)("height"))
            mouseb = True
            Me.PictureBox1.Invalidate()
        End If
    End Sub
    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        _md = Now
        If TrackBar1.Value <> 100 Then
            MsgBox("The Zoom must be set to defaul when selecting your template")
            Me.TrackBar1.Value = 100
            Me.PictureBox1.Image = ZoomImage(Me.TrackBar1.Value / 100)
        End If
        RectValidate.Width = 0
        RectValidate.Height = 0
        Rect.Location = e.Location
        Rect.Size = New Size(e.X - Rect.X, e.Y - Rect.Y)


        mouseb = True
    End Sub
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If e.Button = MouseButtons.Left Then
            Rect.Size = New Size(e.X - Rect.X, e.Y - Rect.Y)


            Me.PictureBox1.Invalidate()
        End If
        Me.Label1.Text = "x : " & Rect.X & " y : " & System.Math.Abs(Me.PictureBox1.Height - Rect.Y) & " Width : " & Rect.Width & " Heigt : " & Rect.Height
    End Sub
    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Dim x As New DateTime

        If _md <= "1/1/1900" Then
            Return
        End If
        'x = Now.AddMilliseconds(-200)
        'If x < _md Then
        '        MsgBox("This ")
        'Return
        'End If
        Dim L, T, W, H As Integer
        L = Rect.X : T = Rect.Y
        W = Rect.Width : H = Rect.Height
        If W < 0 Then
            'L += W : W = -W
            Exit Sub


        End If
        If H < 0 Then
            Exit Sub
        End If
        If Me.Rect.Width > 20 Then
            Me.loadedbool = True
        End If
        If _mode = 1 Then
            If Me.loadedbool Then
                If MsgBox("Is this the area you want to scan?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'If startover = 1 Then
                    ' _dtt = Nothing
                    'setuptable()
                    'Me.DataGridView1.DataSource = _dtt
                    '  startover = 0
                    'End If
                    '_DT.Rows.Add(dr)
                    'Me.DataGridView1.DataSource = _DT
                    Me.ExtractTextFromRegionOfPdf(Me.OpenFileDialog1.FileName)
                Else
                    '            Rect.Width = -1
                    PictureBox1.Invalidate()
                End If
            End If
        End If

        '     loadtemplate()
        '        mouseb = False
    End Sub
    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint

        Dim L, T, W, H As Integer
        L = Rect.X : T = Rect.Y
        W = Rect.Width : H = Rect.Height
        If W < 0 Then
            'L += W : W = -W
            Exit Sub


        End If
        If H < 0 Then
            '        T += H : H = -H
            Exit Sub
        End If

        Dim L1, T1, W1, H1 As Integer
        L1 = RectValidate.X : T1 = RectValidate.Y
        W1 = RectValidate.Width : H1 = RectValidate.Height
        If W1 < 0 Then
            'L += W : W = -W
            Exit Sub


        End If
        If H1 < 0 Then
            '        T += H : H = -H
            Exit Sub
        End If
        If mouseb Then
            e.Graphics.DrawRectangle(Pens.Blue, New Rectangle(L, T, W, H))
            e.Graphics.DrawRectangle(Pens.Green, New Rectangle(L1, T1, W1, H1))
        End If




    End Sub
#End Region
#Region "ParseAddresses"
    Private Function parseaddress(ByVal s As String, validatetext As String, ByRef batchnode As System.Xml.XmlNode, ByVal startpage As Integer, ByRef endpage As System.Xml.XmlNode, ByRef envelope As System.Xml.XmlNode, ByRef startingpage As System.Xml.XmlNode, ByRef ai As addressitem) As Boolean

        s = Regex.Replace(s, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
        Dim parts As String() = s.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)


        'Validate Text
        If _dtt.Rows(1)("width") > 0 Then 'We're looking for blank
            If _validatetext = "" And Not _validatetext = validatetext Then
                Return False 'It was not blank
            End If
            validatetext = validatetext.Replace(vbCr, " ").Replace(vbLf, " ").Replace("  ", " ")
            Dim v As String = Microsoft.VisualBasic.Left(Trim(validatetext).ToUpper, _validatetext.Length + 1)

            If Not Trim(v) = Trim(_validatetext).ToUpper And Not batchnode Is Nothing Then
                Return False
            End If
        End If


        'It is a first page, but now lets see if there is a valid address
        'If address1 validates
        '

        If batchnode Is Nothing Then
            XMLDOC = New System.Xml.XmlDocument
            Dim docNode As System.Xml.XmlNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            XMLDOC.AppendChild(docNode)
            batchnode = XMLDOC.CreateElement("batch")
            Dim ns As System.Xml.XmlNode
            ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/")
            ns.Value = "http://www.w3.org/2001/XMLSchema-instance"
            batchnode.Attributes.Append(ns)
            XMLDOC.AppendChild(batchnode)

            Dim un As Xml.XmlNode = XMLDOC.CreateElement("username")
            batchnode.AppendChild(un)
            un.InnerText = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")

            Dim pw As Xml.XmlNode = XMLDOC.CreateElement("password")
            pw.InnerText = Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc"))
            batchnode.AppendChild(pw)
            Dim fn As Xml.XmlNode = XMLDOC.CreateElement("filename")
            fn.InnerText = _sourcefilename
            batchnode.AppendChild(fn)
            Dim as1 As Xml.XmlNode = XMLDOC.CreateElement("appSignature")
            batchnode.AppendChild(as1)
            as1.InnerText = _dtt.Select("setting = true and fieldname = 'appSignature'")(0)("misc")

        End If



        If Not endpage Is Nothing Then
            endpage.InnerText = startpage - 1
            If "Printing One side" = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") Then
                '28 - 23 is 5, but that is 6 pages
                If endpage.InnerText - startingpage.InnerText + 1 > 5 Then
                    envelope.InnerText = oversized
                End If
            Else
                If endpage.InnerText - startingpage.InnerText + 1 > 10 Then
                    envelope.InnerText = oversized
                End If
            End If
        End If
        If Not ai Is Nothing Then
            ai.endpage = startpage - 1
        End If
        endpage = Nothing

        ai = New addressitem
        ai.row = startpage
        ai.startpage = startpage
        ai.validatedStatus = False
        ai.uspsStatus = "Not Processed"


        '   If startpage = 8 Then
        'Console.Write(Trim(parts(parts.Length - 1)))
        'End If
        Dim rp As New Regex("((?:\w|\s)+),\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\d{5}(-\d{4}|\d{4}|$)))$")

        If Not rp.IsMatch(Trim(parts(parts.Length - 1))) Then
            If _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc") = True Then
                Console.WriteLine(startpage & " is not a valid address.")
                badaddress &= "Document starting on page " & startpage & " is not a valid address, this was omitted." & vbCrLf
                ai.ommitted = True
                Return True
            Else
                ai.validatedStatus = False
            End If
        Else
            ai.validatedStatus = True
        End If



        Dim AddressName As String = parts(0)
        Dim Addressorganization As String = String.Empty
        Dim Address1 As String = String.Empty
        Dim Address2 As String = String.Empty
        Dim Address3 As String = String.Empty
        Dim City As String = String.Empty
        Dim State As String = String.Empty
        Dim Zip As String = String.Empty
        ai.nname = AddressName


        Try
            Dim Words() As String = Trim(parts(parts.Length - 1)).Split(" "c)

            'GRAB ZIP (ALWAYS LAST IN THE ARRAY)
            Zip = Words(Words.Length - 1)
            'GRAB STATE ABBR (ALWAYS SECOND TO LAST IN ARRAY)
            State = Words(Words.Length - 2)
            'LOOP REMAINING ARRAY ELEMENT TO FORM CITY NAME 
            '(THIS WORKS FOR ANY NUMBER OF WORDS IN CITY NAME)
            For i As Integer = 0 To Words.Length - 3
                City &= Words(i) & " " 'ADD SPACE BACK IN BETWEEN WORDS
            Next
            City = Replace(City, ",", "")
        Catch
        End Try


        Dim checkstandard As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
        Dim checkstandardwarning As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")

        'We only want last two lines
        If parts.Length >= 5 Then
            Address1 = Trim(parts(parts.Length - 4))
            Address2 = Trim(parts(parts.Length - 3))
            Address3 = Trim(parts(parts.Length - 2))

            ai.norganization = Address1
            ai.Address1 = Address2
            ai.Address2 = Address3
        ElseIf parts.Length = 4 Then
            Address1 = Trim(parts(parts.Length - 3))
            Address2 = Trim(parts(parts.Length - 2))
            ai.Address1 = Address1
            ai.Address2 = Address2
        ElseIf parts.Length = 3 Then
            Address1 = Trim(parts(parts.Length - 2))
            ai.Address1 = Address1
        End If


        ai.city = City
        ai.state = State
        ai.zip5 = Zip

        standardizeaddress(ai)

        If Not ai.uspsStatus = "1" Then
            If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                Console.WriteLine(startpage & " is not a valid address.")
                badaddress &= "Document starting on page " & startpage & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                ai.ommitted = True
                Return True
            ElseIf ai.uspsStatus = "2" Then
                Console.WriteLine(startpage & " is allowed through even though there is a warning.")
            ElseIf checkstandard = True Then
                Console.WriteLine(startpage & " is not a valid address.")
                badaddress &= "Document starting on page " & startpage & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf
                ai.ommitted = True
                Return True
            End If
        Else

            Address1 = ai.Address1
            Address2 = ai.Address2
            City = ai.city
            State = ai.state
            Zip = ai.zip5 & "-" & ai.zip4
        End If





        Dim job As Xml.XmlNode = XMLDOC.CreateElement("job")
        batchnode.AppendChild(job)

        startingpage = XMLDOC.CreateElement("startingPage")
        job.AppendChild(startingpage)
        startingpage.InnerText = startpage
        endpage = XMLDOC.CreateElement("endingPage")
        job.AppendChild(endpage)
        Dim printProductionOptions As Xml.XmlNode = XMLDOC.CreateElement("printProductionOptions")
        job.AppendChild(printProductionOptions)
        Dim docclass As Xml.XmlNode = XMLDOC.CreateElement("documentClass")
        docclass.InnerText = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")
        printProductionOptions.AppendChild(docclass)

        Dim layout As Xml.XmlNode = XMLDOC.CreateElement("layout")
        layout.InnerText = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
        printProductionOptions.AppendChild(layout)

        Dim productiontime As Xml.XmlNode = XMLDOC.CreateElement("productionTime")
        productiontime.InnerText = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
        printProductionOptions.AppendChild(productiontime)

        envelope = XMLDOC.CreateElement("envelope")
        envelope.InnerText = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")
        printProductionOptions.AppendChild(envelope)

        Dim color As Xml.XmlNode = XMLDOC.CreateElement("color")
        color.InnerText = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
        printProductionOptions.AppendChild(color)

        Dim papertype As Xml.XmlNode = XMLDOC.CreateElement("paperType")
        papertype.InnerText = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
        printProductionOptions.AppendChild(papertype)


        Dim printoption As Xml.XmlNode = XMLDOC.CreateElement("printOption")
        printoption.InnerText = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
        printProductionOptions.AppendChild(printoption)

        Dim mailclass As Xml.XmlNode = XMLDOC.CreateElement("mailClass")
        mailclass.InnerText = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
        printProductionOptions.AppendChild(mailclass)


        If Not _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = "" Or Not _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = "" Then
            Dim returnAddress As Xml.XmlNode = XMLDOC.CreateElement("returnAddress")
            job.AppendChild(returnAddress)

            Dim raname As Xml.XmlNode = XMLDOC.CreateElement("name")
            raname.InnerText = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
            returnAddress.AppendChild(raname)

            Dim raorg As Xml.XmlNode = XMLDOC.CreateElement("organization")
            raorg.InnerText = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
            returnAddress.AppendChild(raorg)


            Dim raaddress1 As Xml.XmlNode = XMLDOC.CreateElement("address1")
            raaddress1.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
            returnAddress.AppendChild(raaddress1)

            Dim raaddress2 As Xml.XmlNode = XMLDOC.CreateElement("address2")
            raaddress2.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
            returnAddress.AppendChild(raaddress2)

            Dim racity As Xml.XmlNode = XMLDOC.CreateElement("city")
            racity.InnerText = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
            returnAddress.AppendChild(racity)

            Dim rastate As Xml.XmlNode = XMLDOC.CreateElement("state")
            rastate.InnerText = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
            returnAddress.AppendChild(rastate)

            Dim rapost As Xml.XmlNode = XMLDOC.CreateElement("postalCode")
            rapost.InnerText = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
            returnAddress.AppendChild(rapost)
        End If




        Dim recipients As Xml.XmlNode = XMLDOC.CreateElement("recipients")
        job.AppendChild(recipients)


        'VARIABLES TO HOLD INDIVIDUAL PARTS



        'SHOW RESULT
        '  MessageBox.Show("Name: " & AddressName)
        'MessageBox.Show("Address1: " & Address1)
        'MessageBox.Show("Address2: " & Address2)
        'MessageBox.Show("Address3: " & Address3)
        'MessageBox.Show("City: " & City)
        'MessageBox.Show("State: " & State)
        'MessageBox.Show("Zip: " & Zip)



        Dim address As Xml.XmlNode = XMLDOC.CreateElement("address")

        Dim xname As Xml.XmlNode = Nothing
        Dim xorginization As Xml.XmlNode = Nothing
        Dim xAddress1 As Xml.XmlNode = Nothing
        Dim xAddress2 As Xml.XmlNode = Nothing
        Dim xAddress3 As Xml.XmlNode = Nothing
        Dim xCity As Xml.XmlNode = Nothing
        Dim xState As Xml.XmlNode = Nothing
        Dim xZip As Xml.XmlNode = Nothing
        Dim xcountry As Xml.XmlNode = Nothing




        xname = XMLDOC.CreateElement("name")
        xname.InnerText = ai.nname
        xorginization = XMLDOC.CreateElement("organization")
        xorginization.InnerText = ai.norganization
        xAddress1 = XMLDOC.CreateElement("address1")
        xAddress1.InnerText = Trim(ai.Address1)
        xAddress2 = XMLDOC.CreateElement("address2")
        xAddress2.InnerText = Trim(ai.Address2)
        xAddress3 = XMLDOC.CreateElement("address3")
        xAddress3.InnerText = Trim(ai.nAddress3)
        xCity = XMLDOC.CreateElement("city")
        xCity.InnerText = Trim(ai.city)
        xState = XMLDOC.CreateElement("state")
        xState.InnerText = Trim(ai.state)
        xZip = XMLDOC.CreateElement("postalCode")
        xZip.InnerText = Trim(ai.zip5 & "-" & ai.zip4)
        xcountry = XMLDOC.CreateElement("country")



        address.AppendChild(xname)
        address.AppendChild(xorginization)

        address.AppendChild(xAddress1)
        address.AppendChild(xAddress2)
        address.AppendChild(xAddress3)
        address.AppendChild(xCity)
        address.AppendChild(xState)
        address.AppendChild(xZip)
        address.AppendChild(xcountry)
        recipients.AppendChild(address)
        Return True
    End Function
    Private Function parseaddresssingledoctomultiple(ByRef ai As addressitem, ByRef batchnode As XmlNode, ByVal s As String, ByVal startpage As Integer, ByVal endingpage As Integer, row As Integer) As Boolean

        Dim parts As String() = s.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        ai = New addressitem
        'It is a first page, but now lets see if there is a valid address
        'If address1 validates
        '
        ai.row = row
        If XMLDOC Is Nothing Then
            XMLDOC = New System.Xml.XmlDocument

            Dim docNode As System.Xml.XmlNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            XMLDOC.AppendChild(docNode)
            batchnode = XMLDOC.CreateElement("batch")
            Dim ns As System.Xml.XmlNode
            ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/")
            ns.Value = "http://www.w3.org/2001/XMLSchema-instance"
            batchnode.Attributes.Append(ns)
            XMLDOC.AppendChild(batchnode)

            Dim un As Xml.XmlNode = XMLDOC.CreateElement("username")
            batchnode.AppendChild(un)
            un.InnerText = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")

            Dim pw As Xml.XmlNode = XMLDOC.CreateElement("password")
            pw.InnerText = Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc"))
            batchnode.AppendChild(pw)
            Dim fn As Xml.XmlNode = XMLDOC.CreateElement("filename")
            fn.InnerText = _sourcefilename
            batchnode.AppendChild(fn)
            Dim as1 As Xml.XmlNode = XMLDOC.CreateElement("appSignature")
            batchnode.AppendChild(as1)
            as1.InnerText = _dtt.Select("setting = true and fieldname = 'appSignature'")(0)("misc")

        End If




        ai = New addressitem
        ai.startpage = startpage
        ai.row = row
        ai.validatedStatus = False
        ai.uspsStatus = "Not Processed"
        ai.endpage = endingpage

        '   If startpage = 8 Then
        'Console.Write(Trim(parts(parts.Length - 1)))
        'End If
        Dim rp As New Regex("((?:\w|\s)+),\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\d{5}(-\d{4}|\d{4}|$)))$")

        If Not rp.IsMatch(Trim(parts(parts.Length - 1))) Then
            If _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc") = True Then
                Console.WriteLine(startpage & " is not a valid address.")
                badaddress &= "Address on row " & row & " is not a valid address, this was omitted." & vbCrLf
                ai.ommitted = True
                Return True
            Else
                ai.validatedStatus = False
            End If
        Else
            ai.validatedStatus = True
        End If



        Dim AddressName As String = parts(0)
        Dim Organization As String = String.Empty
        Dim Address1 As String = String.Empty
        Dim Address2 As String = String.Empty
        Dim Address3 As String = String.Empty
        Dim City As String = String.Empty
        Dim State As String = String.Empty
        Dim Zip As String = String.Empty
        ai.nname = AddressName
        Try
            Dim Words() As String = Trim(parts(parts.Length - 1)).Split(" "c)

            'GRAB ZIP (ALWAYS LAST IN THE ARRAY)
            Zip = Words(Words.Length - 1)
            'GRAB STATE ABBR (ALWAYS SECOND TO LAST IN ARRAY)
            State = Words(Words.Length - 2)
            'LOOP REMAINING ARRAY ELEMENT TO FORM CITY NAME 
            '(THIS WORKS FOR ANY NUMBER OF WORDS IN CITY NAME)
            For i As Integer = 0 To Words.Length - 3
                City &= Words(i) & " " 'ADD SPACE BACK IN BETWEEN WORDS
            Next
            City = Replace(City, ",", "")
        Catch
        End Try


        Dim checkstandard As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
        Dim checkstandardwarning As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")

        If parts.Length >= 5 Then
            Address1 = Trim(parts(1))
            Address2 = Trim(parts(2))
            Address3 = Trim(parts(3))

            If Address3 = "" And Address2 = "" Then
                ai.Address1 = Address1
            ElseIf Not Address3 = "" And Address2 = "" Then
                ai.Address1 = Address1
                ai.Address2 = Address3
            ElseIf Not Address3 = "" And Not Address2 = "" Then
                ai.Address1 = Address2
                ai.Address2 = Address3
            Else
                ai.Address1 = Address1
                ai.Address2 = Address2
            End If

            ai.city = City
            ai.state = State
            ai.zip5 = Zip
            standardizeaddress(ai)

            If Not ai.uspsStatus = "1" Then
                If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Address on row " & row & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                    ai.ommitted = True
                    Return True
                ElseIf ai.uspsStatus = "2" Then
                    Console.WriteLine(startpage & " is allowed through even though there is a warning.")
                ElseIf checkstandard = True Then
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Address on row " & row & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf
                    ai.ommitted = True
                    Return True
                End If
            Else
                Organization = Address1
                Address1 = ai.Address1
                Address2 = ai.Address2
                City = ai.city
                State = ai.state
                Zip = ai.zip5 & "-" & ai.zip4
            End If

        ElseIf parts.Length = 4 Then
            Address1 = Trim(parts(1))
            Address2 = Trim(parts(2))

            ai.Address1 = Address1
            ai.Address2 = Address2
            ai.city = City
            ai.state = State
            ai.zip5 = Zip
            standardizeaddress(ai)
            If Not ai.uspsStatus = "1" Then
                If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Address on row " & row & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                    ai.ommitted = True
                    Return True
                ElseIf ai.uspsStatus = "2" Then
                    Console.WriteLine(startpage & " is allowed through even though there is a warning.")
                ElseIf checkstandard = True Then
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Address on row " & row & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf
                    ai.ommitted = True
                    Return True
                End If
            Else
                Address2 = ai.Address2
                Address1 = ai.Address1
                City = ai.city
                State = ai.state
                Zip = ai.zip5 & "-" & ai.zip4
            End If
        ElseIf parts.Length >= 3 Then
            Address1 = Trim(parts(1))
            ai.Address1 = Address1
            ai.city = City
            ai.state = State
            ai.zip5 = Zip


            standardizeaddress(ai)
            If Not ai.uspsStatus = "1" Then
                If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Address on row " & row & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                    ai.ommitted = True
                    Return True
                ElseIf ai.uspsStatus = "2" Then
                    Console.WriteLine(startpage & " is allowed through even though there is a warning.")
                ElseIf checkstandard = True Then
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Address on row " & row & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf
                    ai.ommitted = True
                    Return True
                End If
            Else
                Address1 = ai.Address1
                Address2 = ai.Address2
                City = ai.city
                State = ai.state
                Zip = ai.zip5 & "-" & ai.zip4
            End If
        End If




        Dim job As Xml.XmlNode = XMLDOC.CreateElement("job")
        batchnode.AppendChild(job)

        Dim startingpage As Xml.XmlNode = XMLDOC.CreateElement("startingPage")
        job.AppendChild(startingpage)
        startingpage.InnerText = startpage
        Dim endpage As XmlNode = XMLDOC.CreateElement("endingPage")
        endpage.InnerText = endingpage
        job.AppendChild(endpage)

        Dim printProductionOptions As Xml.XmlNode = XMLDOC.CreateElement("printProductionOptions")
        job.AppendChild(printProductionOptions)
        Dim docclass As Xml.XmlNode = XMLDOC.CreateElement("documentClass")
        docclass.InnerText = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")
        printProductionOptions.AppendChild(docclass)

        Dim layout As Xml.XmlNode = XMLDOC.CreateElement("layout")
        layout.InnerText = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
        printProductionOptions.AppendChild(layout)

        Dim productiontime As Xml.XmlNode = XMLDOC.CreateElement("productionTime")
        productiontime.InnerText = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
        printProductionOptions.AppendChild(productiontime)

        Dim envelope As Xml.XmlNode = XMLDOC.CreateElement("envelope")
        envelope.InnerText = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")
        printProductionOptions.AppendChild(envelope)



        If "Printing One side" = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") Then
            If endingpage > 5 Then
                envelope.InnerText = oversized
            End If

        Else
            If endingpage > 10 Then
                envelope.InnerText = oversized
            End If

        End If




        Dim color As Xml.XmlNode = XMLDOC.CreateElement("color")
        color.InnerText = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
        printProductionOptions.AppendChild(color)

        Dim papertype As Xml.XmlNode = XMLDOC.CreateElement("paperType")
        papertype.InnerText = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
        printProductionOptions.AppendChild(papertype)


        Dim printoption As Xml.XmlNode = XMLDOC.CreateElement("printOption")
        printoption.InnerText = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
        printProductionOptions.AppendChild(printoption)

        Dim mailclass As Xml.XmlNode = XMLDOC.CreateElement("mailClass")
        mailclass.InnerText = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
        printProductionOptions.AppendChild(mailclass)


        If Not _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = "" Or Not _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = "" Then
            Dim returnAddress As Xml.XmlNode = XMLDOC.CreateElement("returnAddress")
            job.AppendChild(returnAddress)

            Dim raname As Xml.XmlNode = XMLDOC.CreateElement("name")
            raname.InnerText = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
            returnAddress.AppendChild(raname)

            Dim raorg As Xml.XmlNode = XMLDOC.CreateElement("organization")
            raorg.InnerText = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
            returnAddress.AppendChild(raorg)


            Dim raaddress1 As Xml.XmlNode = XMLDOC.CreateElement("address1")
            raaddress1.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
            returnAddress.AppendChild(raaddress1)

            Dim raaddress2 As Xml.XmlNode = XMLDOC.CreateElement("address2")
            raaddress2.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
            returnAddress.AppendChild(raaddress2)

            Dim racity As Xml.XmlNode = XMLDOC.CreateElement("city")
            racity.InnerText = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
            returnAddress.AppendChild(racity)

            Dim rastate As Xml.XmlNode = XMLDOC.CreateElement("state")
            rastate.InnerText = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
            returnAddress.AppendChild(rastate)

            Dim rapost As Xml.XmlNode = XMLDOC.CreateElement("postalCode")
            rapost.InnerText = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
            returnAddress.AppendChild(rapost)
        End If




        Dim recipients As Xml.XmlNode = XMLDOC.CreateElement("recipients")
        job.AppendChild(recipients)


        'VARIABLES TO HOLD INDIVIDUAL PARTS



        'SHOW RESULT
        '  MessageBox.Show("Name: " & AddressName)
        'MessageBox.Show("Address1: " & Address1)
        'MessageBox.Show("Address2: " & Address2)
        'MessageBox.Show("Address3: " & Address3)
        'MessageBox.Show("City: " & City)
        'MessageBox.Show("State: " & State)
        'MessageBox.Show("Zip: " & Zip)



        Dim address As Xml.XmlNode = XMLDOC.CreateElement("address")

        Dim xname As Xml.XmlNode = Nothing
        Dim xorginization As Xml.XmlNode = Nothing
        Dim xAddress1 As Xml.XmlNode = Nothing
        Dim xAddress2 As Xml.XmlNode = Nothing
        Dim xAddress3 As Xml.XmlNode = Nothing
        Dim xCity As Xml.XmlNode = Nothing
        Dim xState As Xml.XmlNode = Nothing
        Dim xZip As Xml.XmlNode = Nothing
        Dim xcountry As Xml.XmlNode = Nothing




        xname = XMLDOC.CreateElement("name")
        xname.InnerText = AddressName
        xorginization = XMLDOC.CreateElement("organization")
        xorginization.InnerText = Organization
        xAddress1 = XMLDOC.CreateElement("address1")
        xAddress1.InnerText = Trim(Address1)
        xAddress2 = XMLDOC.CreateElement("address2")
        xAddress2.InnerText = Trim(Address2)
        xAddress3 = XMLDOC.CreateElement("address3")
        xAddress3.InnerText = Trim(Address3)
        xCity = XMLDOC.CreateElement("city")
        xCity.InnerText = Trim(City)
        xState = XMLDOC.CreateElement("state")
        xState.InnerText = Trim(State)
        xZip = XMLDOC.CreateElement("postalCode")
        xZip.InnerText = Trim(Zip)
        xcountry = XMLDOC.CreateElement("country")



        address.AppendChild(xname)
        address.AppendChild(xorginization)

        address.AppendChild(xAddress1)
        address.AppendChild(xAddress2)
        address.AppendChild(xAddress3)
        address.AppendChild(xCity)
        address.AppendChild(xState)
        address.AppendChild(xZip)
        address.AppendChild(xcountry)
        recipients.AppendChild(address)
        Return True
    End Function
    Private Function parseaddresssingle_GeneratedPDF(ByRef ai As addressitem, ByRef BatchNode As XmlNode) As Boolean
        Dim startpage As Integer = 1


        'Validate Text


        'It is a first page, but now lets see if there is a valid address
        'If address1 validates
        '

        If BatchNode Is Nothing Then


            XMLDOC = New System.Xml.XmlDocument
            Dim docNode As System.Xml.XmlNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            XMLDOC.AppendChild(docNode)
            BatchNode = XMLDOC.CreateElement("batch")
            Dim ns As System.Xml.XmlNode
            ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/")
            ns.Value = "http://www.w3.org/2001/XMLSchema-instance"
            BatchNode.Attributes.Append(ns)
            XMLDOC.AppendChild(BatchNode)

            Dim un As Xml.XmlNode = XMLDOC.CreateElement("username")
            BatchNode.AppendChild(un)
            un.InnerText = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")

            Dim pw As Xml.XmlNode = XMLDOC.CreateElement("password")
            pw.InnerText = Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc"))
            BatchNode.AppendChild(pw)
            Dim fn As Xml.XmlNode = XMLDOC.CreateElement("filename")
            fn.InnerText = _sourcefilename
            BatchNode.AppendChild(fn)
            Dim as1 As Xml.XmlNode = XMLDOC.CreateElement("appSignature")
            BatchNode.AppendChild(as1)
            as1.InnerText = _dtt.Select("setting = true and fieldname = 'appSignature'")(0)("misc")
        End If

        ' ai = New addressitem

        



        Dim job As Xml.XmlNode = XMLDOC.CreateElement("job")
        BatchNode.AppendChild(job)

        Dim startingpage As Xml.XmlNode = XMLDOC.CreateElement("startingPage")
        job.AppendChild(startingpage)
        startingpage.InnerText = ai.startpage
        Dim endpage As Xml.XmlNode = XMLDOC.CreateElement("endingPage")
        endpage.InnerText = ai.endpage
        job.AppendChild(endpage)


        Dim printProductionOptions As Xml.XmlNode = XMLDOC.CreateElement("printProductionOptions")
        job.AppendChild(printProductionOptions)
        Dim docclass As Xml.XmlNode = XMLDOC.CreateElement("documentClass")
        docclass.InnerText = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")
        printProductionOptions.AppendChild(docclass)

        Dim layout As Xml.XmlNode = XMLDOC.CreateElement("layout")
        layout.InnerText = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
        printProductionOptions.AppendChild(layout)

        Dim productiontime As Xml.XmlNode = XMLDOC.CreateElement("productionTime")
        productiontime.InnerText = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
        printProductionOptions.AppendChild(productiontime)

        Dim envelope As Xml.XmlNode = XMLDOC.CreateElement("envelope")
        envelope.InnerText = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")

        If "Printing One side" = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") Then
            If (ai.endpage - ai.startpage) + 1 > 5 Then
                envelope.InnerText = oversized
            End If

        Else
            If (ai.endpage - ai.startpage) + 1 > 10 Then
                envelope.InnerText = oversized
            End If

        End If

        printProductionOptions.AppendChild(envelope)

        Dim color As Xml.XmlNode = XMLDOC.CreateElement("color")
        color.InnerText = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
        printProductionOptions.AppendChild(color)

        Dim papertype As Xml.XmlNode = XMLDOC.CreateElement("paperType")
        papertype.InnerText = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
        printProductionOptions.AppendChild(papertype)


        Dim printoption As Xml.XmlNode = XMLDOC.CreateElement("printOption")
        printoption.InnerText = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
        printProductionOptions.AppendChild(printoption)

        Dim mailclass As Xml.XmlNode = XMLDOC.CreateElement("mailClass")
        mailclass.InnerText = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
        printProductionOptions.AppendChild(mailclass)


        If Not _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = "" Or Not _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = "" Then
            Dim returnAddress As Xml.XmlNode = XMLDOC.CreateElement("returnAddress")
            job.AppendChild(returnAddress)

            Dim raname As Xml.XmlNode = XMLDOC.CreateElement("name")
            raname.InnerText = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
            returnAddress.AppendChild(raname)

            Dim raorg As Xml.XmlNode = XMLDOC.CreateElement("organization")
            raorg.InnerText = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
            returnAddress.AppendChild(raorg)


            Dim raaddress1 As Xml.XmlNode = XMLDOC.CreateElement("address1")
            raaddress1.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
            returnAddress.AppendChild(raaddress1)

            Dim raaddress2 As Xml.XmlNode = XMLDOC.CreateElement("address2")
            raaddress2.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
            returnAddress.AppendChild(raaddress2)

            Dim racity As Xml.XmlNode = XMLDOC.CreateElement("city")
            racity.InnerText = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
            returnAddress.AppendChild(racity)

            Dim rastate As Xml.XmlNode = XMLDOC.CreateElement("state")
            rastate.InnerText = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
            returnAddress.AppendChild(rastate)

            Dim rapost As Xml.XmlNode = XMLDOC.CreateElement("postalCode")
            rapost.InnerText = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
            returnAddress.AppendChild(rapost)
        End If




        Dim recipients As Xml.XmlNode = XMLDOC.CreateElement("recipients")
        job.AppendChild(recipients)


        'VARIABLES TO HOLD INDIVIDUAL PARTS



        'SHOW RESULT
        '  MessageBox.Show("Name: " & AddressName)
        'MessageBox.Show("Address1: " & Address1)
        'MessageBox.Show("Address2: " & Address2)
        'MessageBox.Show("Address3: " & Address3)
        'MessageBox.Show("City: " & City)
        'MessageBox.Show("State: " & State)
        'MessageBox.Show("Zip: " & Zip)



        Dim address As Xml.XmlNode = XMLDOC.CreateElement("address")

        Dim xname As Xml.XmlNode = Nothing
        Dim xorginization As Xml.XmlNode = Nothing
        Dim xAddress1 As Xml.XmlNode = Nothing
        Dim xAddress2 As Xml.XmlNode = Nothing
        Dim xAddress3 As Xml.XmlNode = Nothing
        Dim xCity As Xml.XmlNode = Nothing
        Dim xState As Xml.XmlNode = Nothing
        Dim xZip As Xml.XmlNode = Nothing
        Dim xcountry As Xml.XmlNode = Nothing


        ' Private _nName As String
        '        Private _norganization As String
        'Private _nAddress3 As String 'This is not used except for single item
        '        Private _Address1 As String
        'Private _Address2 As String
        '       Private _City As String
        '      Private _State As String
        '     Private _Zip5 As String
        '    Private _Zip4 As String

        xname = XMLDOC.CreateElement("name")
        xname.InnerText = ai.nname
        xorginization = XMLDOC.CreateElement("organization")
        xorginization.InnerText = ai.norganization
        xAddress1 = XMLDOC.CreateElement("address1")
        xAddress1.InnerText = Trim(ai.Address1)
        xAddress2 = XMLDOC.CreateElement("address2")
        xAddress2.InnerText = Trim(ai.Address2)
        xAddress3 = XMLDOC.CreateElement("address3")
        xAddress3.InnerText = Trim(ai.nAddress3)
        xCity = XMLDOC.CreateElement("city")
        xCity.InnerText = Trim(ai.city)
        xState = XMLDOC.CreateElement("state")
        xState.InnerText = Trim(ai.state)
        xZip = XMLDOC.CreateElement("postalCode")
        xZip.InnerText = Trim(ai.zip5 & "-" & ai.zip4)
        xcountry = XMLDOC.CreateElement("country")



        address.AppendChild(xname)
        address.AppendChild(xorginization)

        address.AppendChild(xAddress1)
        address.AppendChild(xAddress2)
        address.AppendChild(xAddress3)
        address.AppendChild(xCity)
        address.AppendChild(xState)
        address.AppendChild(xZip)
        address.AppendChild(xcountry)
        recipients.AppendChild(address)
        Return True
    End Function
    Private Function parseaddresssingle(ByRef ai As addressitem, totalpages As Integer) As Boolean
        Dim startpage As Integer = 1
        Dim batchnode As XmlNode

        'Validate Text


        'It is a first page, but now lets see if there is a valid address
        'If address1 validates
        '


        XMLDOC = New System.Xml.XmlDocument
        Dim docNode As System.Xml.XmlNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
        XMLDOC.AppendChild(docNode)
        batchnode = XMLDOC.CreateElement("batch")
        Dim ns As System.Xml.XmlNode
        ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/")
        ns.Value = "http://www.w3.org/2001/XMLSchema-instance"
        batchnode.Attributes.Append(ns)
        XMLDOC.AppendChild(batchnode)

        Dim un As Xml.XmlNode = XMLDOC.CreateElement("username")
        batchnode.AppendChild(un)
        un.InnerText = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")

        Dim pw As Xml.XmlNode = XMLDOC.CreateElement("password")
        pw.InnerText = Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc"))
        batchnode.AppendChild(pw)
        Dim fn As Xml.XmlNode = XMLDOC.CreateElement("filename")
        fn.InnerText = _sourcefilename
        batchnode.AppendChild(fn)
        Dim as1 As Xml.XmlNode = XMLDOC.CreateElement("appSignature")
        batchnode.AppendChild(as1)
        as1.InnerText = _dtt.Select("setting = true and fieldname = 'appSignature'")(0)("misc")


        ' ai = New addressitem
        ai.startpage = 1
        ai.row = startpage

        ai.validatedStatus = False
        ai.uspsStatus = "Not Processed"


        Dim rp As New Regex("((?:\w|\s)+),\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\d{5}(-\d{4}|\d{4}|$)))$")

        If Not rp.IsMatch(ai.city & ", " & ai.state & " " & ai.zip5) Then
            If _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc") = True Then
                Console.WriteLine(1 & " is not a valid address.")
                badaddress &= "Document starting on page " & 1 & " is not a valid address, this was omitted." & vbCrLf
                ai.ommitted = True
                Return True
            Else
                ai.validatedStatus = False
            End If
        Else
            ai.validatedStatus = True
        End If



        Dim AddressName As String = ai.nname
        Dim Organization As String = String.Empty
        Dim Address1 As String = ai.Address1
        Dim Address2 As String = ai.Address2
        Dim Address3 As String = ai.nAddress3
        Dim City As String = ai.city
        Dim State As String = ai.state
        Dim Zip As String = ai.zip5



        Dim checkstandard As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
        Dim checkstandardwarning As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")

        If Not Address3 = "" Then
            Address1 = ai.Address1
            Address2 = ai.Address2
            Address3 = ai.nAddress3

            If Address3 = "" And Address2 = "" Then
                ai.Address1 = Address1
            ElseIf Not Address3 = "" And Address2 = "" Then
                ai.Address1 = Address1
                ai.Address2 = Address3
            ElseIf Not Address3 = "" And Not Address2 = "" Then
                ai.Address1 = Address2
                ai.Address2 = Address3
            Else
                ai.Address1 = Address1
                ai.Address2 = Address2
            End If

            ai.city = City
            ai.state = State
            ai.zip5 = Zip
            standardizeaddress(ai)

            If Not ai.uspsStatus = "1" Then
                If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                    Console.WriteLine(1 & " is not a valid address.")
                    badaddress &= "Document starting on page " & 1 & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                    ai.ommitted = True
                    Return True
                ElseIf ai.uspsStatus = "2" Then
                    Console.WriteLine(1 & " is allowed through even though there is a warning.")
                ElseIf checkstandard = True Then
                    Console.WriteLine(1 & " is not a valid address.")
                    badaddress &= "Document starting on page " & 1 & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf
                    ai.ommitted = True
                    Return True
                End If
            Else
                Organization = Address1
                Address1 = ai.Address1
                Address2 = ai.Address2
                City = ai.city
                State = ai.state
                Zip = ai.zip5 & "-" & ai.zip4
            End If

        Else

            standardizeaddress(ai)
            If Not ai.uspsStatus = "1" Then
                If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Document starting on page " & startpage & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                    ai.ommitted = True
                    Return True
                ElseIf ai.uspsStatus = "2" Then
                    Console.WriteLine(startpage & " is allowed through even though there is a warning.")
                ElseIf checkstandard = True Then
                    Console.WriteLine(startpage & " is not a valid address.")
                    badaddress &= "Document starting on page " & startpage & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf
                    ai.ommitted = True
                    Return True
                End If
            Else
                Address2 = ai.Address2
                Address1 = ai.Address1
                City = ai.city
                State = ai.state
                Zip = ai.zip5 & "-" & ai.zip4
            End If

        End If




        Dim job As Xml.XmlNode = XMLDOC.CreateElement("job")
        batchnode.AppendChild(job)

        Dim startingpage As Xml.XmlNode = XMLDOC.CreateElement("startingPage")
        job.AppendChild(startingpage)
        startingpage.InnerText = startpage
        Dim endpage As Xml.XmlNode = XMLDOC.CreateElement("endingPage")
        endpage.InnerText = totalpages
        job.AppendChild(endpage)


        Dim printProductionOptions As Xml.XmlNode = XMLDOC.CreateElement("printProductionOptions")
        job.AppendChild(printProductionOptions)
        Dim docclass As Xml.XmlNode = XMLDOC.CreateElement("documentClass")
        docclass.InnerText = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")
        printProductionOptions.AppendChild(docclass)

        Dim layout As Xml.XmlNode = XMLDOC.CreateElement("layout")
        layout.InnerText = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
        printProductionOptions.AppendChild(layout)

        Dim productiontime As Xml.XmlNode = XMLDOC.CreateElement("productionTime")
        productiontime.InnerText = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
        printProductionOptions.AppendChild(productiontime)

        Dim envelope As Xml.XmlNode = XMLDOC.CreateElement("envelope")
        envelope.InnerText = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")

        If "Printing One side" = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") Then
            If totalpages > 5 Then
                envelope.InnerText = oversized
            End If

        Else
            If totalpages > 10 Then
                envelope.InnerText = oversized
            End If

        End If

        printProductionOptions.AppendChild(envelope)

        Dim color As Xml.XmlNode = XMLDOC.CreateElement("color")
        color.InnerText = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
        printProductionOptions.AppendChild(color)

        Dim papertype As Xml.XmlNode = XMLDOC.CreateElement("paperType")
        papertype.InnerText = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
        printProductionOptions.AppendChild(papertype)


        Dim printoption As Xml.XmlNode = XMLDOC.CreateElement("printOption")
        printoption.InnerText = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
        printProductionOptions.AppendChild(printoption)

        Dim mailclass As Xml.XmlNode = XMLDOC.CreateElement("mailClass")
        mailclass.InnerText = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
        printProductionOptions.AppendChild(mailclass)


        If Not _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = "" Or Not _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = "" Then
            Dim returnAddress As Xml.XmlNode = XMLDOC.CreateElement("returnAddress")
            job.AppendChild(returnAddress)

            Dim raname As Xml.XmlNode = XMLDOC.CreateElement("name")
            raname.InnerText = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
            returnAddress.AppendChild(raname)

            Dim raorg As Xml.XmlNode = XMLDOC.CreateElement("organization")
            raorg.InnerText = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
            returnAddress.AppendChild(raorg)


            Dim raaddress1 As Xml.XmlNode = XMLDOC.CreateElement("address1")
            raaddress1.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
            returnAddress.AppendChild(raaddress1)

            Dim raaddress2 As Xml.XmlNode = XMLDOC.CreateElement("address2")
            raaddress2.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
            returnAddress.AppendChild(raaddress2)

            Dim racity As Xml.XmlNode = XMLDOC.CreateElement("city")
            racity.InnerText = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
            returnAddress.AppendChild(racity)

            Dim rastate As Xml.XmlNode = XMLDOC.CreateElement("state")
            rastate.InnerText = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
            returnAddress.AppendChild(rastate)

            Dim rapost As Xml.XmlNode = XMLDOC.CreateElement("postalCode")
            rapost.InnerText = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
            returnAddress.AppendChild(rapost)
        End If




        Dim recipients As Xml.XmlNode = XMLDOC.CreateElement("recipients")
        job.AppendChild(recipients)


        'VARIABLES TO HOLD INDIVIDUAL PARTS



        'SHOW RESULT
        '  MessageBox.Show("Name: " & AddressName)
        'MessageBox.Show("Address1: " & Address1)
        'MessageBox.Show("Address2: " & Address2)
        'MessageBox.Show("Address3: " & Address3)
        'MessageBox.Show("City: " & City)
        'MessageBox.Show("State: " & State)
        'MessageBox.Show("Zip: " & Zip)



        Dim address As Xml.XmlNode = XMLDOC.CreateElement("address")

        Dim xname As Xml.XmlNode = Nothing
        Dim xorginization As Xml.XmlNode = Nothing
        Dim xAddress1 As Xml.XmlNode = Nothing
        Dim xAddress2 As Xml.XmlNode = Nothing
        Dim xAddress3 As Xml.XmlNode = Nothing
        Dim xCity As Xml.XmlNode = Nothing
        Dim xState As Xml.XmlNode = Nothing
        Dim xZip As Xml.XmlNode = Nothing
        Dim xcountry As Xml.XmlNode = Nothing




        xname = XMLDOC.CreateElement("name")
        xname.InnerText = AddressName
        xorginization = XMLDOC.CreateElement("organization")
        xorginization.InnerText = Organization
        xAddress1 = XMLDOC.CreateElement("address1")
        xAddress1.InnerText = Trim(Address1)
        xAddress2 = XMLDOC.CreateElement("address2")
        xAddress2.InnerText = Trim(Address2)
        xAddress3 = XMLDOC.CreateElement("address3")
        xAddress3.InnerText = Trim(Address3)
        xCity = XMLDOC.CreateElement("city")
        xCity.InnerText = Trim(City)
        xState = XMLDOC.CreateElement("state")
        xState.InnerText = Trim(State)
        xZip = XMLDOC.CreateElement("postalCode")
        xZip.InnerText = Trim(Zip)
        xcountry = XMLDOC.CreateElement("country")



        address.AppendChild(xname)
        address.AppendChild(xorginization)

        address.AppendChild(xAddress1)
        address.AppendChild(xAddress2)
        address.AppendChild(xAddress3)
        address.AppendChild(xCity)
        address.AppendChild(xState)
        address.AppendChild(xZip)
        address.AppendChild(xcountry)
        recipients.AppendChild(address)
        Return True
    End Function
    Private Function ParseAddressReprocess(ByRef ai As addressitem, ByRef Batchnode As XmlNode) As Boolean

        'It is a first page, but now lets see if there is a valid address
        'If address1 validates
        Dim prioromit As String = ai.ommitted

        ai.ommitted = False
        If XMLDOC Is Nothing Then
            XMLDOC = New System.Xml.XmlDocument

            Dim docNode As System.Xml.XmlNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            XMLDOC.AppendChild(docNode)
            Batchnode = XMLDOC.CreateElement("batch")
            Dim ns As System.Xml.XmlNode
            ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/")
            ns.Value = "http://www.w3.org/2001/XMLSchema-instance"
            Batchnode.Attributes.Append(ns)
            XMLDOC.AppendChild(Batchnode)

            Dim un As Xml.XmlNode = XMLDOC.CreateElement("username")
            Batchnode.AppendChild(un)
            un.InnerText = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")

            Dim pw As Xml.XmlNode = XMLDOC.CreateElement("password")
            pw.InnerText = Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc"))
            Batchnode.AppendChild(pw)
            Dim fn As Xml.XmlNode = XMLDOC.CreateElement("filename")
            fn.InnerText = _sourcefilename
            Batchnode.AppendChild(fn)
            Dim as1 As Xml.XmlNode = XMLDOC.CreateElement("appSignature")
            Batchnode.AppendChild(as1)
            as1.InnerText = _dtt.Select("setting = true and fieldname = 'appSignature'")(0)("misc")

        End If






        '   If startpage = 8 Then
        'Console.Write(Trim(parts(parts.Length - 1)))
        'End If
        Dim rp As New Regex("((?:\w|\s)+),\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\d{5}(-\d{4}|\d{4}|$)))$")

        If Not rp.IsMatch(ai.city & ", " & ai.state & " " & ai.zip5) Then
            If _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc") = True Then
                Console.WriteLine(ai.row & "row is not a valid address.")
                badaddress &= "Address on row " & ai.row & " is not a valid address, this was omitted." & vbCrLf
                If prioromit Then
                    ai.ommitted = True
                    Return True
                End If

            Else
                ai.validatedStatus = False
            End If
        Else
            ai.validatedStatus = True
        End If



        Dim AddressName As String = ai.nname
        Dim Organization As String = String.Empty
        Dim Address1 As String = ai.Address1
        Dim Address2 As String = ai.Address2
        Dim Address3 As String = ai.nAddress3
        Dim City As String = ai.city
        Dim State As String = ai.state
        Dim Zip As String = ai.zip5



        Dim checkstandard As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
        Dim checkstandardwarning As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")


        If Not ai.uspsStatus = "1" Then
            If ai.uspsStatus = "2" And checkstandardwarning = True Then 'Omit for non standard
                Console.WriteLine(ai.row & " row is not a valid address.")
                badaddress &= "Address on row " & ai.row & " IS Valid VIA THE USPS, But this was omitted due to warning." & vbCrLf
                If prioromit Then
                    ai.ommitted = True
                    Return True
                End If

            ElseIf ai.uspsStatus = "2" Then
                Console.WriteLine(ai.row & " row is allowed through even though there is a warning.")
            ElseIf checkstandard = True Then
                Console.WriteLine(ai.row & " row  is not a valid address.")
                badaddress &= "Address on row " & ai.row & " is not a valid address VIA THE USPS, this was omitted." & vbCrLf

                If prioromit Then
                    ai.ommitted = True
                    Return True
                End If
            End If
        Else
            Organization = ai.nAddress3
            Address1 = ai.Address1
            Address2 = ai.Address2
            City = ai.city
            State = ai.state
            Zip = ai.zip5 & "-" & ai.zip4
        End If




        Dim job As Xml.XmlNode = XMLDOC.CreateElement("job")
        Batchnode.AppendChild(job)

        Dim startingpage As Xml.XmlNode = XMLDOC.CreateElement("startingPage")
        job.AppendChild(startingpage)
        startingpage.InnerText = ai.startpage
        Dim endpage As XmlNode = XMLDOC.CreateElement("endingPage")
        endpage.InnerText = ai.endpage
        job.AppendChild(endpage)

        Dim printProductionOptions As Xml.XmlNode = XMLDOC.CreateElement("printProductionOptions")
        job.AppendChild(printProductionOptions)
        Dim docclass As Xml.XmlNode = XMLDOC.CreateElement("documentClass")
        docclass.InnerText = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")
        printProductionOptions.AppendChild(docclass)

        Dim layout As Xml.XmlNode = XMLDOC.CreateElement("layout")
        layout.InnerText = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
        printProductionOptions.AppendChild(layout)

        Dim productiontime As Xml.XmlNode = XMLDOC.CreateElement("productionTime")
        productiontime.InnerText = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
        printProductionOptions.AppendChild(productiontime)

        Dim envelope As Xml.XmlNode = XMLDOC.CreateElement("envelope")
        envelope.InnerText = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")
        printProductionOptions.AppendChild(envelope)

        If "Printing One side" = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") Then
            If ai.endpage - ai.startpage + 1 > 5 Then
                envelope.InnerText = oversized
            End If

        Else
            If ai.endpage - ai.startpage + 1 > 10 Then
                envelope.InnerText = oversized
            End If

        End If


        Dim color As Xml.XmlNode = XMLDOC.CreateElement("color")
        color.InnerText = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
        printProductionOptions.AppendChild(color)

        Dim papertype As Xml.XmlNode = XMLDOC.CreateElement("paperType")
        papertype.InnerText = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
        printProductionOptions.AppendChild(papertype)


        Dim printoption As Xml.XmlNode = XMLDOC.CreateElement("printOption")
        printoption.InnerText = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
        printProductionOptions.AppendChild(printoption)

        Dim mailclass As Xml.XmlNode = XMLDOC.CreateElement("mailClass")
        mailclass.InnerText = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
        printProductionOptions.AppendChild(mailclass)


        If Not _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = "" Or Not _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = "" Then
            Dim returnAddress As Xml.XmlNode = XMLDOC.CreateElement("returnAddress")
            job.AppendChild(returnAddress)

            Dim raname As Xml.XmlNode = XMLDOC.CreateElement("name")
            raname.InnerText = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
            returnAddress.AppendChild(raname)

            Dim raorg As Xml.XmlNode = XMLDOC.CreateElement("organization")
            raorg.InnerText = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
            returnAddress.AppendChild(raorg)


            Dim raaddress1 As Xml.XmlNode = XMLDOC.CreateElement("address1")
            raaddress1.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
            returnAddress.AppendChild(raaddress1)

            Dim raaddress2 As Xml.XmlNode = XMLDOC.CreateElement("address2")
            raaddress2.InnerText = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
            returnAddress.AppendChild(raaddress2)

            Dim racity As Xml.XmlNode = XMLDOC.CreateElement("city")
            racity.InnerText = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
            returnAddress.AppendChild(racity)

            Dim rastate As Xml.XmlNode = XMLDOC.CreateElement("state")
            rastate.InnerText = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
            returnAddress.AppendChild(rastate)

            Dim rapost As Xml.XmlNode = XMLDOC.CreateElement("postalCode")
            rapost.InnerText = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
            returnAddress.AppendChild(rapost)
        End If




        Dim recipients As Xml.XmlNode = XMLDOC.CreateElement("recipients")
        job.AppendChild(recipients)


        'VARIABLES TO HOLD INDIVIDUAL PARTS



        'SHOW RESULT
        '  MessageBox.Show("Name: " & AddressName)
        'MessageBox.Show("Address1: " & Address1)
        'MessageBox.Show("Address2: " & Address2)
        'MessageBox.Show("Address3: " & Address3)
        'MessageBox.Show("City: " & City)
        'MessageBox.Show("State: " & State)
        'MessageBox.Show("Zip: " & Zip)



        Dim address As Xml.XmlNode = XMLDOC.CreateElement("address")

        Dim xname As Xml.XmlNode = Nothing
        Dim xorginization As Xml.XmlNode = Nothing
        Dim xAddress1 As Xml.XmlNode = Nothing
        Dim xAddress2 As Xml.XmlNode = Nothing
        Dim xAddress3 As Xml.XmlNode = Nothing
        Dim xCity As Xml.XmlNode = Nothing
        Dim xState As Xml.XmlNode = Nothing
        Dim xZip As Xml.XmlNode = Nothing
        Dim xcountry As Xml.XmlNode = Nothing




        xname = XMLDOC.CreateElement("name")
        xname.InnerText = AddressName
        xorginization = XMLDOC.CreateElement("organization")
        xorginization.InnerText = Organization
        xAddress1 = XMLDOC.CreateElement("address1")
        xAddress1.InnerText = Trim(Address1)
        xAddress2 = XMLDOC.CreateElement("address2")
        xAddress2.InnerText = Trim(Address2)
        xAddress3 = XMLDOC.CreateElement("address3")
        xAddress3.InnerText = Trim(Address3)
        xCity = XMLDOC.CreateElement("city")
        xCity.InnerText = Trim(City)
        xState = XMLDOC.CreateElement("state")
        xState.InnerText = Trim(State)
        xZip = XMLDOC.CreateElement("postalCode")
        xZip.InnerText = Trim(Zip)
        xcountry = XMLDOC.CreateElement("country")



        address.AppendChild(xname)
        address.AppendChild(xorginization)

        address.AppendChild(xAddress1)
        address.AppendChild(xAddress2)
        address.AppendChild(xAddress3)
        address.AppendChild(xCity)
        address.AppendChild(xState)
        address.AppendChild(xZip)
        address.AppendChild(xcountry)
        recipients.AppendChild(address)
        Return True
    End Function
#End Region
#Region "Page Manipulation"
    Private Sub dg_confirms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim gr As New DataGridView
        gr = sender
        Select Case e.ColumnIndex
            Case Is > -1
                Dim v1 As addressitem

                v1 = gr.CurrentRow.DataBoundItem


                CurrentPage = v1.startpage - 1
                Dim img As System.Drawing.Image = getImageFromFile(_sourcefilename, CurrentPage, 72)
                bimg = New Bitmap(img, PictureBox1.Width, PictureBox1.Height)
                Me.PictureBox1.Image = bimg
                setpage()
        End Select




    End Sub
    Private Sub NextPage()

        If CurrentPage < rasterizer.PageCount - 1 Then
            CurrentPage += 1
            Dim img As System.Drawing.Image = getImageFromFile(Me.OpenFileDialog1.FileName, CurrentPage, 72)
            bimg = New Bitmap(img, PictureBox1.Width, PictureBox1.Height)
            Me.PictureBox1.Image = bimg
        End If
        setpage()
    End Sub
    Private Sub setpage()
        If CurrentPage = rasterizer.PageCount - 1 Then
            Me.Button3.Enabled = False
        Else
            Me.Button3.Enabled = True
        End If
        If CurrentPage = 0 Then
            Me.Button4.Enabled = False
        Else
            Me.Button4.Enabled = True
        End If
        Me.lbl_pages.Text = "Page " & CurrentPage + 1 & " of " & rasterizer.PageCount
    End Sub
    Private Sub PriorPage()
        If CurrentPage > 0 Then
            CurrentPage -= 1
            Try
                Dim img As System.Drawing.Image = getImageFromFile(Me.OpenFileDialog1.FileName, CurrentPage, 72)
                bimg = New Bitmap(img, PictureBox1.Width, PictureBox1.Height)
                Me.PictureBox1.Image = bimg
            Catch ex As Exception
            End Try
        End If
        setpage()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        NextPage()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        PriorPage()
    End Sub
#End Region
#Region "PDF Load and Extraction of Text"
    Public Function ExtractTextFromRegionOfPdf(ByVal sourceFileName As String) As String

        Dim x As New FileStream(sourceFileName, FileMode.Open)
        Dim reader As New iTextSharp.text.pdf.PdfReader(x)
        'AWESOME!!

        Dim rect1 As New System.util.RectangleJ(Rect.X, System.Math.Abs(Me.PictureBox1.Height - Rect.Y) - Rect.Height, Rect.Width, Rect.Height)
        Dim rf As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect1)
        Dim mystrat As New iTextSharp.text.pdf.parser.LocationTextExtractionStrategy
        Dim rtrf(1) As iTextSharp.text.pdf.parser.RegionTextRenderFilter
        rtrf(0) = rf
        'Dim rect2 As New System.util.RectangleJ(0, 700, 800, 140)
        'Dim rf2 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
        Dim textExtractionStrategy As New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)

        'rtrf(0) = rf2
        'textExtractionStrategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)
        'MsgBox(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy))
        ' iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy)
        x.Close()
        x.Dispose()
        reader.Close()
        If _mode = 1 Then


            If Me.loadedbool Then
                Dim s As String = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, CurrentPage + 1, textExtractionStrategy)

                Dim y As MsgBoxResult = MsgBox("This field is showing : " & s & vbCrLf & "Is this the correct variable on this page?", MsgBoxStyle.YesNo)
                If y = MsgBoxResult.Yes Then





                    _dtt.Rows(_CurrentCount)("x") = Rect.X
                    _dtt.Rows(_CurrentCount)("y") = System.Math.Abs(Me.PictureBox1.Height - Rect.Y) - Rect.Height
                    _dtt.Rows(_CurrentCount)("width") = Rect.Width
                    _dtt.Rows(_CurrentCount)("height") = Rect.Height

                    If _CurrentCount = 0 Then
                        Dim xx As MsgBoxResult = MsgBox("There is an optional Parimeter where you can select something that only appears on the first page, do you want to add this.  It can be part of a string like Page 1 of XX?", MsgBoxStyle.YesNo)
                        If xx = MsgBoxResult.No Then
                            _CurrentCount = 2
                        End If
                    End If

                    If _dtt.Rows(_CurrentCount)("FieldName") = "FirstPageConstant" Then
                        _validatetext = InputBox("Enter Charectors to match, if you enter ""1 of "" it will be true for anything after the of")
                        _dtt.Rows(_CurrentCount + 1)("misc") = _validatetext
                        _CurrentCount += 1
                    End If
                    _CurrentCount += 1



                    If _CurrentCount = 3 Then
                        _CurrentCount = 0
                        Me.Label2.Text = "OK, you have completed the template, if you wish to start over simply do it again and start by selecting the area with:" & _dtt.Rows(0)("fieldname")
                        startover = 1
                        drawrectangles()

                        MsgBox("Make sure you save this if you want to use it in the future.")

                    Else
                        Me.Label2.Text = "Now Select : " & _dtt.Rows(_CurrentCount)("fieldname")
                    End If
                End If
            End If
        Else
            Me.Label2.Text = "OK, you have completed the template, if you wish to start over simply do it again and start by selecting the area with:" & _dtt.Rows(0)("fieldname")
        End If
        Return ""
    End Function
    Private Sub loadpdf(ByVal file As String)
        Me.Label2.Visible = True
        loadtemplate()
        Try


            Me.DataGridView1.DataSource = _dtt
            updategrid(DataGridView1)
            'pdfv = New PDFView.PDFViewer
            Me.Panel1.AutoScroll = True
            Me.Panel1.AutoSize = False

            'Me.PictureBox1.SizeMode = Windows.Forms.PictureBoxSizeMode.Zoom
            'Me.Label1.Text = "Page 1 of " & img.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page)

            Me.PictureBox1.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize

            'pdfv.FileName1 = file
            If file <> _file Then
                'pdfv.Dispose()
                rasterizer.Close()
                _file = file
                rasterizer.Open(file, gvi, False)

            End If

            Me.PictureBox1.Image = getImageFromFile(file, 0, 72)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Dim x As New FileStream("C:\image\test.pdf", FileMode.Open)
        'Dim reader As New iTextSharp.text.pdf.PdfReader(x)
        '
        'ww = PictureBox1.Width = reader.GetPageSize(1).Width * 3
        'hh = PictureBox1.Height = reader.GetPageSize(1).Height * 3
        'tt = PictureBox1.Top = reader.GetPageSize(1).Top
        'll = PictureBox1.Left = reader.GetPageSize(1).Left
        'Me.PictureBox1.Update()
        'reader.Close()
        '  Dim reader As New iTextSharp.text.pdf.PdfReader("C:\image\test.pdf")
        'reader.GetPageSize(1)
        'PictureBox1.Width = reader.GetPageSize(1).Width
        'PictureBox1.Height = reader.GetPageSize(1).Height
        'PictureBox1.Top = reader.GetPageSize(1).Top
        'PictureBox1.Left = reader.GetPageSize(1).Left
        setpage()
        Me.Label2.Text = "OK, you are ready to start, use your mouse to highlight the entire field where this field is located : " & _dtt.Rows(0)("fieldname")
        Me.Invalidate()

    End Sub
#End Region
#Region "General Functions"
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        startload()
    End Sub
    Private Sub startload()
        Try


            CurrentPage = 0
            _mode = 1

            Me.OpenFileDialog1.Multiselect = False
            Me.OpenFileDialog1.Filter = "PDF|"
            Dim y As System.Windows.Forms.DialogResult
            OpenFileDialog1.FileName = "*.pdf"
            y = OpenFileDialog1.ShowDialog()
            If y = Windows.Forms.DialogResult.OK Then

                loadpdf(Me.OpenFileDialog1.FileName)

                btn_upload.Enabled = True
                Dim fi As New FileInfo(Me.OpenFileDialog1.FileName)

            End If
            updatetest()
            loadtemplate()
        Catch ex As Exception
            MsgBox("You must select a file")
            Me.Close()
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Public Function Beautify(doc As System.Xml.XmlDocument) As String
        Dim sb As New System.Text.StringBuilder()
        Dim settings As New System.Xml.XmlWriterSettings() With { _
            .Indent = True, _
             .IndentChars = "  ", _
             .NewLineChars = vbCr & vbLf, _
             .NewLineHandling = Xml.NewLineHandling.Replace
        }
        Using writer As System.Xml.XmlWriter = System.Xml.XmlWriter.Create(sb, settings)
            doc.Save(writer)
        End Using
        Return sb.ToString()
    End Function
    Private Sub verifydocument(Optional ByVal startuploadwhendone = False)
        If _dtt.Rows(0)("width") = 0 Then
            MsgBox("You have not Selected the Address Block yet.  Please read the text in black and click and drag your mouse over the correct areas.")
            Return
        End If
        If _dtt.Select("setting = true and fieldname = 'username'")(0)("misc") = "" Then
            MsgBox("You have not setup this Print Document Yet.")
            openform()
        End If

        _startuploadwhendone = startuploadwhendone

        Me.Button1.Enabled = False
        Me.Button2.Enabled = False
        Me.Button3.Enabled = False
        Me.Button4.Enabled = False
        Me.Button5.Enabled = False
        Me.Button6.Enabled = False
        Me.Button7.Enabled = False
        Me.btn_upload.Enabled = False
        Me.ControlBox = False
        If _loadtype = loadtype.regular Then
            _sourcefilename = Me.OpenFileDialog1.FileName
        End If
        badaddress = ""

        Dim x As New Threading.Thread(AddressOf verify)
        x.IsBackground = False
        x.Start()

    End Sub
    Public Sub verifysingledocument(ByVal ai As addressitem, filename As String, template As String, Optional ByVal startuploadwhendone As Boolean = False)

        _sourcefilename = filename
        _CurrentTemplate = _path & "\" & template
        _aiSingle = ai
        _startuploadwhendone = startuploadwhendone
        CurrentPage = 1
    End Sub
#End Region
#Region "Page and Layout"
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        updatefiles(TextBox1.Text)
    End Sub
    Private Sub updategrid(ByVal dgv As DataGridView)
        Try


            dgv.BackgroundColor = Color.LightGray
            dgv.BorderStyle = BorderStyle.Fixed3D

            ' Set property values appropriate for read-only display and  
            ' limited interactivity. 
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.AllowUserToOrderColumns = True
            dgv.ReadOnly = False
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
            dgv.MultiSelect = False
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
            dgv.AllowUserToResizeColumns = True
            dgv.ColumnHeadersHeightSizeMode = _
                DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dgv.AllowUserToResizeRows = False
            dgv.RowHeadersWidthSizeMode = _
                DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders

            ' Set the selection background color for all the cells.
            dgv.DefaultCellStyle.SelectionBackColor = Color.White
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black

            ' Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            ' value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty

            ' Set the background color for all rows and for alternating rows.  
            ' The value for alternating rows overrides the value for all rows. 
            dgv.RowsDefaultCellStyle.BackColor = Color.LightCyan
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Cyan
            ' Set the row and column header styles.
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.Black






            For Each datacolumn As DataGridViewColumn In dgv.Columns

                If InStr(datacolumn.Name, "Visible", CompareMethod.Text) > 0 Then
                    For Each dr As DataGridViewRow In dgv.Rows
                        If dr.Cells("Visible").Value = False Then
                            dr.Visible = False


                        End If
                    Next
                    datacolumn.Visible = False

                Else
                    '   datacolumn.ReadOnly = True
                End If
                If InStr(datacolumn.Name, "Setting", CompareMethod.Text) > 0 Then
                    datacolumn.Visible = False
                End If
                If InStr(datacolumn.Name, "startpage", CompareMethod.Text) > 0 Then
                    datacolumn.DisplayIndex = 0
                End If
                If InStr(datacolumn.Name, "endpage", CompareMethod.Text) > 0 Then
                    datacolumn.DisplayIndex = 0
                End If
                If InStr(datacolumn.Name, "uspsStatus", CompareMethod.Text) > 0 Then
                    datacolumn.DisplayIndex = 0

                End If
                If InStr(datacolumn.Name, "validatedStatus", CompareMethod.Text) > 0 Then
                    datacolumn.DisplayIndex = 0

                End If

                datacolumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Next

            If dgv.Columns.Contains("row") And Not dgv.Columns.Contains("Recheck Address") Then
                Dim c As New DataGridViewButtonColumn
                c.Name = "Recheck Address"
                c.HeaderText = "Recheck Address"
                c.Text = "ReCheck Address"
                c.UseColumnTextForButtonValue = True
                dgv.Columns.Add(c)

            End If

            If dgv.Columns.Contains("Recheck Address") Then
                dgv.Columns("Recheck Address").DisplayIndex = 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub updatetest()
        Me.lbl_Live.Text = "Processing Notes: " & IIf(_dtt.Select("setting = true and fieldname = 'testMode'")(0)("misc"), "TEST MODE", "LIVE MODE")
    End Sub
    Private Sub setuptable()
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
        dr("fieldname") = "AddressBlock"
        dr("rowid") = i + 1
        dr("Visible") = 1
        _dtt.Rows.Add(dr)
        i += 1



        dr = _dtt.NewRow()
        dr("fieldname") = "FirstPageConstant"
        dr("rowid") = i + 1
        dr("Visible") = 1
        _dtt.Rows.Add(dr)
        i += 1

        dr = _dtt.NewRow()
        dr("fieldname") = "FirstPageConstantCompare"
        dr("rowid") = i + 1
        dr("Visible") = 1
        _dtt.Rows.Add(dr)
        i += 1
        dr = _dtt.NewRow()
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
        dr("Misc") = True
        _dtt.Rows.Add(dr)

        i += 1

        dr = _dtt.NewRow()

        dr("fieldname") = "omitNonStandardWarning"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        dr("Misc") = True
        _dtt.Rows.Add(dr)



        i += 1

        dr = _dtt.NewRow()
        dr("fieldname") = "omitNonValidated"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        dr("Misc") = True

        _dtt.Rows.Add(dr)


        i += 1

        dr = _dtt.NewRow()
        dr("fieldname") = "testMode"
        dr("rowid") = i + 1
        dr("Visible") = False
        dr("Setting") = True
        dr("Misc") = True
        _dtt.Rows.Add(dr)

    End Sub
#End Region
#Region "Template Functions"
    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Try
            If Not Me.lb_SavedTemplates.SelectedItem = "" Then
                If Not InStr(Me.lb_SavedTemplates.SelectedItem, "system_") <= 0 Then
                    MsgBox("This is a sysem template you cannot delete this.")
                Else
                    Dim y As MsgBoxResult = MsgBox("Are you sure you want to delete template: " & Me.lb_SavedTemplates.SelectedItem, MsgBoxStyle.YesNo)
                    If y = MsgBoxResult.Yes Then
                        File.Delete(_path & "\" & Me.lb_SavedTemplates.SelectedItem & ".c2m")
                        MsgBox("Deleted")
                        updatefiles()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub lb_SavedTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lb_SavedTemplates.SelectedIndexChanged

        If Not Me.PictureBox1.Image Is Nothing Then




            If _CurrentTemplate = "" And _dtt.Rows(0)("x") = 0 Then
                If Me.lb_SavedTemplates.Items.Count > 0 Then

                    _CurrentTemplate = _path & "\" & Me.lb_SavedTemplates.SelectedItem & ".c2m"
                    loadtemplate()
                End If
            ElseIf _CurrentTemplate <> Me.lb_SavedTemplates.SelectedItem Then
                Dim y As MsgBoxResult = MsgBox("You have selected a new template, do you want to Load this?", MsgBoxStyle.YesNo)
                If y = MsgBoxResult.Yes Then
                    _CurrentTemplate = _path & "\" & Me.lb_SavedTemplates.SelectedItem & ".c2m"
                    loadtemplate()
                End If

            End If
            updatetest()
        End If
        If _mode = 2 Then
            Dim y As MsgBoxResult = MsgBox("You are currently in Single Item to Multiple Recipients mode.  You MUST reload your data to reflect changes", MsgBoxStyle.YesNo)
            If y = MsgBoxResult.Yes Then
                singletomultiple()
            End If
        End If
    End Sub
    Private Sub reloadtemplate()
        If Not _CurrentTemplate = "" Then
            loadtemplate()
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        openform()
    End Sub
    Private Sub openform()
        Dim frm As New frm_settings

        frm.tb_username.Text = _dtt.Select("setting = true and fieldname = 'username'")(0)("misc")

        frm.tb_password.Text = Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc"))

        frm.cb_documentclass.SelectedItem = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc")

        frm.cb_layout.SelectedItem = _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc")
        frm.cb_productiontime.SelectedItem = _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc")
        frm.cb_envelope.SelectedItem = _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc")
        frm.cb_color.SelectedItem = _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc")
        frm.cb_papertype.SelectedItem = _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc")
        frm.cb_printoption.SelectedItem = _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc")
        frm.cb_mailclass.SelectedItem = _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc")
        frm.tb_raName.Text = _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc")
        frm.tb_raOrganization.Text = _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc")
        frm.Tb_raAddress1.Text = _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc")
        frm.tb_raAddress2.Text = _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc")
        frm.tb_raCity.Text = _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc")
        frm.tb_raState.Text = _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc")
        frm.tb_PostalCode.Text = _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc")
        frm.Chkbox_NonStandardized.Checked = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
        frm.Chkbox_NonValidated.Checked = _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc")
        frm.chk_OmitUSPSWarning.Checked = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")
        frm.chk_TEST.Checked = _dtt.Select("setting = true and fieldname = 'testMode'")(0)("misc")
        frm.ShowDialog()

        _dtt.Select("setting = true and fieldname = 'username'")(0)("misc") = frm.tb_username.Text

        _dtt.Select("setting = true and fieldname = 'password'")(0)("misc") = Encrypt(frm.tb_password.Text)
        _dtt.Select("setting = true and fieldname = 'appSignature'")(0)("misc") = "SRC_AutoMailer VSenese"
        _dtt.Select("setting = true and fieldname = 'poDocumentClass'")(0)("misc") = frm.cb_documentclass.SelectedItem

        _dtt.Select("setting = true and fieldname = 'poLayout'")(0)("misc") = frm.cb_layout.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poProductionTime'")(0)("misc") = frm.cb_productiontime.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poEnvelope'")(0)("misc") = frm.cb_envelope.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poColor'")(0)("misc") = frm.cb_color.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poPaperType'")(0)("misc") = frm.cb_papertype.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poPrintOption'")(0)("misc") = frm.cb_printoption.SelectedItem
        _dtt.Select("setting = true and fieldname = 'poMailClass'")(0)("misc") = frm.cb_mailclass.SelectedItem
        _dtt.Select("setting = true and fieldname = 'raName'")(0)("misc") = frm.tb_raName.Text
        _dtt.Select("setting = true and fieldname = 'raOrganization'")(0)("misc") = frm.tb_raOrganization.Text
        _dtt.Select("setting = true and fieldname = 'raAddress1'")(0)("misc") = frm.Tb_raAddress1.Text
        _dtt.Select("setting = true and fieldname = 'raAddress2'")(0)("misc") = frm.tb_raAddress2.Text
        _dtt.Select("setting = true and fieldname = 'raCity'")(0)("misc") = frm.tb_raCity.Text
        _dtt.Select("setting = true and fieldname = 'raState'")(0)("misc") = frm.tb_raState.Text
        _dtt.Select("setting = true and fieldname = 'raPostalCode'")(0)("misc") = frm.tb_PostalCode.Text
        _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc") = frm.Chkbox_NonStandardized.Checked
        _dtt.Select("setting = true and fieldname = 'omitNonValidated'")(0)("misc") = frm.Chkbox_NonValidated.Checked
        _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc") = frm.chk_OmitUSPSWarning.Checked
        _dtt.Select("setting = true and fieldname = 'testMode'")(0)("misc") = frm.chk_TEST.Checked

        Dim mypath = "defaults.xml"
        If System.IO.File.Exists(mypath) Then
            Dim _dtt1 As DataTable
            ds1.Clear()
            ds1.ReadXml(mypath)
            _dtt1 = ds1.Tables(0)
            If _dtt1.Select("setting = true and fieldname = 'templatePath'").Count > 0 Then
                If _dtt1.Select("setting = true and fieldname = 'templatePath'")(0)("misc") <> "" Then
                    _path = _dtt1.Select("setting = true and fieldname = 'templatePath'")(0)("misc")
                    updatefiles()
                End If
            End If
        End If
        updatetest()
        If _mode = 2 Then
            Dim y As MsgBoxResult = MsgBox("You have changed this template, you MUST re-load your mail list Do you want to do that now?", MsgBoxStyle.YesNo)
            If y = MsgBoxResult.Yes Then
                singletomultiple()
            End If
        End If
    End Sub
    Private Sub savexml()
        Dim x As String = ""
        'If System.IO.Directory.Exists(x) = False Then
        'System.IO.Directory.CreateDirectory(x)
        'End If
        If ds.Tables.Count = 0 Then
            ds.Tables.Add(_dtt)
        End If
        Dim fn As String = ""
        If Not _CurrentTemplate = "" Then
            fn = Replace(New FileInfo(_CurrentTemplate).Name, ".c2m", "")
        End If

        Dim s As String = InputBox("Enter Name of Template", , fn)

        If s.Length > 3 Then
            s = s & ".c2m"
        Else
            MsgBox("Not a proper name, Must be at least 3 charectors")
            Return
        End If

        Me._dtt.WriteXml(_path & "\" & s)
        _CurrentTemplate = s
    End Sub
    Private Sub loadtemplate()
        Dim mypath As String = _CurrentTemplate

        '  MsgBox(_CurrentTemplate)
        If mypath <> "" And System.IO.File.Exists(mypath) Then
            ds.Clear()
            ds.ReadXml(mypath)
            _dtt = ds.Tables(0)
            Me.DataGridView1.DataSource = _dtt
            updategrid(DataGridView1)
            drawrectangles()
            For Each dr As DataRow In _dtt.Rows


                If dr("FieldName") = "FirstPageConstantCompare" Then
                    _validatetext = dr("misc")
                End If
                Me.Label2.Text = "OK, you have completed the template, if you wish to start over simply do it again and start by selecting the area with:" & _dtt.Rows(0)("fieldname")
                startover = 1

            Next
            Dim t As New FileInfo(_CurrentTemplate)
            Me.lbl_CurrentTemplate.Text = "Current Template: " & Replace(t.Name, ".c2m", "")
        Else
            Me.lbl_CurrentTemplate.Text = ""
            setuptable()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        savexml()
        MsgBox("Item has saved")
        loadtemplate()
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        loadtemplate()
    End Sub
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        _CurrentCount = 0
        _CurrentTemplate = ""
        _dtt = Nothing
        loadtemplate()

        Me.DataGridView1.DataSource = _dtt
        updategrid(Me.DataGridView1)
        mouseb = False
        Me.PictureBox1.Invalidate()
    End Sub
#End Region
#Region "Address Functions"
    Private Sub revalidate()
        XMLDOC = Nothing
        badaddress = ""
        Dim batchnode As XmlNode = Nothing
        For Each ai As addressitem In DataGridView2.DataSource
            ParseAddressReprocess(ai, batchnode)
        Next
        Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC, badaddress})
        'Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
        Invoke(New updatedatagrid(AddressOf updatedatagridonMail), New Object() {DataGridView2.DataSource})
        Invoke(New updatecomplete(AddressOf updatecompleted), New Object() {})
    End Sub
    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        '       Dim checkstandard As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
        '        Dim checkstandardwarning As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            Dim y As addressitem = senderGrid.Rows(e.RowIndex).DataBoundItem
            standardizeaddress(y, True)
            If y.uspsStatus = "1" Then
                MsgBox("This address successfully passed standard address check")
            ElseIf y.uspsStatus = "2" Then
                MsgBox("This address successfully passed standard address check, but still is not complete and needs more info")
            Else
                MsgBox("This address is still bad.")
            End If
            Dim yy As MsgBoxResult = MsgBox("In order for any changes to addresses to take place, you need to revalidate this, do you want to do that now?", MsgBoxStyle.YesNo)
            If yy = MsgBoxResult.Yes Then
                Dim x As New Threading.Thread(AddressOf revalidate)
                x.IsBackground = False
                x.Start()
            End If

        End If


    End Sub
    Public Sub verify()
        Dim aic As New addresscollection
        Dim ai As addressitem = Nothing
        Dim sourceFileName As String = _sourcefilename
        Dim x As New FileStream(sourceFileName, FileMode.Open)
        Dim reader As New iTextSharp.text.pdf.PdfReader(x)
        'AWESOME!!
        x.Close()
        x.Dispose()
        Dim s As String = ""
        Dim s1 As String = ""
        Dim ep As System.Xml.XmlNode = Nothing
        Dim batch As System.Xml.XmlNode = Nothing
        Dim startingpage As System.Xml.XmlNode = Nothing
        Dim envelope As System.Xml.XmlNode = Nothing
        Dim pages As Integer = reader.NumberOfPages
        For i = 0 To reader.NumberOfPages - 1
            Me.Label2.Invoke(New updatetext(AddressOf updatelabel1text), New Object() {"Processing Page " & i + 1 & " of " & pages})
            Dim dr As DataRow = _dtt.Rows(0)
            Dim rect1 As New System.util.RectangleJ(dr("x"), dr("y"), dr("width"), dr("height"))
            Dim rf As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect1)
            Dim mystrat As New iTextSharp.text.pdf.parser.LocationTextExtractionStrategy
            Dim rtrf(1) As iTextSharp.text.pdf.parser.RegionTextRenderFilter
            rtrf(0) = rf
            'Dim rect2 As New System.util.RectangleJ(0, 700, 800, 140)
            'Dim rf2 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
            Dim textExtractionStrategy As New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)


            Dim dr1 As DataRow = _dtt.Rows(1)
            Dim rect2 As New System.util.RectangleJ(dr1("x"), dr1("y"), dr1("width"), dr1("height"))
            Dim rf1 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
            Dim mystrat1 As New iTextSharp.text.pdf.parser.LocationTextExtractionStrategy
            Dim rtrf1(1) As iTextSharp.text.pdf.parser.RegionTextRenderFilter
            rtrf1(0) = rf1
            'Dim rect2 As New System.util.RectangleJ(0, 700, 800, 140)
            'Dim rf2 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
            Dim textExtractionStrategy1 As New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat1, rtrf1)

            s = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i + 1, textExtractionStrategy)
            If Not s = "" Then

                s1 = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i + 1, textExtractionStrategy1)






                If parseaddress(s, s1, batch, i + 1, ep, envelope, startingpage, ai) Then
                    'Des not catch a single first page, anytime this is true it's a new first page
                    '    ep.InnerText = i + 1
                    ' Console.WriteLine(ai.Address1)
                    aic.Add(ai)
                End If

            End If

            If i = reader.NumberOfPages - 1 And Not ep Is Nothing Then
                ep.InnerText = i + 1
            End If

            If i = reader.NumberOfPages - 1 And Not ai Is Nothing Then
                ai.endpage = i + 1
            End If
            ' CurrentPage = CurrentPage + 1
        Next
        reader.Close()
        'Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC.OuterXml, badaddress})
        Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC, badaddress})
        'Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
        Invoke(New updatedatagrid(AddressOf updatedatagridonMail), New Object() {aic})
        Invoke(New updatecomplete(AddressOf updatecompleted), New Object() {})


        'rtrf(0) = rf2
        'textExtractionStrategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)
        'MsgBox(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy))
        ' iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy)





    End Sub
    Private Sub verifySingleItem()
        Dim aic As New addresscollection
        Dim sourceFileName As String = _sourcefilename
        Dim x As New FileStream(sourceFileName, FileMode.Open)
        Dim reader As New iTextSharp.text.pdf.PdfReader(x)

        'AWESOME!!
        '      x.Close()

        'x.Dispose()
        Dim s As String = ""
        Dim s1 As String = ""
        Dim ep As System.Xml.XmlNode = Nothing
        Dim batch As System.Xml.XmlNode = Nothing
        Dim pages As Integer = reader.NumberOfPages
        reader.Close()
        x.Close()
        If parseaddresssingle(_aiSingle, pages) Then
            'Des not catch a single first page, anytime this is true it's a new first page
            '    ep.InnerText = i + 1
            ' Console.WriteLine(ai.Address1)

        End If
        aic.Add(_aiSingle)


        reader.Close()
        'Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC.OuterXml, badaddress})
        Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC, badaddress})
        'Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
        Invoke(New updatedatagrid(AddressOf updatedatagridonMail), New Object() {aic})
        Invoke(New updatecomplete(AddressOf updatecompleted), New Object() {})


        'rtrf(0) = rf2
        'textExtractionStrategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)
        'MsgBox(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy))
        ' iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy)





    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        verifydocument()

    End Sub
    Public Class addressitem
        'Add Name
        'Add Address3


        Private _nName As String
        Private _norganization As String
        Private _nAddress3 As String 'This is not used except for single item
        Private _Address1 As String
        Private _Address2 As String
        Private _City As String
        Private _State As String
        Private _Zip5 As String
        Private _Zip4 As String
        Private _ReturnText As String
        Private _uspsStatus As String
        Private _ValidatedStatus As String
        Private _startpage As Integer
        Private _endpage As Integer
        Private _ommitted As Boolean
        Private _row As Integer
        Public Property ommitted As Boolean

            Get
                Return _ommitted
            End Get
            Set(value As Boolean)
                _ommitted = value
            End Set
        End Property
        Public Property norganization As String

            Get
                Return _norganization
            End Get
            Set(value As String)
                _norganization = value
            End Set
        End Property
        Public Property row As Integer

            Get
                Return _row
            End Get
            Set(value As Integer)
                _row = value
            End Set
        End Property
        Public Property nname As String

            Get
                Return _nName
            End Get
            Set(value As String)
                _nName = value
            End Set
        End Property
        Public Property nAddress3 As String

            Get
                Return _nAddress3
            End Get
            Set(value As String)
                _nAddress3 = value
            End Set
        End Property
        Public Property Address1 As String

            Get
                Return _Address1
            End Get
            Set(value As String)
                _Address1 = value
            End Set
        End Property

        Public Property Address2 As String

            Get
                Return _Address2
            End Get
            Set(value As String)
                _Address2 = value
            End Set
        End Property

        Public Property city As String

            Get
                Return _City
            End Get
            Set(value As String)
                _City = value
            End Set
        End Property

        Public Property state As String

            Get
                Return _State
            End Get
            Set(value As String)
                _State = value
            End Set
        End Property

        Public Property zip5 As String

            Get
                Return _Zip5
            End Get
            Set(value As String)
                _Zip5 = value
            End Set
        End Property

        Public Property zip4 As String

            Get
                Return _Zip4
            End Get
            Set(value As String)
                _Zip4 = value
            End Set
        End Property
        Public Property returntext As String

            Get
                Return _ReturnText
            End Get
            Set(value As String)
                _ReturnText = value
            End Set
        End Property
        Public Property uspsStatus As String

            Get
                Return _uspsStatus
            End Get
            Set(value As String)
                _uspsStatus = value
            End Set
        End Property
        Public Property validatedStatus As String

            Get
                Return _ValidatedStatus
            End Get
            Set(value As String)
                _ValidatedStatus = value
            End Set
        End Property
        Public Property startpage As Integer

            Get
                Return _startpage
            End Get
            Set(value As Integer)
                _startpage = value
            End Set
        End Property
        Public Property endpage As Integer

            Get
                Return _endpage
            End Get
            Set(value As Integer)
                _endpage = value
            End Set
        End Property
    End Class
    Public Class addresscollection

        Inherits CollectionBase
        Public Function Add(ByVal value As addressitem) As Integer
            Return List.Add(value)
        End Function 'Add
        Public Function IndexOf(ByVal value As addressitem) As Integer
            Return List.IndexOf(value)
        End Function 'IndexOf

        Public Sub Insert(ByVal index As Integer, ByVal value As addressitem)
            List.Insert(index, value)
        End Sub 'Insert
        Public Sub Remove(ByVal value As addressitem)
            List.Remove(value)
        End Sub 'Remove
        Public Function Item(ByVal index As Integer) As addressitem
            Return List.Item(index)
        End Function 'Remove
        Public Function Contains(ByVal Startpage As Integer) As addressitem
            For Each Col As addressitem In Me
                If Startpage = Startpage Then
                    Return Col
                End If
            Next
            Return Nothing
        End Function
    End Class
    Private Sub standardizeaddress(ByRef ai As addressitem, Optional ByVal bypassvalidate As Boolean = False)
        If bypassvalidate = False Then
            If ai.validatedStatus = False Then
                Return
            End If
        End If

        If ai.Address1 <> "" Then
            ai.Address1 = Replace(Replace(ai.Address1, "#", " APT "), "  ", " ")
            ai.Address1 = Replace(ai.Address1.ToUpper, " APT APT ", " APT ")
        End If
        If ai.Address2 <> "" Then
            ai.Address2 = Replace(Replace(ai.Address2, "#", " APT "), "  ", " ")
            ai.Address2 = Replace(ai.Address2.ToUpper, " APT APT ", " APT ")
        End If




        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim address As Uri
        Dim postStream As Stream = Nothing

        Dim _url = "http://production.shippingapis.com/ShippingAPITest.dll?API=Verify&XML="
        Dim xm As XmlDocument = New XmlDocument()
        Dim xadr As XmlNode = xm.CreateElement("AddressValidateRequest")
        Dim attr As XmlAttribute = xm.CreateAttribute("USERID")
        attr.Value = "264NPWU04230"
        xadr.Attributes.Append(attr)



        Dim xad As XmlNode = xm.CreateElement("Address")
        Dim xad1 As XmlNode = xm.CreateElement("Address1")
        xad1.InnerText = ai.Address1


        Dim xad2 As XmlNode = xm.CreateElement("Address2")
        xad2.InnerText = ai.Address2




        Dim xcity As XmlNode = xm.CreateElement("City")
        xcity.InnerText = ai.city
        Dim xstate As XmlNode = xm.CreateElement("State")
        xstate.InnerText = ai.state
        Dim xzip As XmlNode = xm.CreateElement("Zip5")
        xzip.InnerText = ai.zip5
        Dim xzip4 As XmlNode = xm.CreateElement("Zip4")

        xzip.InnerText = Microsoft.VisualBasic.Left(ai.zip5, 5)
        xad.AppendChild(xad1)
        xad.AppendChild(xad2)
        xad.AppendChild(xcity)
        xad.AppendChild(xstate)
        xad.AppendChild(xzip)
        xad.AppendChild(xzip4)

        xadr.AppendChild(xad)
        xm.AppendChild(xadr)


        Console.WriteLine(_url & xm.OuterXml)
        address = New Uri(_url & xm.OuterXml)


        ' Create the web request  
        request = DirectCast(WebRequest.Create(address), HttpWebRequest)
        request.Method = "GET"
        request.ContentType = "text/xml"

        'data = New StringBuilder()
        'data.Append("test=1")
        'data.Append("&layout=" + WebUtility.UrlEncode("Address on Separate Page"))
        'data.Append("&productionTime=" + WebUtility.UrlEncode("Next Day"))
        'data.Append("&envelope=" + WebUtility.UrlEncode("#10 Double Window"))
        'data.Append("&color=" + WebUtility.UrlEncode("Black and White"))
        'data.Append("&paperType=" + WebUtility.UrlEncode("White 24#"))
        'data.Append("&printOption=" + WebUtility.UrlEncode("Printing One side"))
        'data.Append("&documentId=" + WebUtility.UrlEncode(c2m.documentid))
        'data.Append("&addressId=" + WebUtility.UrlEncode(c2m.addressid))
        'Console.Write(data.ToString)
        'byteData = UTF8Encoding.UTF8.GetBytes(Data.ToString())

        '        postStream = request.GetRequestStream()
        '       postStream.Write(byteData, 0, byteData.Length)

        Try
            response = request.GetResponse()
        Catch wex As WebException
            ' This exception will be raised if the server didn't return 200 - OK  
            ' Try to retrieve more information about the network error  
            If Not wex.Response Is Nothing Then
                Dim errorResponse As HttpWebResponse = Nothing
                Try
                    errorResponse = DirectCast(wex.Response, HttpWebResponse)
                    Console.WriteLine( _
                      "The server returned '{0}' with the status code {1} ({2:d}).", _
                      errorResponse.StatusDescription, errorResponse.StatusCode, _
                      errorResponse.StatusCode)
                Finally
                    If Not errorResponse Is Nothing Then errorResponse.Close()
                End Try
            End If
        Finally
            If Not postStream Is Nothing Then postStream.Close()
        End Try
        '
        Try

            reader = New StreamReader(response.GetResponseStream())

            ' Console application output  
            Dim s As String = reader.ReadToEnd()
            reader.Close()
            If parsexml(s, "City") = "" Then
                ai.uspsStatus = "BAD ADDRESS"
                Return
            Else

                ai.Address1 = parsexml(s, "Address1")
                ai.Address2 = parsexml(s, "Address2")
                ai.city = parsexml(s, "City")
                ai.state = parsexml(s, "State")
                ai.zip5 = parsexml(s, "Zip5")
                ai.zip4 = parsexml(s, "Zip4")
                ai.returntext = parsexml(s, "ReturnText")
                ai.uspsStatus = "1"
                If Not ai.returntext = "" Then
                    ai.uspsStatus = "2"
                End If

            End If
            'Return parsexml(s, "id")
            '    c2m.StatusPick.jobStatus = parsexml(s, "status")
            'MsgBox(s)

        Finally
            ' If c2m.jobid = 0 Then
            '            c2m.StatusPick.jobStatus = 99
            'End If
            'If Not response Is Nothing Then response.Close()
        End Try
    End Sub
    Private Function parsexml(strxml As String, lookfor As String) As String

        Dim s As String = ""

        ' Create an XmlReader
        Try


            Using reader As XmlReader = XmlReader.Create(New StringReader(strxml))

                '            reader.ReadToFollowing(lookfor)
                'reader.MoveToFirstAttribute()
                'Dim genre As String = reader.Value
                'output.AppendLine("The genre value: " + genre)

                reader.ReadToFollowing(lookfor)
                s = reader.ReadElementContentAsString()
                reader.Close()

            End Using
        Catch ex As Exception
        End Try
        Return s



    End Function
#End Region
#Region "Send Data to Click to Mail Final Steps"
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btn_upload.Click
        keepopen = True
        If _mode = 2 Then
            Dim y As MsgBoxResult = MsgBox("You are sending this ENTIRE PDF to every name that is not Ommitted, CLick yes to continue", MsgBoxStyle.YesNo)
            If y = MsgBoxResult.No Then
                Return
            End If
            If File.Exists("tmp.pdf") Then
                Try
                    File.Delete("tmp.pdf")
                Catch ex As Exception
                    MsgBox("Please Close file and try again")
                    Exit Sub
                End Try

            End If
            Dim aic1 As addresscollection = Me.DataGridView2.DataSource
            utils.Merge(_sourcefilename, "tmp.pdf", aic1)
            Dim batchnode As XmlNode = Nothing

            For Each ai As addressitem In aic1
                If Not ai.ommitted Then
                    parseaddresssingle_GeneratedPDF(ai, batchnode)
                End If
            Next

            _sourcefilename = "tmp.pdf"

            Invoke(New updatedatagrid(AddressOf updatedatagridonMail), New Object() {aic1})
            'Invoke(New updatecomplete(AddressOf updatecompleted), New Object() {})
            'Process.Start("tmp.pdf")

            startupload()
            loadpdf(_sourcefilename)

        Else



            If XMLDOC Is Nothing Then
                verifydocument(True) '
                Return
            Else

                Dim y As MsgBoxResult = MsgBox("This is about to submit Do you want to Re Verify before Submit, if not any template changes since the last run will not be included?", MsgBoxStyle.YesNo)
                If y = MsgBoxResult.Yes Then
                    verifydocument(True)
                    Return
                Else
                    startupload()
                End If
            End If
        End If
    End Sub
    Private Sub startupload()
        rasterizer.Close()

        If _dtt.Select("setting = true and fieldname = 'testMode'")(0)("misc") = True Then
            mode = frm_clicktomail.mode.test
        Else
            mode = frm_clicktomail.mode.live
        End If
        sentXML = XMLDOC.OuterXml
        Console.Write(sentXML)
        Dim x As New frm_clicktomail(XMLDOC, _sourcefilename, mode, _dtt.Select("setting = true and fieldname = 'username'")(0)("misc"), Decrypt(_dtt.Select("setting = true and fieldname = 'password'")(0)("misc")), Me)
        x.keepopen = keepopen
        x.Show()

    End Sub
#End Region
#Region "File Watch"
    Private Sub FileSystemWatcher1_Changed(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Changed
        updatefiles()
    End Sub
    Private Sub FileSystemWatcher1_Created(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Created
        updatefiles()
    End Sub
    Private Sub updatefiles(Optional ByVal text As String = "")
        Try


            Dim s() As String = System.IO.Directory.GetFiles(_path & "\", text & "*.c2m")
            Me.lb_SavedTemplates.Items.Clear()
            For Each ss In s
                Dim f As New FileInfo(ss)
                Me.lb_SavedTemplates.Items.Add(Replace(f.Name, ".c2m", ""))
            Next
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Single to Multiple"
    Private Sub singletomultiple()

        If _xlsfilename = "" Then
            Dim y As MsgBoxResult = MsgBox("This feature is if you are sending EVERY PAGE of this PDF to multiple recipients, are you sure this is what you want to do?", MsgBoxStyle.YesNo)
            If y = MsgBoxResult.No Then
                Return
            End If
        End If

        Dim frm As New frm_sendSingleDocument
        frm._filename = _xlsfilename
        frm.TextBox1.Text = _xtemplate
        frm.ShowDialog()
        If frm.TextBox1.Text = "" Then
            Return
        End If
        Dim xTemplate As String = frm.TextBox1.Text

        XMLDOC = Nothing
        _xlsfilename = frm._filename
        _xtemplate = xTemplate
        _sourcefilename = Me.OpenFileDialog1.FileName
        badaddress = ""
        _startuploadwhendone = False
        Dim dt As DataTable = frm.DataGridView1.DataSource
        Me.DataGridView2.DataSource = Nothing
        If Not dt Is Nothing Then
            _mode = 2
            Dim x = New Threading.Thread(Sub() Me.verifysendsingletomultiple(xTemplate, dt))
            x.IsBackground = False
            x.Start()
        End If
    End Sub
    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        singletomultiple()
    End Sub
    Private Sub verifysendsingletomultiple(ByVal xtemplate As String, dt As DataTable)

        Dim aic As New addresscollection
        Dim x As String
        x = xtemplate
        Dim batchnode As Xml.XmlNode = Nothing

        Dim x1 As New FileStream(_sourcefilename, FileMode.Open)
        Dim reader As New iTextSharp.text.pdf.PdfReader(x1)
        'AWESOME!!
        x1.Close()
        x1.Dispose()
        Dim s As String = ""
        Dim s1 As String = ""
        Dim nop As Integer = reader.NumberOfPages
        reader.Close()
        Dim ep As System.Xml.XmlNode = Nothing
        Dim batch As System.Xml.XmlNode = Nothing
        Dim pages As Integer = reader.NumberOfPages
        Dim i As Integer = 0
        For Each r As DataRow In dt.Rows
            Me.Label2.Invoke(New updatetext(AddressOf updatelabel1text), New Object() {"Processing Page " & i + 1 & " of " & dt.Rows.Count})
            i += 1
            For Each c As DataColumn In dt.Columns
                Try
                    If Not dt.Rows(1)(c) Is DBNull.Value Then
                        x = Replace(x, "{" & c.ColumnName & "}", r(c))
                    Else
                        x = Replace(x, "{" & c.ColumnName & "}", "")
                    End If

                Catch
                End Try
            Next
            x = Regex.Replace(x, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
            Dim ai As addressitem = Nothing

            If parseaddresssingledoctomultiple(ai, batchnode, x, 1, nop, i) Then
            End If
            'EVERY PAGE IS GOOD
            aic.Add(ai)
            x = xtemplate
        Next
        'Me.DataGridView2.DataSource = aic


        Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC, badaddress})
        'Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
        Invoke(New updatedatagrid(AddressOf updatedatagridonMail), New Object() {aic})
        Invoke(New updatecomplete(AddressOf updatecompleted), New Object() {})
    End Sub
#End Region

    Private Sub SetupStationaryFields_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        'rasterizer.Close()
    End Sub
    Private Sub SetupStationaryFields_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If _file <> "" Then
            rasterizer.Close()
        End If

    End Sub

    Public Enum loadtype
        singledoc
        templatemulti
        regular
    End Enum
    Private _loadtype As loadtype = loadtype.regular
    Public Sub templatemulti(filename As String, template As String, Optional ByVal startuploadwhendone As Boolean = False)
        _loadtype = loadtype.templatemulti
        _sourcefilename = filename
        _CurrentTemplate = _path & "\" & template
        _startuploadwhendone = startuploadwhendone
        CurrentPage = 1
    End Sub
    Private Sub SetupStationaryFields_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _hideform Then
            '  Me.Hide()
            loadtemplate()

            If _dtt.Select("setting = true and fieldname = 'username'")(0)("misc") = "" Then
                MsgBox("You have not setup this Print Document Yet.")
                openform()
            End If
            badaddress = ""
            XMLDOC = Nothing
            If _loadtype = loadtype.singledoc Then
                Dim x As New Threading.Thread(AddressOf verifySingleItem)
                x.IsBackground = False
                x.Start()
            ElseIf _loadtype = loadtype.templatemulti Then
                verifydocument(True)
            End If
        End If
        gvi = New GhostscriptVersionInfo(sDLLPath)
        rasterizer = New GhostscriptRasterizer

        FileSystemWatcher1.Path = _path
        If _hideform = False Then
            startload()
        End If

        updatefiles()
        Me.WindowState = FormWindowState.Normal
        Me.Activate()

    End Sub
End Class

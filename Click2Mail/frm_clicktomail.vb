Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Public Class frm_clicktomail
    Private wr As HttpWebRequest
    Private _XMLDOC As XmlDocument = Nothing
    Private _file As String = String.Empty
    Private Const _Smainurl As String = "https://stage-batch.click2mail.com"
    Private Const _Lmainurl As String = "https://batch.click2mail.com"
    Private _url As String = String.Empty
    Private _authinfo As String = String.Empty
    Public Delegate Sub UpdateStatusTextCallback(ByVal text As String)
    Public Delegate Sub UpdateCountTextCallback(ByVal text As String)
    Public Delegate Sub processdone(ByVal success As Integer, results As String)
    Private frm As SetupStationaryFields

    Public Sub updatestatuslabel(ByVal text As String)
        Me.lbl_status.Text = text
    End Sub


    Public Sub updatecountlabel(ByVal text As String)
        Me.lbl_count.Text = text
    End Sub


    Public Sub webrequestdo()
        Try
            Dim batchid As Integer = 0
            Me.lbl_status.Invoke(New UpdateStatusTextCallback(AddressOf updatestatuslabel), New Object() {"Creating Batch"})
            batchid = createbatch()
            Me.lbl_status.Invoke(New UpdateStatusTextCallback(AddressOf updatestatuslabel), New Object() {"Uploading XML"})
            'uploadfilexml(batchid)
            uploadxml(batchid)
            Me.lbl_status.Invoke(New UpdateStatusTextCallback(AddressOf updatestatuslabel), New Object() {"Uploading Document"})
            uploadPDF(batchid)

            Me.lbl_status.Invoke(New UpdateStatusTextCallback(AddressOf updatestatuslabel), New Object() {"Submit"})
            submitbatch(batchid)
            Dim results As String = String.Empty
            If getbatchstatus(batchid, results) = "false" Then
                If parsexml(results, "submitted") = "true" Then
                    Invoke(New processdone(AddressOf processdonesub), New Object() {1, results})
                Else
                    Invoke(New processdone(AddressOf processdonesub), New Object() {2, results})
                End If

            Else
                Invoke(New processdone(AddressOf processdonesub), New Object() {3, results})
            End If

        Catch ex As Exception
            Dim results As String = "<error>There was an Error and this request did not complete, Please check your username and PW and also verify it is correctly set for the LIVE or Stage servers.  Keep in mind each server requires it's own login and credentials " & ex.Message & "</error>"
            Invoke(New processdone(AddressOf processdonesub), New Object() {3, results})
        End Try

    End Sub
    Public Sub processdonesub(ByVal success As Integer, text As String)

        _XMLDOC.LoadXml(text)
        frm.RichTextBox1.Text = frm.Beautify(_XMLDOC)


        If success = 1 Then
            MsgBox("Successfully Sent")
            frm.btn_upload.Enabled = False
        ElseIf success = 2 Then
            MsgBox("Batch Did not SUBMIT Successfully READ ERROR RESULTS", MsgBoxStyle.Critical)
            frm.merror = text
        ElseIf success = 3 Then
            MsgBox("Batch Did not complete Successfully", MsgBoxStyle.Critical)
            frm.merror = text
        End If
        Me.Close()
        frm.iscomplete = True
        frm.Close()
    End Sub
    Private Function getbatchstatus(ByVal batchid As Integer, ByRef results As String) As String
        Dim strURI As String = String.Empty
        strURI = _url & "/v1/batches/" & batchid
        Console.WriteLine(strURI)
        Dim request As HttpWebRequest = CType(WebRequest.Create(strURI), HttpWebRequest)
        Dim authinfo As String
        authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo))
        request.Headers("Authorization") = "Basic " + authinfo
        request.Method = System.Net.WebRequestMethods.Http.Get
        Dim result As String
        Try
            Using response As WebResponse = request.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream())
                    result = reader.ReadToEnd()

                End Using
            End Using
            results = result


            Return parsexml(result, "hasErrors")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return ""
    End Function
    Private Function createbatch() As Integer
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim address As Uri
        Dim data As StringBuilder = Nothing
        Dim byteData() As Byte = Nothing
        Dim postStream As Stream = Nothing

        address = New Uri(_url & "/v1/batches")

        Dim authinfo As String
        authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo))

        ' Create the web request  
        request = DirectCast(WebRequest.Create(address), HttpWebRequest)
        request.Headers("Authorization") = "Basic " + authinfo
        request.Method = "POST"
        request.ContentType = "text/plain"
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
            Console.Write(s)
            Return parsexml(s, "id")
            '    c2m.StatusPick.jobStatus = parsexml(s, "status")
            'MsgBox(s)

        Finally
            ' If c2m.jobid = 0 Then
            '            c2m.StatusPick.jobStatus = 99
            'End If
            'If Not response Is Nothing Then response.Close()
        End Try
    End Function
    Private Sub submitbatch(batchid As Integer)
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim address As Uri

        Dim postStream As Stream = Nothing

        address = New Uri(_url & "/v1/batches/" & batchid)

        Dim authinfo As String
        authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo))

        ' Create the web request  
        request = DirectCast(WebRequest.Create(address), HttpWebRequest)
        request.Headers("Authorization") = "Basic " + authinfo
        request.Method = "POST"
     
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
        Finally
        End Try

    End Sub
    Private Sub uploadxml(batchid As Integer)

        Dim strURI As String = String.Empty
        strURI = _url & "/v1/batches/" & batchid
        Dim request As HttpWebRequest = CType(WebRequest.Create(strURI), HttpWebRequest)
        Dim authinfo As String
        authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo))
        request.Headers("Authorization") = "Basic " + authinfo
        request.Accept = "application/xml"
        request.Method = "PUT"
        Using ms As New MemoryStream()
            _XMLDOC.Save(ms)
            request.ContentLength = ms.Length
            ms.WriteTo(request.GetRequestStream())
        End Using
        Dim result As String

        Using response As WebResponse = request.GetResponse()
            Using reader As New StreamReader(response.GetResponseStream())
                result = reader.ReadToEnd()
            End Using
        End Using

        Console.WriteLine(result)
    End Sub
    Private Sub uploadPDF(batchid As Integer)

        Dim client As New WebClient

        Dim strURI As String = String.Empty
        strURI = _url & "/v1/batches/" & batchid
        Dim authinfo As String
        authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo))
        client.Headers("Authorization") = "Basic " + authinfo
        client.Headers.Add("Content-Type", "application/pdf")
        'Dim sentXml As Byte() = System.Text.Encoding.ASCII.GetBytes(_XMLDOC.OuterXml)

        Dim fInfo As New FileInfo(_file)

        Dim numBytes As Long = fInfo.Length

        Dim fStream As New FileStream(_file, FileMode.Open, FileAccess.Read)

        Dim br As New BinaryReader(fStream)

        Dim data As Byte() = br.ReadBytes(CInt(numBytes))

        ' Show the number of bytes in the array.


        br.Close()

        fStream.Close()




        Dim response As Byte() = client.UploadData(strURI, "PUT", data)

        Console.WriteLine(System.Text.Encoding.Default.GetString(response))


        'Console.WriteLine(response.ToString())
    End Sub
    Private Function parsexml(strxml As String, lookfor As String) As String

        Dim s As String = 99

        ' Create an XmlReader
        Using reader As XmlReader = XmlReader.Create(New StringReader(strxml))

            '            reader.ReadToFollowing(lookfor)
            'reader.MoveToFirstAttribute()
            'Dim genre As String = reader.Value
            'output.AppendLine("The genre value: " + genre)

            reader.ReadToFollowing(lookfor)
            s = reader.ReadElementContentAsString()
            reader.Close()



        End Using

        Return s



    End Function
    Private Sub frm_clicktomail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ControlBox = False
        Me.TopMost = True
        Me.lbl_count.Text = _file
        Dim worker As New System.Threading.Thread(AddressOf webrequestdo)
        worker.IsBackground = True
        worker.Start()
    End Sub
    Public Enum mode
        test
        live
    End Enum
    Public Sub New(ByVal xml As XmlDocument, file As String, m As mode, username As String, pw As String, caller As SetupStationaryFields)
        frm = caller
        ' This call is required by the designer.
        InitializeComponent()
        If m = mode.live Then
            _url = _Lmainurl
        Else
            _url = _Smainurl
        End If
        _authinfo = username + ":" + pw
        _XMLDOC = xml
        _file = file
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
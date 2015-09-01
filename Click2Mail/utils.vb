Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class utils
    Public Shared Sub Merge(file As String, OutFile As String, ByRef aic As SetupStationaryFields.addresscollection)
        Using stream As New FileStream(OutFile, FileMode.Create)
            Using doc As New Document()
                Using pdf As New PdfCopy(doc, stream)
                    doc.Open()

                    Dim reader As PdfReader = Nothing
                    Dim page As PdfImportedPage = Nothing

                    'fixed typo
                    Dim ii As Integer = 0
                    Dim count As Integer = 0
                    For Each ai As SetupStationaryFields.addressitem In aic

                        If (Not ai.ommitted) Then


                            reader = New PdfReader(file)
                            PdfReader.unethicalreading = True
                            count = reader.NumberOfPages
                            For i As Integer = 0 To reader.NumberOfPages - 1
                                page = pdf.GetImportedPage(reader, i + 1)
                                pdf.AddPage(page)
                            Next

                            pdf.FreeReader(reader)
                            reader.Close()



                            ai.startpage = ((ii) * count) + 1
                            ai.endpage = (ii * count) + count
                            ii = ii + 1

                        End If
                    Next
                End Using
            End Using
            stream.Close()
        End Using
    End Sub
End Class

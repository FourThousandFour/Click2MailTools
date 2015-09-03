
'# Public API Tool for Click2Mail
'
'This file is part ofPublic API Tool for Click2Mail, Developed by Vincent D. Senese.
'
'Public API Tool for Click2Mail is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'Public API Tool for Click2Mail is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with Click2Mail Too.  If not, see <http://www.gnu.org/licenses/>.
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

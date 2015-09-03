
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

Public Class Launcher

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
        Dim fForm As New Click2Mail.SetupStationaryFields()
        fForm.ShowDialog()
        Me.Close()
    End Sub
End Class

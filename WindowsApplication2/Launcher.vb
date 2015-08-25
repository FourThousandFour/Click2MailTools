Public Class Launcher

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
        Dim fForm As New Click2Mail.SetupStationaryFields()
        fForm.ShowDialog()
        Me.Close()
    End Sub
End Class

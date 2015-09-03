<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_clicktomail
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
        Me.lbl_status = New System.Windows.Forms.Label()
        Me.lbl_count = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbl_status
        '
        Me.lbl_status.Font = New System.Drawing.Font("Lucida Bright", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_status.ForeColor = System.Drawing.SystemColors.InfoText
        Me.lbl_status.Location = New System.Drawing.Point(278, 87)
        Me.lbl_status.Name = "lbl_status"
        Me.lbl_status.Size = New System.Drawing.Size(547, 34)
        Me.lbl_status.TabIndex = 11
        Me.lbl_status.Text = "Wait"
        Me.lbl_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_count
        '
        Me.lbl_count.AutoSize = True
        Me.lbl_count.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_count.ForeColor = System.Drawing.SystemColors.InfoText
        Me.lbl_count.Location = New System.Drawing.Point(309, 147)
        Me.lbl_count.Name = "lbl_count"
        Me.lbl_count.Size = New System.Drawing.Size(0, 20)
        Me.lbl_count.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(12, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(288, 39)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Document Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(4, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(251, 39)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Current Status:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Britannic Bold", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(29, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(782, 41)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Items are currently being mailed Automatically"
        '
        'frm_clicktomail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(847, 233)
        Me.Controls.Add(Me.lbl_status)
        Me.Controls.Add(Me.lbl_count)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frm_clicktomail"
        Me.Text = "frm_clicktomail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_status As System.Windows.Forms.Label
    Friend WithEvents lbl_count As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

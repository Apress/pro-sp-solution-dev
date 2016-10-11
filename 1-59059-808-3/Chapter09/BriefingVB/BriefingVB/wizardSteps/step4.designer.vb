<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class step4
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(step4))
        Me.btnNext = New System.Windows.Forms.Button
        Me.lblInstructions = New System.Windows.Forms.Label
        Me.pnlLinks = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(22, 267)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 8
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblInstructions
        '
        Me.lblInstructions.AutoSize = True
        Me.lblInstructions.Location = New System.Drawing.Point(19, 20)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(197, 117)
        Me.lblInstructions.TabIndex = 6
        Me.lblInstructions.Text = resources.GetString("lblInstructions.Text")
        '
        'pnlLinks
        '
        Me.pnlLinks.AutoScroll = True
        Me.pnlLinks.Location = New System.Drawing.Point(22, 154)
        Me.pnlLinks.Name = "pnlLinks"
        Me.pnlLinks.Size = New System.Drawing.Size(194, 95)
        Me.pnlLinks.TabIndex = 9
        '
        'step4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlLinks)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblInstructions)
        Me.Name = "step4"
        Me.Size = New System.Drawing.Size(250, 391)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblInstructions As System.Windows.Forms.Label
    Friend WithEvents pnlLinks As System.Windows.Forms.Panel

End Class

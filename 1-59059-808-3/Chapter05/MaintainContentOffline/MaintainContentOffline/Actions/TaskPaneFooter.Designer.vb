<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class TaskPaneFooter
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.lblFooterMessage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblFooterMessage
        '
        Me.lblFooterMessage.AutoSize = True
        Me.lblFooterMessage.Location = New System.Drawing.Point(4, 4)
        Me.lblFooterMessage.Name = "lblFooterMessage"
        Me.lblFooterMessage.Size = New System.Drawing.Size(0, 0)
        Me.lblFooterMessage.TabIndex = 0
        '
        'TaskPaneFooter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblFooterMessage)
        Me.Name = "TaskPaneFooter"
        Me.Size = New System.Drawing.Size(300, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFooterMessage As System.Windows.Forms.Label

End Class

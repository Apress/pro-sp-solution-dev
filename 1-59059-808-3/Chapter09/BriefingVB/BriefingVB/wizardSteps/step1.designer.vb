<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class step1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(step1))
        Me.lblUrl = New System.Windows.Forms.Label
        Me.txtSiteUrl = New System.Windows.Forms.TextBox
        Me.btnNext = New System.Windows.Forms.Button
        Me.lblWelcome = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblUrl
        '
        Me.lblUrl.AutoSize = True
        Me.lblUrl.Location = New System.Drawing.Point(21, 117)
        Me.lblUrl.Name = "lblUrl"
        Me.lblUrl.Size = New System.Drawing.Size(23, 13)
        Me.lblUrl.TabIndex = 11
        Me.lblUrl.Text = "Url:"
        '
        'txtSiteUrl
        '
        Me.txtSiteUrl.Location = New System.Drawing.Point(44, 114)
        Me.txtSiteUrl.Name = "txtSiteUrl"
        Me.txtSiteUrl.Size = New System.Drawing.Size(194, 20)
        Me.txtSiteUrl.TabIndex = 10
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(24, 150)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 9
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblWelcome
        '
        Me.lblWelcome.AutoSize = True
        Me.lblWelcome.Location = New System.Drawing.Point(19, 20)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(191, 78)
        Me.lblWelcome.TabIndex = 8
        Me.lblWelcome.Text = resources.GetString("lblWelcome.Text")
        '
        'step1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblUrl)
        Me.Controls.Add(Me.txtSiteUrl)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblWelcome)
        Me.Name = "step1"
        Me.Size = New System.Drawing.Size(250, 391)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUrl As System.Windows.Forms.Label
    Friend WithEvents txtSiteUrl As System.Windows.Forms.TextBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblWelcome As System.Windows.Forms.Label

End Class

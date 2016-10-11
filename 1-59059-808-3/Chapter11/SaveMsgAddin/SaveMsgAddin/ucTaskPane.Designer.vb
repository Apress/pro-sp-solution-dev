<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTaskPane
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.btnGo = New System.Windows.Forms.Button
        Me.pnlDetails = New System.Windows.Forms.Panel
        Me.btnSave = New System.Windows.Forms.Button
        Me.chkSaveEmail = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optEmailSender = New System.Windows.Forms.RadioButton
        Me.optEmailSubject = New System.Windows.Forms.RadioButton
        Me.optRoot = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.lstLibrary = New System.Windows.Forms.ComboBox
        Me.lblMessage = New System.Windows.Forms.Label
        Me.pnlDetails.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enter the URL of the  SharePoint site where" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "you would like to save the email."
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(17, 48)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(173, 20)
        Me.txtURL.TabIndex = 1
        '
        'btnGo
        '
        Me.btnGo.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnGo.Location = New System.Drawing.Point(196, 48)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(36, 23)
        Me.btnGo.TabIndex = 2
        Me.btnGo.Text = "GO"
        Me.btnGo.UseVisualStyleBackColor = False
        '
        'pnlDetails
        '
        Me.pnlDetails.AutoScroll = True
        Me.pnlDetails.Controls.Add(Me.btnSave)
        Me.pnlDetails.Controls.Add(Me.chkSaveEmail)
        Me.pnlDetails.Controls.Add(Me.GroupBox1)
        Me.pnlDetails.Controls.Add(Me.Label2)
        Me.pnlDetails.Controls.Add(Me.lstLibrary)
        Me.pnlDetails.Enabled = False
        Me.pnlDetails.Location = New System.Drawing.Point(3, 74)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(240, 236)
        Me.pnlDetails.TabIndex = 3
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnSave.Location = New System.Drawing.Point(14, 193)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'chkSaveEmail
        '
        Me.chkSaveEmail.AutoSize = True
        Me.chkSaveEmail.Location = New System.Drawing.Point(14, 51)
        Me.chkSaveEmail.Name = "chkSaveEmail"
        Me.chkSaveEmail.Size = New System.Drawing.Size(114, 17)
        Me.chkSaveEmail.TabIndex = 3
        Me.chkSaveEmail.Text = "Save original email"
        Me.chkSaveEmail.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optEmailSender)
        Me.GroupBox1.Controls.Add(Me.optEmailSubject)
        Me.GroupBox1.Controls.Add(Me.optRoot)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 84)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 103)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Attachment Options"
        '
        'optEmailSender
        '
        Me.optEmailSender.AutoSize = True
        Me.optEmailSender.Location = New System.Drawing.Point(7, 68)
        Me.optEmailSender.Name = "optEmailSender"
        Me.optEmailSender.Size = New System.Drawing.Size(174, 17)
        Me.optEmailSender.TabIndex = 2
        Me.optEmailSender.TabStop = True
        Me.optEmailSender.Text = "Save in folders by e-mail sender"
        Me.optEmailSender.UseVisualStyleBackColor = True
        '
        'optEmailSubject
        '
        Me.optEmailSubject.AutoSize = True
        Me.optEmailSubject.Location = New System.Drawing.Point(7, 44)
        Me.optEmailSubject.Name = "optEmailSubject"
        Me.optEmailSubject.Size = New System.Drawing.Size(176, 17)
        Me.optEmailSubject.TabIndex = 1
        Me.optEmailSubject.TabStop = True
        Me.optEmailSubject.Text = "Save in folders by e-mail subject"
        Me.optEmailSubject.UseVisualStyleBackColor = True
        '
        'optRoot
        '
        Me.optRoot.AutoSize = True
        Me.optRoot.Location = New System.Drawing.Point(7, 20)
        Me.optRoot.Name = "optRoot"
        Me.optRoot.Size = New System.Drawing.Size(111, 17)
        Me.optRoot.TabIndex = 0
        Me.optRoot.TabStop = True
        Me.optRoot.Text = "Save in root folder"
        Me.optRoot.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Library:"
        '
        'lstLibrary
        '
        Me.lstLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstLibrary.FormattingEnabled = True
        Me.lstLibrary.Location = New System.Drawing.Point(66, 14)
        Me.lstLibrary.Name = "lstLibrary"
        Me.lstLibrary.Size = New System.Drawing.Size(159, 21)
        Me.lstLibrary.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(14, 326)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(97, 13)
        Me.lblMessage.TabIndex = 6
        Me.lblMessage.Text = "Complete Message"
        Me.lblMessage.Visible = False
        '
        'ucTaskPane
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ucTaskPane"
        Me.Size = New System.Drawing.Size(243, 383)
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstLibrary As System.Windows.Forms.ComboBox
    Friend WithEvents chkSaveEmail As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optEmailSender As System.Windows.Forms.RadioButton
    Friend WithEvents optEmailSubject As System.Windows.Forms.RadioButton
    Friend WithEvents optRoot As System.Windows.Forms.RadioButton
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label

End Class

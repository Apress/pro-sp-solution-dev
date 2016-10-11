<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SyncOptions
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblLastSyncMessage = New System.Windows.Forms.Label
        Me.btnSync = New System.Windows.Forms.Button
        Me.lblSyncChanges = New System.Windows.Forms.Label
        Me.lblSyncNewLists = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblSyncStatus = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 52)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Use this panel to sync the data in your spreadsheet" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "with site content. If there " & _
            "are items that you have " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "changed and that have changed in the site, you " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "will " & _
            "be asked which change should win."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(306, 87)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Instructions"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnRefresh)
        Me.GroupBox2.Controls.Add(Me.lblLastSyncMessage)
        Me.GroupBox2.Controls.Add(Me.btnSync)
        Me.GroupBox2.Controls.Add(Me.lblSyncChanges)
        Me.GroupBox2.Controls.Add(Me.lblSyncNewLists)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 102)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(306, 112)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pending Changes"
        '
        'lblLastSyncMessage
        '
        Me.lblLastSyncMessage.AutoSize = True
        Me.lblLastSyncMessage.Location = New System.Drawing.Point(10, 20)
        Me.lblLastSyncMessage.Name = "lblLastSyncMessage"
        Me.lblLastSyncMessage.Size = New System.Drawing.Size(101, 13)
        Me.lblLastSyncMessage.TabIndex = 3
        Me.lblLastSyncMessage.Text = "Last Sync Occured:"
        '
        'btnSync
        '
        Me.btnSync.Location = New System.Drawing.Point(13, 80)
        Me.btnSync.Name = "btnSync"
        Me.btnSync.Size = New System.Drawing.Size(75, 23)
        Me.btnSync.TabIndex = 2
        Me.btnSync.Text = "Sync Now"
        Me.btnSync.UseVisualStyleBackColor = True
        '
        'lblSyncChanges
        '
        Me.lblSyncChanges.AutoSize = True
        Me.lblSyncChanges.Location = New System.Drawing.Point(10, 59)
        Me.lblSyncChanges.Name = "lblSyncChanges"
        Me.lblSyncChanges.Size = New System.Drawing.Size(197, 13)
        Me.lblSyncChanges.TabIndex = 1
        Me.lblSyncChanges.Text = "0: Number of changes you made to data"
        '
        'lblSyncNewLists
        '
        Me.lblSyncNewLists.AutoSize = True
        Me.lblSyncNewLists.Location = New System.Drawing.Point(10, 42)
        Me.lblSyncNewLists.Name = "lblSyncNewLists"
        Me.lblSyncNewLists.Size = New System.Drawing.Size(162, 13)
        Me.lblSyncNewLists.TabIndex = 0
        Me.lblSyncNewLists.Text = "0: Number of new lists to capture"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Sync Status:"
        '
        'lblSyncStatus
        '
        Me.lblSyncStatus.AutoSize = True
        Me.lblSyncStatus.Location = New System.Drawing.Point(90, 232)
        Me.lblSyncStatus.Name = "lblSyncStatus"
        Me.lblSyncStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblSyncStatus.TabIndex = 4
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(3, 257)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(306, 228)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Manage Collisions"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(226, 9)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'SyncOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.lblSyncStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SyncOptions"
        Me.Size = New System.Drawing.Size(312, 600)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSync As System.Windows.Forms.Button
    Friend WithEvents lblSyncChanges As System.Windows.Forms.Label
    Friend WithEvents lblSyncNewLists As System.Windows.Forms.Label
    Friend WithEvents lblLastSyncMessage As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSyncStatus As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button

End Class

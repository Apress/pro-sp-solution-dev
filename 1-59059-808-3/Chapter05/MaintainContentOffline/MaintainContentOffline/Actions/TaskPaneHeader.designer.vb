<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class TaskPaneHeader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TaskPaneHeader))
        Me.taskPaneToolStrip = New System.Windows.Forms.ToolStrip
        Me.btnSearch = New System.Windows.Forms.ToolStripButton
        Me.btnData = New System.Windows.Forms.ToolStripButton
        Me.taskPaneToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'taskPaneToolStrip
        '
        Me.taskPaneToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSearch, Me.btnData})
        Me.taskPaneToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.taskPaneToolStrip.Name = "taskPaneToolStrip"
        Me.taskPaneToolStrip.Size = New System.Drawing.Size(300, 25)
        Me.taskPaneToolStrip.TabIndex = 0
        Me.taskPaneToolStrip.Text = "ToolStrip1"
        '
        'btnSearch
        '
        Me.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(23, 22)
        Me.btnSearch.Text = "Browse"
        '
        'btnData
        '
        Me.btnData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnData.Image = CType(resources.GetObject("btnData.Image"), System.Drawing.Image)
        Me.btnData.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnData.Name = "btnData"
        Me.btnData.Size = New System.Drawing.Size(23, 22)
        Me.btnData.Text = "Data"
        '
        'TaskPaneHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.taskPaneToolStrip)
        Me.Name = "TaskPaneHeader"
        Me.Size = New System.Drawing.Size(300, 36)
        Me.taskPaneToolStrip.ResumeLayout(False)
        Me.taskPaneToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents taskPaneToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnData As System.Windows.Forms.ToolStripButton

End Class

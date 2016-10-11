
Public Class ThisWorkbook

    Friend WithEvents m_taskPaneHeader As TaskPaneHeader
    Friend WithEvents m_taskPaneBody As TaskPaneBody
    Friend WithEvents m_taskPaneFooter As TaskPaneFooter

    Public SharePointNamespacePrefix As String = "sp"
    Public SharePointNamespaceUri As String = "http://schemas.microsoft.com/sharepoint/soap/"
    Public ListItemsNamespacePrefix As String = "z"
    Public ListItemsNamespaceUri As String = "#RowsetSchema"

    Private Sub ThisWorkbook_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup
        SetupTaskPane()
    End Sub

    Private Sub ThisWorkbook_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

    Private Sub SetupTaskPane()
        With Me.Application.CommandBars("Task Pane")
            .Width = 350
            .Position = Microsoft.Office.Core.MsoBarPosition.msoBarLeft
        End With

        Globals.Sheet1.Select()

        m_taskPaneHeader = New TaskPaneHeader()
        m_taskPaneBody = New TaskPaneBody()
        m_taskPaneFooter = New TaskPaneFooter()

        m_taskPaneHeader.Dock = DockStyle.Top
        m_taskPaneFooter.Dock = DockStyle.Bottom

        Me.ActionsPane.Controls.Add(m_taskPaneHeader)
        Me.ActionsPane.Controls.Add(m_taskPaneBody)
        Me.ActionsPane.Controls.Add(m_taskPaneFooter)
    End Sub

End Class

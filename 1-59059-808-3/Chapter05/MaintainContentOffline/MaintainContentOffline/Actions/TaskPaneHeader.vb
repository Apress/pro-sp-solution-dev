Public Class TaskPaneHeader


    Dim m_TaskPaneBody As TaskPaneBody

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        HideCurrent()
        m_TaskPaneBody.m_MaintainSites.Visible = True
    End Sub

    Private Sub HideCurrent()
        Dim ctl As Control
        For Each ctl In m_TaskPaneBody.Controls
            If ctl.Visible Then ctl.Visible = False
        Next
    End Sub

    Private Sub TaskPaneHeader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        m_TaskPaneBody = Globals.ThisWorkbook.m_taskPaneBody
    End Sub

    Private Sub btnData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnData.Click
        Me.DisplaySyncDialog()
    End Sub

    Public Sub DisplaySyncDialog()
        HideCurrent()
        m_TaskPaneBody.m_SyncOptions.Visible = True
    End Sub
End Class

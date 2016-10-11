Imports Microsoft.Office.Tools

Public Class ThisAddIn
    Public ctp As CustomTaskPane


    Private Sub ThisAddIn_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup
        ctp = Globals.ThisAddIn.CustomTaskPanes.Add( _
                        New ucTaskPane(), "Custom Briefing")
        ctp.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight
        ctp.Width = 250
    End Sub

    Private Sub ThisAddIn_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

End Class

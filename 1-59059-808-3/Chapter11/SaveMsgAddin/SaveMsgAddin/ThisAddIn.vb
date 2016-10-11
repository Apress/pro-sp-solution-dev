Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Microsoft.Office.Tools
Imports System.Collections

Public Class ThisAddIn
    Dim ctp As CustomTaskPane
    Private mInspectors As ArrayList = New ArrayList()

    Private Sub ThisAddIn_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup

    End Sub

    Private Sub ThisAddIn_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

    Public Sub ShowTaskPane()
        Dim currInspector As Outlook.Inspector = Me.Application.ActiveInspector
        ctp = Me.CustomTaskPanes.Add(New ucTaskPane(), "Save  to SharePoint", currInspector)
        ctp.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight
        ctp.Width = 250
        ctp.Visible = True
        mInspectors.Add(currInspector)
        AddHandler currInspector.Close, AddressOf Window_Close
    End Sub

    Public Sub RemoveTaskPane()
        Dim currInspector As Outlook.Inspector = Me.Application.ActiveInspector
        CleanUp(currInspector)

    End Sub

    Private Sub CleanUp(ByVal activeInspector As Outlook.Inspector)
        Dim i As Integer = 0
        For i = 0 To mInspectors.Count - 1
            Dim item As Outlook.Inspector = CType(mInspectors(i), Outlook.Inspector)
            If (item Is activeInspector) Then
                Me.ribbon.ResetState()
                Me.CustomTaskPanes.RemoveAt(i)
                mInspectors.RemoveAt(i)
            End If
        Next
    End Sub


    Private Sub Window_Close()
        Dim currInspector As Outlook.Inspector = Me.Application.ActiveInspector
        CleanUp(currInspector)
    End Sub

    Public Function GetPressed() As Boolean
        Dim currInspector As Outlook.Inspector = Me.Application.ActiveInspector
        Dim i As Integer = 0
        For i = 0 To mInspectors.Count - 1
            Dim item As Outlook.Inspector = CType(mInspectors(i), Outlook.Inspector)
            If (item Is currInspector) Then
                Return Me.CustomTaskPanes(i).Visible
            End If
        Next
        Return False
    End Function

    Private Sub Application_Quit() Handles Application.Quit
        'MessageBox.Show(Me.CustomTaskPanes.Count)
    End Sub

End Class

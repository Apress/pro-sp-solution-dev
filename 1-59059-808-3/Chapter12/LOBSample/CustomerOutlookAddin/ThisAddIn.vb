Imports Outlook = Microsoft.Office.Interop.Outlook
public class ThisAddIn



    Private Sub ThisAddIn_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup

    End Sub

    Private Sub ThisAddIn_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

    Protected Overrides Function RequestService(ByVal serviceGuid As System.Guid) As Object
        If serviceGuid = GetType(Outlook.FormRegionStartup).GUID Then
            Return New FormRegionHelper()
        Else
            Return MyBase.RequestService(serviceGuid)
        End If
    End Function

End class

Public Class ConnectionManager

    Private Declare Function InternetGetConnectedState Lib "wininet" (ByRef dwflags As Int32, ByVal dwReserved As Int32) As Boolean

   
    Public Shared Function IsOnline() As Boolean
        Dim dwflags As Long
        Dim WebTest As Boolean
        WebTest = InternetGetConnectedState(dwflags, 0&)
        Return WebTest
    End Function

    


End Class

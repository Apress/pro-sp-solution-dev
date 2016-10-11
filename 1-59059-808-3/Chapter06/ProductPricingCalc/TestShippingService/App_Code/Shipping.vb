Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Shipping
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function CalcShipping(ByVal startZipCode As String, ByVal endZipCode As String, ByVal totalWeight As Double) As Double
        Dim retVal As Double = -1
        Select Case endZipCode
            Case "02108"
                If (totalWeight <= 70) Then
                    retVal = 13.74
                Else
                    retVal = 23.74
                End If

            Case "32803"
                If (totalWeight <= 70) Then
                    retVal = 17.56
                Else
                    retVal = 27.36
                End If
            Case Else
                Throw New ApplicationException("We do not ship to that location")
        End Select
        Return retVal
    End Function

End Class

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Office = Microsoft.Office.Core

' TODO:
' This is an override of the RequestService method in the ThisAddIn class.
' To hook up your custom ribbon uncomment this code.
Partial Public Class ThisAddIn

    Private ribbon As Ribbon1

    Protected Overrides Function RequestService(ByVal serviceGuid As Guid) As Object
        If serviceGuid = GetType(Office.IRibbonExtensibility).GUID Then
            If ribbon Is Nothing Then
                ribbon = New Ribbon1()
            End If
            Return ribbon
        End If

        Return MyBase.RequestService(serviceGuid)
    End Function

End Class

<ComVisible(True)> _
    Public Class Ribbon1
    Implements Office.IRibbonExtensibility

    Private ribbon As Office.IRibbonUI

    Public Sub New()
    End Sub

    Public Function GetCustomUI(ByVal ribbonID As String) As String Implements Office.IRibbonExtensibility.GetCustomUI
        Dim xmlMarkup As String = String.Empty
        Select Case (ribbonID)
            Case "Microsoft.Outlook.Mail.Read"
                xmlMarkup = My.Resources.Ribbon1
        End Select
        Return xmlMarkup
    End Function

#Region "Ribbon Callbacks"

    Public Sub OnLoad(ByVal ribbonUI As Office.IRibbonUI)
        Me.ribbon = ribbonUI
    End Sub

    Public Sub OnToggleButton1(ByVal control As Office.IRibbonControl, ByVal isPressed As Boolean)
        If isPressed Then
            Globals.ThisAddIn.ShowTaskPane()

        Else
            Globals.ThisAddIn.RemoveTaskPane()
        End If
    End Sub
    Public Function GetPressed(ByVal control As Office.IRibbonControl) As Boolean
        Return Globals.ThisAddIn.GetPressed()
    End Function

    Public Sub ResetState()
        Me.ribbon.InvalidateControl("toggleButton1")
    End Sub

    Public Function GetImage(ByVal control As Office.IRibbonControl) As stdole.IPictureDisp
        Dim pic As stdole.IPictureDisp = Nothing
        Select Case control.Id
            Case "toggleButton1"
                pic = PictureDispMaker.ConvertIcon(My.Resources.share)
        End Select
        Return pic
    End Function




#End Region

#Region "Helpers"

    Private Shared Function GetResourceText(ByVal resourceName As String) As String
        Dim asm As Assembly = Assembly.GetExecutingAssembly()
        Dim resourceNames() As String = asm.GetManifestResourceNames()
        For i As Integer = 0 To resourceNames.Length - 1
            If String.Compare(resourceName, resourceNames(i), StringComparison.OrdinalIgnoreCase) = 0 Then
                Using resourceReader As StreamReader = New StreamReader(asm.GetManifestResourceStream(resourceNames(i)))
                    If resourceReader IsNot Nothing Then
                        Return resourceReader.ReadToEnd()
                    End If
                End Using
            End If
        Next
        Return Nothing
    End Function

#End Region

End Class

Friend Class PictureDispMaker
    Inherits System.Windows.Forms.AxHost

    Sub New()
        MyBase.New(Nothing)
    End Sub

    Public Shared Function ConvertImage(ByVal image As System.Drawing.Image) As stdole.IPictureDisp
        Return AxHost.GetIPictureDispFromPicture(image)
    End Function

    Public Shared Function ConvertIcon(ByVal icon As System.Drawing.Icon) As stdole.IPictureDisp
        Return ConvertImage(icon.ToBitmap())
    End Function
End Class



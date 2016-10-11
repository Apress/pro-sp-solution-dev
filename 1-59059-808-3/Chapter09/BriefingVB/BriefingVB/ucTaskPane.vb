Imports System.Xml
Imports System.Collections


Public Class ucTaskPane
    Dim WithEvents wizard1 As step1 = New step1()
    Dim WithEvents wizard2 As step2 = New step2()
    Dim WithEvents wizard3 As step3 = New step3()
    Dim WithEvents wizard4 As step4 = New step4()
    Dim WithEvents wizard5 As step5 = New step5()


    Public HasAgendaList As Boolean = True
    Public HasObjectivesList As Boolean = True
    Public HasSlideLibrary As Boolean = True
    Public SlideLibraries As ArrayList = New ArrayList()

    Public SiteUrl As String

    Public Const SharePointNamespacePrefix As String = "sp"
    Public Const SharePointNamespaceUri As String = "http://schemas.microsoft.com/sharepoint/soap/"
    Public Const ListItemsNamespacePrefix As String = "z"
    Public Const ListItemsNamespaceUri As String = "#RowsetSchema"
    
    Private currentStep As Integer = 1


    Private Sub ucTaskPane_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Controls.Add(wizard1)
        Me.Controls.Add(wizard2)
        Me.Controls.Add(wizard3)
        Me.Controls.Add(wizard4)
        Me.Controls.Add(wizard5)
        wizard1.Start()
    End Sub


    Private Sub wizard_Completed(ByVal sender As Object, ByVal e As EventArgs) _
        Handles wizard1.Completed, wizard2.Completed, wizard3.Completed, wizard4.Completed
        If currentStep < 5 Then Me.MoveToNextStep()
    End Sub

    Private Sub MoveToNextStep()
        Me.Controls(currentStep - 1).Visible = False
        currentStep += 1
        CType(Me.Controls(currentStep - 1), IStep).Start()
    End Sub

End Class

Public Structure LibraryItem
    Public Url As String
    Public Name As String
End Structure


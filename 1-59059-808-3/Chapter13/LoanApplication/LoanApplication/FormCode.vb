Imports Microsoft.Office.InfoPath
Imports System
Imports System.Xml
Imports System.Xml.XPath

Namespace LoanApplication
    Public Class FormCode
        ' Member variables are not supported in browser-enabled forms.
        ' Instead, write and read these values from the FormState
        ' dictionary using code such as the following:
        '
        ' Private Property _memberVariable() As Object
        '     Get
        '         _memberVariable = FormState("_memberVariable")
        '     End Get
        '     Set
        '         FormState("_memberVariable") = value
        '     End Set
        ' End Property

        ' NOTE: The following procedure is required by Microsoft Office InfoPath.
        ' It can be modified using Microsoft Office InfoPath.
        Private Sub InternalStartup(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Startup
            AddHandler EventManager.XmlEvents("/my:Loan/my:myFields").Changed, AddressOf myFields_Changed
            AddHandler DirectCast(EventManager.ControlEvents("btnManaged"), ButtonEvent).Clicked, AddressOf btnManaged_Clicked
        End Sub

        Public Sub myFields_Changed(ByVal sender As Object, ByVal e As XmlEventArgs)
            ' Write your code here to change the main data source.
        End Sub

        
        Public Sub btnManaged_Clicked(ByVal sender As Object, ByVal e As ClickedEventArgs)
            ' Write your code here.
            Dim root, user As System.Xml.XPath.XPathNavigator
            root = Me.MainDataSource.CreateNavigator
            user = root.SelectSingleNode("/my:Loan/my:FirstName", Me.NamespaceManager)
            user.SetValue("Hi Susie")
        End Sub
    End Class
End Namespace

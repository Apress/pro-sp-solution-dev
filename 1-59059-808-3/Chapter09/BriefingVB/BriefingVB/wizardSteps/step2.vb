Imports System.Xml
Imports System.Text


Public Class step2
    Implements IStep

    Public Event Completed(ByVal sender As Object, ByVal e As System.EventArgs) Implements IStep.Completed
    Delegate Sub UIContinue()
    Private taskPane As ucTaskPane

    Public ReadOnly Property ParentPane() As ucTaskPane Implements IStep.ParentPane
        Get
            Return taskPane
        End Get
    End Property

    Public Sub Start() Implements IStep.Start
        If (ParentPane.HasObjectivesList) Then
            'show this step
            Me.Visible = True
        Else
            'skip over this step
            Me.WorkComplete()
        End If

    End Sub

    Public Sub WorkComplete() Implements IStep.WorkComplete
        RaiseEvent Completed(Me, New EventArgs())
    End Sub

    Private Sub step2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        taskPane = DirectCast(Me.Parent, ucTaskPane)
    End Sub

    Private Sub btnBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuild.Click
        Me.UseWaitCursor = True

        'retrieve objectives
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = ParentPane.SiteUrl + "/_vti_bin/lists.asmx"

        'build query parameters for list items
        Dim xmlDoc As XmlDocument = New XmlDocument()
        Dim ndQuery As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "")
        Dim ndViewFields As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "")
        Dim ndQueryOptions As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "")

        ndQueryOptions.InnerXml = "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>"
        ndViewFields.InnerXml = "<FieldRef Name='Objective'/>"
        ndQuery.InnerXml = ""

        AddHandler listService.GetListItemsCompleted, AddressOf GetListItemsCompleted
        listService.GetListItemsAsync("Objectives", Nothing, ndQuery, ndViewFields, Nothing, ndQueryOptions, Nothing)

    End Sub

    Public Sub GetListItemsCompleted(ByVal sender As Object, ByVal e As WSLists.GetListItemsCompletedEventArgs)
        If (e.Error Is Nothing) Then

            Dim ndItems As XmlNode
            ndItems = e.Result

            Dim xmlDoc As XmlDocument = New XmlDocument()
            xmlDoc.LoadXml(ndItems.OuterXml)
            Dim namespaceMgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDoc.NameTable)
            namespaceMgr.AddNamespace(ucTaskPane.ListItemsNamespacePrefix, ucTaskPane.ListItemsNamespaceUri)
            Dim objectiveNodeList As XmlNodeList = xmlDoc.SelectNodes("//z:row", namespaceMgr)

            Dim slide As PowerPoint.Slide
            Dim presentation As PowerPoint.Presentation
            presentation = Globals.ThisAddIn.Application.ActivePresentation
            slide = presentation.Slides.Add(presentation.Slides.Count + 1, PowerPoint.PpSlideLayout.ppLayoutText)
            slide.Shapes.Item(1).TextFrame.TextRange.Text = "Objectives"

            Dim sBuilder As StringBuilder = New StringBuilder()
            Dim objNode As XmlNode
            For Each objNode In objectiveNodeList
                sBuilder.Append(objNode.Attributes("ows_Objective").InnerText)
                sBuilder.Append(vbCr)
            Next
            slide.Shapes.Item(2).TextFrame.TextRange.Text = sBuilder.ToString()
            Globals.ThisAddIn.Application.ActiveWindow.View.GotoSlide(slide.SlideIndex)
        End If

        Me.UseWaitCursor = False
        'go back to the main thread to continue
        Dim uiContinueDelegate As New UIContinue(AddressOf Me.WorkComplete)
        Me.Parent.Invoke(uiContinueDelegate)

    End Sub

    
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.WorkComplete()
    End Sub

End Class

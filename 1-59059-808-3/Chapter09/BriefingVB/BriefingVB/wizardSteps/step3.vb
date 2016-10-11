Imports System.Xml
Imports System.Text
Imports System.Text.RegularExpressions



Public Class step3
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
        If (ParentPane.HasAgendaList) Then
            'show this step
            Me.Visible = True
        Else
            'skip this step
            Me.WorkComplete()
        End If
    End Sub

    Public Sub WorkComplete() Implements IStep.WorkComplete
        RaiseEvent Completed(Me, New EventArgs())
    End Sub

    Private Sub step3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        taskPane = DirectCast(Me.Parent, ucTaskPane)
    End Sub

    Private Sub btnBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuild.Click
        Dim parentPane As ucTaskPane = DirectCast(Me.Parent, ucTaskPane)
        Me.UseWaitCursor = True

        'retrieve objectives
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = parentPane.SiteUrl + "/_vti_bin/lists.asmx"

        'build query parameters for list items
        Dim xmlDoc As XmlDocument = New XmlDocument()
        Dim ndQuery As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "")
        Dim ndViewFields As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "")
        Dim ndQueryOptions As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "")

        ndQueryOptions.InnerXml = "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>"
        ndViewFields.InnerXml = "<FieldRef Name='Title'/><FieldRef Name='Owner'/><FieldRef Name='Time'/><FieldRef Name='Notes'/>"
        ndQuery.InnerXml = ""

        AddHandler listService.GetListItemsCompleted, AddressOf GetListItemsCompleted
        listService.GetListItemsAsync("Agenda", Nothing, ndQuery, ndViewFields, Nothing, ndQueryOptions, Nothing)

    End Sub

    Public Sub GetListItemsCompleted(ByVal sender As Object, ByVal e As WSLists.GetListItemsCompletedEventArgs)
        If (e.Error Is Nothing) Then
            Dim ndItems As XmlNode
            ndItems = e.Result

            Dim xmlDoc As XmlDocument = New XmlDocument()
            xmlDoc.LoadXml(ndItems.OuterXml)
            Dim namespaceMgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDoc.NameTable)
            namespaceMgr.AddNamespace(ucTaskPane.ListItemsNamespacePrefix, ucTaskPane.ListItemsNamespaceUri)
            Dim agendaNodeList As XmlNodeList = xmlDoc.SelectNodes("//z:row", namespaceMgr)

            Dim slide As PowerPoint.Slide
            Dim presentation As PowerPoint.Presentation
            presentation = Globals.ThisAddIn.Application.ActivePresentation
            slide = presentation.Slides.Add(presentation.Slides.Count + 1, PowerPoint.PpSlideLayout.ppLayoutTable)
            slide.Shapes.Item(1).TextFrame.TextRange.Text = "Agenda"
            Dim tblAgenda As PowerPoint.Shape = slide.Shapes.AddTable(agendaNodeList.Count, 2)
            tblAgenda.Table.Columns(1).Width = 200
            tblAgenda.Table.Columns(2).Width = 400
            tblAgenda.Table.FirstRow = False


            Dim notesText As StringBuilder = New StringBuilder()


            Dim i As Integer
            For i = 1 To agendaNodeList.Count
                Dim time As String = agendaNodeList(i - 1).Attributes("ows_Time").InnerText
                Dim title As String = agendaNodeList(i - 1).Attributes("ows_Title").InnerText
                Dim owner As String = agendaNodeList(i - 1).Attributes("ows_Owner").InnerText
                Dim notes As String = String.Empty
                If (agendaNodeList(i - 1).Attributes("ows_Notes") IsNot Nothing) Then
                    notes = agendaNodeList(i - 1).Attributes("ows_Notes").InnerText
                End If


                tblAgenda.Table.Cell(i, 1).Shape.TextFrame2.TextRange.Text = time
                tblAgenda.Table.Cell(i, 2).Shape.TextFrame2.TextRange.Text = title

                ConstructNotesItem(notesText, time, title, owner, notes)

            Next

            BuildNotesPage(slide, notesText)
            Globals.ThisAddIn.Application.ActiveWindow.View.GotoSlide(slide.SlideIndex)

        End If

        Me.UseWaitCursor = False
        'go back to the main thread to continue
        Dim uiContinueDelegate As New UIContinue(AddressOf Me.WorkComplete)
        Me.Parent.Invoke(uiContinueDelegate)

    End Sub

    Private Sub ConstructNotesItem(ByVal builder As StringBuilder, ByVal time As String, ByVal title As String, ByVal owner As String, ByVal notes As String)
        builder.Append(time)
        builder.Append(" : ")
        builder.Append(title)
        builder.Append(" (")
        builder.Append(owner)
        builder.Append(")")
        notes = Me.StripHTML(notes)
        If (notes.Trim() <> String.Empty) Then
            builder.Append(vbCr & vbTab)
            builder.Append(notes)
        End If
        builder.Append(vbCr)
    End Sub

    Private Function StripHTML(ByVal HTML As String) As String
        Return Regex.Replace(HTML, "<[^>]*>", String.Empty)
    End Function

    Private Sub BuildNotesPage(ByVal slide As PowerPoint.Slide, ByVal notesText As StringBuilder)
        'Find the first shape that has a textframe where we can put the notes
        Dim oShape As PowerPoint.Shape
        For Each oShape In slide.NotesPage.Shapes
            If (oShape.Type = Microsoft.Office.Core.MsoShapeType.msoPlaceholder) Then
                If (oShape.HasTextFrame) Then
                    oShape.TextFrame2.TextRange.Text = notesText.ToString()
                    Exit For
                End If
            End If
        Next
    End Sub

    
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.WorkComplete()
    End Sub

    
End Class

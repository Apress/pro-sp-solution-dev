Imports System.Xml

Public Class step1
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
        Me.Visible = True
    End Sub

    Public Sub WorkComplete() Implements IStep.WorkComplete
        RaiseEvent Completed(Me, New EventArgs())
    End Sub

    Private Sub step1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        taskPane = DirectCast(Me.Parent, ucTaskPane)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnNext.Click
        Me.UseWaitCursor = True

        'check to see if there is there are lists for agenda and objectives
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = Me.txtSiteUrl.Text + "/_vti_bin/lists.asmx"
        AddHandler listService.GetListCollectionCompleted, AddressOf GetListCollectionCompleted
        listService.GetListCollectionAsync()
    End Sub

    Public Sub GetListCollectionCompleted(ByVal sender As Object, ByVal e As WSLists.GetListCollectionCompletedEventArgs)
        If (e.Error Is Nothing) Then
            ParentPane.SiteUrl = Me.txtSiteUrl.Text

            Dim ndLists As XmlNode
            ndLists = e.Result
            Dim xmlDoc As XmlDocument = New XmlDocument()
            xmlDoc.LoadXml(ndLists.OuterXml)
            Dim namespaceMgr As XmlNamespaceManager = _
                                    New XmlNamespaceManager(xmlDoc.NameTable)
            namespaceMgr.AddNamespace(ucTaskPane.SharePointNamespacePrefix, _
                                      ucTaskPane.SharePointNamespaceUri)

            'is there a list named agenda
            Dim agendaListNode As XmlNodeList = _
                        xmlDoc.SelectNodes("//sp:List[@Title='Agenda']", namespaceMgr)
            If (agendaListNode Is Nothing Or agendaListNode.Count = 0) Then
                ParentPane.HasAgendaList = False
            End If
            'is there a list named objectives
            Dim objListNode As XmlNodeList = xmlDoc.SelectNodes("//sp:List[@Title='Objectives']", namespaceMgr)
            If (objListNode Is Nothing Or objListNode.Count = 0) Then
                ParentPane.HasObjectivesList = False
            End If
            'are there any slide libraries
            Dim slideLibNode As XmlNodeList = xmlDoc.SelectNodes("//sp:List[@ServerTemplate='2100']", namespaceMgr)
            If (slideLibNode Is Nothing Or slideLibNode.Count = 0) Then
                ParentPane.HasSlideLibrary = False

            Else
                'store the name  and url of each slide library
                Dim xmlNode As XmlNode
                For Each xmlNode In slideLibNode
                    Dim item As LibraryItem = New LibraryItem
                    item.Name = xmlNode.Attributes("Title").InnerText
                    item.Url = xmlNode.Attributes("DefaultViewUrl").InnerText
                    item.Url = item.Url.Replace(xmlNode.Attributes("WebFullUrl").InnerText, String.Empty)
                    ParentPane.SlideLibraries.Add(item)
                Next
            End If

        End If
        Me.UseWaitCursor = False
        'go back to the main thread to continue
        Dim uiContinueDelegate As New UIContinue(AddressOf Me.WorkComplete)
        Me.Parent.Invoke(uiContinueDelegate)

    End Sub

End Class

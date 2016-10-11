Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.WebControls
Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.WebControls
Imports System.IO
Imports System.IO.Packaging
Imports System.Xml



Public Class DynamicPowerPoint
    Inherits System.Web.UI.WebControls.WebParts.WebPart

    Private m_libraryName As String = String.Empty
    Private m_fileName As String = String.Empty
    Private m_templateName As String = String.Empty
    Private m_errorMessage As String = String.Empty

    Private m_web As SPWeb
    Private nsManager As XmlNamespaceManager

    Const documentRelationshipType As String = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"
    Const presentationmlNamespace As String = "http://schemas.openxmlformats.org/presentationml/2006/main"
    Const drawingmlNamespace As String = "http://schemas.openxmlformats.org/drawingml/2006/main"


    Private WithEvents btn_Generate As Button
    Private lbl_Message As Label



    <WebBrowsable(), Personalizable(PersonalizationScope.Shared)> _
    Public Property LibraryName() As String
        Get
            Return m_libraryName
        End Get
        Set(ByVal value As String)
            m_libraryName = value
        End Set
    End Property

    <WebBrowsable(), Personalizable(PersonalizationScope.Shared)> _
        Public Property FileName() As String
        Get
            Return m_fileName
        End Get
        Set(ByVal value As String)
            m_fileName = value
        End Set
    End Property
    <WebBrowsable(), Personalizable(PersonalizationScope.Shared)> _
        Public Property TemplateName() As String
        Get
            Return m_templateName
        End Get
        Set(ByVal value As String)
            m_templateName = value
        End Set
    End Property

    'Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
    '    If m_errorMessage <> String.Empty Then
    '        writer.Write(m_errorMessage)
    '    Else
    '        MyBase.Render(writer)
    '    End If
    'End Sub
    Protected Overrides Sub RenderContents(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.EnsureChildControls()
        If m_errorMessage <> String.Empty Then
            writer.Write(m_errorMessage)
        Else
            MyBase.RenderContents(writer)
        End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        Me.btn_Generate = New Button()
        btn_Generate.Text = "Build Briefing"
        btn_Generate.Visible = True
        Me.Controls.Add(btn_Generate)
        Me.lbl_Message = New Label()
        lbl_Message.Text = String.Format("The presentation has been created as {0} in the {1} library", m_fileName, m_libraryName)
        lbl_Message.Visible = False
        Me.Controls.Add(lbl_Message)
        MyBase.CreateChildControls()
    End Sub


    Private Sub btn_Generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Generate.Click
        Dim templateStream As Stream = Nothing
        Try
            m_web = SPControl.GetContextWeb(Me.Context)
            Dim sharedDocs As SPFolder = m_web.GetFolder(m_libraryName)
            Dim templateFile As SPFile = sharedDocs.Files.Item(m_templateName)
            templateStream = templateFile.OpenBinaryStream()
            Me.ProcessSlides(templateStream)

            'save the modified(File)
            'templateStream.Position = 0
            sharedDocs.Files.Add(m_fileName, templateStream, True)
            Me.btn_Generate.Visible = False
            Me.lbl_Message.Visible = True
        Catch ex As Exception
            m_errorMessage = ex.Message

        Finally
            If (templateStream IsNot Nothing) Then templateStream.Close()
        End Try

    End Sub


    

    Public Sub ProcessSlides(ByVal fileStream As Stream)
        Dim titles As New List(Of String)
        Dim documentPart As PackagePart = Nothing
        Dim documentUri As Uri = Nothing

        Using pptPackage As Package = Package.Open(fileStream, FileMode.Open, FileAccess.ReadWrite)
            ' Get the main document part (presentation.xml).
            For Each relationship As PackageRelationship In pptPackage.GetRelationshipsByType(documentRelationshipType)
                documentUri = PackUriHelper.ResolvePartUri(New Uri("/", UriKind.Relative), relationship.TargetUri)
                documentPart = pptPackage.GetPart(documentUri)

                ' There is only one document part. Get out now.
                Exit For
            Next

            ' Manage namespaces to perform Xml XPath queries.
            Dim nt As New NameTable()
            nsManager = New XmlNamespaceManager(nt)
            nsManager.AddNamespace("p", presentationmlNamespace)
            nsManager.AddNamespace("a", drawingmlNamespace)

            '  Iterate through the slides and extract the title string from each.
            Dim xDoc As New XmlDocument(nt)
            xDoc.Load(documentPart.GetStream())

            Dim sheetNodes As XmlNodeList = xDoc.SelectNodes("//p:sldIdLst/p:sldId", nsManager)
            If sheetNodes IsNot Nothing Then
                Dim relAttr As XmlAttribute = Nothing
                Dim sheetRelationship As PackageRelationship = Nothing
                Dim sheetPart As PackagePart = Nothing
                Dim sheetUri As Uri = Nothing
                Dim sheetDoc As XmlDocument = Nothing
                Dim titleNode As XmlNode = Nothing

                ' Look at each sheet node, retrieving the relationship id.
                For Each xNode As XmlNode In sheetNodes
                    relAttr = xNode.Attributes("r:id")
                    If relAttr IsNot Nothing Then
                        ' Retrieve the PackageRelationship object for the sheet:
                        sheetRelationship = documentPart.GetRelationship(relAttr.Value)
                        If sheetRelationship IsNot Nothing Then
                            sheetUri = PackUriHelper.ResolvePartUri(documentUri, sheetRelationship.TargetUri)
                            sheetPart = pptPackage.GetPart(sheetUri)

                            If sheetPart IsNot Nothing Then
                                ' You've got a reference to the sheet. Now load its contents and
                                ' find the title.
                                sheetDoc = New XmlDocument(nt)
                                sheetDoc.Load(sheetPart.GetStream())


                                titleNode = sheetDoc.SelectSingleNode("//p:sp//p:ph[@type='title' or @type='ctrTitle']", nsManager)
                                If titleNode IsNot Nothing Then
                                    Dim title As String = titleNode.ParentNode.SelectSingleNode("../../p:txBody/a:p/a:r/a:t", nsManager).InnerText

                                    Select Case title
                                        Case "Site Title"
                                            BuildTitleSlide(sheetDoc, titleNode)
                                        Case "Hardware Issues"
                                            BuildHardwareSlide(sheetDoc)
                                        Case "Software Issues"
                                            BuildSoftwareSlide(sheetDoc)
                                        Case "Other"
                                            BuildOtherSlide(sheetDoc)
                                    End Select
                                    sheetDoc.Save(sheetPart.GetStream(FileMode.Create, FileAccess.Write))
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End Using
    End Sub

    Private Sub BuildTitleSlide(ByVal doc As XmlDocument, ByVal titleNode As XmlNode)
        'the title is the first text node
        titleNode.ParentNode.SelectSingleNode("//a:t", nsManager).InnerText = m_web.Title
        'locate the subtitle
        Dim subTitleNode As XmlNode
        subTitleNode = doc.SelectSingleNode("//p:sp//p:ph[@type='subTitle']", nsManager)
        If (subTitleNode IsNot Nothing) Then
            'there are two text elements in this part: user name and date/time
            Dim textNodes As XmlNodeList = subTitleNode.SelectNodes("../../../p:txBody/a:p/a:r/a:t", nsManager)
            If (textNodes IsNot Nothing) Then
                textNodes(0).InnerText = Me.Context.User.Identity.Name
                textNodes(1).InnerText = DateTime.Today.ToShortDateString()
            End If
        End If
    End Sub
    Private Sub BuildHardwareSlide(ByVal doc As XmlDocument)
        Dim list As SPList = m_web.Lists("Issues")
        Dim query As SPQuery = New SPQuery()
        query.Query = "<Where><Eq><FieldRef Name='Category'/><Value Type='CHOICE'>Hardware</Value></Eq></Where>"
        Dim items As SPListItemCollection = list.GetItems(query)

        'Find the part containing rectangle two
        Dim rectangleNode As XmlNode
        rectangleNode = doc.SelectSingleNode("//p:sp//p:cNvPr[@name='Content Placeholder 1']", nsManager)
        If (rectangleNode IsNot Nothing) Then
            'Locate that part's txt area to add the bullet items to
            Dim textNode As XmlNode = rectangleNode.SelectSingleNode("../../p:txBody", nsManager)
            If (textNode IsNot Nothing) Then
                Dim item As SPListItem
                For Each item In items
                    'append the item to the slide as a bullet
                    Dim paraNode As XmlNode = BuildTextPar(item("Title"), textNode)
                    textNode.InsertBefore(paraNode, textNode.LastChild)
                Next
            End If
        End If
    End Sub

    Private Sub BuildSoftwareSlide(ByVal doc As XmlDocument)
        Dim list As SPList = m_web.Lists("Issues")
        Dim query As SPQuery = New SPQuery()
        query.Query = "<Where><Eq><FieldRef Name='Category'/><Value Type='CHOICE'>Software</Value></Eq></Where>"
        Dim items As SPListItemCollection = list.GetItems(query)

        'Find the part containing rectangle two
        Dim tableNode As XmlNode
        tableNode = doc.SelectSingleNode("//p:graphicFrame//a:tbl", nsManager)
        If (tableNode IsNot Nothing) Then
            Dim item As SPListItem
            For Each item In items
                'append the item to the slide as a bullet
                Dim rowNode As XmlNode = BuildTableRow(item, tableNode)
                tableNode.InsertAfter(rowNode, tableNode.LastChild)
            Next
        End If
    End Sub
    
    Private Sub BuildOtherSlide(ByVal doc As XmlDocument)
        Dim list As SPList = m_web.Lists("Issues")
        Dim query As SPQuery = New SPQuery()
        query.Query = "<Where><Eq><FieldRef Name='Category'/><Value Type='CHOICE'>Other</Value></Eq></Where>"
        Dim items As SPListItemCollection = List.GetItems(query)

        'Find the part containing rectangle two
        Dim rectangleNode As XmlNode
        rectangleNode = doc.SelectSingleNode("//p:sp//p:cNvPr[@name='Content Placeholder 1']", nsManager)
        If (rectangleNode IsNot Nothing) Then
            'Locate that part's txt area to add the bullet items to
            Dim textNode As XmlNode = rectangleNode.SelectSingleNode("../../p:txBody", nsManager)
            If (textNode IsNot Nothing) Then
                Dim item As SPListItem
                For Each item In items
                    'append the item to the slide as a bullet
                    Dim paraNode As XmlNode = BuildTextPar(item("Title"), textNode)
                    textNode.InsertBefore(paraNode, textNode.LastChild)
                Next
            End If
        End If
    End Sub

    Private Function BuildTableRow(ByVal item As SPListItem, ByVal tableNode As XmlNode) As XmlNode
        'Create a new table row node
        Dim rowNode As XmlNode = tableNode.OwnerDocument.CreateElement("a", "tr", drawingmlNamespace)
        Dim rowAttr As XmlAttribute = tableNode.OwnerDocument.CreateAttribute("h")
        rowAttr.Value = "0"
        rowNode.Attributes.Append(rowAttr)
        'add row cells based on item
        rowNode.AppendChild(BuildTableCell(item("Title"), rowNode))
        'the assigned to column has a # between id and name
        Dim name As String = item("Assigned To")
        If (name IsNot Nothing) Then
            Dim parts() As String = name.Split("#")
            name = parts(1)
        End If
        rowNode.AppendChild(BuildTableCell(name, rowNode))
        rowNode.AppendChild(BuildTableCell(item("Priority"), rowNode))

        Return rowNode
    End Function

    Private Function BuildTableCell(ByVal text As String, ByVal tableRow As XmlNode) As XmlNode
        'Create a new table cell node
        Dim cellNode As XmlNode = tableRow.OwnerDocument.CreateElement("a", "tc", drawingmlNamespace)
        Dim txBodyNode As XmlNode = tableRow.OwnerDocument.CreateElement("a", "txBody", drawingmlNamespace)
        Dim bodyPrNode As XmlNode = tableRow.OwnerDocument.CreateElement("a", "bodyPr", drawingmlNamespace)
        Dim lstStyleNode As XmlNode = tableRow.OwnerDocument.CreateElement("a", "lstStyle", drawingmlNamespace)
        Dim tcPrNode As XmlNode = tableRow.OwnerDocument.CreateElement("a", "tcPr", drawingmlNamespace)

        txBodyNode.AppendChild(bodyPrNode)
        txBodyNode.AppendChild(lstStyleNode)
        txBodyNode.AppendChild(Me.BuildTextPar(text, tableRow))
        cellNode.AppendChild(txBodyNode)
        cellNode.AppendChild(tcPrNode)

        Return cellNode
    End Function

    Private Function BuildTextPar(ByVal text As String, ByVal textNode As XmlNode) As XmlNode
        'Create a new paragraph node
        Dim paraNode As XmlNode = textNode.OwnerDocument.CreateElement("a", "p", drawingmlNamespace)
        Dim rNode As XmlNode = textNode.OwnerDocument.CreateElement("a", "r", drawingmlNamespace)
        Dim rPrNode As XmlNode = textNode.OwnerDocument.CreateElement("a", "rPr", drawingmlNamespace)
        Dim tNode As XmlNode = textNode.OwnerDocument.CreateElement("a", "t", drawingmlNamespace)
        'setup attibutes
        Dim langAttr As XmlAttribute = textNode.OwnerDocument.CreateAttribute("lang")
        Dim dirtyAttr As XmlAttribute = textNode.OwnerDocument.CreateAttribute("dirty")
        Dim smtCleanAttr As XmlAttribute = textNode.OwnerDocument.CreateAttribute("smtClean")
        langAttr.Value = "en-US"
        dirtyAttr.Value = "0"
        smtCleanAttr.Value = "0"
        'setup the text for the bullet
        Dim titleValueNode As XmlNode = textNode.OwnerDocument.CreateTextNode(text)
        tNode.AppendChild(titleValueNode)
        'put together xml fragmennt
        rPrNode.Attributes.Append(langAttr)
        rPrNode.Attributes.Append(dirtyAttr)
        rPrNode.Attributes.Append(smtCleanAttr)

        rNode.AppendChild(rPrNode)
        rNode.AppendChild(tNode)
        paraNode.AppendChild(rNode)

        Return paraNode
    End Function


    
End Class

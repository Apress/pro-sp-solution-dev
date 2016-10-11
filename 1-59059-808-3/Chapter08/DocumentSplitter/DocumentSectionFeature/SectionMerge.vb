Imports System.IO.Packaging
Imports System.Xml
Imports System.IO
Imports Microsoft.SharePoint



Public Class SectionMerge

    Const documentRelationshipType As String = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"
    Const wordmlNamespace As String = "http://schemas.openxmlformats.org/wordprocessingml/2006/main"

    ' Manage namespaces to perform Xml XPath queries.
    Dim nt As New NameTable()
    Dim nsManager As New XmlNamespaceManager(nt)

    Public Sub New()
        nsManager.AddNamespace("w", wordmlNamespace)
    End Sub

    Public Sub Merge(ByVal sectionLibrary As SPFolder, ByVal targetLibrary As SPFolder, ByVal fileName As String, ByVal fileUrl As String)
        'determine number of sections
        Dim numberSections = sectionLibrary.Files.Count
        'create a memory stream for the merged file - start with first file
        Dim docStream As Stream = New MemoryStream
        Dim firstFile As SPFile = sectionLibrary.Files(GenerateNum(1) + fileName)
        Dim firstStream = firstFile.OpenBinaryStream()
        Dim writer As BinaryWriter = New BinaryWriter(docStream)
        Dim reader As BinaryReader = New BinaryReader(firstStream)
        writer.Write(reader.ReadBytes(CInt(firstStream.Length)))
        writer.Flush()

        'open up as a package and get documentxml
        Using wdPackage As Package = Package.Open(docStream, FileMode.Open, FileAccess.ReadWrite)
            Dim documentPart As PackagePart = Nothing
            Dim documentUri As Uri = Nothing
            ' Get the main document part (document.xml).
            For Each relationship As PackageRelationship In wdPackage.GetRelationshipsByType(documentRelationshipType)
                documentUri = PackUriHelper.ResolvePartUri(New Uri("/", UriKind.Relative), relationship.TargetUri)
                documentPart = wdPackage.GetPart(documentUri)
                ' There is only one document.
                Exit For
            Next
            ' Get the document part from the package.
            ' Load the XML in the part into an XmlDocument instance:
            Dim xdoc As XmlDocument = New XmlDocument(nt)
            xdoc.Load(documentPart.GetStream())
            Dim documentNode As XmlNode = xdoc.SelectSingleNode("//w:customXml[@w:uri='http://tempuri.org/SectionedDocument.xsd' and @w:element='Document']", nsManager)

            'loop through others and append
            Dim i As Integer = 0
            For i = 2 To numberSections
                AddSection(i, documentNode, sectionLibrary, fileName)
            Next
            'save to target location
            xdoc.Save(documentPart.GetStream(FileMode.Create, FileAccess.Write))

            documentPart.Package.Close()

        End Using

        'save to target location
        docStream.Position = 0
        Dim slashIndex = fileUrl.IndexOf("/")
        Dim fileDest As String = fileUrl.Substring(slashIndex + 1, fileUrl.Length - slashIndex - 1)
        targetLibrary.Files.Add(fileDest, docStream, True)
    End Sub
    Private Sub AddSection(ByVal index As Integer, ByVal documentNode As XmlNode, ByVal sectionLibrary As SPFolder, ByVal fileName As String)
        Dim sectionFile As SPFile = sectionLibrary.Files(GenerateNum(index) + fileName)
        Dim sectionStream = sectionFile.OpenBinaryStream()
        Using wdPackage As Package = Package.Open(sectionStream, FileMode.Open, FileAccess.Read)
            Dim documentPart As PackagePart = Nothing
            Dim documentUri As Uri = Nothing
            ' Get the main document part (document.xml).
            For Each relationship As PackageRelationship In wdPackage.GetRelationshipsByType(documentRelationshipType)
                documentUri = PackUriHelper.ResolvePartUri(New Uri("/", UriKind.Relative), relationship.TargetUri)
                documentPart = wdPackage.GetPart(documentUri)
                ' There is only one document.
                Exit For
            Next
            ' Get the document part from the package.
            ' Load the XML in the part into an XmlDocument instance:
            Dim xdoc As XmlDocument = New XmlDocument(nt)
            xdoc.Load(documentPart.GetStream())

            'retrieve the list of section nodes
            Dim sectionNode As XmlNode = xdoc.SelectSingleNode("//w:customXml[@w:uri='http://tempuri.org/SectionedDocument.xsd' and @w:element='Section']", nsManager)
            If (sectionNode IsNot Nothing) Then
                Dim newNode As XmlNode = documentNode.OwnerDocument.ImportNode(sectionNode, True)
                documentNode.InsertAfter(newNode, documentNode.LastChild)
            End If
            documentPart.Package.Close()
        End Using
    End Sub

    Private Function GenerateNum(ByVal num As Integer) As String
        Dim val As String = num.ToString()
        Dim i As Integer = 1
        For i = 2 To val.Length Step -1
            val = "0" + val
        Next
        Return val
    End Function
End Class

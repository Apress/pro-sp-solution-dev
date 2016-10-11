Imports System.IO.Packaging
Imports System.Xml
Imports System.IO
Imports Microsoft.SharePoint



Public Class Splitter

    Const documentRelationshipType As String = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"
    Const wordmlNamespace As String = "http://schemas.openxmlformats.org/wordprocessingml/2006/main"

    ' Manage namespaces to perform Xml XPath queries.
    Dim nt As New NameTable()
    Dim nsManager As New XmlNamespaceManager(nt)

    Public Sub New()
        nsManager.AddNamespace("w", wordmlNamespace)
    End Sub
    


    Public Sub SplitDocument(ByVal docStream As Stream, ByVal library As SPFolder, ByVal fileName As String)
        Using wdPackage As Package = Package.Open(docStream, FileMode.Open, FileAccess.Read)
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
            Dim nodes As XmlNodeList = xdoc.SelectNodes("//w:customXml[@w:uri='http://tempuri.org/SectionedDocument.xsd' and @w:element='Section']", nsManager)
            Dim numSections As Integer = nodes.Count
            documentPart.Package.Close()
            Me.GenDocs(numSections, docStream, library, fileName)
        End Using
    End Sub

    Public Sub GenDocs(ByVal numSections As Integer, ByVal docStream As Stream, ByVal library As SPFolder, ByVal fileName As String)
        Dim i As Integer
        Dim reader As BinaryReader = New BinaryReader(docStream)
        For i = 0 To numSections - 1
            'make copies
            Dim instanceStream As Stream = New MemoryStream
            docStream.Position = 0
            Dim writer As BinaryWriter = New BinaryWriter(instanceStream)
            writer.Write(reader.ReadBytes(CInt(docStream.Length)))
            writer.Flush()

            Using wdPackage As Package = Package.Open(instanceStream, FileMode.Open, FileAccess.ReadWrite)
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
                Dim nodes As XmlNodeList = xdoc.SelectNodes("//w:customXml[@w:uri='http://tempuri.org/SectionedDocument.xsd' and @w:element='Section']", nsManager)
                Dim j As Integer = 0
                Dim sectionNode As XmlNode = Nothing
                For Each sectionNode In nodes
                    If (i <> j) Then
                        sectionNode.ParentNode.RemoveChild(sectionNode)
                    End If
                    j += 1
                Next
                'save changes to XML

                xdoc.Save(documentPart.GetStream(FileMode.Create, FileAccess.Write))
                'save this as a document
                
                instanceStream.Position = 0
                library.Files.Add(GenerateNum(i) + fileName, instanceStream, True)
                
            End Using
        Next
        reader.Close()
    End Sub

    Private Function GenerateNum(ByVal num As Integer) As String
        Dim val As String = (num + 1).ToString()
        Dim i As Integer = 1
        For i = 2 To val.Length Step -1
            val = "0" + val
        Next
        Return val
    End Function
End Class

Imports System.Xml


Public Class DataManager

    Public Function CreateProjectBatch() As XmlDocument
        Dim batchXml As XmlDocument = New XmlDocument()
        Dim rootNode As XmlElement = batchXml.CreateElement("Batch")
        Dim attribute As XmlAttribute
        attribute = batchXml.CreateAttribute("OnError")
        attribute.InnerText = "Continue"
        rootNode.Attributes.Append(attribute)
        batchXml.AppendChild(rootNode)
        Return batchXml
    End Function

    Public Sub BatchAddProject(ByVal projectRow As DataRowView, ByVal batchXml As XmlDocument)
        Dim methodNode As XmlElement = batchXml.CreateElement("Method")
        Dim attribute As XmlAttribute
        attribute = batchXml.CreateAttribute("ID")
        attribute.InnerText = batchXml.DocumentElement.ChildNodes.Count + 1
        methodNode.Attributes.Append(attribute)
        attribute = batchXml.CreateAttribute("Cmd")
        attribute.InnerText = "New"
        methodNode.Attributes.Append(attribute)
        CreateFieldNode(methodNode, "Budget", projectRow.Item("Budget").ToString())
        CreateFieldNode(methodNode, "ProjectNumber", projectRow.Item("ProjectNumber").ToString())
        CreateFieldNode(methodNode, "StartDate", CType(projectRow.Item("StartDate"), DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
        CreateFieldNode(methodNode, "Title", projectRow.Item("Title").ToString())
        batchXml.DocumentElement.AppendChild(methodNode)
    End Sub
    Public Sub BatchDeleteProject(ByVal projectRow As DataRowView, ByVal batchXml As XmlDocument)
        Dim methodNode As XmlElement = batchXml.CreateElement("Method")
        Dim attribute As XmlAttribute
        attribute = batchXml.CreateAttribute("ID")
        attribute.InnerText = batchXml.DocumentElement.ChildNodes.Count + 1
        methodNode.Attributes.Append(attribute)
        attribute = batchXml.CreateAttribute("Cmd")
        attribute.InnerText = "Delete"
        methodNode.Attributes.Append(attribute)
        CreateFieldNode(methodNode, "ID", projectRow.Item("ListItemID").ToString())
        batchXml.DocumentElement.AppendChild(methodNode)
    End Sub
    Public Sub BatchUpdateProject(ByVal projectRow As DataRowView, ByVal batchXml As XmlDocument)
        Dim methodNode As XmlElement = batchXml.CreateElement("Method")
        Dim attribute As XmlAttribute
        attribute = batchXml.CreateAttribute("ID")
        attribute.InnerText = batchXml.DocumentElement.ChildNodes.Count + 1
        methodNode.Attributes.Append(attribute)
        attribute = batchXml.CreateAttribute("Cmd")
        attribute.InnerText = "Update"
        methodNode.Attributes.Append(attribute)
        CreateFieldNode(methodNode, "ID", projectRow.Item("ListItemID").ToString())
        CreateFieldNode(methodNode, "Budget", projectRow.Item("Budget").ToString())
        CreateFieldNode(methodNode, "ProjectNumber", projectRow.Item("ProjectNumber").ToString())
        CreateFieldNode(methodNode, "StartDate", CType(projectRow.Item("StartDate"), DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
        CreateFieldNode(methodNode, "Title", projectRow.Item("Title").ToString())
        batchXml.DocumentElement.AppendChild(methodNode)
    End Sub
    Public Sub CommitBatch(ByVal batchXml As XmlDocument, ByVal listName As String, ByVal Url As String)
        'MessageBox.Show(batchXml.OuterXml)
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = Url + "/_vti_bin/lists.asmx"
        Dim returnInfo As XmlNode = listService.UpdateListItems(listName, batchXml.DocumentElement)
        'MessageBox.Show(returnInfo.OuterXml)
    End Sub

    Private Sub CreateFieldNode(ByVal methodNode As XmlElement, ByVal fieldName As String, ByVal fieldValue As String)
        Dim fieldNode As XmlElement = methodNode.OwnerDocument.CreateElement("Field")
        Dim attribute As XmlAttribute
        attribute = methodNode.OwnerDocument.CreateAttribute("Name")
        attribute.InnerText = fieldName
        fieldNode.Attributes.Append(attribute)
        Dim valueNode = methodNode.OwnerDocument.CreateTextNode(fieldValue)
        fieldNode.AppendChild(valueNode)
        methodNode.AppendChild(fieldNode)
    End Sub

    Public Function GetListLastModified(ByVal listName As String, ByVal url As String) As DateTime
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = url + "/_vti_bin/lists.asmx"
        Dim listInfo As XmlNode = listService.GetList(listName)
        Return DateTime.ParseExact(listInfo.Attributes("Modified").InnerText, "yyyyMMdd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Function GetListItems(ByVal listName As String, ByVal url As String) As XmlNode
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = url + "/_vti_bin/lists.asmx"
        Dim xmlDoc As XmlDocument = New XmlDocument()
        Dim ndQuery As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "")
        Dim ndViewFields As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "")
        Dim ndQueryOptions As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "")

        ndQueryOptions.InnerXml = "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>"
        ndViewFields.InnerXml = "<FieldRef Name='Title'/><FieldRef Name='ProjectNumber'/><FieldRef Name='StartDate'/><FieldRef Name='Budget'/>"
        ndQuery.InnerXml = ""

        Dim ndItems As XmlNode
        ndItems = listService.GetListItems(listName, Nothing, ndQuery, ndViewFields, Nothing, ndQueryOptions, Nothing)
        Return ndItems
    End Function
End Class

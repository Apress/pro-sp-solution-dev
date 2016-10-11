<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %> 
<%@ Assembly Name="WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"%> 
<%@ Page Language="VB" MasterPageFile="~/_layouts/application.master" Inherits="Microsoft.SharePoint.WebControls.LayoutsPageBase" EnableViewState="true" EnableViewStateMac="false"    %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %> 
<%@ Import Namespace="Microsoft.SharePoint.Administration" %> 
<%@ Import Namespace="System.IO.Packaging" %> 


<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<script runat="server">

    Dim siteCollection As SPSite = Nothing
    Dim webObj As SPWeb = Nothing
    Dim listId As Guid = Nothing
    Dim itemId As Integer = 0
    Dim customerDocLib As SPFolder = Nothing
    Dim dataStoreID As String = "{9CBB321F-B9AE-4BDE-84BC-641A5A11251C}"
        
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Try
            
            siteCollection = SPContext.Current.Site
            webObj = SPContext.Current.Web
            listId = New Guid(Server.UrlDecode(Request.QueryString("ListId")))
            itemId = Integer.Parse(Request.QueryString("ItemId"))
            customerDocLib = webObj.GetFolder("Customer Documents")
            'build button event handler     
            If (Not Me.IsPostBack) Then
                
                Me.btnCancel.OnClientClick = "javascript:history.go(-1);return false;"
                Dim contentTypes As Generic.IList(Of SPContentType) = customerDocLib.ContentTypeOrder
                'populate drop down
                Dim contType As SPContentType = Nothing
                Dim i As Integer = 0
                For Each contType In contentTypes
                    If (contType.Name <> "Document") Then
                        Dim item = New ListItem(contType.Name, i)
                        lstContentTypes.Items.Add(item)
                    End If
                    i += 1
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try
    
    End Sub
    
    
    
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Write("Cancel")
    End Sub
    
    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            
            Dim docStream As Stream = New MemoryStream
            Dim contentType As SPContentType = customerDocLib.ContentTypeOrder.Item(lstContentTypes.SelectedValue)
            
            Dim templateFile As SPFile = contentType.ResourceFolder.Files(contentType.DocumentTemplate)
            Dim templateStream = templateFile.OpenBinaryStream()
            Dim reader As BinaryReader = New BinaryReader(templateStream)
            Dim writer As BinaryWriter = New BinaryWriter(docStream)
            writer.Write(reader.ReadBytes(CInt(templateStream.Length)))
            writer.Flush()
            reader.Close()
            templateStream.Dispose()
            
            'insert custom xml part into the document stream
            Dim contactItem As SPListItem = Nothing
            contactItem = webObj.Lists(Me.listId).GetItemById(Me.itemId)
            
            'open .docx file in memory stream as package file
            docStream.Position = 0
            Dim pkgFile As Package = Package.Open(docStream, FileMode.Open, FileAccess.ReadWrite)
            'retrieve package part with XML data
            Dim partNameTemplate As String = "/customXml/item{0}.xml"
            Dim partName As String = Nothing
            Dim pkgprtData As PackagePart = Nothing
            Dim xDoc As XmlDocument = Nothing

            Dim i As Integer = 1
            While True
                partName = String.Format(partNameTemplate, i)
                If pkgFile.PartExists(New Uri(partName, UriKind.Relative)) Then
                    pkgprtData = pkgFile.GetPart(New Uri(partName, UriKind.Relative))
                    xDoc = New XmlDocument()
                    xDoc.Load(pkgprtData.GetStream())
                    If (xDoc.DocumentElement.NamespaceURI = "http://www.sample.com/2006/schemas/contact/") Then
                        'this is the one we are looking for
                        Exit While
                    End If
                End If
                ' Pick an arbitrary number of part names to try. 
                If i = 1000 Then
                    Throw New InvalidOperationException("Unable to find XML part.")
                End If
                i += 1
            End While
    
            'serialize the contact item into this customXml part                    
            Dim rootNode As XmlNode = xDoc.DocumentElement
            rootNode.RemoveAll()
            Dim field As SPField
            For Each field In contactItem.Fields
                Dim fieldNode As XmlNode = xDoc.CreateElement("sc", XmlConvert.EncodeName(field.Title), "http://www.sample.com/2006/schemas/contact/")
                If (contactItem(field.Id) IsNot Nothing) Then
                    Dim fieldVal As XmlNode = xDoc.CreateTextNode(contactItem(field.Id).ToString())
                    fieldNode.AppendChild(fieldVal)
                End If
                rootNode.AppendChild(fieldNode)
            Next
            xDoc.Save(pkgprtData.GetStream(FileMode.Create, FileAccess.Write))
            
            'check the itemsProps to sync dataStoreID
            partNameTemplate = "/customXml/itemProps{0}.xml"
            partName = String.Format(partNameTemplate, i)
            pkgprtData = pkgFile.GetPart(New Uri(partName, UriKind.Relative))
            xDoc = New XmlDocument()
            xDoc.Load(pkgprtData.GetStream())
            xDoc.DocumentElement.Attributes(0).InnerText = dataStoreID
            xDoc.Save(pkgprtData.GetStream(FileMode.Create, FileAccess.Write))
            
            'deliver stream to client
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("content-disposition", "attachment; filename=" & templateFile.Name)
            Me.Response.ContentType = "application/vnd.ms-word.document.12"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            docStream.Position = 0
            Const size As Integer = 4096
            Dim bytes(4096) As Byte
            Dim numBytes As Integer
            numBytes = docStream.Read(bytes, 0, size)
            While numBytes > 0
                Response.OutputStream.Write(bytes, 0, numBytes)
                numBytes = docStream.Read(bytes, 0, size)
            End While
            'clean up
            Response.Flush()
            docStream.Close()
            docStream.Dispose()
            Response.Close()
        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try
        
    End Sub
    

</script>

<asp:Content ID="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <table border="1" cellpadding="5" cellspacing="0" style="width:100%; font-size: 9pt" >

        <tr>
            <td>Document Type:</td>
            <td><asp:DropDownList ID="lstContentTypes"  runat="server" EnableViewState="true"/></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnCancel"  Text="Return" runat="server" OnClick="btnCancel_Click" />&nbsp;<asp:Button ID="btnOK" Text="Generate" runat="server" OnClick="btnOK_Click" /></td>
        </tr>

    </table>

</asp:Content>



<asp:Content ID="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
	Build Customer Documents
</asp:Content>


<asp:Content ID="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
	Build Customer Documents
</asp:Content>

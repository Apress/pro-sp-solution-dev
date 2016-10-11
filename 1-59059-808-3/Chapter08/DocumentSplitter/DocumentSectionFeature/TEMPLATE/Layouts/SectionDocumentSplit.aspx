<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %> 
<%@ Assembly Name="WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"%> 
<%@ Assembly Name="DocumentSectionFeature, Version=1.0.0.0, Culture=neutral, PublicKeyToken=45a2811e9ad438d2"%> 

<%@ Page Language="VB" MasterPageFile="~/_layouts/application.master" Inherits="Microsoft.SharePoint.WebControls.LayoutsPageBase" EnableViewState="true" EnableViewStateMac="false"    %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %> 
<%@ Import Namespace="Microsoft.SharePoint.Administration" %> 
<%@ Import Namespace="System.IO.Packaging" %> 
<%@ Import Namespace="DocumentSectionFeature" %> 


<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<script runat="server">

    Dim siteCollection As SPSite = Nothing
    Dim webObj As SPWeb = Nothing
    Dim listId As Guid = Nothing
    Dim itemId As Integer = 0
        
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Try
            siteCollection = SPContext.Current.Site
            webObj = SPContext.Current.Web
            listId = New Guid(Server.UrlDecode(Request.QueryString("ListId")))
            itemId = Integer.Parse(Request.QueryString("ItemId"))
            lblMessage.Visible = False
            lnkResult.Visible = False
            If (Not Me.IsPostBack) Then
                'populate drop down of section libraries
                Dim libs As SPListCollection = webObj.GetListsOfType(SPBaseType.DocumentLibrary)
                Dim library As SPList
                For Each library In libs
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(library.PropertiesXml)
                    If (doc.DocumentElement.GetAttribute("ServerTemplate").ToString() = "10001") Then
                        Dim libraryUrl As String = doc.DocumentElement.GetAttribute("DefaultViewUrl").ToString()
                        Dim webUrl As String = doc.DocumentElement.GetAttribute("WebFullUrl").ToString()
                        libraryUrl = libraryUrl.Replace(webUrl, String.Empty)
                        libraryUrl = libraryUrl.Substring(1, libraryUrl.IndexOfAny("/", 1) - 1)
                        Dim item = New ListItem(library.Title, libraryUrl)
                        lstLibs.Items.Add(item)
                    End If
                Next
                'populate label with title of textbox
                Dim sourceItem As SPListItem = webObj.Lists(listId).GetItemById(itemId)
                If (sourceItem IsNot Nothing) Then
                    lblDocumentName.Text = Server.HtmlEncode(sourceItem("Name"))
                    Me.btnCancel.OnClientClick = "javascript:document.location.href='" + sourceItem.ParentList.DefaultViewUrl + "'; return false;"
                End If
               
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " + ex.Message
            lblMessage.Visible = True
        End Try
    
    End Sub
    
    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim targetLibrary As SPFolder = Nothing
            If (Me.rdExistingLib.Checked) Then
                targetLibrary = webObj.Folders(Me.lstLibs.SelectedValue)
            Else
                'create new library with name in textbox
                Dim template As SPListTemplate = webObj.ListTemplates("Document Sections Library")
                Dim newListId As Guid = webObj.Lists.Add(Me.txtLibName.Text, _
                    "Sections of " + Me.lblDocumentName.Text, template)
                webObj.Update()
                Dim newLib As SPFolder = webObj.Folders.Add(Me.txtLibName.Text)
                targetLibrary = webObj.Folders(Me.txtLibName.Text)
            End If
            
            Dim sourceItem As SPListItem = webObj.Lists(listId).GetItemById(itemId)
            Dim sourceFile As SPFile = sourceItem.File
            If (sourceFile IsNot Nothing) Then
                Dim sourceStream As Stream = sourceFile.OpenBinaryStream()
                Dim splitObj As Splitter = New Splitter()
                splitObj.SplitDocument(sourceStream, targetLibrary, Me.lblDocumentName.Text)
                'store the source file info in the property bag of target library
                If (Me.rdCreateNewLib.Checked) Then
                    targetLibrary.Properties.Add("SourceListId", listId.ToString())
                    targetLibrary.Properties.Add("SourceFileUrl", sourceFile.Url)
                    targetLibrary.Properties.Add("SourceFileName", Me.lblDocumentName.Text)
                    targetLibrary.Update()
                End If
                'completion message
                lblMessage.Text = "The sections of your document have been saved in the requested library. Use the link below to navigate there"
                lblMessage.Visible = True
                lnkResult.NavigateUrl = targetLibrary.ServerRelativeUrl
                lnkResult.Text = targetLibrary.Name
                lnkResult.Visible = True
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " + ex.Message
            lblMessage.Visible = True
        End Try
    End Sub

</script>

<asp:Content ID="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <table border="1" cellpadding="5" cellspacing="0" style="width:100%; font-size: 9pt" >

        <tr>
            <td nowrap="true">Instructions:</td>
            <td>This page will enable you to split the selected MS Word 2007 document 
                into separate files for each section. For this action to be performed,
                the file must be associated with the SectionDocument schema and appropriately
                marked up. You have the option to have the section files to be stored in 
                a new document library or an existing one. Creating a new section document
                library will enable you to merge the sections and save back to the original
                file location.
             </td>
        </tr>
        <tr>
            <td nowrap="true">Document to Split:</td>
            <td><asp:Label ID="lblDocumentName"  runat="server" EnableViewState="true"/></td>
        </tr>
        <tr>
            <td nowrap="true"><asp:RadioButton ID="rdCreateNewLib" runat="server" EnableViewState="true" Text="Create New Section Library" GroupName="LibraryOption" /></td>
            <td>Name:<asp:TextBox ID="txtLibName" runat="server" EnableViewState="true" /></td>
        </tr>
        <tr>
            <td nowrap="true"><asp:RadioButton ID="rdExistingLib" runat="server" EnableViewState="true" Text="Use Existing Section Library" GroupName="LibraryOption" /></td>
            <td><asp:DropDownList ID="lstLibs"  runat="server" EnableViewState="true"/><br />
            Warning: You may accidentally overwrite files in this library and your
            ability to merge the results back to the original file location may be 
            limited.
            </td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnCancel"  Text="Return" runat="server" />&nbsp;<asp:Button ID="btnOK" Text="Generate" runat="server" OnClick="btnOK_Click" /></td>
        </tr>

    </table>
    <asp:Label ID="lblMessage" runat="server" EnableViewState="true" /><br />
    <asp:HyperLink ID="lnkResult" runat="server" EnableViewState="true" />

</asp:Content>



<asp:Content ID="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
	Split Document into Sections
</asp:Content>


<asp:Content ID="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
	Split Document into Sections
</asp:Content>

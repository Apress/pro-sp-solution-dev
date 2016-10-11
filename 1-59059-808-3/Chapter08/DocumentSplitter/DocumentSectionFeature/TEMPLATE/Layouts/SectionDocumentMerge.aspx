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
    Dim targetListId As Guid = Nothing
    Dim targetFileUrl As String = Nothing
    Dim targetFileName As String = Nothing
    
    
        
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Try
            siteCollection = SPContext.Current.Site
            webObj = SPContext.Current.Web
            listId = New Guid(Server.UrlDecode(Request.QueryString("ListId")))
            Dim currentLibrary As SPFolder = webObj.Lists(listId).RootFolder
            targetListId = New Guid(currentLibrary.Properties.Item("SourceListId").ToString())
            targetFileUrl = currentLibrary.Properties.Item("SourceFileUrl").ToString()
            targetFileName = currentLibrary.Properties.Item("SourceFileName").ToString()
            
            lblMessage.Visible = False
            lnkResult.Visible = False
            If (Not Me.IsPostBack) Then
                'populate drop down of document libraries
                Dim libs As SPListCollection = webObj.GetListsOfType(SPBaseType.DocumentLibrary)
                Dim library As SPList
                For Each library In libs
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(library.PropertiesXml)
                    If (doc.DocumentElement.GetAttribute("ServerTemplate").ToString() = "101") Then
                        Dim libraryUrl As String = doc.DocumentElement.GetAttribute("DefaultViewUrl").ToString()
                        Dim webUrl As String = doc.DocumentElement.GetAttribute("WebFullUrl").ToString()
                        libraryUrl = libraryUrl.Replace(webUrl, String.Empty)
                        libraryUrl = libraryUrl.Substring(1, libraryUrl.IndexOfAny("/", 1) - 1)
                        Dim item = New ListItem(library.Title, libraryUrl)
                        lstLibs.Items.Add(item)
                    End If
                Next
                'populate label with name of original file
                Me.lblDocumentName.Text = Server.HtmlEncode(targetFileUrl)
                Me.btnCancel.OnClientClick = "javascript:document.location.href='" + webObj.Lists(listId).DefaultViewUrl + "'; return false;"
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " + ex.Message
            lblMessage.Visible = True
        End Try
    
    End Sub
    
    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim sourceLibrary As SPFolder = webObj.Lists(listId).RootFolder
            Dim targetLibrary As SPFolder = Nothing
            If (Me.rdOriginal.Checked) Then
                targetLibrary = webObj.Lists(targetListId).RootFolder
            Else
                targetLibrary = webObj.Folders(Me.lstLibs.SelectedValue)
                targetFileUrl = lstLibs.SelectedValue + "/" + Me.txtFileName.Text
            End If
            Dim mergeObj As SectionMerge = New SectionMerge()
            mergeObj.Merge(sourceLibrary, targetLibrary, targetFileName, targetFileUrl)
            lblMessage.Text = "The sections of your document have been merged in the requested library. Use the link below to navigate there"
            lblMessage.Visible = True
            lnkResult.NavigateUrl = targetLibrary.ServerRelativeUrl
            lnkResult.Text = targetLibrary.Name
            lnkResult.Visible = True
            
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
            <td>This page will enable you to merge the section files of this library 
                into a single MS Word 2007 document. You have the option to have the merged result 
                to overwrite the original source document or to be placed as a new file in an existing
                document library.
             </td>
        </tr>
        <tr>
            <td nowrap="true">Original Document:</td>
            <td><asp:Label ID="lblDocumentName"  runat="server" EnableViewState="true"/></td>
        </tr>
        <tr>
            <td nowrap="true"><asp:RadioButton ID="rdOriginal" runat="server" EnableViewState="true" Text="Use Original Location" GroupName="LibraryOption" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td nowrap="true"><asp:RadioButton ID="rdOtherLib" runat="server" EnableViewState="true" Text="Use Document Library" GroupName="LibraryOption" /></td>
            <td><asp:DropDownList ID="lstLibs"  runat="server" EnableViewState="true"/><br />
                File Name:<asp:TextBox ID="txtFileName" runat="server" EnableViewState="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnCancel"  Text="Return" runat="server" />&nbsp;<asp:Button ID="btnOK" Text="Merge" runat="server" OnClick="btnOK_Click" /></td>
        </tr>

    </table>
    <asp:Label ID="lblMessage" runat="server" EnableViewState="true" /><br />
    <asp:HyperLink ID="lnkResult" runat="server" EnableViewState="true" />

</asp:Content>



<asp:Content ID="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
	Merge Document Sections
</asp:Content>


<asp:Content ID="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
	Merge Document Sections
</asp:Content>

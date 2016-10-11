Imports System.Xml
Imports System.IO
Imports FPRPC
Imports System.Threading

Public Class ucTaskPane

    Public Const SharePointNamespacePrefix As String = "sp"
    Public Const SharePointNamespaceUri As String = "http://schemas.microsoft.com/sharepoint/soap/"
    Public Const ListItemsNamespacePrefix As String = "z"
    Public Const ListItemsNamespaceUri As String = "#RowsetSchema"


    Dim siteDocLibraries As ArrayList = New ArrayList()
    Dim path As String = System.Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User)

    Delegate Sub UIContinue()

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Me.UseWaitCursor = True
        'check to see if there is there are lists for agenda and objectives
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = Me.txtURL.Text + "/_vti_bin/lists.asmx"
        AddHandler listService.GetListCollectionCompleted, AddressOf GetListCollectionCompleted
        listService.GetListCollectionAsync()
    End Sub


    Public Sub GetListCollectionCompleted(ByVal sender As Object, ByVal e As WSLists.GetListCollectionCompletedEventArgs)

        Dim ndLists As XmlNode
        ndLists = e.Result
        Dim xmlDoc As XmlDocument = New XmlDocument()
        xmlDoc.LoadXml(ndLists.OuterXml)
        Dim namespaceMgr As XmlNamespaceManager = _
                                New XmlNamespaceManager(xmlDoc.NameTable)
        namespaceMgr.AddNamespace(SharePointNamespacePrefix, _
                                    SharePointNamespaceUri)

        'are there any document libraries
        Dim libNode As XmlNodeList = xmlDoc.SelectNodes("//sp:List[@ServerTemplate='101']", namespaceMgr)
        If (libNode IsNot Nothing AndAlso libNode.Count > 0) Then
            'store the name and url of each doc library
            Dim xmlNode As XmlNode
            For Each xmlNode In libNode
                Dim item As LibraryItem = New LibraryItem
                item.Name = xmlNode.Attributes("Title").InnerText
                item.Url = xmlNode.Attributes("DefaultViewUrl").InnerText
                item.Url = item.Url.Replace(xmlNode.Attributes("WebFullUrl").InnerText, String.Empty)
                Me.siteDocLibraries.Add(item)
            Next
        End If


        Me.UseWaitCursor = False
        'go back to the main thread to continue
        Dim uiContinueDelegate As New UIContinue(AddressOf Me.ListGatherComplete)
        Me.Invoke(uiContinueDelegate)

    End Sub

    Public Sub ListGatherComplete()
        Me.lstLibrary.DisplayMember = "Name"
        Me.lstLibrary.DataSource = Me.siteDocLibraries
        Me.pnlDetails.Enabled = True
    End Sub

    Public Sub PublishEmailComplete()
        'set complete message
        lblMessage.Text = "The email has been saved successfully."
        lblMessage.Visible = True
        Me.pnlDetails.Enabled = False
    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.UseWaitCursor = True
        Dim item As LibraryItem = CType(Me.lstLibrary.SelectedValue, LibraryItem)
        'check if need to create folder
        Dim folderUrl As String = String.Empty
        Dim currInspector As Outlook.Inspector = Globals.ThisAddIn.Application.ActiveInspector
        Dim message As Outlook.MailItem = CType(currInspector.CurrentItem, Outlook.MailItem)

        If (Me.optEmailSender.Checked) Then
            folderUrl = CreateFolder(message.SenderName, item)
        ElseIf (Me.optEmailSubject.Checked) Then
            folderUrl = CreateFolder(message.Subject, item)
        Else
            'root
            folderUrl = Me.txtURL.Text + item.Url.Substring(0, item.Url.IndexOfAny("/", 1))
        End If
        'MessageBox.Show(folderUrl)
        'upload attachements
        Dim rpc As FrontPageRPC = New FrontPageRPC()
        Dim attachmentItem As Outlook.Attachment
        For Each attachmentItem In message.Attachments
            attachmentItem.SaveAsFile(path + "\" + attachmentItem.FileName)
            Dim fs As FileStream = File.OpenRead(path + "\" + attachmentItem.FileName)
            rpc.PutDocument(folderUrl + "/" + attachmentItem.FileName, fs)
            fs.Close()
        Next
        'save and upload email
        If (Me.chkSaveEmail.Checked) Then
            Dim messageFileName As String = message.Subject + ".txt"
            message.SaveAs(path + "\" + messageFileName, Outlook.OlSaveAsType.olTXT)
            Dim fs As FileStream = File.OpenRead(path + "\" + messageFileName)
            rpc.PutDocument(folderUrl + "/" + messageFileName, fs)
            fs.Close()
        End If
        Me.UseWaitCursor = False
        Me.PublishEmailComplete()
    End Sub

    Private Function CreateFolder(ByVal folderName As String, ByVal item As LibraryItem) As String
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = Me.txtURL.Text + "/_vti_bin/lists.asmx"
        Dim strBatch As String = "<Method ID='1' Cmd='New'> " + _
              "<Field Name='ID'>New</Field>" + _
              "<Field Name='FSObjType'>1</Field>" + _
              "<Field Name='BaseName'>{0}</Field>" + _
           "</Method>"

        Dim xmlDoc As XmlDocument = New System.Xml.XmlDocument()
        Dim elBatch As System.Xml.XmlElement = xmlDoc.CreateElement("Batch")
        elBatch.SetAttribute("OnError", "Continue")
        elBatch.SetAttribute("ListVersion", "0")
        elBatch.InnerXml = String.Format(strBatch, folderName)

        Dim ndReturn As XmlNode = listService.UpdateListItems(item.Name, elBatch)
        Dim errorCode As String = String.Empty
        errorCode = ndReturn.FirstChild.Item("ErrorCode").InnerText
        Dim folderUrl As String = String.Empty

        If (errorCode = "0x00000000") Then
            'the folder was created successfully

            Dim returnNode As XmlNode = ndReturn.FirstChild.Item("z:row")
            If (returnNode IsNot Nothing) Then
                folderUrl = returnNode.Attributes("ows_EncodedAbsUrl").InnerText
            End If
        ElseIf (errorCode = "0x8107090d") Then
            'The folder already existed
            folderUrl = Me.txtURL.Text + item.Url.Substring(0, item.Url.IndexOfAny("/", 1)) + "/" + folderName
        End If
        Return folderUrl
    End Function
End Class

Public Class LibraryItem
    Private m_name As String
    Private m_url As String


    Public Property Name() As String
        Get
            Return m_name
        End Get
        Set(ByVal value As String)
            m_name = value
        End Set
    End Property

    Public Property Url() As String
        Get
            Return m_url
        End Get
        Set(ByVal value As String)
            m_url = value
        End Set
    End Property
    
End Class




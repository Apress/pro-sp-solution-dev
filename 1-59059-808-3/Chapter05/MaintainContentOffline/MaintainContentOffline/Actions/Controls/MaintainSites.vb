Imports System.Xml

Public Class MaintainSites

    Dim WorkSheetData1 As WorksheetData = Globals.Sheet1.WorksheetData1
    Dim foundLists As WorksheetData.ListsDataTable = New WorksheetData.ListsDataTable()
    Delegate Sub UIContinue()



    Private Sub MaintainSites_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetAddState(False)
    End Sub

    Public Sub PopulateCheckBoxList()
        Me.lstConnections.Items.Clear()
        Dim row As WorksheetData.ListsRow = Nothing
        For Each row In WorkSheetData1.Lists.Rows
            Me.lstConnections.Items.Add(row.ConnectionName, False)
        Next
    End Sub

    Private Sub btnDeleteSite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSite.Click
        'remove the sites and their data from the dataset
        If (WorkSheetData1.Projects.GetChanges() Is Nothing) Then
            'we can make the deletion
            Dim items As ListBox.SelectedObjectCollection = lstConnections.SelectedItems
            Dim connectionName As String
            For Each connectionName In items
                Dim listRow As WorksheetData.ListsRow = WorkSheetData1.Lists.FindByConnectionName(connectionName)
                If (listRow IsNot Nothing) Then listRow.Delete()
                'Dim datavw As DataView = New DataView(WorkSheetData1.Lists)
                'datavw.RowFilter = "ConnectionName='" + connectionName + "'"
                'If (datavw.Count > 0) Then datavw.Delete(0)
            Next
            WorkSheetData1.AcceptChanges()
            PopulateCheckBoxList()
        Else
            MessageBox.Show("You must be sync before removing a site connection.")
        End If


    End Sub

    Private Sub btnExamine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExamine.Click
        'connect to the site and find lists that are instances of the project template
        If (Not ConnectionManager.IsOnline) Then
            MessageBox.Show("You need an internet connection add new lists.")
            Exit Sub
        End If
        Dim listService As WSLists.Lists = New WSLists.Lists
        listService.Credentials = System.Net.CredentialCache.DefaultCredentials
        listService.Url = Me.txtUrl.Text + "/_vti_bin/lists.asmx"
        AddHandler listService.GetListCollectionCompleted, AddressOf GetListCollectionCompleted
        listService.GetListCollectionAsync()
        
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        'add a new entry
        Dim selectedRowView As DataRowView = CType(lstLists.SelectedItem, DataRowView)
        Dim selectedRow As WorksheetData.ListsRow = CType(selectedRowView.Row, WorksheetData.ListsRow)
        If (Me.txtName.Text = String.Empty) Then
            selectedRow.ConnectionName = selectedRow.ListName
        Else
            selectedRow.ConnectionName = Me.txtName.Text
        End If
        'store the minimum value since we know this is a new row that we will need to sync
        selectedRow.LastModified = DateTime.MinValue
        WorkSheetData1.Lists.ImportRow(selectedRow)
        WorkSheetData1.Lists.AcceptChanges()
        PopulateCheckBoxList()
        SetAddState(False)

        MessageBox.Show("This list's data will not be retrieved until you sync your work.")
    End Sub

    Public Sub GetListCollectionCompleted(ByVal sender As Object, ByVal e As WSLists.GetListCollectionCompletedEventArgs)
        If (e.Error Is Nothing) Then
            Dim ndLists As XmlNode
            ndLists = e.Result
            Dim xmlDoc As XmlDocument = New XmlDocument()
            xmlDoc.LoadXml(ndLists.OuterXml)
            Dim namespaceMgr As XmlNamespaceManager = _
                                    New XmlNamespaceManager(xmlDoc.NameTable)
            namespaceMgr.AddNamespace(Globals.ThisWorkbook.SharePointNamespacePrefix, _
                                      Globals.ThisWorkbook.SharePointNamespaceUri)
            Dim projLibNode As XmlNodeList = xmlDoc.SelectNodes("//sp:List[@ServerTemplate='1001']", namespaceMgr)
            If (projLibNode Is Nothing Or projLibNode.Count = 0) Then
                'alert that none were found
                MessageBox.Show("No lists of type Projects were found in the site")

            Else
                'store the name  and url of each proj list
                foundLists.Rows.Clear()
                Dim xmlNode As XmlNode
                For Each xmlNode In projLibNode
                    Dim listRow As WorksheetData.ListsRow = foundLists.NewListsRow()
                    listRow.ListName = xmlNode.Attributes("Title").InnerText
                    listRow.ConnectionName = listRow.ListName
                    Dim webAppUrl As String = Me.txtUrl.Text.Substring(0, txtUrl.Text.IndexOf("/", 7))
                    Dim listUrl As String = xmlNode.Attributes("DefaultViewUrl").InnerText
                    listRow.Url = webAppUrl + listUrl.Substring(0, listUrl.IndexOf("/Lists"))
                    listRow.LastModified = DateTime.ParseExact(xmlNode.Attributes("Modified").InnerText, "yyyyMMdd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                    foundLists.Rows.Add(listRow)
                Next
                foundLists.AcceptChanges()
                Dim cont As UIContinue = New UIContinue(AddressOf PopulateListBox)
                Me.Invoke(cont)
            End If
        End If
    End Sub

   
    Public Sub PopulateListBox()
        Me.lstLists.DataSource = foundLists
        Me.lstLists.DisplayMember = "ListName"
        SetAddState(True)
    End Sub

    Public Sub SetAddState(ByVal state As Boolean)
        If (state) Then
            Me.lstLists.Enabled = True
            Me.btnAdd.Enabled = True
            Me.txtName.Enabled = True
        Else
            Me.lstLists.Enabled = False
            Me.btnAdd.Enabled = False
            Me.txtName.Enabled = False
            Me.lstLists.DataSource = Nothing
            Me.txtName.Text = String.Empty
        End If
    End Sub
End Class

Imports System.Xml

Public Class SyncOptions

    Dim WorkSheetData1 As WorksheetData = Globals.Sheet1.WorksheetData1
    Dim newListsView As DataView
    Dim changedItemsView As DataView

    Private Sub SyncOptions_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If (Me.Visible) Then
            UpdateSyncStats()
        End If
    End Sub

    Private Sub btnSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSync.Click
        If (Not ConnectionManager.IsOnline) Then
            MessageBox.Show("You need an internet connection to sync.")
            Exit Sub
        End If
        Me.lblSyncStatus.Text = "Saving Changes"
        Me.UseWaitCursor = True
        Dim list As WorksheetData.ListsRow
        'Save changes made by user by connection (add, update, and delete)
        Dim data As DataTable = WorkSheetData1.Projects.GetChanges()
        If (data IsNot Nothing) Then
            For Each list In WorkSheetData1.Lists
                Dim dManager As DataManager = New DataManager()
                Dim batch As XmlDocument = dManager.CreateProjectBatch()
                'filter by each connection and build a batch
                Dim foundChanges = False
                'new and modified rows
                Dim dv As DataView = New DataView(data, "ConnectionName='" + list.ConnectionName + "'", String.Empty, DataViewRowState.CurrentRows)
                If (dv.Count > 0) Then
                    SaveChanges(dManager, batch, dv)
                    foundChanges = True
                End If
                'deleted rows
                dv.RowStateFilter = DataViewRowState.Deleted
                If (dv.Count > 0) Then
                    SaveChanges(dManager, batch, dv)
                    foundChanges = True
                End If
                'save?
                If (foundChanges) Then dManager.CommitBatch(batch, list.ListName, list.Url)
            Next
        End If
        WorkSheetData1.Projects.AcceptChanges()

        'if the list timestamp has changed then remove items from cache and get them again
        Me.lblSyncStatus.Text = "Getting Items from Updated Lists"
        For Each list In WorkSheetData1.Lists
            Dim dManager As DataManager = New DataManager()
            Dim currentModifiedDate As DateTime = dManager.GetListLastModified(list.ListName, list.Url)
            If (list.LastModified < currentModifiedDate) Then
                'delete all items of this list
                DeleteItems(list)
                'and get new ones
                GetAllItemsForList(list)
                WorkSheetData1.AcceptChanges()
            End If
        Next

        'Get Items for New Lists
        Me.lblSyncStatus.Text = "Getting new List Items"
        GetNewListItems()
        Me.UseWaitCursor = False
        'commit changes
        SettingsManager.LastSyncTime = DateTime.Now.ToString()
        WorkSheetData1.AcceptChanges()
        Me.lblSyncStatus.Text = "Done"
        lblLastSyncMessage.Text = SettingsManager.LastSyncTime
        UpdateSyncStats()
    End Sub

    Public Sub DeleteItems(ByVal list As WorksheetData.ListsRow)
        Dim dv As DataView = New DataView(WorkSheetData1.Projects)
        dv.RowFilter = "ConnectionName='" + list.ConnectionName + "'"
        While (dv.Count > 0)
            dv(0).Delete()
        End While
        WorkSheetData1.Projects.AcceptChanges()
    End Sub

    Public Sub SaveChanges(ByVal dManager As DataManager, ByVal batch As XmlDocument, ByVal dv As DataView)
        Dim drv As DataRowView
        For Each drv In dv
            Dim project As WorksheetData.ProjectsRow = CType(drv.Row, WorksheetData.ProjectsRow)
            Select Case project.RowState
                Case DataRowState.Modified
                    dManager.BatchUpdateProject(drv, batch)
                Case DataRowState.Added
                    dManager.BatchAddProject(drv, batch)
                Case DataRowState.Deleted
                    dManager.BatchDeleteProject(drv, batch)
            End Select
        Next
    End Sub

    Public Sub GetNewListItems()
        Dim listRow As DataRowView
        For Each listRow In newListsView
            GetAllItemsForList(listRow)
        Next
    End Sub

    Public Sub GetAllItemsForList(ByVal listRow As DataRowView)
        Dim row As WorksheetData.ListsRow = CType(listRow.Row, WorksheetData.ListsRow)
        GetAllItemsForList(row)
    End Sub
    Public Sub GetAllItemsForList(ByVal row As WorksheetData.ListsRow)
        Dim dManager As DataManager = New DataManager()
        'record the last modified time
        row.LastModified = dManager.GetListLastModified(row.ListName, row.Url)
        'Get items of this list
        Dim ndItems As XmlNode
        ndItems = dManager.GetListItems(row.ListName, row.Url)
        'add these items to the spreadsheet's dataset
        AddNewItems(ndItems, row.ConnectionName)
    End Sub

    Public Sub AddNewItems(ByVal ndItems As XmlNode, ByVal connectionName As String)
        Dim xmlDoc As XmlDocument = New XmlDocument()
        xmlDoc.LoadXml(ndItems.OuterXml)
        Dim namespaceMgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDoc.NameTable)
        namespaceMgr.AddNamespace(Globals.ThisWorkbook.ListItemsNamespacePrefix, Globals.ThisWorkbook.ListItemsNamespaceUri)
        Dim itemsNodeList As XmlNodeList = xmlDoc.SelectNodes("//z:row", namespaceMgr)
        Dim itemNode As XmlNode
        For Each itemNode In itemsNodeList
            Dim itemRow As WorksheetData.ProjectsRow = WorkSheetData1.Projects.NewProjectsRow()
            itemRow.ConnectionName = connectionName
            itemRow.ListItemID = itemNode.Attributes("ows_ID").InnerText
            itemRow.ProjectNumber = itemNode.Attributes("ows_ProjectNumber").InnerText
            itemRow.StartDate = DateTime.ParseExact(itemNode.Attributes("ows_StartDate").InnerText, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            itemRow.Budget = itemNode.Attributes("ows_Budget").InnerText
            itemRow.Title = itemNode.Attributes("ows_Title").InnerText
            WorkSheetData1.Projects.Rows.Add(itemRow)
        Next
    End Sub

    Public Sub UpdateSyncStats()
        newListsView = New DataView(WorkSheetData1.Lists)
        newListsView.RowFilter = "LastModified='" + DateTime.MinValue.ToString() + "'"
        Me.lblSyncNewLists.Text = String.Format("{0}: Number of new lists to capture", newListsView.Count)

        Dim changedItemsTable As DataTable = WorkSheetData1.Projects.GetChanges()
        If (changedItemsTable IsNot Nothing) Then
            Me.lblSyncChanges.Text = String.Format("{0}: Number of changes you made to data", changedItemsTable.Rows.Count)
        Else
            Me.lblSyncChanges.Text = String.Format("{0}: Number of changes you made to data", 0)
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UpdateSyncStats()
    End Sub
End Class


Public Class Sheet1

    Private Sub Sheet1_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup
        InitData()
    End Sub

    Private Sub Sheet1_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

    Private Sub InitData()
        If Not (Me.DataHost.IsCacheInitialized) Then
            'This is a clean run. Nothing in the cache
            Dim settingRow As WorksheetData.SettingsRow = Globals.Sheet1.WorksheetData1.Settings.NewRow()
            settingRow.FieldName = "LastSyncTime"
            settingRow.FieldValue = String.Empty
            Globals.Sheet1.WorksheetData1.Settings.Rows.Add(settingRow)
            WorksheetData1.Settings.AcceptChanges()
        End If

        Globals.ThisWorkbook.m_taskPaneBody.m_MaintainSites.PopulateCheckBoxList()
        Globals.ThisWorkbook.m_taskPaneBody.m_SyncOptions.lblLastSyncMessage.Text = SettingsManager.LastSyncTime

        If (ConnectionManager.IsOnline) Then
            If (Me.WorksheetData1.HasChanges) Then
                MessageBox.Show("You are online and have changes that have not been saved. Please Sync.")
                Globals.ThisWorkbook.m_taskPaneHeader.DisplaySyncDialog()
            End If
        End If
    End Sub

End Class

Public Class SettingsManager

    Public Shared Property LastSyncTime() As String
        Get
            Dim settingRow As WorksheetData.SettingsRow = Globals.Sheet1.WorksheetData1.Settings.FindByFieldName("LastSyncTime")
            If (settingRow IsNot Nothing) Then
                Return settingRow.FieldValue
            End If
            Return Nothing
        End Get
        Set(ByVal value As String)
            Dim settingRow As WorksheetData.SettingsRow = Globals.Sheet1.WorksheetData1.Settings.FindByFieldName("LastSyncTime")
            settingRow.FieldValue = value
            Globals.Sheet1.WorksheetData1.Settings.AcceptChanges()
        End Set
    End Property

End Class

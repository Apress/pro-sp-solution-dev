'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.42
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On



'''
<Microsoft.VisualStudio.Tools.Applications.Runtime.StartupObjectAttribute(1), _
 System.Runtime.InteropServices.ComVisibleAttribute(False), _
 System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
Partial Public NotInheritable Class Sheet1
    Inherits Microsoft.Office.Tools.Excel.Worksheet
    Implements Microsoft.VisualStudio.Tools.Applications.Runtime.IStartup

    Friend WithEvents List1 As Microsoft.Office.Tools.Excel.ListObject

    <Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private RuntimeCallback As Microsoft.VisualStudio.Tools.Applications.Runtime.IRuntimeServiceProvider

    <Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private HostItemHost As Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider

    <Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private DataHost As Microsoft.VisualStudio.Tools.Applications.Runtime.ICachedDataProvider

    <Microsoft.VisualStudio.Tools.Applications.Runtime.CachedAttribute()> _
    Public WithEvents WorksheetData1 As MaintainContentOffline.WorksheetData

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Public Sub New(ByVal RuntimeCallback As Microsoft.VisualStudio.Tools.Applications.Runtime.IRuntimeServiceProvider)
        MyBase.New(CType(RuntimeCallback.GetService(GetType(Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider)), Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider), RuntimeCallback, "Sheet1", Nothing, "Sheet1")
        Me.RuntimeCallback = RuntimeCallback
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Public Sub Initialize() Implements Microsoft.VisualStudio.Tools.Applications.Runtime.IStartup.Initialize
        Me.HostItemHost = CType(Me.RuntimeCallback.GetService(GetType(Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider)), Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider)
        Me.DataHost = CType(Me.RuntimeCallback.GetService(GetType(Microsoft.VisualStudio.Tools.Applications.Runtime.ICachedDataProvider)), Microsoft.VisualStudio.Tools.Applications.Runtime.ICachedDataProvider)
        Globals.Sheet1 = Me
        System.Windows.Forms.Application.EnableVisualStyles()
        Me.InitializeCachedData()
        Me.InitializeControls()
        Me.InitializeComponents()
        Me.InitializeData()
        Me.BeginInitialization()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Public Sub FinishInitialization() Implements Microsoft.VisualStudio.Tools.Applications.Runtime.IStartup.FinishInitialization
        Me.OnStartup()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Public Sub InitializeDataBindings() Implements Microsoft.VisualStudio.Tools.Applications.Runtime.IStartup.InitializeDataBindings
        Me.BindToData()
        Me.EndInitialization()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Public Overrides Sub OnShutdown() Implements Microsoft.VisualStudio.Tools.Applications.Runtime.IStartup.OnShutdown
        Me.List1.Dispose()
        MyBase.OnShutdown()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub InitializeCachedData()
        If (Me.DataHost Is Nothing) Then
            Return
        End If
        If Me.DataHost.IsCacheInitialized Then
            Me.DataHost.FillCachedData(Me)
        End If
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub InitializeData()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub BindToData()
        Me.List1.SetDataBinding(Me.WorksheetData1, "Projects", "ConnectionName", "ListItemID", "Title", "ProjectNumber", "StartDate", "Budget")
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Sub StartCaching(ByVal MemberName As String)
        Me.DataHost.StartCaching(Me, MemberName)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Sub StopCaching(ByVal MemberName As String)
        Me.DataHost.StopCaching(Me, MemberName)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Function IsCached(ByVal MemberName As String) As Boolean
        Return Me.DataHost.IsCached(Me, MemberName)
    End Function

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub BeginInitialization()
        Me.BeginInit()
        Me.List1.BeginInit()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub EndInitialization()
        Me.List1.EndInit()
        Me.EndInit()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub InitializeControls()
        Me.List1 = New Microsoft.Office.Tools.Excel.ListObject(Me.HostItemHost, Me.RuntimeCallback, "Sheet1:List1", Me, "List1")
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Sub InitializeComponents()
        If (Me.WorksheetData1 Is Nothing) Then
            'Instantiate the object if not yet loaded from the data cache.
            Me.WorksheetData1 = New MaintainContentOffline.WorksheetData
        End If
        CType(Me.List1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WorksheetData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'WorksheetData1
        '
        Me.WorksheetData1.DataSetName = "WorksheetData"
        Me.WorksheetData1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Sheet1
        '
        CType(Me.List1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WorksheetData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
    Private Function NeedsFill(ByVal MemberName As String) As Boolean
        Return Me.DataHost.NeedsFill(Me, MemberName)
    End Function
End Class

Partial Friend NotInheritable Class Globals

    Private Shared _Sheet1 As Sheet1

    Friend Shared Property Sheet1() As Sheet1
        Get
            Return _Sheet1
        End Get
        Set(ByVal value As Sheet1)
            If (_Sheet1 Is Nothing) Then
                _Sheet1 = value
            Else
                Throw New System.NotSupportedException
            End If
        End Set
    End Property
End Class

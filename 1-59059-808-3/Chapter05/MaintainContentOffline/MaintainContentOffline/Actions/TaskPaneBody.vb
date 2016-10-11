Public Class TaskPaneBody

    Public WithEvents m_MaintainSites As MaintainSites
    Public WithEvents m_SyncOptions As SyncOptions


    Private Sub TaskPaneBody_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        m_MaintainSites = New MaintainSites()
        Me.Controls.Add(m_MaintainSites)
        m_MaintainSites.Visible = True
        m_SyncOptions = New SyncOptions()
        Me.Controls.Add(m_SyncOptions)
        m_SyncOptions.Visible = False
    End Sub

    

End Class

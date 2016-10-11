Public Class step5
    Implements IStep

    Public Event Completed(ByVal sender As Object, ByVal e As System.EventArgs) Implements IStep.Completed
    Delegate Sub UIContinue()
    Private taskPane As ucTaskPane

    Public ReadOnly Property ParentPane() As ucTaskPane Implements IStep.ParentPane
        Get
            Return taskPane
        End Get
    End Property

    Public Sub Start() Implements IStep.Start
        'show this step
        Me.Visible = True
    End Sub

    Public Sub WorkComplete() Implements IStep.WorkComplete
        RaiseEvent Completed(Me, New EventArgs())
    End Sub

    Private Sub step5_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        taskPane = DirectCast(Me.Parent, ucTaskPane)
    End Sub
End Class


Public Interface IStep
    ReadOnly Property ParentPane() As ucTaskPane
    Event Completed(ByVal sender As Object, ByVal e As EventArgs)
    Sub WorkComplete()
    Sub Start()

End Interface

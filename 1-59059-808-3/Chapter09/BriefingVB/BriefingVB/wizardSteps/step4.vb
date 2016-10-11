Public Class step4
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
        If (ParentPane.HasSlideLibrary) Then
            'add links to panel
            ListSlideLibraryLinks()
            
            'show this step
            Me.Visible = True
        Else
            'skip this step
            Me.WorkComplete()
        End If
    End Sub

    Public Sub WorkComplete() Implements IStep.WorkComplete
        RaiseEvent Completed(Me, New EventArgs())
    End Sub

    Private Sub step4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        taskPane = DirectCast(Me.Parent, ucTaskPane)
    End Sub

    Private Sub ListSlideLibraryLinks()
        pnlLinks.Controls.Clear()
        Dim i As Integer

        Dim linkControls(ParentPane.SlideLibraries.Count) As LinkLabel
        For i = 0 To ParentPane.SlideLibraries.Count - 1
            linkControls(i) = New LinkLabel()
            linkControls(i).Location = New Point(5, 25 * i)
            AddHandler linkControls(i).LinkClicked, AddressOf LinkLabel_Click
            linkControls(i).LinkBehavior = LinkBehavior.AlwaysUnderline
            linkControls(i).Text = ParentPane.SlideLibraries(i).Name
            Dim link As LinkLabel.Link = linkControls(i).Links.Add(0, ParentPane.SlideLibraries(i).Name.Length)
            link.LinkData = ParentPane.SiteUrl & ParentPane.SlideLibraries(i).Url


        Next
        pnlLinks.Controls.AddRange(linkControls)
    End Sub
    Private Sub LinkLabel_Click(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start(e.Link.LinkData)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.WorkComplete()
    End Sub
End Class

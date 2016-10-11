Public Class TaskPaneFooter
    Public Property FooterMessage() As String
        Get
            Return lblFooterMessage.Text
        End Get
        Set(ByVal value As String)
            lblFooterMessage.Text = value
        End Set
    End Property
End Class

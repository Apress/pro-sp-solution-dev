Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.WebControls
Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.WebControls
Imports Microsoft.Office.InfoPath.Server.Controls
Imports System.Xml



Public Class FormViewWebPart
    Inherits System.Web.UI.WebControls.WebParts.WebPart

    Const defaultXmlLocation = ""

    Private m_xmlLocation As String = defaultXmlLocation
    Private WithEvents m_xmlFormView As XmlFormView
    Private m_errorMessage As String = String.Empty

    
    <WebBrowsable(), Personalizable(PersonalizationScope.User), WebDisplayName("XMLLocation"), WebDescription("URL of web-enabled InfoPath form")> _
    Public Property XMLLocation() As String
        Get
            Return m_xmlLocation
        End Get
        Set(ByVal value As String)
            m_xmlLocation = value
        End Set
    End Property


    Protected Overrides Sub RenderContents(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.EnsureChildControls()

        If m_errorMessage <> String.Empty Then
            writer.Write(m_errorMessage)
        Else
            If (Me.m_xmlLocation.Length > 0) Then
                m_xmlFormView.XmlLocation = m_xmlLocation
                m_xmlFormView.DataBind()
                m_xmlFormView.Visible = True
            End If

            MyBase.RenderContents(writer)
        End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()
        m_xmlFormView = New XmlFormView()
        m_xmlFormView.Visible = False
        Me.Controls.Add(Me.m_xmlFormView)

        
        m_xmlFormView.EditingStatus = XmlFormView.EditingState.Editing

    End Sub


    Private Sub m_xmlFormView_Initialize(ByVal sender As Object, ByVal e As Microsoft.Office.InfoPath.Server.Controls.InitializeEventArgs) Handles m_xmlFormView.Initialize
        'Try
        '    Dim nav As XPath.XPathNavigator = Me.m_xmlFormView.XmlForm.MainDataSource.CreateNavigator()
        '    Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(New NameTable())
        '    mgr.AddNamespace("my", Me.m_xmlFormView.XmlForm.NamespaceManager.LookupNamespace("my").ToString())
        '    Dim fSummary As XPath.XPathNavigator = nav.SelectSingleNode("/my:myFields/my:Summary", mgr)
        '    If (fSummary IsNot Nothing) Then
        '        fSummary.SetValue("Hello InfoPath")
        '    Else
        '        Me.m_errorMessage = "fSummary not found"
        '    End If
        'Catch ex As Exception
        '    Me.m_errorMessage = ex.Message.ToString()
        'End Try

    End Sub
    <ConnectionConsumer("XMLLocation")> _
    Public Sub GetConnectionInterface(ByVal providerPart As IWebPartField)
        Dim callback As FieldCallback = New FieldCallback(AddressOf Me.ReceiveField)
        providerPart.GetFieldValue(callback)
    End Sub

    Public Sub ReceiveField(ByVal field As Object)
        Me.EnsureChildControls()
        If (field IsNot Nothing) Then
            Me.m_xmlLocation = CType(field, String)
        End If

    End Sub

End Class
